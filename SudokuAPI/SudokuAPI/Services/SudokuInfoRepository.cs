using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SudokuAPI.Entities;
using AutoMapper;
using SudokuAPI.CreateContracts;
using Microsoft.EntityFrameworkCore;
using SudokuAPI.UpdateContracts;
using SudokuAPI.Enumerations;

namespace SudokuAPI.Services
{
    public class SudokuInfoRepository : ISudokuInfoRepository
    {
        private SudokuInfoContext _context;

        public SudokuInfoRepository(SudokuInfoContext context)
        {
            _context = context;
        }

        //Users
        public IEnumerable<User> GetUsersList()
        {
            return _context.Users
                .OrderBy(c => c.Username)
                .ToList();
        }

        public User GetUser(int userId)
        {
            return _context.Users
                .Include(u => u.CreatedChallengeList)
                .Include(u => u.AssignedChallengesList)
                .Include(u => u.DailySudokuScoresList)
                .Include(u => u.AcceptedFriendshipsList)
                .Include(u => u.RequestedFriendshipsList)
                .Where(c => c.Id == userId)
                .FirstOrDefault();
        }

        public User GetUser(string userName)
        {
            return _context.Users
               .Where(c => c.Username == userName)
               .FirstOrDefault();
        }

        public bool CreateUser(User user)
        {
            _context.Users.Add(user);

            return (_context.SaveChanges() >= 0);
        }

        public bool CreateFriendship(UserUser friendshipEntity)
        {
            User user = GetUser(friendshipEntity.UserId);

            user.RequestedFriendshipsList.Add(friendshipEntity);

            return (_context.SaveChanges() >= 0);
        }

        public bool UpdateUser(User user, UserCreate userUpdate)
        {
			//Mapper.Map(userUpdate, user);
			user.Username = userUpdate.Username;
			user.Password = userUpdate.Password;
			user.Email = userUpdate.Email;
            return (_context.SaveChanges() >= 0);
        }

        public bool DeleteUser(User user)
        {
            _context.Users.Remove(user);
            return (_context.SaveChanges() >= 0);
        }

        public bool UpdateFriendship(int userId, int friendId, FriendshipStatus status)
        {
            _context.Users
                .Where(c => c.Id == userId)
                .FirstOrDefault()
                .AcceptedFriendshipsList
                .First(f => f.User1Id == userId && f.UserId == friendId)
                .Status = status;
            return (_context.SaveChanges() >= 0);
        }

        public bool UserExists(int userId)
        {
            return _context.Users
                .Where(c => c.Id == userId)
                .FirstOrDefault() != null;
        }

        public bool IsAdmin(int userId)
        {
            return _context.Users
                .Where(c => c.Id == userId)
                .FirstOrDefault().Authorization == Authorization.Admin;
        }

        public bool FriendshipExists(int userId, int friendId)
        {
            User user = GetUser(userId);

            var request = user.RequestedFriendshipsList
                .Where(c => c.User1Id == friendId)
                .FirstOrDefault();

            var accepted = user.AcceptedFriendshipsList
                .Where(c => c.UserId == friendId)
                .FirstOrDefault();

            bool requestExists = request != null && 
                request.Status == FriendshipStatus.Accepted;

            bool acceptedExists = accepted != null && 
                accepted. Status == FriendshipStatus.Accepted;

            return requestExists || acceptedExists;
        }

        //DailySudoku
        public IEnumerable<DailySudoku> GetDailySudokuList()
        {
            return _context.DailySudoku
                .OrderBy(c => c.Date)
                .ToList();
        }

        public DailySudoku GetDailySudoku(int dailySudokuId)
        {
            return _context.DailySudoku
                .Include(c => c.ScoresList)
                .Where(c => c.Id == dailySudokuId)
                .FirstOrDefault();
        }

        public bool CreateDailySudoku(DailySudoku dailySudoku)
        {
            _context.DailySudoku.Add(dailySudoku);

            return (_context.SaveChanges() >= 0);
        }

        public bool UpdateDailySudoku(DailySudoku dailySudoku, DailySudokuCreate dailySudokuUpdate)
        {
            Mapper.Map(dailySudokuUpdate, dailySudoku);

            return (_context.SaveChanges() >= 0);
        }

