import * as types from './actionTypes';

function authHeader() {
    // return authorization header with jwt token
    let user = JSON.parse(localStorage.getItem('user'));
 
    if (user && user.token) {
        return { 'Authorization': 'Bearer ' + user.token };
    } else {
        return {};
    }
}

export let submitUserRegistration = (user) => {
    return (dispatch) => {
        fetch(`http://localhost:51240/api/users`, {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(user)
        });
    };
};

export function loginRequest() {
    return {type: types.LOGIN_REQUEST};
}

export function loginSuccess(user) {
    return {type: types.LOGIN_SUCCESS, user};
}

export let login = (login) => {
    loginRequest();
    return (dispatch) => {
        fetch(`http://localhost:51240/api/users/login`, {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify(login)
        }).then(response => {
            if (response.ok) {
                response.json().then(user => {
                    dispatch(loginSuccess(user));
                });
            }
        });
    };
};

export let logout = () => {
    return (dispatch) => {
        dispatch({type: types.LOGOUT});
    };
};