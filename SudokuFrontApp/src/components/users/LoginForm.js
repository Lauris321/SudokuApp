import React from 'react';
import { bindActionCreators } from 'redux';
import {connect} from 'react-redux';
import { LocalForm, Control } from 'react-redux-form';
import { Link } from 'react-router';
import { browserHistory } from 'react-router'

import TextField from 'material-ui/TextField'
import RaisedButton from 'material-ui/RaisedButton';

import * as usersActions from '../../actions/usersActions';

 class LoginForm extends React.Component {
    componentDidMount() {
        this.props.actions.logout();
    }

    handleChange(values) {}
    handleUpdate(form) {}
    handleSubmit(values) {
        browserHistory.push('/');
        this.props.actions.login(values);
    }
    render() {
        return (
            <div className="onlyForm">
                <h1>Log In</h1>
                <LocalForm
                    onUpdate={(form) => this.handleUpdate(form)}
                    onChange={(values) => this.handleChange(values)}
                    onSubmit={(values) => this.handleSubmit(values)}
                >
                    <Control.text model=".username" floatingLabelText="Username" component={TextField} fullWidth={true} />
                    <br/>
                    <Control.text model=".password" floatingLabelText="Password" component={TextField} type="password" fullWidth={true} />
                    <br/>
                    <RaisedButton type="submit" label="Login" primary={true} />
                </LocalForm>
            </div>
        );
    }
}

function mapDispatchToProps(dispatch) {
  return {
    actions: bindActionCreators(usersActions, dispatch)
  };
}

export default connect(null, mapDispatchToProps)(LoginForm)