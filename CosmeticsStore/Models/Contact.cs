namespace CosmeticsStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Contact
    {
        public int ContactID { get; set; }

        [StringLength(250)]
        [DisplayName("Tên shop")]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        public string Name { get; set; }

        [StringLength(250)]
        [DisplayName("Logo")]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        public string Logo { get; set; }

        [StringLength(250)]
        [DisplayName("Địa chỉ")]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        public string Address { get; set; }

        [StringLength(12)]
        [DisplayName("Phone 1")]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        public string Phone1 { get; set; }

        [StringLength(12)]
        public string Phone2 { get; set; }

        [StringLength(100)]
        [DisplayName("Phone 2")]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        public string Email { get; set; }

        [StringLength(250)]
        [DisplayName("Địa chỉ Map")]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        public string LinkMap { get; set; }

        [StringLength(250)]
        public string Facebook { get; set; }

        [StringLength(250)]
        public string Tiwtter { get; set; }

        [StringLength(250)]
        public string Pinterest { get; set; }

        [StringLength(250)]
        public string Instargram { get; set; }

        [StringLength(250)]
        public string Youtobe { get; set; }
        [DisplayName("Trạng Thái")]
        public bool? Status { get; set; }
    }
}
