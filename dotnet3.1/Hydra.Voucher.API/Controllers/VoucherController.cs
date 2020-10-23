using System.Collections.Generic;
using System.Threading.Tasks;
using Hydra.Voucher.API.Data;
using Hydra.Voucher.API.Models;
using Hydra.WebAPI.Core.Controllers;
using Hydra.WebAPI.Core.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hydra.Voucher.API.Controllers
{
    public class VoucherController : MainController
    {
        private readonly IVoucherRepository _voucherRepository;

        public VoucherController(IVoucherRepository voucherRepository)
        {
            _voucherRepository = voucherRepository;
        }
        
        [HttpGet("voucher")]
        //[ClaimsAuthorize("voucher", "read")]
        public async Task<List<Vouchers>> GetVouchers()
        {
            return await _voucherRepository.FindAll();
        }

        [HttpGet("voucher/{code}")]
    //    [ClaimsAuthorize("voucher", "read")]
        public async Task<Vouchers> GetVoucherByCode(string code)
        {
            return await _voucherRepository.FindByCode(code);
        }

        [HttpPost("voucher")]
      //  [ClaimsAuthorize("voucher", "write")]
        public async Task<IActionResult> InsertVoucher(Vouchers voucher)
        {
            await _voucherRepository.Insert(voucher);
            return CustomResponse();
        }
    }
}