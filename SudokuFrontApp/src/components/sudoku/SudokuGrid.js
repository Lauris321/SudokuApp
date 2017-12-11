import React from 'react';
import Box from './SudokuGridBox';

const convertStringToGrid = (string) => {
	const array = string.split("").map(Number);
	let grid = [
		array.slice(0, 9),
		array.slice(9, 18),
		array.slice(18, 27),
		array.slice(27, 36),
		array.slice(36, 45),
		array.slice(45, 54),
		array.slice(54, 63),
		array.slice(63, 72),
		array.slice(72, 81)
	];
	
	return grid;
};

/* Grid Component */
class SudokuGrid extends React.Component {

	render() {
		const {grid} = this.props;
		const convGrid = convertStringToGrid(grid);
		const renderBox = (row, val, col) => {
			return (
				<Box
					row={row}
					col={col}
					val={val}
					grid={grid}
				/>
			);
		};
		const renderRow = (vals, row) => {
			let colNum = 0;
			let boxesArray = [];
			vals.map(val =>{
				boxesArray.push(renderBox(row, val, colNum));
				colNum++;
			});
			return (
				<tr key={row}>
					{boxesArray}
				</tr>
			);			
		};

		const renderGrid = () => {
			let rowNum = 0;
			let linesArray = [];
			convGrid.map(row => {
				linesArray.push(renderRow(row, rowNum));
				rowNum++;
			})
			return (
				<table>
					<tbody>
						{linesArray}
					</tbody>
				</table>
			);			
		};
		
		return (
			renderGrid()
		);
	}
}

export default SudokuGrid;
