using Microsoft.EntityFrameworkCore;
using SyncStyle.ChatGpts;
using SyncStyle.DbContexts;
using SyncStyle.OpenWeatherMaps;
using SyncStyle.Services.Logins;
using SyncStyle.Services.Members;
using SyncStyle.Services.StyleSyncProds;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StyleSyncContext>(o => {
    o.UseNpgsql("User ID=postgres;Password=Passw0rd01.;Server=localhost;Port=5432;Database=StyleSyncDb;Integrated Security=true;Pooling=true;");
});


builder.Services.AddCors(options =>
             {
                 options.AddPolicy("CorsPolicy",
                     builder => builder
                      //.AllowAnyOrigin()
                      .WithOrigins(
                             "http://localhost:8527"
                             )
                     .AllowAnyMethod()
                     .AllowAnyHeader()
                     .AllowCredentials()
                     .SetIsOriginAllowed((host) => true) //for signalr cors                
                         );
             });


builder.Services.AddTransient<IMemberService, MemberService>();
builder.Services.AddTransient<IStyleSyncProdService, StyleSyncProdService>();
builder.Services.AddTransient<IChatGptService, ChatGptService>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddHttpClient<IWeatherService, WeatherService>(client => { client.BaseAddress = new Uri("http://api.openweathermap.org/"); });
builder.Services.AddSingleton<IWeatherService>(sp => { var httpClient = sp.GetRequiredService<HttpClient>();

var apiKey = "openWeathermapApiKey"; return new WeatherService(httpClient, apiKey);});

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

using (var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope())
    {
        var context = scope.ServiceProvider.GetService<StyleSyncContext>();
        context.Database.Migrate();
    }

app.Run();

