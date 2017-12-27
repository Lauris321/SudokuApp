import * as types from '../actions/actionTypes';

const defaultState = {
  list: [],
  fetching: true
};

export default function dailySudokuList(state = defaultState, action) {
  switch (action.type) {
    case types.GET_DAILY_SUDOKU_LIST_SUCCESS:
      return Object.assign({}, state, {
        list: action.dailySudokuList,
        fetching: false
      });

    case types.GET_DAILY_SUDOKU_LIST:
      return Object.assign({}, state, {
        fetching: true
      });

    default:
      return state;
  }
}
