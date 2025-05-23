
namespace GithubActionDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var argsList = args.Select(a => a.ToLower()).ToList();

            if (argsList.Contains("run-processor"))
            {
                Console.WriteLine($"CsvExpiryProcessor started at UTC: {DateTime.UtcNow}...");
                var processor = new CsvExpiryProcessor();
                processor.Process();
                Console.WriteLine("CsvExpiryProcessor completed.");
                return; // Exit before starting the Web API
            }
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
