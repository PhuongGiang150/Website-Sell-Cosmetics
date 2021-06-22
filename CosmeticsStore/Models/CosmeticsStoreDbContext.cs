using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CosmeticsStore.Models
{
    public partial class CosmeticsStoreDbContext : DbContext
    {
        public CosmeticsStoreDbContext()
            : base("name=CosmeticsStoreDbContext")
        {
        }
        public virtual DbSet<About> Abouts { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<DetailCoupon> DetailCoupons { get; set; }
        public virtual DbSet<DetailOrder> DetailOrders { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductType> ProductTypes { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .Property(e => e.Phone1)
                .IsFixedLength();

            modelBuilder.Entity<Contact>()
                .Property(e => e.Phone2)
                .IsFixedLength();

            modelBuilder.Entity<Contact>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.LinkMap)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Facebook)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Tiwtter)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Pinterest)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Instargram)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Youtobe)
                .IsUnicode(false);

            modelBuilder.Entity<Coupon>()
                .HasMany(e => e.DetailCoupons)
                .WithOptional(e => e.Coupon)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Customer>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.Customer)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Reviews)
                .WithOptional(e => e.Customer)
                .HasForeignKey(e => e.CusomerID)
                .WillCascadeOnDelete();

            modelBuilder.Entity<DetailCoupon>()
                .Property(e => e.EntryPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DetailOrder>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<News>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<Producer>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Producer)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Product>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.DetailCoupons)
                .WithOptional(e => e.Product)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Product>()
                .HasMany(e => e.DetailOrders)
                .WithOptional(e => e.Product)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Reviews)
                .WithOptional(e => e.Product)
                .WillCascadeOnDelete();

            modelBuilder.Entity<ProductType>()
                .Property(e => e.MetaTitle)
                .IsUnicode(false);

            modelBuilder.Entity<ProductType>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.ProductType)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.Supplier)
                .WillCascadeOnDelete();
        }
    }
}
