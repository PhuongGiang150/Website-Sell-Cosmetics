namespace CosmeticsStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Orders = new HashSet<Order>();
            Reviews = new HashSet<Review>();
        }

        public int CustomerID { get; set; }

        [StringLength(100)]
        [DisplayName("Tài khoản")]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        public string UserName { get; set; }

        [StringLength(250)]
        [DisplayName("Mật khẩu")]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        public string Password { get; set; }

        [StringLength(100)]
        [DisplayName("Tên khách hàng")]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        public string Name { get; set; }

        [StringLength(250)]
        [DisplayName("Avartar")]
        public string Image { get; set; }

        [StringLength(250)]
        [DisplayName("Địa chỉ")]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        public string Address { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        public string Email { get; set; }

        [StringLength(12)]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        [StringLength(50)]
        [DisplayName("Quyền")]
        public string Role { get; set; }

        [DisplayName("Trạng thái")]
        public bool? Status { get; set; }

        [DisplayName("Câu hỏi")]
        public string Question { get; set; }

        [StringLength(250)]
        [DisplayName("Câu trả lời")]
        public string Answer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
