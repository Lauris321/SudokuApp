import * as types from '../actions/actionTypes';

const defaultState = {
    authorization: 0,
    id: null,
    username: '',
    email: '',
    token: '',
    dailySudokuScoresList: [],
    fetching: false
};

export default function user(state = defaultState, action) {
    switch (action.type) {

    case types.LOGIN_SUCCESS:
        return Object.assign({}, state, {
            authorization: action.user.user.authorization,
            id: action.user.user.id,
            username: action.user.user.username,
            email: action.user.user.email,
            token: action.user.token,
            dailySudokuScoresList: action.user.user.dailySudokuScoresList,
            fetching: false
        });

    case types.LOGIN_REQUEST:
        return Object.assign({}, state, {
            fetching: true
        });

    case types.LOGOUT:{
        return Object.assign({}, ...state, defaultState);
    }
    default:
        return state;
    }
}
