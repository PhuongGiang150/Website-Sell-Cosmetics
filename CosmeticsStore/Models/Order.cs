namespace CosmeticsStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            DetailOrders = new HashSet<DetailOrder>();
        }

        public int OrderID { get; set; }

        //[DataType(DataType.Date)]
        [DisplayName("Ngày đặt hàng")]
        public DateTime? OrderDate { get; set; }

        [DisplayName("Tình trạng giao hàng")]
        public bool? Status { get; set; }

        [DisplayName("Ngày nhận hàng")]
        public DateTime? DeliveryDate { get; set; }

        [DisplayName("Thanh toán")]
        public bool? Paid { get; set; }

        public int? CustomerID { get; set; }

        [DisplayName("Giảm giá")]
        public int? Discount { get; set; }

        [DisplayName("Đã Hủy")]
        public bool? Cancelled { get; set; }

        [DisplayName("Đã Duyệt")]
        public bool? Approved { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailOrder> DetailOrders { get; set; }
    }
}
