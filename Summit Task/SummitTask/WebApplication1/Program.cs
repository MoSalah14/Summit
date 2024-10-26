using Microsoft.EntityFrameworkCore;
using Summit_Task.Context;
using Summit_Task.Exetenstion;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            #region Db Context

            builder.Services.AddDbContext<ApplicationContext>(
                e => e.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

            #endregion Db Context

            #region Allow Auto Mapper

            builder.Services.AddAutoMapper(typeof(Program));

            #endregion Allow Auto Mapper

            #region AllowServices

            builder.Services.SummitServices();

            #endregion AllowServices

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader()
                          .AllowAnyOrigin()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}