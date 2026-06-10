import { useState } from "react";
import "./App.css";

interface LeagueEntry {
  queueType: string;
  tier: string;
  rank: string;
  leaguePoints: number;
  wins: number;
  losses: number;
}

function App() {
  const [searchField, setSearchField] = useState("");
  const [rankedData, setRankedData] = useState<LeagueEntry[]>([]);

  return (
    <>
      <h1>LOL-Companion</h1>
      <input
        value={searchField}
        onChange={(e) => setSearchField(e.target.value)}
        placeholder="Riot Id"
      />
      <button
        onClick={async () => {
          const response = await fetch(
            "http://localhost:5134/api/leagueentries/Coco%20Otravez/6156",
          );
          const data = await response.json();
          console.log(data);
          setRankedData(data);
        }}
      >
        Sök
      </button>

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
    </>
  );
}

export default App;
