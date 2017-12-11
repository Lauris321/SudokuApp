import React from 'react';
import { bindActionCreators } from 'redux';
import {connect} from 'react-redux';

import * as dailySudokuActions from '../../actions/dailySudokuActions';

/* Box Component */

String.prototype.replaceAt=function(index, character) {
	return this.substr(0, index) + character + this.substr(index+character.length);
}

class Box extends React.Component {
	constructor(props) {
		super(props);
	}

	handleChange(e){
		const {row, col} = this.props;
		const range = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
		let val = parseInt(e.target.value || 0);
		console.log(val);
		if (range.indexOf(val) > -1) {
			val = val.toString();
			const index = row * 9 + col;
			const newGrid = this.props.grid.replaceAt(index, val);
			this.props.actions.updateDailySudoku(newGrid);
		}
	}

	render() {
		const {row, col, val} = this.props;
		const input = (
			<input
				ref='input'
				value={val ? val : ''}
				className="sudoku-box-input"
				onChange={this.handleChange.bind(this)}
			/>
		);

		return (
			<td>
                {input}
			</td>
		);
	}
}

function mapStateToProps(state, ownProps) {
    return {
        difficulty: state.dailySudoku.difficulty
    };
}

function mapDispatchToProps(dispatch) {
	return {
		actions: bindActionCreators(dailySudokuActions, dispatch)
	};
}

export default connect(mapStateToProps, mapDispatchToProps)(Box);
