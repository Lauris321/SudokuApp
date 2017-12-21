using SudokuAPI.CreateContracts;
using SudokuAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SudokuAPI.UpdateContracts;
using SudokuAPI.Enumerations;

namespace SudokuAPI
{
    public interface ISudokuInfoRepository
    {
        IEnumerable<User> GetUsersList();
        User GetUser(int userId);
        User GetUser(string userName);
        bool CreateUser(User user);
        bool CreateFriendship(UserUser friendshipEntity);
        bool UpdateUser(User user, UserCreate userUpdate);
        bool DeleteUser(User user);
        bool UserExists(int userId);
        bool UpdateFriendship(int userId, int friendId, FriendshipStatus status);
        bool FriendshipExists(int userId, int friendId);
        bool IsAdmin(int userId);

        IEnumerable<DailySudoku> GetDailySudokuList();
        DailySudoku GetDailySudoku(int dailySudokuId);
        bool CreateDailySudoku(DailySudoku dailySudoku);
        bool UpdateDailySudoku(DailySudoku dailySudoku, DailySudokuCreate dailySudokuUpdate);
        bool DeleteDailySudoku(DailySudoku dailySudoku);
        bool createSudokuScore(DailySudokuUser dailySudokuUserEntity);
        bool SudokuExists(int dailySudokuId);
        bool SudokuScoreExists(int dailySudokuId, int userId);

        IEnumerable<Challenge> GetChallengesList();
        Challenge GetChallenge(int challengeId);
        bool CreateChallenge(Challenge challenge);
        bool UpdateChallenge(Challenge challenge);
        bool DeleteChallenge(Challenge challenge);
        bool ChallengeExists(int challengeId);

        IEnumerable<Comment> GetCommentsList(int challengeId);
        Comment GetComment(int challengeId, int commentId);
        bool CreateComment(Comment commentEntity);
        bool UpdateComment(int challengeId, int commentId, CommentCreate comment);
        bool DeleteComment(Comment comment);

		void DropDB();
	}
}
