using Hydra.Voucher.API.Models;

namespace Hydra.Voucher.API.Dtos
{
    public class VoucherDTO
    {
        public decimal? Discount{ get; set; }
        public string Code { get; set; }
        public int DiscountType { get; set; }

        public VoucherDTO Map(Vouchers voucher)
        {
            return new VoucherDTO
            {
                Code = voucher.Code,
                Discount = voucher.VoucherType == VoucherType.Percentage ? voucher.DiscountPercentage : voucher.DiscountAmount,
                DiscountType = (int)voucher.VoucherType
            };
        }
    }
}