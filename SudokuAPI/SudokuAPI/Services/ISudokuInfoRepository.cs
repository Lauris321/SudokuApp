using SudokuAPI.CreateContracts;
using SudokuAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SudokuAPI.UpdateContracts;

namespace SudokuAPI
{
    public interface ISudokuInfoRepository
    {
        IEnumerable<User> GetUsersList();
        User GetUser(int userId);
        bool CreateUser(User user);
        bool UpdateUser(User user, UserCreate userUpdate);
        bool DeleteUser(User user);
        bool UserExists(int userId);
        IEnumerable<DailySudoku> GetDailySudokuList();
        DailySudoku GetDailySudoku(int dailySudokuId);
        bool CreateDailySudoku(DailySudoku dailySudoku);
        bool UpdateDailySudoku(DailySudoku dailySudoku, DailySudokuCreate dailySudokuUpdate);
        bool DeleteDailySudoku(DailySudoku dailySudoku);
        IEnumerable<Challenge> GetDailyChallengesList();
        Challenge GetChallenge(int challengeId);
        bool CreateChallenge(Challenge challenge);
        bool UpdateChallenge(Challenge challenge, ChallengeUpdate challengeUpdate);
        bool DeleteChallenge(Challenge challenge);
        bool ChallengeExists(int challengeId);
        IEnumerable<Comment> GetCommentsList(int challengeId);
        Comment GetComment(int challengeId, int commentId);
        bool CreateComment(Comment commentEntity);
        bool UpdateComment(int challengeId, int commentId, CommentCreate comment);
        bool DeleteComment(Comment comment);
    }
}
