using Hydra.Core.Specification.Validation;

namespace Hydra.Voucher.API.Models.Specs
{
     public class VoucherValidation : SpecValidator<Vouchers>
    {
        public VoucherValidation()
        {
            var dataSpec = new VoucherDataSpecification();
            var qtySpec = new VoucherQuantitySpecification();
            var activeSpec = new VoucherActiveSpecification();

            Add("dataSpec", new Rule<Vouchers>(dataSpec, "Voucher expired"));
            Add("qtySpec", new Rule<Vouchers>(qtySpec, "Voucher was used"));
            Add("activeSpec", new Rule<Vouchers>(activeSpec, "Voucher is not active"));
        }
    }
}