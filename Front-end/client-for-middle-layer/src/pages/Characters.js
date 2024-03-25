import React, { useState, useEffect } from 'react';
import { GraphQLClient, gql } from 'graphql-request';
import './Characters.css'; 

const endpoint = 'https://localhost:7238/graphql/';
const charactersPerPage = 10;

function Characters() 
{
    const [characters, setCharacters] = useState([]);
    const [selectedCharacter, setSelectedCharacter] = useState(null);
    const [currentPage, setCurrentPage] = useState(1);
    const query = gql`
    {
      allCharacters {
        id
        name
        gender
        born
        died
        titles
      }
    }`;

    useEffect(() => {
        const fetchCharacters = async () => {
          try {
            const client = new GraphQLClient(endpoint);
            const data = await client.request(query);
            setCharacters(data.allCharacters);
          } catch (error) {
            console.error('Error fetching characters:', error);
          }
        };
      
        fetchCharacters();
      }, []);
    
    const indexOfLastCharacter = currentPage * charactersPerPage;
    const indexOfFirstCharacter = indexOfLastCharacter - charactersPerPage;
    const currentCharacters = characters.slice(indexOfFirstCharacter, indexOfLastCharacter);

    const handleCharacterClick = (character) => {
        setSelectedCharacter(character);
    };

    const handleDelete = async (id) => {
        try {
            const client = new GraphQLClient(endpoint);
            const mutation = gql`
                mutation DeleteCharacter($input: DeleteCharacterInput!) {
                    deleteCharacter(input: $input) {
                        boolean
                    }
                }`;
            await client.request(mutation, { input: { id } });
            setSelectedCharacter(null);
            const updatedCharacters = characters.filter(character => character.id !== id);
            setCharacters(updatedCharacters);
        } catch (error) {
            console.error('Error deleting character:', error);
        }
    };

    const paginate = (pageNumber) => {
        setCurrentPage(pageNumber);
    };

    return (
        <div className="home-container">
            <h2>Our characters</h2>
            <div className="character-list-container">
                {currentCharacters.map((character) => (
                    <div key={character.id} className="character-item" onClick={() => handleCharacterClick(character)}>
                        <p className="character-name">{character.name}</p>
                    </div>
                ))}
            </div>
            {selectedCharacter && (
                <div className="character-lightbox" onClick={() => setSelectedCharacter(null)}>
                    <div className="character-lightbox-content">
                        <h2>{selectedCharacter.name}</h2>
                        <p className="character-info">Gender: {selectedCharacter.gender}</p>
                        <p className="character-info">Born: {new Date(selectedCharacter.born).toLocaleDateString()}</p>
                        <p className="character-info">Died: {selectedCharacter.died ? new Date(selectedCharacter.died).toLocaleDateString() : "Still Alive"}</p>
                        <p className="character-info">Titles: {selectedCharacter.titles.join(', ')}</p>
                        <button className="delete-button" onClick={() => handleDelete(selectedCharacter.id)}>Delete</button>
                    </div>
                </div>
            )}
            <div className="pagination">
                {[...Array(Math.ceil(characters.length / charactersPerPage)).keys()].map((number) => (
                    <button key={number + 1} onClick={() => paginate(number + 1)}>{number + 1}</button>
                ))}
            </div>
        </div>
    );
}

export default Characters;