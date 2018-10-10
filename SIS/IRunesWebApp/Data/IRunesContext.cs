using IRunesWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace IRunesWebApp.Data
{
    public class IRunesContext : DbContext
    {
        public IRunesContext()
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Track> Tracks { get; set; }

        public DbSet<Album> Albums { get; set; }

        public DbSet<TrackAlbum> TracksAlbums { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TrackAlbum>()
                .HasKey(x => new { x.AlbumId, x.TrackId});
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ServerConfig.ConnectionString)
                .UseLazyLoadingProxies();

            base.OnConfiguring(optionsBuilder);
        }
    }
}