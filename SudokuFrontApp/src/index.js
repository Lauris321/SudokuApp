import 'babel-polyfill';

import React from 'react';
import { render } from 'react-dom';
import {Provider} from 'react-redux';
import {Router, browserHistory} from 'react-router';
import routes from './routes';
import configureStore from './configureStore';
import '../node_modules/bootstrap/dist/css/bootstrap.min.css';
import darkBaseTheme from 'material-ui/styles/baseThemes/darkBaseTheme';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import getMuiTheme from 'material-ui/styles/getMuiTheme';

import SudokuApp from './components/SudokuApp';
import * as dailySudokuActions from './actions/dailySudokuActions';

const persistedState = localStorage.getItem('sudokuState') ? JSON.parse(localStorage.getItem('sudokuState')) : {}

const store = configureStore(persistedState);

store.subscribe(()=>{
  localStorage.setItem('sudokuState', JSON.stringify(store.getState()))
})

render(
  <Provider store={store}>
    <MuiThemeProvider>
      <Router history={browserHistory} routes={routes}/>
    </MuiThemeProvider>
  </Provider>,
  document.getElementById('sudokuApp')
);