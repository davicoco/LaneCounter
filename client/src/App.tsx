import { useState } from "react";
import "./App.css";

function App() {
  const [searchField, setSearchField] = useState("");

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
        }}
      >
        Sök
      </button>
    </>
  );
}

export default App;
