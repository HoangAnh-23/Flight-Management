using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Models
{
    public partial class FlightManagementDBContext : DbContext
    {
        public FlightManagementDBContext()
        {
        }

        public FlightManagementDBContext(DbContextOptions<FlightManagementDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountMember> AccountMembers { get; set; } = null!;
        public virtual DbSet<Airline> Airlines { get; set; } = null!;
        public virtual DbSet<Airport> Airports { get; set; } = null!;
        public virtual DbSet<Baggage> Baggages { get; set; } = null!;
        public virtual DbSet<Booking> Bookings { get; set; } = null!;
        public virtual DbSet<BookingPlatform> BookingPlatforms { get; set; } = null!;
        public virtual DbSet<Flight> Flights { get; set; } = null!;
        public virtual DbSet<Passenger> Passengers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountMember>(entity =>
            {
                entity.ToTable("accountMember");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .HasColumnName("address");

                entity.Property(e => e.DOb)
                    .HasColumnType("date")
                    .HasColumnName("dOB");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .HasColumnName("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone");

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .HasColumnName("role");
            });

            modelBuilder.Entity<Airline>(entity =>
            {
                entity.ToTable("airline");

                entity.HasIndex(e => e.Code, "UQ__airline__357D4CF90086E0F8")
                    .IsUnique();

                entity.HasIndex(e => e.Name, "UQ__airline__72E12F1B09F48460")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .HasColumnName("country");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Airport>(entity =>
            {
                entity.ToTable("airport");

                entity.HasIndex(e => e.Code, "UQ__airport__357D4CF92571B7A2")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .HasColumnName("city");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .HasColumnName("country");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .HasColumnName("state");
            });

            modelBuilder.Entity<Baggage>(entity =>
            {
                entity.ToTable("baggage");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BookingId).HasColumnName("booking_id");

                entity.Property(e => e.WeightInKg)
                    .HasColumnType("decimal(4, 2)")
                    .HasColumnName("weight_in_kg");

                entity.HasOne(d => d.Booking)
                    .WithMany(p => p.Baggages)
                    .HasForeignKey(d => d.BookingId)
                    .HasConstraintName("FK__baggage__booking__6383C8BA");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.ToTable("booking");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BookingPlatformId).HasColumnName("booking_platform_id");

                entity.Property(e => e.BookingTime)
                    .HasColumnType("datetime")
                    .HasColumnName("booking_time");

                entity.Property(e => e.FlightId).HasColumnName("flight_id");

                entity.Property(e => e.PassengerId).HasColumnName("passenger_id");

                entity.HasOne(d => d.BookingPlatform)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.BookingPlatformId)
                    .HasConstraintName("FK__booking__booking__60A75C0F");

                entity.HasOne(d => d.Flight)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.FlightId)
                    .HasConstraintName("FK__booking__flight___5FB337D6");

                entity.HasOne(d => d.Passenger)
                    .WithMany(p => p.Bookings)
                    .HasForeignKey(d => d.PassengerId)
                    .HasConstraintName("FK__booking__passeng__5EBF139D");
            });

            modelBuilder.Entity<BookingPlatform>(entity =>
            {
                entity.ToTable("bookingPlatform");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Url)
                    .HasMaxLength(200)
                    .HasColumnName("url");
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.ToTable("flight");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AirlineId).HasColumnName("airline_id");

                entity.Property(e => e.ArrivalTime)
                    .HasColumnType("datetime")
                    .HasColumnName("arrival_time");

                entity.Property(e => e.ArrivingAirport).HasColumnName("arriving_airport");

                entity.Property(e => e.ArrivingGate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("arriving_gate");

                entity.Property(e => e.DepartingAirport).HasColumnName("departing_airport");

                entity.Property(e => e.DepartingGate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("departing_gate");

                entity.Property(e => e.DepartureTime)
                    .HasColumnType("datetime")
                    .HasColumnName("departure_time");

                entity.HasOne(d => d.Airline)
                    .WithMany(p => p.Flights)
                    .HasForeignKey(d => d.AirlineId)
                    .HasConstraintName("FK__flight__airline___5629CD9C");

                entity.HasOne(d => d.ArrivingAirportNavigation)
                    .WithMany(p => p.FlightArrivingAirportNavigations)
                    .HasForeignKey(d => d.ArrivingAirport)
                    .HasConstraintName("FK__flight__arriving__5812160E");

                entity.HasOne(d => d.DepartingAirportNavigation)
                    .WithMany(p => p.FlightDepartingAirportNavigations)
                    .HasForeignKey(d => d.DepartingAirport)
                    .HasConstraintName("FK__flight__departin__571DF1D5");
            });

            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.ToTable("passenger");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .HasColumnName("country");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("date_of_birth");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .HasColumnName("first_name");

                entity.Property(e => e.Gender)
                    .HasMaxLength(20)
                    .HasColumnName("gender");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .HasColumnName("last_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
