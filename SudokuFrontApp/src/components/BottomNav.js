import React, {Component} from 'react';
import FontIcon from 'material-ui/FontIcon';
import {BottomNavigation, BottomNavigationItem} from 'material-ui/BottomNavigation';
import Paper from 'material-ui/Paper';
import IconLocationOn from 'material-ui/svg-icons/communication/location-on';
import FlatButton from 'material-ui/FlatButton';
import EditIcon from 'material-ui/svg-icons/action/build';
import HomeIcon from 'material-ui/svg-icons/action/home';
import { Link } from 'react-router';
import {connect} from 'react-redux';

class BottomNav extends Component {
    constructor(props) {
        super(props);
        this.state = {
            selectedIndex: 0
        };
    }
  

    select(index) {
        this.setState({selectedIndex: index});
    }

    render() {
        return (
        <Paper zDepth={1} className="bottomNav navbar-fixed-bottom">
            <BottomNavigation selectedIndex={this.state.selectedIndex}>
                <Link to={'/'}><BottomNavigationItem
                    label="Home"
                    icon={<HomeIcon />}
                    onClick={() => this.select(2)}
                /></Link>
                {this.props.user.authorization === 2 &&
                    <Link to={'admin'}><BottomNavigationItem
                        label="Admin Tools"
                        icon={<EditIcon />}
                        onClick={() => this.select(1)}
                    /></Link>
                }
                
                <BottomNavigationItem
                    label="GitHub Link"
                    icon={<FontIcon className="muidocs-icon-custom-github" />}
                    onClick={() => this.select(3)}
                    href="https://github.com/Lauris321/SudokuApp"
                    target="_blank"
                />
                
            </BottomNavigation>
        </Paper>
        );
    }
}

function mapStateToProps(state, ownProps) {
    return {
      user: state.user,
      fetching: state.user.fetching
    };
  }

export default connect(mapStateToProps)(BottomNav);