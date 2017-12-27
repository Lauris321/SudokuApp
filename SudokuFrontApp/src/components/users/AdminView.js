import React from 'react';
import { bindActionCreators } from 'redux';
import {connect} from 'react-redux';
import { LocalForm, Control } from 'react-redux-form';
import { Link } from 'react-router';
import DailySudokuListDelete from '../dailySudoku/DailySudokuListDelete';

import TextField from 'material-ui/TextField'
import RaisedButton from 'material-ui/RaisedButton';
import SelectField from 'material-ui/SelectField';
import MenuItem from 'material-ui/MenuItem';

import DailySudokuForm from './DailySudokuForm';
import * as usersActions from '../../actions/usersActions';

 class AdminView extends React.Component {
    constructor(props) {
        super(props)
        this.state = {
            value: 1
        }
    }
    componentDidMount() {
    }

    handleChange(values) {}
    handleUpdate(form) {}
    handleSubmit(values) {
    }
    
    render() {
        return (
            <div>
                <div className="onlyFormWide">
                    <h1>Create new sudoku</h1>
                    <DailySudokuForm />
                    
                </div>
                {/* <DailySudokuListDelete/> */}
            </div>
        );
    }
}

function mapDispatchToProps(dispatch) {
  return {
    actions: bindActionCreators(usersActions, dispatch)
  };
}

export default connect(null, mapDispatchToProps)(AdminView)