namespace CosmeticsStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            DetailOrders = new HashSet<DetailOrder>();
            DetailCoupons = new HashSet<DetailCoupon>();
            Reviews = new HashSet<Review>();
        }

        public int ProductID { get; set; }

        [StringLength(255)]
        [DisplayName("Tên sản phẩm")]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        public string Name { get; set; }

        [DisplayName("Giá")]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        public decimal? Price { get; set; }

        [DisplayName("Giảm giá %")]
        public int? Discount { get; set; }

        [StringLength(1000)]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        [DisplayName("Giới thiệu")]
        public string About { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        [DisplayName("Mô tả")]
        public string Description { get; set; }

        [DisplayName("Bí danh")]
        public string MetaTitle { get; set; }

        [DataType(DataType.Date)]
        [DisplayName("Ngày cập nhật")]
        public DateTime? UpdateDate { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        [DisplayName("Số lượng kho")]
        public int? InventoryNumber { get; set; }

        [DisplayName("Lượt đánh giá")]
        public int? ReviewCounts { get; set; }

        [DisplayName("Lượt mua")]
        public int? SellCounts { get; set; }

        [DisplayName("Độ hót")]
        public int? Hot { get; set; }

        [DisplayName("Độ mới")]
        public int? New { get; set; }

        public int? SupplierID { get; set; }

        public int? ProducerID { get; set; }

        public int? ProductTypeID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        [DisplayName("Ảnh đại điện")]
        public string Image1 { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        [DisplayName("Ảnh 2")]
        public string Image2 { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        [DisplayName("Ảnh 3")]
        public string Image3 { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        [DisplayName("Ảnh 4")]
        public string Image4 { get; set; }

        [DisplayName("Trạng thái")]
        public bool? Status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailOrder> DetailOrders { get; set; }

        public virtual Producer Producer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailCoupon> DetailCoupons { get; set; }

        public virtual ProductType ProductType { get; set; }

        public virtual Supplier Supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
