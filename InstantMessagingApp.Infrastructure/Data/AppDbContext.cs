using InstantMessagingApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InstantMessagingApp.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    public DbSet<Message> Messages { get; set; }
    public DbSet<User> Users { get; set; }
}