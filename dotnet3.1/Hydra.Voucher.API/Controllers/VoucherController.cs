using System.Collections.Generic;
using System.Threading.Tasks;
using Hydra.Voucher.API.Data;
using Hydra.Voucher.API.Dtos;
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
        [ClaimsAuthorize("admin-voucher", "read")]
        public async Task<IActionResult> GetVouchers()
        {
            var voucher = await _voucherRepository.FindAll();
            
            if(voucher == null) return NotFound();

            return CustomResponse(voucher);
        }

        [HttpGet("voucher/{code}")]
        [ClaimsAuthorize("voucher", "read")]
        public async Task<IActionResult> GetVoucherByCode(string code)
        {
            var voucher =  await _voucherRepository.FindByCode(code);

            if(voucher == null) return NotFound();

            return CustomResponse(new VoucherDTO().Map(voucher));
        }

        [HttpPost("voucher")]
        [ClaimsAuthorize("admin-voucher", "write")]
        public async Task<IActionResult> InsertVoucher(Vouchers voucher)
        {
            await _voucherRepository.Insert(voucher);
            return CustomResponse();
        }
    }
}