namespace CosmeticsStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DetailCoupon")]
    public partial class DetailCoupon
    {
        public int DetailCouponID { get; set; }

        public int? CouponID { get; set; }

        public int? ProductID { get; set; }

        [DisplayName("Giá nhập")]
        public decimal? EntryPrice { get; set; }

        [DisplayName("Số lượng nhập")]
        public int? Quantity { get; set; }

        public virtual Coupon Coupon { get; set; }

        public virtual Product Product { get; set; }
    }
}
