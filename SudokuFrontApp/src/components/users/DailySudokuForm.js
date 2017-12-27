import React from 'react';
import { bindActionCreators } from 'redux';
import {connect} from 'react-redux';
import { LocalForm, Control } from 'react-redux-form';
import { Link } from 'react-router';
import moment from 'moment';

import TextField from 'material-ui/TextField'
import RaisedButton from 'material-ui/RaisedButton';
import SelectField from 'material-ui/SelectField';
import MenuItem from 'material-ui/MenuItem';

import * as dailySudokuActions from '../../actions/dailySudokuActions';

 class DailySudokuForm extends React.Component {
    handleChangeEasy(values) {}
    handleUpdateEasy(form) {}
    handleSubmitEasy(values) {
        this.props.actions.newDailySudoku({
            date: moment().format(),
            difficulty: 0
        });
    }

    handleChangeMedium(values) {}
    handleUpdateMedium(form) {}
    handleSubmitMedium(values) {
        this.props.actions.newDailySudoku({
            date: moment().format(),
            difficulty: 1
        });
    }

    handleChangeHard(values) {}
    handleUpdateHard(form) {}
    handleSubmitHard(values) {
        this.props.actions.newDailySudoku({
            date: moment().format(),
            difficulty: 2
        });
    }
    
    render() {
        return (
            <div>
                <LocalForm
                    className="newSudokuButton"
                    onUpdate={(form) => this.handleUpdateEasy(form)}
                    onChange={(values) => this.handleChangeEasy(values)}
                    onSubmit={(values) => this.handleSubmitEasy(values)}
                >
                    <RaisedButton type="submit" label="Create Easy Sudoku" />
                </LocalForm>
                <LocalForm
                    className="newSudokuButton"
                    onUpdate={(form) => this.handleUpdateMedium(form)}
                    onChange={(values) => this.handleChangeMedium(values)}
                    onSubmit={(values) => this.handleSubmitMedium(values)}
                >
                    <RaisedButton type="submit" label="Create Medium Sudoku" />
                </LocalForm>
                <LocalForm
                    className="newSudokuButton"
                    onUpdate={(form) => this.handleUpdateHard(form)}
                    onChange={(values) => this.handleChangeHard(values)}
                    onSubmit={(values) => this.handleSubmitHard(values)}
                >
                    <RaisedButton type="submit" label="Create Hard Sudoku" />
                </LocalForm>
            </div>
        );
    }
}

function mapDispatchToProps(dispatch) {
  return {
    actions: bindActionCreators(dailySudokuActions, dispatch)
  };
}

export default connect(null, mapDispatchToProps)(DailySudokuForm)