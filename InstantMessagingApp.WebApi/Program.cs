using InstantMessagingApp.Application.Interfaces;
using InstantMessagingApp.Infrastructure.Data;
using InstantMessagingApp.Infrastructure.Extensions;
using InstantMessagingApp.Infrastructure.Repositories;
using InstantMessagingApp.Infrastructure.Services;
using InstantMessagingApp.Infrastructure.Services.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["DEFAULT_CONNECTION"]
                       ?? builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddAppSwagger();
builder.Services.AddAppCors();
builder.Services.AddAppServices(builder.Configuration);
builder.Services.Configure<AuthSettings>(builder.Configuration.GetSection("AuthSettings"));
builder.Services.AddAuth(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}
app.UseCors();
app.MapHub<ChatHub>("/chathub");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
