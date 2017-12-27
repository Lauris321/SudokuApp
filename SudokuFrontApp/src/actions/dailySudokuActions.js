import * as types from './actionTypes';

function authHeader() {
    // return authorization header with jwt token
    let user = JSON.parse(localStorage.getItem('user'));
 
    if (user && user.token) {
        return { 'Authorization': 'Bearer ' + user.token };
    } else {
        return {};
    }
}

export function getDailySudokuList() {
    return {type: types.GET_DAILY_SUDOKU_LIST};
}

export function getDailySudokuListSuccess(dailySudokuList) {
    return {type: types.GET_DAILY_SUDOKU_LIST_SUCCESS, dailySudokuList};
}

export let getAllDailySudoku = () => {
    return (dispatch) => {
        dispatch(getDailySudokuList());
        fetch('http://localhost:51240/api/sudoku', { 
            method: 'GET'
        }).then(response => {
            if (response.ok) {
                response.json().then(dailySudokuList => {
                    dispatch(getDailySudokuListSuccess(dailySudokuList));
                });
            }
        });
    };
};

export function getDailySudoku() {
    return {type: types.GET_DAILY_SUDOKU};
}

export function getDailySudokuSuccess(dailySudoku) {
    return {type: types.GET_DAILY_SUDOKU_SUCCESS, dailySudoku};
}

export let getOneDailySudoku = (id) => {
    return (dispatch) => {
        dispatch(getDailySudoku());
        fetch(`http://localhost:51240/api/sudoku/${id}`, {
            method: 'GET'
        }).then(response => {
            if (response.ok) {
                response.json().then(dailySudoku => {
                    dispatch(getDailySudokuSuccess(dailySudoku));
                });
            }
        });
    };
};

export let newDailySudoku = (sudoku) => {
    let sudokuState = JSON.parse(localStorage.getItem('sudokuState'));
    return (dispatch) => {
        fetch(`http://localhost:51240/api/sudoku`, {
            method: 'POST',
            headers: { 'Authorization': 'Bearer ' + sudokuState.user.token , 'Content-Type': 'application/json' },
            body: JSON.stringify(sudoku)
        }).then(response => {
            if (response.ok) {
            }
        });
    };
};

export function updateDailySudoku(sudokuGridString) {
    return {type: types.UPDATE_DAILY_SUDOKU, sudokuGridString};
}

export let postDailySudokuScore = (id, score) => {
    let sudokuState = JSON.parse(localStorage.getItem('sudokuState'));
    return (dispatch) => {
        fetch(`http://localhost:51240/api/sudoku/${id}/addScore`, {
            method: 'POST',
            headers: { 'Authorization': 'Bearer ' + sudokuState.user.token , 'Content-Type': 'application/json' },
            body: JSON.stringify(score)
        }).then(response => {
            if (response.ok) {
                dispatch(getOneDailySudoku(id));
            }
        });
    };
};