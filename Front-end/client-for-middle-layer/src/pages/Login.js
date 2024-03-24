import React, { useState } from 'react';
import axios from 'axios';
import { useDispatch } from 'react-redux';
import { loginSuccess } from '../reducers/loginReducer';
import { useNavigate } from 'react-router-dom';
import './Login.css';

const Login = () => {
  const [formData, setFormData] = useState({
    email: '',
    password: ''
  });

  const navigate = useNavigate();
  const dispatch = useDispatch();
  const [error, setError] = useState(() => {
    return ''; 
  })

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    const { email, password } = formData;
    try {
        const response = await axios.post('https://localhost:7238/Account/login', { email, password });
        const { token } = response.data;
        localStorage.setItem('jwtToken', token);
        dispatch(loginSuccess(token));
        navigate('/');
    } catch (error) {
      if (error.response && error.response.status === 400) {
        const errorMessage = error.response.data; 
        setError(errorMessage); 
      } else {
        console.error('Error logging in:', error.message);
      }
    }
  };

  return (
    <div className="container">
      <h2>Login</h2>
      {error && <div className="error" style={{ backgroundColor: 'red' }}>{error}</div>}
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label>Email:</label>
          <input
            type="email"
            className="form-control"
            name="email"
            value={formData.email}
            onChange={handleChange}
            required
          />
        </div>
        <div className="form-group">
          <label>Password:</label>
          <input
            type="password"
            className="form-control"
            name="password"
            value={formData.password}
            onChange={handleChange}
            required
          />
        </div>
        <button type="submit" className="btn btn-primary">Login</button>
      </form>
    </div>
  );
};

export default Login;