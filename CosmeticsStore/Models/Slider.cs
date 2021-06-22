namespace CosmeticsStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Slider
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SliderID { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        [DisplayName("Hình ảnh")]
        public string Image { get; set; }

        [DisplayName("Hiển thị")]
        public int? DisplayOrder { get; set; }

        [StringLength(250)]
        [DisplayName("Đường dẫn")]
        public string Link { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        [DisplayName("Tiêu đề")]
        public string Info { get; set; }

        [StringLength(250)]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        [DisplayName("Mô tả")]
        public string Description { get; set; }

        [DisplayName("Trạng thái")]
        public bool? Status { get; set; }
    }
}
