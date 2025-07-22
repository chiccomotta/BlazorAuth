using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Blazor.Data;

public class AppDbContext : DbContext
{
    public virtual DbSet<Album> Albums { get; set; }
    public virtual DbSet<Song> Songs { get; set; }
        
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
}