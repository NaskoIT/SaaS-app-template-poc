using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MultitenancyAppTemplate.Data;
using MultitenancyAppTemplate.Data.Platform;
using MultitenancyAppTemplate.Data.Platform.Models;
using MultitenancyAppTemplate.Infrastructure;
using MultitenancyAppTemplate.Models.Tenants;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultitenancyAppTemplate.Controllers
{
    public class TenantsController : Controller
    {
        private readonly PlatformDbContext platoformDbContext;
        private readonly IConfiguration configuration;
        private readonly IApplicationDbContextBuilder applicationContextBuilder;

        public TenantsController(
            PlatformDbContext context,
            IConfiguration configuration,
            IApplicationDbContextBuilder applicationContextBuilder)
        {
            this.platoformDbContext = context;
            this.configuration = configuration;
            this.applicationContextBuilder = applicationContextBuilder;
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(TenantInputModel model)
        {
            var id = Guid.NewGuid().ToString();

            if (platoformDbContext.Tenants.Any(t => t.Name == model.Name))
            {
                this.ModelState.AddModelError(nameof(model.Name), "Tenant with the same name already exists");
                return View();
            }

            // TODO: the name should be unique
            await platoformDbContext.Tenants
                .AddAsync(new Tenant
                {
                    Id = id,
                    Name = model.Name
                });

            using (var applicationDbContext = applicationContextBuilder.Build(id))
            {
                applicationDbContext.Database.EnsureCreated();
                applicationDbContext.Database.Migrate();
            }

            return Ok();
        }
    }
}
