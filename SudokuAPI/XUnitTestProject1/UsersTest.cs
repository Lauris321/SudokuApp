using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SudokuAPI.Controllers;
using SudokuAPI.CreateContracts;
using SudokuAPI.Entities;
using SudokuAPI.Enumerations;
using SudokuAPI.Models;
using SudokuAPI.Services;
using Xunit;

namespace XUnitTestProject1
{
    public class UsersTest
    {
		private UsersController _usersController;
		private SudokuController _sudokuController;

		public UsersTest()
		{
			AutoMapper.Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<SudokuAPI.CreateContracts.UserCreate, SudokuAPI.Entities.User>();
				cfg.CreateMap<SudokuAPI.Entities.User, SudokuAPI.Models.UserListDto>();
				cfg.CreateMap<SudokuAPI.Entities.DailySudoku, SudokuAPI.Models.DailySudokuListDto>();
				cfg.CreateMap<SudokuAPI.CreateContracts.DailySudokuCreate, SudokuAPI.Entities.DailySudoku>();
				cfg.CreateMap<SudokuAPI.UpdateContracts.ChallengeUpdate, SudokuAPI.Entities.Challenge>();
				cfg.CreateMap<SudokuAPI.Entities.Comment, SudokuAPI.Models.CommentDto>();
				cfg.CreateMap<SudokuAPI.Entities.Challenge, SudokuAPI.Models.ChallengeDto>();
				cfg.CreateMap<SudokuAPI.Entities.DailySudoku, SudokuAPI.Models.DailySudokuDto>();
				cfg.CreateMap<SudokuAPI.CreateContracts.CommentCreate, SudokuAPI.Entities.Comment>();
			});

			var connectionStr = "Server=(localdb)\\mssqllocaldb;Database=SudokuInfoDBTest;Trusted_Connection=True;";
			var optionsBuilder = new DbContextOptionsBuilder<SudokuInfoContext>();
			optionsBuilder.UseSqlServer(connectionStr);
			var sudokuRepository = new SudokuInfoRepository(new SudokuInfoContext(optionsBuilder.Options));
			sudokuRepository.DropDB();

			_usersController = new UsersController(sudokuRepository, new HttpContextAccessor(), new HashService());
			_sudokuController = new SudokuController(sudokuRepository, new SudokuGeneratorService(), new HttpContextAccessor());

			_usersController.CreateUser(new UserCreate
			{
				Username = "admin",
				Password = "admin",
				Email = "admin@admin.com"
			});

			_usersController.CreateUser(new UserCreate
			{
				Username = "user",
				Password = "user",
				Email = "user@user.com"
			});

			_usersController.CreateUser(new UserCreate
			{
				Username = "userToDelete",
				Password = "userToDelete",
				Email = "userToDelete@userToDelete.com"
			});

			_sudokuController.CreateDailySudoku(new DailySudokuCreate
			{
				Date = DateTime.Now,
				Difficulty = Difficulty.Easy
			});

			_sudokuController.CreateDailySudoku(new DailySudokuCreate
			{
				Date = DateTime.Now,
				Difficulty = Difficulty.Intermediate
			});

