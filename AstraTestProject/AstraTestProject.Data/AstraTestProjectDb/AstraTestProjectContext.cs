using System;
using System.Collections.Generic;
using AstraTestProject.Data.AstraTestProjectDb.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AstraTestProject.Data.AstraTestProjectDb
{
    public partial class AstraTestProjectContext : DbContext
    {
        public AstraTestProjectContext()
        {
        }

        public AstraTestProjectContext(DbContextOptions<AstraTestProjectContext> options)
            : base(options)
        {
			Database.EnsureCreated();
		}

        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer(ConfigWorker.GetSqlConnection("ConnectionStrings:AstraTestProjectDbConnection"));
			}
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Car>(entity =>
            {
                entity.Property(e => e.Cost).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Model)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasIndex(e => e.CarId, "FK_Orders_Cars");

                entity.HasIndex(e => e.CustomerId, "FK_Orders_Customers");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderNumber).HasComputedColumnSql("([Id])", false);

                entity.HasOne(d => d.Car)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Cars");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Customers");
            });

            OnModelCreatingPartial(modelBuilder);

            //инициализация БД начальными данными 

            var cars = new List<Car>()
            {
                new Car { Id = 1, Model = "Tesla", Cost = 69999.99M },
				new Car { Id = 2, Model = "Mercedes", Cost = 99999.99M },
				new Car { Id = 3, Model = "BMW", Cost = 59999.99M },
				new Car { Id = 4, Model = "LADA", Cost = 14999.99M }
			};

            var customers = new List<Customer>()
            {
				new Customer { Id = 1, Name = "B1", Address = "Saint Petersburg" },
				new Customer { Id = 2, Name = "Yandex", Address = "Moscow" },
				new Customer { Id = 3, Name = "AsstrA", Address = "Minsk" },
				new Customer { Id = 4, Name = "Vesta", Address = "Vitebsk" }
			};

            var orders = new List<Order>()
            {
				new Order { Id = 1, OrderDate = DateTime.Now, Amount = 1, CustomerId = 1, CarId = 1 },
				new Order { Id = 2, OrderDate = DateTime.Now, Amount = 2, CustomerId = 2, CarId = 2 },
				new Order { Id = 3, OrderDate = DateTime.Now, Amount = 3, CustomerId = 3, CarId = 1 },
				new Order { Id = 4, OrderDate = DateTime.Now, Amount = 5, CustomerId = 3, CarId = 2 },
				new Order { Id = 5, OrderDate = DateTime.Now, Amount = 14, CustomerId = 4, CarId = 3 },
			    new Order { Id = 6, OrderDate = DateTime.Now, Amount = 6, CustomerId = 1, CarId = 4 },
				new Order { Id = 7, OrderDate = DateTime.Now, Amount = 20, CustomerId = 2, CarId = 3 },
				new Order { Id = 8, OrderDate = DateTime.Now, Amount = 9, CustomerId = 4, CarId = 2 },
				new Order { Id = 9, OrderDate = DateTime.Now, Amount = 11, CustomerId = 1, CarId = 1 },
				new Order { Id = 10, OrderDate = DateTime.Now, Amount = 4, CustomerId = 2, CarId = 4 },
				new Order { Id = 11, OrderDate = DateTime.Now, Amount = 6, CustomerId = 3, CarId = 3 },
				new Order { Id = 12, OrderDate = DateTime.Now, Amount = 8, CustomerId = 4, CarId = 1 },
				new Order { Id = 13, OrderDate = DateTime.Now, Amount = 5, CustomerId = 1, CarId = 2 },
				new Order { Id = 14, OrderDate = DateTime.Now, Amount = 10, CustomerId = 3, CarId = 1 },
				new Order { Id = 15, OrderDate = DateTime.Now, Amount = 15, CustomerId = 3, CarId = 4 },
				new Order { Id = 16, OrderDate = DateTime.Now, Amount = 25, CustomerId = 4, CarId = 2 },
				new Order { Id = 17, OrderDate = DateTime.Now, Amount = 2, CustomerId = 1, CarId = 1 },
				new Order { Id = 18, OrderDate = DateTime.Now, Amount = 1, CustomerId = 1, CarId = 1 }
		};


			modelBuilder.Entity<Car>().HasData(cars);
			modelBuilder.Entity<Customer>().HasData(customers);
			modelBuilder.Entity<Order>().HasData(orders);

		}

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
