using Hubla.Sales.API.Filters;
using Hubla.Sales.Application;

namespace Hubla.Sales.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<NotificationFilter>();
            });
            
            /* inicio custom*/
            ConfigurationManager configuration = builder.Configuration; // allows both to access and to set up the config
            IWebHostEnvironment environment = builder.Environment;

            builder.Services.AddApplication(configuration);
            /* fim custom*/


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}