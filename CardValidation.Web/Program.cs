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

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }
        else
        {
            app.UseRouting(); 
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run("http://0.0.0.0:80"); 
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
