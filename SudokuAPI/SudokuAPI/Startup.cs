using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SudokuAPI.Entities;
using Microsoft.EntityFrameworkCore;
using SudokuAPI.Services;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace SudokuAPI
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;

                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = Configuration["Tokens:Issuer"],
                    ValidAudience = Configuration["Tokens:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                };

            });

            services.AddMvc();
            var connectionStr = "Server=(localdb)\\mssqllocaldb;Database=SudokuInfoDB;Trusted_Connection=True;";
            services.AddDbContext<SudokuInfoContext>(o => o.UseSqlServer(connectionStr));

            services.AddScoped<ISudokuInfoRepository, SudokuInfoRepository>();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("help", new Info
                {
                    Title = "Sudoku API",
                    Version = "v1"
                });
            });

            services.AddSingleton<SudokuGeneratorService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IHashService, HashService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();

            app.UseAuthentication();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<CreateContracts.UserCreate, Entities.User>();
                cfg.CreateMap<Entities.User, Models.UserListDto>();
                cfg.CreateMap<Entities.DailySudoku, Models.DailySudokuListDto>();
                cfg.CreateMap<CreateContracts.DailySudokuCreate, Entities.DailySudoku>();
                cfg.CreateMap<UpdateContracts.ChallengeUpdate, Entities.Challenge>();
                cfg.CreateMap<Entities.Comment, Models.CommentDto>();
                cfg.CreateMap<Entities.Challenge, Models.ChallengeDto>();
                cfg.CreateMap<Entities.DailySudoku, Models.DailySudokuDto>();
                cfg.CreateMap<CreateContracts.CommentCreate, Entities.Comment>();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(
                  "/swagger/help/swagger.json", "Sudoku API Help Endpoint");
            });
            

            app.UseMvc();
        }
    }
}
