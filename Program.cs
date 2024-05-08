using HLTVScrapperAPI.Utility;

namespace HLTVScrapperAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();


            // End leftover processes
            AppDomain.CurrentDomain.ProcessExit += (sender, eventArgs) =>
            {
                WebAutomation.CleanupProcesses();
            };

            AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) =>
            {
                WebAutomation.CleanupProcesses();
            };

            app.Run();
        }
    }
}
