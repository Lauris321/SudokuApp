using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SudokuAPI.Entities;
using AutoMapper;
using SudokuAPI.CreateContracts;
using Microsoft.EntityFrameworkCore;
using SudokuAPI.UpdateContracts;

namespace SudokuAPI.Services
{
    public class SudokuInfoRepository : ISudokuInfoRepository
    {
        private SudokuInfoContext _context;

        public SudokuInfoRepository(SudokuInfoContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetUsersList()
        {
            return _context.Users
                .OrderBy(c => c.Username)
                .ToList();
        }

        public User GetUser(int userId)
        {
            return _context.Users
                .Where(c => c.Id == userId)
                .FirstOrDefault();
        }

        public bool CreateUser(User user)
        {
            _context.Users.Add(user);

            return (_context.SaveChanges() >= 0);
        }

        public bool UpdateUser(User user, UserCreate userUpdate)
        {
            Mapper.Map(userUpdate, user);

            return (_context.SaveChanges() >= 0);
        }

        public bool DeleteUser(User user)
        {
            _context.Users.Remove(user);
            return (_context.SaveChanges() >= 0);
        }

        public bool UserExists(int userId)
        {
            return _context.Users
                .Where(c => c.Id == userId)
                .FirstOrDefault() != null;
        }

        public IEnumerable<DailySudoku> GetDailySudokuList()
        {
            return _context.DailySudoku
                .OrderBy(c => c.Date)
                .ToList();
        }

        public DailySudoku GetDailySudoku(int dailySudokuId)
        {
            return _context.DailySudoku
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

        public IEnumerable<Challenge> GetDailyChallengesList()
        {
            return _context.Challenges
                .OrderBy(c => c.Date)
                .ToList();
        }

        public Challenge GetChallenge(int challengeId)
        {
            return _context.Challenges
                .Where(c => c.Id == challengeId)
                .FirstOrDefault();
        }

        public bool CreateChallenge(Challenge challenge)
        {
            _context.Challenges.Add(challenge);

            return (_context.SaveChanges() >= 0);
        }

        public bool UpdateChallenge(Challenge challenge, ChallengeUpdate challengeUpdate)
        {
            Mapper.Map(challengeUpdate, challenge);

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
    }
}
