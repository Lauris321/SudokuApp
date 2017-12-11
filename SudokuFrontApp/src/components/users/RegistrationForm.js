import React from 'react';
import { bindActionCreators } from 'redux';
import {connect} from 'react-redux';
import { LocalForm, Control } from 'react-redux-form';
import { Link } from 'react-router';

import TextField from 'material-ui/TextField'
import RaisedButton from 'material-ui/RaisedButton';

import * as usersActions from '../../actions/usersActions';

 class RegistrationForm extends React.Component {
  handleChange(values) {}
  handleUpdate(form) {}
  handleSubmit(values) {
    browserHistory.push('/login');
    this.props.actions.submitUserRegistration(values);
  }
  render() {
    return (
      <div className="onlyForm">
        <h1>Sign Up</h1>
        <LocalForm
          onUpdate={(form) => this.handleUpdate(form)}
          onChange={(values) => this.handleChange(values)}
          onSubmit={(values) => this.handleSubmit(values)}
        >
          <Control.text model=".username" floatingLabelText="Username" component={TextField} fullWidth={true} />
          <br/>
          <Control.text model=".email" floatingLabelText="Email" component={TextField} fullWidth={true} />
          <br/>
          <Control.text model=".password" floatingLabelText="Password" component={TextField} type="password" fullWidth={true} />
          <br/>
          <Link to="login"><RaisedButton type="submit" label="Register" primary={true} /></Link>
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

export default connect(null, mapDispatchToProps)(RegistrationForm)