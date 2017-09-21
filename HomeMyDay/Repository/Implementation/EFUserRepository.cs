using HomeMyDay.Database;
using System.Threading.Tasks;
using HomeMyDay.Identity;
using HomeMyDay.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HomeMyDay.Repository.Implementation
{
    public class EFUserRepository : IUserRepository
    {
        private readonly AppIdentityDbContext context;

        public EFUserRepository(AppIdentityDbContext ctx)
        {
            context = ctx;
        }
    }
}