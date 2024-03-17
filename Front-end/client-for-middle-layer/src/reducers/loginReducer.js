import { createAction } from 'redux-actions';

const initialState = {
  isLoggedIn: false,
  token: '',
};

const loginSuccess = createAction('LOGIN_SUCCESS', (token) => ({ token }));
const logout = createAction('LOGOUT');

function loginReducer(state = initialState, action) {
  switch (action.type) {
    case loginSuccess.toString():
      return {
        isLoggedIn: true,
        token: action.payload.token,
      };
    case logout.toString():
      localStorage.removeItem('jwtToken');
      return {
        isLoggedIn: false,
        token: '',
      };
    default:
      return state;
  }
}

export default loginReducer;
export { loginSuccess, logout };