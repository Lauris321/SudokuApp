import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import {connect} from 'react-redux';
import { Link } from 'react-router';
import SudokuGrid from '../sudoku/SudokuGrid';
import moment from 'moment';

import Dialog from 'material-ui/Dialog';
import FlatButton from 'material-ui/FlatButton';
import CircularProgress from 'material-ui/CircularProgress';
import ListItem from 'material-ui/List/ListItem';
import Avatar from 'material-ui/Avatar';
import {Card, CardActions, CardHeader, CardMedia, CardTitle, CardText} from 'material-ui/Card';

import {
    blue300,
    indigo900,
    orange200,
    deepOrange300,
    pink400,
    purple500
  } from 'material-ui/styles/colors';

import * as dailySudokuActions from '../../actions/dailySudokuActions';

class DailySudokuGame extends Component {
    constructor(props) {
        super(props);
        this.state = {
            startMoment: moment(),
            open: false,
            finished: false,
            time: null
        }
        
    }

    componentDidMount() {
        this.props.actions.getOneDailySudoku(parseInt(this.props.params.id, 10));
    }

    handleOpen() {
        let seconds = moment().diff(this.state.startMoment, 'seconds') % 60;
        let minutes = moment().diff(this.state.startMoment, 'minutes') % 60;
        let hours = moment().diff(this.state.startMoment, 'hours') % 24;
        seconds < 10 ? seconds = `0${seconds}` : seconds;
        minutes < 10 ? minutes = `0${minutes}` : minutes;
        hours < 10 ? hours = `0${hours}` : hours;
        const time = `${hours}:${minutes}:${seconds}`;
        this.setState({open: true, finished: true, time: time});
    }

    handleClose() {
        this.setState({open: false});
        this.props.actions.getOneDailySudoku(parseInt(this.props.params.id, 10));
    }

    handleCloseSubmit() {
        this.setState({open: false});

        this.props.actions.postDailySudokuScore(parseInt(this.props.params.id, 10), {
            "completionTime": this.state.time
        });
        this.props.actions.getOneDailySudoku(parseInt(this.props.params.id, 10));
    }

    render() {
        const actions = [
            <FlatButton
              label="Cancel"
              primary={true}
              onClick={this.handleClose.bind(this)}
            />
        ];

        if (this.props.user.authorization !== 0) {
            actions.push(<FlatButton
            label="Submit"
            primary={true}
            keyboardFocused={true}
            onClick={this.handleCloseSubmit.bind(this)}
        />)}
        
        if (this.props.sudokuGrid.indexOf('0') === -1 && !this.state.finished && !this.props.fetching) {
            this.handleOpen();
        }
        const list = this.props.scoresList.map(item => 
            <Link to={`/dailySudoku/${item.id}`}>
              <ListItem
                leftAvatar={<Avatar
                    color={deepOrange300}
                    backgroundColor={purple500}
                    size={40}
                  >
                    {item.username.charAt(0).toUpperCase()}
                  </Avatar>}
                primaryText={item.username}
                secondaryText={item.completionTime}
              />
            </Link>
          );
        
        if (!this.props.fetching) {
            return (
                <div>
                    <Dialog
                        title="Sudoku finished in:"
                        actions={actions}
                        modal={false}
                        open={this.state.open}
                        onRequestClose={this.handleClose.bind(this)}
                    >
                        {`${this.state.time}`}
                    </Dialog>
                    <Card className="gameCard">
                        <CardMedia>
                            <SudokuGrid grid={this.props.sudokuGrid}/>
                        </CardMedia>
                        <CardTitle title="Scores List"/>
                        {list}
                    </Card>
                </div>
            );
        } else {
            return (
                <CircularProgress size={80} thickness={7} />
            );
        }
    }
}

function mapStateToProps(state, ownProps) {
    return {
        date: state.dailySudoku.date,
        sudokuGrid: state.dailySudoku.sudokuGrid,
        difficulty: state.dailySudoku.difficulty,
        scoresList: state.dailySudoku.scoresList,
        fetching: state.dailySudoku.fetching,
        user: state.user
    };
}

function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(dailySudokuActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(DailySudokuGame);