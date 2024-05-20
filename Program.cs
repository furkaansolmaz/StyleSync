using Microsoft.EntityFrameworkCore;
using SyncStyle.ChatGpts;
using SyncStyle.DbContexts;
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


builder.Services.AddTransient<IMemberService, MemberService>();
builder.Services.AddTransient<IStyleSyncProdService, StyleSyncProdService>();
builder.Services.AddTransient<IChatGptService, ChatGptService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

