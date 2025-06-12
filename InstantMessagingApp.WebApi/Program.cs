using InstantMessagingApp.Application.Interfaces;
using InstantMessagingApp.Infrastructure.Data;
using InstantMessagingApp.Infrastructure.Extentions;
using InstantMessagingApp.Infrastructure.Repositories;
using InstantMessagingApp.Infrastructure.Services;
using InstantMessagingApp.Infrastructure.Services.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddAppSwagger();
builder.Services.AddAppCors();
builder.Services.AddAppServices();
builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));
builder.Services.AddAuth(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.MapHub<ChatHub>("/chathub");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
