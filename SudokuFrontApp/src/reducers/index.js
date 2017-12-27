import { combineReducers } from 'redux';
import { combineForms, createForms } from 'react-redux-form';
import dailySudokuList from './dailySudokuListReducer';
import dailySudoku from './dailySudokuReducer';
import user from './usersReducer';

const rootReducer = combineReducers({
    dailySudokuList,
    dailySudoku,
    user
});

export default rootReducer;