import React from 'react';
import { Link, NavLink } from 'react-router-dom';
import { useSelector } from 'react-redux';
import { logout } from '../reducers/loginReducer';
import { connect } from 'react-redux';
import { useDispatch } from 'react-redux';
import './Navbar.css';

const Navbar = () => {
  const dispatch = useDispatch();
  const { isLoggedIn } = useSelector((state) => state.login);

  const handleLogout = () => {
    dispatch(logout());

    localStorage.removeItem('jwtToken');
  };

  return (
    <nav className="navbar">
      <div>
        <Link to="/" className="navbar-brand">
          MiddleLayer
        </Link>
        <NavLink to="/" exact="true" className="navbar-link">
          Home
        </NavLink>
      </div>
      <div>
        {isLoggedIn ? (
          <NavLink to="/" className="navbar-link" onClick={handleLogout}>
            Logout
          </NavLink>
        ) : (
          <>
            <NavLink to="/Login" className="navbar-link">
              Login
            </NavLink>
            <NavLink to="/Register" className="navbar-link">
              Register
            </NavLink>
          </>
        )}
      </div>
    </nav>
  );
};

export default connect(null, { logout })(Navbar);