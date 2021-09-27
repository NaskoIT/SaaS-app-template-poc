using Microsoft.Extensions.Configuration;
using MultitenancyAppTemplate.Data;

namespace MultitenancyAppTemplate.Infrastructure
{
    public class ApplicationDbContextBuilder : IApplicationDbContextBuilder
    {
        private readonly IConfiguration configuration;

        public ApplicationDbContextBuilder(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public ApplicationDbContext Build(string tenantId)
        {
            string connectionStringTemplate = configuration["ApplicationDbContext"];
            string connectionString = string.Format(connectionStringTemplate, tenantId);

            return new ApplicationDbContext(connectionString);
        }
    }
}
