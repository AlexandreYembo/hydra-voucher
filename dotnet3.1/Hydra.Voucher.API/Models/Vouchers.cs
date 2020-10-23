using System;
using Hydra.Core.DomainObjects;
using Hydra.Voucher.API.Models.Specs;

namespace Hydra.Voucher.API.Models
{
    public class Vouchers : Entity
    {
        public Vouchers()
        {
           
        }
        
        public string Code { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public VoucherType VoucherType { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UsedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool Active { get; set; }

        // public ICollection<Order> Order { get; set; }

        public bool VoucherIsApplicable() =>
            new VoucherActiveSpecification()
                            .And(new VoucherDataSpecification())
                            .And(new VoucherQuantitySpecification())
                            .IsSatisfiedBy(this);

        internal void MaskAsUsed()
        {
            Active = false;
            Quantity = 0;
            UsedDate = DateTime.Now;
        }

        public void RemoveQuantity()
        {
            Quantity -= 1;
            if(Quantity >= 1) return;

            MaskAsUsed();
        }
    }

    public enum VoucherType
    {
        Percentage = 0,
        Value = 1
    }
}