using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hydra.Voucher.API.Models;

namespace Hydra.Voucher.API.Data
{
    public interface IVoucherRepository
    {
        Task Insert(Vouchers voucher);
        Task Delete(Guid id);
        Task<Vouchers> Find(Guid id);
        Task<List<Vouchers>> FindAll();
        Task<Vouchers> FindByCode(string code);
        Task Update(Vouchers basket);
    }
}