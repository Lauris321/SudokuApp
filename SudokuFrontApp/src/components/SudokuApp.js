import React, {Component} from 'react';
import PropTypes from 'prop-types';
import {connect} from 'react-redux';
import { Link } from 'react-router';

import ListItem from 'material-ui/List/ListItem';
import IconMenu from 'material-ui/IconMenu';
import IconButton from 'material-ui/IconButton';
import FontIcon from 'material-ui/FontIcon';
import NavigationExpandMoreIcon from 'material-ui/svg-icons/navigation/expand-more';
import MenuItem from 'material-ui/MenuItem';
import DropDownMenu from 'material-ui/DropDownMenu';
import RaisedButton from 'material-ui/RaisedButton';
import Avatar from 'material-ui/Avatar';
import {Toolbar, ToolbarGroup, ToolbarSeparator, ToolbarTitle} from 'material-ui/Toolbar';
import {AppBar, Tabs, Tab} from 'material-ui';
import {
  blue300,
  indigo900,
  orange200,
  deepOrange300,
  pink400,
  purple500
} from 'material-ui/styles/colors';

import BottomNav from './BottomNav';
import * as usersActions from '../actions/usersActions';

class SudokuApp extends Component {
  render() {
    console.log(this.props);
    return (
      <div>
        <AppBar title="Sudoku App">
            {this.props.user.authorization === 0 ? 
                <ToolbarGroup>
                  <Link to="/dailySudoku"><RaisedButton label="Daily Sudoku" secondary={true} /></Link>
                  <ToolbarSeparator />
                  <Link to="registration"><RaisedButton label="Sign Up" primary={true} /></Link>
                  <Link to="login"><RaisedButton label="Log In" /></Link>
                </ToolbarGroup> : <ToolbarGroup>
                  <Link to="/dailySudoku"><RaisedButton label="Daily Sudoku" secondary={true} /></Link>
                  <ToolbarSeparator />
                  <ListItem
                    disabled={true}
                    leftAvatar={
                      <Avatar
                        color={deepOrange300}
                        backgroundColor={purple500}
                        size={40}
                      >
                        {this.props.user.username.charAt(0).toUpperCase()}
                      </Avatar>
                    }
                  >
                    {this.props.user.username}
                  </ListItem>
                  <Link to="login"><RaisedButton label="Logout" /></Link>
                </ToolbarGroup>
            }
        </AppBar>
        <div className="container-fluid">
          {this.props.children}
        </div>
        <BottomNav />
      </div>
    );
  }
}

SudokuApp.propTypes = {
  children: PropTypes.object.isRequired
};

function mapStateToProps(state, ownProps) {
  return {
    user: state.user,
    fetching: state.user.fetching
  };
}

export default connect(mapStateToProps)(SudokuApp);