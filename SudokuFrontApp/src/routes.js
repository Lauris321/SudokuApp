import React from 'react';
import {Route, IndexRoute} from 'react-router';
import SudokuApp from './components/SudokuApp';
import HomePage from './components/home/HomePage';
import DailySudokuList from './components/dailySudoku/DailySudokuList';
import DailySudokuGame from './components/dailySudoku/DailySudokuGame';
import RegistrationForm from './components/users/RegistrationForm';
import LoginForm from './components/users/LoginForm';
import AdminView from './components/users/AdminView';

export default (
  <Route path="/" component={SudokuApp}>
    <IndexRoute component={HomePage}/>
    <Route path="dailySudoku" component={DailySudokuList}/>
    <Route path="dailySudoku/:id" component={DailySudokuGame}/>
    <Route path="registration" component={RegistrationForm}/>
    <Route path="login" component={LoginForm}/>
    <Route path="admin" component={AdminView}/>
  </Route>
);