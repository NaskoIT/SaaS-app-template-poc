using Microsoft.AspNetCore.Mvc;
using MultitenancyAppTemplate.Data.Platform;
using MultitenancyAppTemplate.Data.Platform.Models;
using MultitenancyAppTemplate.Models.Tenants;
using System;
using System.Threading.Tasks;

namespace MultitenancyAppTemplate.Controllers
{
    public class TenantsController : Controller
    {
        private readonly PlatformDbContext context;

        public TenantsController(PlatformDbContext context)
        {
            this.context = context;
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(TenantInputModel model)
        {
            var id = Guid.NewGuid().ToString();

            await context.Tenants
                .AddAsync(new Tenant
                {
                    Id = id,
                    Name = model.Name
                });

            return Ok();
        }
    }
}
