using MediaGuide.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MediaGuide.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Episode> Episodes => Set<Episode>();
    public virtual DbSet<Show> Shows => Set<Show>();

    public virtual DbSet<Media> Media => Set<Media>();


}
