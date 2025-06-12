using InstantMessagingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InstantMessagingApp.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Message> Messages { get; set; }
}