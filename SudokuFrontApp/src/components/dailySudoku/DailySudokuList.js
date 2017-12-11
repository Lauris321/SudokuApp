import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import PropTypes from 'prop-types';
import {connect} from 'react-redux';
import { Link } from 'react-router';
import moment from 'moment';

import {List, ListItem} from 'material-ui/List';
import ActionInfo from 'material-ui/svg-icons/action/info';
import Divider from 'material-ui/Divider';
import Subheader from 'material-ui/Subheader';
import Avatar from 'material-ui/Avatar';
import FileFolder from 'material-ui/svg-icons/image/grid-on';
import ActionAssignment from 'material-ui/svg-icons/action/assignment';
import {blue300, blue500, blue700} from 'material-ui/styles/colors';
import EditorInsertChart from 'material-ui/svg-icons/editor/insert-chart';
import CircularProgress from 'material-ui/CircularProgress';

import * as dailySudokuActions from '../../actions/dailySudokuActions';

const mapDifficulty = {
  0: {
    color: blue300,
    text: "Easy"
  },
  1: {
      color: blue500,
      text: "Medium"
  },
  2: {
    color: blue700,
    text: "Hard"
  }
};

class DailySudokuList extends Component {
  constructor(props) {
    super(props);
  }

  componentDidMount() {
    this.props.actions.getAllDailySudoku();
  }

  render() {
    if (!this.props.fetching) {
      const list = this.props.dailySudokuList.map(item => 
        <Link to={`/dailySudoku/${item.id}`}>
          <ListItem
            leftAvatar={<Avatar icon={<FileFolder />} />}
            primaryText={mapDifficulty[item.difficulty].text}
            secondaryText={moment(item.date).calendar()}
            key={item.id}
            style={{backgroundColor: mapDifficulty[item.difficulty].color}}
          />
        </Link>
      );
      return (
        <div className="onlyForm">
          <h1>Daily Sudoku List </h1>
          <List>
            {/* <Subheader inset={true}>Daily Sudoku List</Subheader> */}
            {list}
          </List>
          <br />
        </div>
      );
    } else {
      return (
        <CircularProgress className="spinner center-block"  size={80} thickness={7} />
      );
    }
  }
}

DailySudokuList.propTypes = {
  actions: PropTypes.object.isRequired,
  dailySudokuList: PropTypes.array.isRequired,
  fetching: PropTypes.bool.isRequired
};

function mapStateToProps(state, ownProps) {
  return {
    dailySudokuList: state.dailySudokuList.list,
    fetching: state.dailySudokuList.fetching
  };
}

function mapDispatchToProps(dispatch) {
  return {
    actions: bindActionCreators(dailySudokuActions, dispatch)
  };
}

export default connect(mapStateToProps, mapDispatchToProps)(DailySudokuList);