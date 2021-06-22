using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CosmeticsStore.Models
{
    public class CartItems
    {
        public int MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        public string HinhAnh { get; set; }
        public CartItems(int iMaSP)
        {
            using (CosmeticsStoreDbContext db = new CosmeticsStoreDbContext())
            {
                this.MaSP = iMaSP;
                Product sp = db.Products.Single(n => n.ProductID == iMaSP);
                this.TenSP = sp.Name;
                this.HinhAnh = sp.Image1;
                this.DonGia = sp.Price.Value;
                this.SoLuong = 1;
                this.ThanhTien = DonGia * SoLuong;
            }
        }
        public CartItems(int iMaSP, int sl)
        {
            using (CosmeticsStoreDbContext db = new CosmeticsStoreDbContext())
            {
                this.MaSP = iMaSP;
                Product sp = db.Products.Single(n => n.ProductID == iMaSP);
                this.TenSP = sp.Name;
                this.HinhAnh = sp.Image1;
                this.DonGia = sp.Price.Value;
                this.SoLuong = sl;
                this.ThanhTien = DonGia * SoLuong;
            }
        }
        public CartItems()
        {


        }

    }

}