			_sudokuController.CreateDailySudoku(new DailySudokuCreate
			{
				Date = DateTime.Now,
				Difficulty = Difficulty.Hard
			});


		}

		[Fact]
        public void TestLogin()
        {
			IActionResult adminRes1 = _usersController.Login(new AuthRequest
			{
				UserName = "admin",
				Password = "admin"
			});

			IActionResult adminRes2 = _usersController.Login(new AuthRequest
			{
				UserName = "admin",
				Password = "asdfgh"
			});

			IActionResult userRes1 = _usersController.Login(new AuthRequest
			{
				UserName = "user",
				Password = "user"
			});

			IActionResult userRes2 = _usersController.Login(new AuthRequest
			{
				UserName = "user",
				Password = "asdfgh"
			});

			Assert.IsType<OkObjectResult>(adminRes1);
			Assert.IsType<BadRequestObjectResult>(adminRes2);
			Assert.IsType<OkObjectResult>(userRes1);
			Assert.IsType<BadRequestObjectResult>(userRes2);
		}

		[Fact]
		public void TestUsersList()
		{
			IActionResult res = _usersController.GetUsersList();

			Assert.IsType<OkObjectResult>(res);

			OkObjectResult ok = res as OkObjectResult;
			ICollection<UserListDto> list = ok.Value as ICollection<UserListDto>;
			Assert.True(list.Count == 3);
		}

		[Fact]
		public void TestCreateUser()
		{
			IActionResult res = _usersController.CreateUser(new UserCreate
			{
				Username = "userCreate",
				Password = "userCreate",
				Email = "userCreate@userCreate.com"
			});

			Assert.IsType<CreatedAtRouteResult>(res);

			CreatedAtRouteResult ok = res as CreatedAtRouteResult;
			User user = ok.Value as User;
			Assert.True(user.Username == "userCreate");
			//Assert.True(user.Password == "userCreate");
			Assert.True(user.Email == "userCreate@userCreate.com");
		}

		[Fact]
		public void TestUserDelete()
		{
			IActionResult res = _usersController.DeleteUser(3);

			Assert.IsType<NoContentResult>(res);
		}

		[Fact]
		public void TestGetUser()
		{
			IActionResult res = _usersController.GetUser(2);

			Assert.IsType<OkObjectResult>(res);
		}

		[Fact]
		public void TestUpdateUser()
		{
			IActionResult res = _usersController.UpdateUser(2, new UserCreate
			{
				Username = "userUpdate",
				Password = "userUpdate",
				Email = "userUpdate@userUpdate.com"
			});

			Assert.IsType<NoContentResult>(res);
		}

		[Fact]
		public void TestPostFriendship()
		{
			IActionResult res = _usersController.AddFriendshipRequest(1, new FriendshipCreate
			{
				FriendId = 2
			});

			CreatedAtRouteResult ok = res as CreatedAtRouteResult;
			User user = ok.Value as User;
			Assert.True(user.Username == "admin");
			//Assert.True(user.Password == "userCreate");
			Assert.True(user.Email == "admin@admin.com");
		}

		[Fact]
		public void TestUpdateFriendship()
		{
			IActionResult res = _usersController.UpdateFriendshipRequest(1, new FriendshipDto
			{
				FriendId = 2,
				Status = FriendshipStatus.Pending
			});

			Assert.IsType<NoContentResult>(res);
		}

		[Fact]
		public void TestSudokuList()
		{
			IActionResult res = _sudokuController.GetDailySudokuList();

			Assert.IsType<OkObjectResult>(res);

			OkObjectResult ok = res as OkObjectResult;
			ICollection<DailySudokuListDto> list = ok.Value as ICollection<DailySudokuListDto>;
			Assert.True(list.Count == 3);
		}

		[Fact]
		public void TestCreateDailySudoku()
		{
			IActionResult res = _sudokuController.CreateDailySudoku(new DailySudokuCreate
			{
				Date = DateTime.Now,
				Difficulty = Difficulty.Intermediate
			});

			Assert.IsType<CreatedAtRouteResult>(res);

			CreatedAtRouteResult ok = res as CreatedAtRouteResult;
			DailySudoku sudoku = ok.Value as DailySudoku;
			Assert.True(sudoku.Difficulty == Difficulty.Intermediate);
		}

		[Fact]
		public void TestSudokuDelete()
		{
			IActionResult res = _sudokuController.DeleteDailySudoku(2);

			Assert.IsType<NoContentResult>(res);
		}

		[Fact]
		public void TestGetSudoku()
		{
			IActionResult res = _sudokuController.GetDailySudoku(2);

			Assert.IsType<OkObjectResult>(res);
		}

		[Fact]
		public void TestUpdateSudoku()
		{
			IActionResult res = _sudokuController.UpdateDailySudoku(2, new DailySudokuCreate
			{
				Date = DateTime.Now,
				Difficulty = Difficulty.Hard
			});

			Assert.IsType<NoContentResult>(res);
		}

		[Fact]
		public void TestPostSudokuScore()
		{
			IActionResult res = _sudokuController.AddSudokuScore(2, new ScoreCreate
			{
				CompletionTime = TimeSpan.Zero
			});
			
			CreatedAtRouteResult ok = res as CreatedAtRouteResult;
			DailySudoku sudoku = ok.Value as DailySudoku;
			Assert.True(sudoku.Difficulty == Difficulty.Intermediate);
		}
	}
}
