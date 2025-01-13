using CardValidation.Core.Services;
using CardValidation.Core.Services.Interfaces;
using CardValidation.Infrustructure;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder.Services);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // If running in Docker, listen on HTTP instead of redirecting to HTTPS
        if (app.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }
        else
        {
            // In Docker, disable HTTPS redirection for simplicity
            app.UseRouting(); // Enable routing without HTTPS redirection
        }

        app.UseAuthorization();

        app.MapControllers();

        // Ensure the app listens on all IPs for Docker
        app.Run("http://0.0.0.0:80"); // This binds the app to port 80 inside the container
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddTransient<ICardValidationService, CardValidationService>();

        services.AddMvc(options =>
        {
            options.Filters.Add(typeof(CreditCardValidationFilter));
        });
    }
}


/*using CardValidation.Core.Services;
using CardValidation.Core.Services.Interfaces;
using CardValidation.Infrustructure;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        ConfigureServices(builder.Services);

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

    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddTransient<ICardValidationService, CardValidationService>();

        services.AddMvc(options =>
        {
            options.Filters.Add(typeof(CreditCardValidationFilter)); ;
        });
    }
}*/
