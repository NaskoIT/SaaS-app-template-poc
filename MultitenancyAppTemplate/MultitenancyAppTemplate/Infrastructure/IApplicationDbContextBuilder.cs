using MultitenancyAppTemplate.Data;

namespace MultitenancyAppTemplate.Infrastructure
{
    public interface IApplicationDbContextBuilder
    {
        ApplicationDbContext Build(string tenantId);
    }
}
