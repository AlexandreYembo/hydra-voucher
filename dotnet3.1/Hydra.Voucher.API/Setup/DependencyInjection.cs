using Hydra.Voucher.API.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Hydra.Voucher.API.Setup
{
    public static class DependencyInjection
    {
         public static void RegisterServices(this IServiceCollection services)
        {
             services.AddSingleton<IVoucherRepository, VoucherRepository>();
        }
    }
}