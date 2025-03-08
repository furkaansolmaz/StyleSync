using Microsoft.EntityFrameworkCore;
using SyncStyle.ChatGpts;
using SyncStyle.DbContexts;
using SyncStyle.OpenWeatherMaps;
using SyncStyle.Services.Logins;
using SyncStyle.Services.Users;
using SyncStyle.Services.StyleSyncProds;
using Amazon.S3;
using SyncStyle.Services.AmazonS3;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure DbContext with connection string from appsettings.json
builder.Services.AddDbContext<StyleSyncContext>(options => 
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseNpgsql(connectionString);
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:8527")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials()
              .SetIsOriginAllowed(_ => true); // For SignalR
    });
});

// Amazon services
builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddScoped<IS3Service, S3Service>();
// Register services
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IStyleSyncProdService, StyleSyncProdService>();
builder.Services.AddTransient<IChatGptService, ChatGptService>();
builder.Services.AddTransient<ILoginService, LoginService>();

// Configure HttpClient for WeatherService
builder.Services.AddHttpClient<IWeatherService, WeatherService>(client =>
{
    client.BaseAddress = new Uri("http://api.openweathermap.org/");
});

builder.Services.AddSingleton<IWeatherService>(sp =>
{
    var httpClient = sp.GetRequiredService<HttpClient>();
    var apiKey = builder.Configuration["WeatherApiKey"];
    return new WeatherService(httpClient, apiKey);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

// Apply pending migrations at startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<StyleSyncContext>();
    context.Database.Migrate();
}

app.Run();