        public bool DeleteDailySudoku(DailySudoku dailySudoku)
        {
            _context.DailySudoku.Remove(dailySudoku);

            return (_context.SaveChanges() >= 0);
        }

        public bool createSudokuScore(DailySudokuUser dailySudokuUserEntity)
        {
            DailySudoku dailySudoku = GetDailySudoku(dailySudokuUserEntity.DailySudokuId);

            dailySudoku.ScoresList.Add(dailySudokuUserEntity);

            return (_context.SaveChanges() >= 0);
        }

        public bool SudokuExists(int dailySudokuId)
        {
            return _context.DailySudoku
                .Where(d => d.Id == dailySudokuId)
                .FirstOrDefault() != null;
        }

        public bool SudokuScoreExists(int dailySudokuId, int userId)
        {
            DailySudoku dailySudoku = GetDailySudoku(dailySudokuId);

            return dailySudoku.ScoresList
                .Where(s => s.UserId == userId)
                .FirstOrDefault() != null;
        }

        //Challenges
        public IEnumerable<Challenge> GetChallengesList()
        {
            return _context.Challenges
                .Include(c => c.Creator)
                .OrderBy(c => c.Date)
                .ToList();
        }

        public Challenge GetChallenge(int challengeId)
        {
            Challenge challenge = _context.Challenges
                .Include(c => c.Creator)
                .Include(c => c.CommentsList)
                .Include(c => c.AssigneesList)
                .Where(c => c.Id == challengeId)
                .FirstOrDefault();

            return challenge;
        }

        public bool CreateChallenge(Challenge challenge)
        {
            _context.Challenges.Add(challenge);

            return (_context.SaveChanges() >= 0);
        }

        public bool UpdateChallenge(Challenge challenge)
        {
            var challengeToupdate = GetChallenge(challenge.Id);
            challengeToupdate = challenge;

            return (_context.SaveChanges() >= 0);
        }

        public bool DeleteChallenge(Challenge challenge)
        {
            _context.Challenges.Remove(challenge);

            return (_context.SaveChanges() >= 0);
        }

        public bool ChallengeExists(int challengeId)
        {
            return _context.Challenges
                .Where(c => c.Id == challengeId)
                .FirstOrDefault() != null;
        }

        //Comments
        public IEnumerable<Comment> GetCommentsList(int challengeId)
        {
            return _context.Comments
                .Where(c => c.ChallengeId == challengeId)
                .OrderBy(c => c.Date)
                .ToList();
        }

        public Comment GetComment(int challengeId, int commentId)
        {
            return _context.Comments
                .Where(c => c.ChallengeId == challengeId && c.Id == commentId)
                .FirstOrDefault();
        }

        public bool CreateComment(Comment commentEntity)
        {
            var challenge = GetChallenge(commentEntity.ChallengeId);
            challenge.CommentsList.Add(commentEntity);

            return (_context.SaveChanges() >= 0);
        }

        public bool UpdateComment(int challengeId, int commentId, CommentCreate commentUpdate)
        {
            var comment = GetComment(challengeId, commentId);
            Mapper.Map(commentUpdate, comment);

            return (_context.SaveChanges() >= 0);
        }

        public bool DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);

            return (_context.SaveChanges() >= 0);
        }

		public void DropDB()
		{
			_context.Database.ExecuteSqlCommand("DELETE FROM USERUSER");
			_context.Database.ExecuteSqlCommand("DELETE FROM DAILYSUDOKUUSER");
			_context.Database.ExecuteSqlCommand("DELETE FROM CHALLENGEUSER");
			_context.Database.ExecuteSqlCommand("DELETE FROM USERS; DBCC CHECKIDENT('SudokuInfoDBTest.dbo.USERS', RESEED, 0)");
			_context.Database.ExecuteSqlCommand("DELETE FROM CHALLENGES; DBCC CHECKIDENT('SudokuInfoDBTest.dbo.CHALLENGES', RESEED, 0)");
			_context.Database.ExecuteSqlCommand("DELETE FROM COMMENTS; DBCC CHECKIDENT('SudokuInfoDBTest.dbo.COMMENTS', RESEED, 0)");
			_context.Database.ExecuteSqlCommand("DELETE FROM DAILYSUDOKU; DBCC CHECKIDENT('SudokuInfoDBTest.dbo.DAILYSUDOKU', RESEED, 0)");
			
		}
	}
}
