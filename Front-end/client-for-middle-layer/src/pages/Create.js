import React, { useState } from 'react';
import { gql, GraphQLClient } from 'graphql-request';
import { useNavigate } from 'react-router-dom';
import { useSelector } from 'react-redux';

const endpoint = 'https://localhost:7238/graphql/';

function Create() {
  const { isLoggedIn } = useSelector((state) => state.login);    
  const [showLoginMessage, setShowLoginMessage] = useState(false);
  const [characterData, setCharacterData] = useState({
    url: '',
    name: '',
    gender: '',
    born: new Date(),
    died: new Date(),
    titles: [],
    isCreated: true
  });

  const navigate = useNavigate();

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setCharacterData(prevData => ({
      ...prevData,
      [name]: value
    }));
  };

  const handleGenderChange = (e) => {
    const { value } = e.target;
    setCharacterData(prevData => ({
      ...prevData,
      gender: value
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!isLoggedIn) {
        setShowLoginMessage(true);
        return;
    }
    const mutation = gql`
    mutation CreateCharacter($input: CreateCharacterInput!) {
        createCharacter(input: $input) {
          boolean
        }
      }
    `;

    const client = new GraphQLClient(endpoint);
    try {
        await client.request(mutation, {
          input: {
            input: { 
              url: characterData.url,
              name: characterData.name,
              gender: characterData.gender,
              born: characterData.born,
              died: characterData.died,
              titles: characterData.titles,
              isCreated: characterData.isCreated
            }
          }
        });
        navigate('/');
      } catch (error) {
        console.error('Error:', error);
      }
  };

  return (
    <div className="container">
  <h2>Create Character</h2>
  {showLoginMessage && <p style={{ color: 'black' }}>Please login first</p>}
  <form onSubmit={handleSubmit}>
    <div className="form-group">
      <label htmlFor="url">URL:</label>
      <input type="text" id="url" name="url" value={characterData.url} onChange={handleInputChange} />
    </div>
    <div className="form-group">
      <label htmlFor="name">Name:</label>
      <input type="text" id="name" name="name" value={characterData.name} onChange={handleInputChange} />
    </div>
    <div className="form-group">
        <label htmlFor="gender">Gender:</label>
        <select id="gender" name="gender" value={characterData.gender} onChange={handleGenderChange} className="styled-select">
            <option value="">Select Gender</option>
            <option value="Male">Male</option>
            <option value="Female">Female</option>
        </select>
    </div>
    <div className="form-group">
        <label htmlFor="born">Born:</label>
        <input type="date" id="born" name="born" value={characterData.born} onChange={handleInputChange} />
    </div>
    <div className="form-group">
        <label htmlFor="died">Died:</label>
        <input type="date" id="died" name="died" value={characterData.died} onChange={handleInputChange} />
    </div>
    <div className="form-group">
      <label htmlFor="titles">Titles:</label>
      <input type="text" id="titles" name="titles" value={characterData.titles} onChange={handleInputChange} />
    </div>
    <button type="submit" className="btn-primary">Create Character</button>
  </form> 
</div>
  );
}

export default Create;