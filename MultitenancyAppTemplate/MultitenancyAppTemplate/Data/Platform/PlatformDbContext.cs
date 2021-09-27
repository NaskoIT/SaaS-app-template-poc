using Microsoft.EntityFrameworkCore;
using MultitenancyAppTemplate.Data.Platform.Models;

namespace MultitenancyAppTemplate.Data.Platform
{
    public class PlatformDbContext : DbContext
    {
        public DbSet<Tenant> Tenants { get; set; }
    }
}
