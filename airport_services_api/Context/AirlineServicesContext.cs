using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using airport_services_api.Models;

namespace airport_services_api.Context;

public partial class AirlineServicesContext : DbContext
{
    public AirlineServicesContext()
    {
    }

    public AirlineServicesContext(DbContextOptions<AirlineServicesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Airline> Airlines { get; set; }

    public virtual DbSet<Airport> Airports { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<Handler> Handlers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=DESKTOP-KBMLUVH\\SQLEXPRESS;Database=airline_services;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Airline>(entity =>
        {
            entity.HasKey(e => e.AirlineId).HasName("PK__Airlines__D04734C9F6DB0773");

            entity.Property(e => e.AirlineId).HasColumnName("airlineId");
            entity.Property(e => e.AirlineName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("airlineName");
            entity.Property(e => e.AirportId).HasColumnName("airportId");

            entity.HasOne(d => d.Airport).WithMany(p => p.Airlines)
                .HasForeignKey(d => d.AirportId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_airport_id");
        });

        modelBuilder.Entity<Airport>(entity =>
        {
            entity.HasKey(e => e.AirportId).HasName("PK__Airports__C85BDFBE7DFBEAEC");

            entity.Property(e => e.AirportId).HasColumnName("airportId");
            entity.Property(e => e.AirportName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("airportName");
            entity.Property(e => e.IataCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("iataCode");
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.FlightId).HasName("PK__Flights__0E018642C116DA12");

            entity.Property(e => e.FlightId).HasColumnName("flightId");
            entity.Property(e => e.AirlineId).HasColumnName("airlineId");
            entity.Property(e => e.CreatedAt).HasColumnName("createdAt");
            entity.Property(e => e.DepartureTime).HasColumnType("datetime");
            entity.Property(e => e.DurationMinutes).HasColumnName("durationMinutes");
            entity.Property(e => e.FlightNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("flightNumber");
            entity.Property(e => e.HandlerId).HasColumnName("handlerId");
            entity.Property(e => e.ServiceType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("serviceType");

            entity.HasOne(d => d.Airline).WithMany(p => p.Flights)
                .HasForeignKey(d => d.AirlineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_airline_id");

            entity.HasOne(d => d.Handler).WithMany(p => p.Flights)
                .HasForeignKey(d => d.HandlerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_handler_id");
        });

        modelBuilder.Entity<Handler>(entity =>
        {
            entity.HasKey(e => e.HandlerId).HasName("PK__Handlers__5AFC0F8A04EF5989");

            entity.Property(e => e.HandlerId).HasColumnName("handlerId");
            entity.Property(e => e.HandlerName)
                .IsUnicode(false)
                .HasColumnName("handlerName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
