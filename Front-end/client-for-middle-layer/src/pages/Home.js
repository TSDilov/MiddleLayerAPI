import React, { useState } from 'react';
import { useSelector } from 'react-redux';
import axios from 'axios';
import './Home.css'; 

function Home() {
  const [character, setCharacter] = useState(() => {
    return {}; 
  });

  const userName = useSelector(state => state.auth?.userName);
  console.log(userName);
  const handleInitialize = async () => {
    try {
      const response = await axios.get('https://localhost:7238/Data');
      setCharacter(response.data);
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };

  const handleDelete = async () => {
    try {
      await axios.delete('https://localhost:7238/Data/Delete');
      setCharacter({});
    } catch (error) {
      console.error('Error deleting data:', error);
    }
  };

  return (
    <div className="home-container">
      <h2>Welcome to the Home Page{userName ? `, ${userName}` : ''}</h2>
      <button className="initialize-button" onClick={handleInitialize}>Initialize</button>
      <button className="delete-button" onClick={handleDelete}>Delete Data</button>
      {Object.keys(character).length > 0 && (
        <div className="character-card">
          <h3>{character.Result.name}</h3>
          {Object.entries(character.Result).map(([key, value]) => (
            <p key={key}><strong>{key}:</strong> {Array.isArray(value) ? value.join(', ') : value}</p>
          ))}
        </div>
      )}
    </div>
  );
}

export default Home;