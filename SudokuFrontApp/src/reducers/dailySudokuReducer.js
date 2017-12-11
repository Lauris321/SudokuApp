import * as types from '../actions/actionTypes';

const defaultState = {
  id: 0,
  date: "",
  sudokuGrid: "",
  difficulty: 0,
  scoresList: [],
  fetching: true
};

export default function dailySudoku(state = defaultState, action) {
  switch (action.type) {
    case types.GET_DAILY_SUDOKU_SUCCESS:
      return Object.assign({}, state, {
        date: action.dailySudoku.date,
        sudokuGrid: action.dailySudoku.sudokuGrid,
        difficulty: action.dailySudoku.difficulty,
        scoresList: action.dailySudoku.scoresList,
        fetching: false
      });

    case types.GET_DAILY_SUDOKU:
      return Object.assign({}, state, {
        fetching: true
      });

    case types.UPDATE_DAILY_SUDOKU:
      return Object.assign({}, state, {
        sudokuGrid: action.sudokuGridString
      });
    default:
      return state;
  }
}
