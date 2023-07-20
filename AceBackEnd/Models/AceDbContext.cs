using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AceBackEnd.Models;

public partial class AceDbContext : DbContext
{
    public AceDbContext()
    {
    }

    public AceDbContext(DbContextOptions<AceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<FuelQuoteForm> FuelQuoteForms { get; set; }

    public virtual DbSet<FuelQuoteHistory> FuelQuoteHistories { get; set; }

    public virtual DbSet<PricingModule> PricingModules { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=tcp:fuelquotesoftware.database.windows.net,1433;Initial Catalog=AceDB;Persist Security Info=True;User ID=AceSa;Password=Weareteam1!;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Clients__E67E1A24E2DC41A0");

            entity.Property(e => e.ClientId).ValueGeneratedNever();
            entity.Property(e => e.Addressone)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Addresstwo)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Fullname)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Zipcode)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FuelQuoteForm>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("FuelQuoteForm");

            entity.Property(e => e.Amount)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("amount");
            entity.Property(e => e.DeliveryAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("deliveryAddress");
            entity.Property(e => e.DeliveryDate)
                .HasColumnType("date")
                .HasColumnName("deliveryDate");
            entity.Property(e => e.FuelQuoteTotal)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("fuelQuoteTotal");
            entity.Property(e => e.GallonsRequested).HasColumnName("gallonsRequested");
            entity.Property(e => e.PricePerGallon)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("pricePerGallon");

            entity.HasOne(d => d.Client).WithMany()
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__FuelQuote__Clien__6EF57B66");
        });

        modelBuilder.Entity<FuelQuoteHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__FuelQuot__3214EC079EED319A");

            entity.ToTable("FuelQuoteHistory");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DeliveryAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.DeliveryDate).HasColumnType("datetime");
            entity.Property(e => e.SuggestedPrice).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalAmountDue).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Client).WithMany(p => p.FuelQuoteHistories)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("FK__FuelQuote__Clien__6E01572D");
        });

        modelBuilder.Entity<PricingModule>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__PricingM__E67E1A242D5B8C7C");

            entity.ToTable("PricingModule");

            entity.Property(e => e.ClientId).ValueGeneratedNever();
            entity.Property(e => e.CompanyProfitFactor).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CurrentPricePerGallon).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.GallonsRequestedFactor).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.LocationFactor).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Margin).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RateHistoryFactor).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.SuggestedPricePerGallon).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.TotalAmountDue).HasColumnType("decimal(10, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
