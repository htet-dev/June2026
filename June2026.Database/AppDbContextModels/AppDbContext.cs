using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace June2026.Database.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAddress> TblAddresses { get; set; }

    public virtual DbSet<TblMovieTicket> TblMovieTickets { get; set; }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblSale> TblSales { get; set; }

    public virtual DbSet<TblSaleDetail> TblSaleDetails { get; set; }

    public virtual DbSet<TblStaff> TblStaffs { get; set; }

    public virtual DbSet<TblStudent> TblStudents { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-J11BIHB\\MSSQLSERVER01;Database=June2026;User ID=sa;Password=sasa@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAddress>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__Tbl_Addr__091C2AFB04D8A7EF");

            entity.ToTable("Tbl_Address");

            entity.Property(e => e.AddressDescription).HasMaxLength(500);
            entity.Property(e => e.AddressType).HasMaxLength(100);
        });

        modelBuilder.Entity<TblMovieTicket>(entity =>
        {
            entity.HasKey(e => e.TicketId).HasName("PK__Tbl_Movi__712CC607DFB687F7");

            entity.ToTable("Tbl_MovieTicket");

            entity.Property(e => e.TicketName).HasMaxLength(100);
            entity.Property(e => e.TicketPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TicketType).HasMaxLength(50);
        });

        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CD8BB4D5D8");

            entity.ToTable("Tbl_Product");

            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProductName).HasMaxLength(100);
        });

        modelBuilder.Entity<TblSale>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PK__Tbl_Sale__1EE3C3FFD05D610D");

            entity.ToTable("Tbl_Sale");

            entity.Property(e => e.SaleDate).HasColumnType("datetime");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<TblSaleDetail>(entity =>
        {
            entity.HasKey(e => e.SaleDetailId).HasName("PK__Tbl_Sale__70DB14FE8A2217F0");

            entity.ToTable("Tbl_SaleDetail");

            entity.Property(e => e.UnitPrice).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.TblSaleDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SaleDetail_Product");

            entity.HasOne(d => d.Sale).WithMany(p => p.TblSaleDetails)
                .HasForeignKey(d => d.SaleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SaleDetail_Sale");
        });

        modelBuilder.Entity<TblStaff>(entity =>
        {
            entity.HasKey(e => e.StaffId);

            entity.ToTable("Tbl_Staff");

            entity.Property(e => e.StaffName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblStudent>(entity =>
        {
            entity.HasKey(e => e.StudentId);

            entity.ToTable("Tbl_Student");

            entity.Property(e => e.DateofBirth)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FatherName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StudentName).HasMaxLength(50);
            entity.Property(e => e.StudentNo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("Tbl_User");

            entity.Property(e => e.Password).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
