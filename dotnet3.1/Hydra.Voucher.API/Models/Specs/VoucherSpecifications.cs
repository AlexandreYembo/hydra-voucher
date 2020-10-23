using System;
using System.Linq.Expressions;
using Hydra.Core.Specification;

namespace Hydra.Voucher.API.Models.Specs
{
    public class VoucherActiveSpecification : Specification<Vouchers>
    {
        public override Expression<Func<Vouchers, bool>> ToExpression() => 
            voucher => voucher.Active;
    }

    public class VoucherDataSpecification : Specification<Vouchers>
    {
        public override Expression<Func<Vouchers, bool>> ToExpression() =>
            voucher => voucher.ExpirationDate >= DateTime.Now;
    }

    public class VoucherQuantitySpecification : Specification<Vouchers>
    {
        public override Expression<Func<Vouchers, bool>> ToExpression() =>
            voucher => voucher.Quantity > 0;
    }
}