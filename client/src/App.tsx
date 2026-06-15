import { useState } from "react";
import "./App.css";

interface Account {
  puuid: string;
}

interface LeagueEntry {
  queueType: string;
  tier: string;
  rank: string;
  leaguePoints: number;
  wins: number;
  losses: number;
}

interface Participant {
  puuid: string;
  championName: string;
  kills: number;
  deaths: number;
  assists: number;
  win: boolean;
}

interface Info {
  gameMode: string;
  gameCreation: number;
  gameDuration: number;
  queueId: number;
  participants: Participant[];
}

interface Match {
  info: Info;
}

function App() {
  const [searchField, setSearchField] = useState("");
  const [rankedData, setRankedData] = useState<LeagueEntry[]>([]);
  const [matches, setMatches] = useState<Match[]>([]);
  const [uiErrorMessage, setUiErrorMessage] = useState("");
  const [myPuuid, setMyPuuid] = useState("");

  const fetchPuuid = async (gameName: string, tagLine: string) => {
    const response = await fetch(`http://localhost:5134/api/account/${gameName}/${tagLine}`);
    const account: Account = await response.json();
    return account.puuid;
  }

  const fetchRanked = async (gameName: string, tagLine: string) => {
    const response = await fetch(`http://localhost:5134/api/leagueentries/${gameName}/${tagLine}`);
    if (!response.ok) {
      setUiErrorMessage("Failed to get data");
      setRankedData([]);
      return
    }
    const data = await response.json();
    console.log(data)
    setUiErrorMessage("");
    setRankedData(data);
  }

  const fetchMatches = async (gameName: string, tagLine: string) => {
    const puuid = await fetchPuuid(gameName, tagLine);
    setMyPuuid(puuid)
    const response = await fetch(`http://localhost:5134/api/match/${gameName}/${tagLine}`)
    if (!response.ok) {
      setUiErrorMessage("Failed to get matches")
      setMatches([]);
      return;
    }
    const matchIds: string[] = await response.json();
    const firstTenMatches = matchIds.slice(0, 10);
    const promises = firstTenMatches.map((matchId) =>
      fetch(`http://localhost:5134/api/match/${matchId}`))
    const responses = await Promise.all(promises)
    const matchData = await Promise.all(responses.map((r) => r.json()));
    setMatches(matchData);
  }

  return (
    <>
      <h1>LOL-Companion</h1>
      <input
        value={searchField}
        onChange={(e) => setSearchField(e.target.value)}
        placeholder="Riot Id"
      />
      <button onClick={async () => {
        const [gameName, tagLine] = searchField.split("#");
        await fetchRanked(gameName, tagLine);
        await fetchMatches(gameName, tagLine);
      }}>Sök</button>
      {uiErrorMessage && <p>{uiErrorMessage}</p>}

      {rankedData.length > 0 && (
        <div>
          <p>Queue: {rankedData[0].queueType}</p>
          <p>Tier: {rankedData[0].tier}</p>
          <p>Rank: {rankedData[0].rank}</p>
          <p>LeaguePoints: {rankedData[0].leaguePoints}</p>
          <p>Wins: {rankedData[0].wins}</p>
          <p>Losses: {rankedData[0].losses}</p>
        </div>
      )}

      {matches.length > 0 && (
        <div>
          {matches.map((match: Match, index: number) => {
            const searchedPlayer = match.info.participants.find((p) => p.puuid === myPuuid);
            return (
            <div key={index}>
              <p>Champion: {searchedPlayer?.championName}</p>
              <p>Kills: {searchedPlayer?.kills}</p>
              <p>Deaths: {searchedPlayer?.deaths}</p>
              <p>Assists: {searchedPlayer?.assists}</p>
              <p>Win/loss: {searchedPlayer?.win ? "Win" : "Loss"}</p>
            </div>)
          })}
        </div>
      )}
    </>
  );
}

export default App;
