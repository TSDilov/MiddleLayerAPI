import React, { useState } from 'react';
import { useSelector } from 'react-redux';
import { gql } from 'graphql-request';
import './Home.css'; 

const endpoint = 'https://localhost:7238/graphql/';

function Home() {
  const { isLoggedIn } = useSelector((state) => state.login);

  const [character, setCharacter] = useState(() => {
    return {}; 
  });

  const [showLoginMessage, setShowLoginMessage] = useState(false);

  const handleInitialize = async () => {
    try {
      if (!isLoggedIn) {
        setShowLoginMessage(true);
        return;
      }
      const query = gql`
      {
        character
        {
          url
          name
          gender
          culture
          born
          died
          titles
          aliases
          father
          mother
          spouse
          allegiances
          books
          povBooks
          tvSeries
          playedBy
        }
      }`;
      const response = await fetch(endpoint, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ query }),
      });

      const data = await response.json();
      if (data.errors) {
        console.error('GraphQL errors:', data.errors);
      } else {
        setCharacter(data.data.character);
      }
    } catch (error) {
      console.error('Error fetching data:', error);
    }
  };

  const handleDelete = async () => {
    try {
      if (!isLoggedIn) {
        setShowLoginMessage(true);
        return;
      }
      const mutation = gql`
        mutation {
          deleteCharacterData {
            boolean
          }
        }
      `;
      const response = await fetch(endpoint, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ query: mutation }),
      });

      const responseData = await response.json();
      if (responseData.errors) {
        console.error('GraphQL errors:', responseData.errors);
      } else {
        console.log('Character data deleted successfully');
        setCharacter({});
      }
    } catch (error) {
      console.error('Error deleting data:', error);
    }
  };

  return (
    <div className="home-container">
      <h2>Welcome to the Home Page</h2>
      {showLoginMessage && <p>Please login first</p>}
      <button className="initialize-button" onClick={handleInitialize}>Initialize</button>
      <button className="delete-button" onClick={handleDelete}>Delete Data</button>
      {Object.keys(character).length > 0 && (
        <div className="character-card">
          <h3>{character.name}</h3>
          {Object.entries(character).map(([key, value]) => (
            <div key={key}>
              {Array.isArray(value) ? (
                <>
                  <strong>{key}:</strong>
                  <ul>
                    {value.map((item, index) => (
                      <li key={index}>{item}</li>))}
                  </ul>
                </>) : (
                <p><strong>{key}:</strong> {value}</p>)}
            </div>))}
        </div>)}
    </div>
  );
}

export default Home;