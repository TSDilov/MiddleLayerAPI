import React, { useState } from 'react';
import axios from 'axios';
import './Home.css'; 

function Home() {
  const [character, setCharacter] = useState(() => {
    return {}; 
  });

  const handleInitialize = async () => {
    try {
      const response = await axios.get('https://localhost:7238/Data');
      setCharacter(response.data);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };

  return (
    <div className="home-container">
      <h2>Welcome to the Home Page</h2>
      <button className="initialize-button" onClick={handleInitialize}>Initialize</button>
      {Object.keys(character).length > 0 && (
        <div className="character-card">
          <h3>{character.Result.name}</h3>
          <p>Gender: {character.Result.gender}</p>
          <p>Culture: {character.Result.culture}</p>
          {/* Add more character details as needed */}
        </div>
      )}
    </div>
  );
}

export default Home;