namespace CosmeticsStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Coupon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Coupon()
        {
            DetailCoupons = new HashSet<DetailCoupon>();
        }

        public int CouponID { get; set; }

        public int? SupplierID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateAdd { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailCoupon> DetailCoupons { get; set; }
    }
}
