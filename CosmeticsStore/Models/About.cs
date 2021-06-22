namespace CosmeticsStore.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class About
    {
        public int AboutID { get; set; }
      
       
        [StringLength(250)]
        [DisplayName("Tên bài giới thiệu")]
        public string Name { get; set; }
        [DisplayName("Nội dung")]
        [Required(ErrorMessage = "Vui lòng nhập trường này!")]
        public string Content { get; set; }

        [DisplayName("Trạng Thái")]
        public bool? Status { get; set; }
    }
}
