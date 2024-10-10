using bad4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace bad4.Data
{
    public class BakeryDbContext : IdentityDbContext<ApiUser>
    {
        public DbSet<Dispatch> Dispatch { get; set; }
        public DbSet<Recipe> Recipe { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Allergen> Allergen { get; set; }
        public DbSet<BakingGoods> BakingGoods { get; set; } 
        public DbSet<Batch> Batch { get; set; }
        public DbSet<CompanyOrders> CompanyOrders { get; set; }
        public DbSet<Ingredients> Ingredients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=mssql-server;Initial Catalog=BAD4;User Id=sa;Password=massakadiablo99;Connect Timeout=30; Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False;");
        }

        public BakeryDbContext(DbContextOptions<BakeryDbContext> options) : base(options) { }

        public IQueryable<Stock> GetCurrentStock()
        {
            return Stock;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationship between CompanyOrders and Dispatch
            modelBuilder.Entity<CompanyOrders>()
                .HasMany(o => o.Dispatch)
                .WithOne(p => p.CompanyOrders)
                .HasForeignKey(p => p.CompanyOrdersID)
                .IsRequired();

            // Relationship between CompanyOrders and BakingGoods
            modelBuilder.Entity<CompanyOrders>()
                .HasMany(o => o.BakingGoods)
                .WithOne(p => p.CompanyOrders)
                .HasForeignKey(p => p.CompanyOrdersID)
                .IsRequired();

            // Define relationship between BakingGoods and Batch
            modelBuilder.Entity<BakingGoods>()
                .HasMany(o => o.Batch)
                .WithOne(o => o.BakingGoods)
                .HasForeignKey(o => o.BakingGoodsID)
                .IsRequired();

            // One-to-one relationship between Recipe and BakingGoods
            modelBuilder.Entity<Recipe>()
                .HasOne(o => o.BakingGoods)
                .WithOne(o => o.Recipe)
                .HasForeignKey<BakingGoods>(e => e.RecipeID)
                .IsRequired();

            // Composite key for Ingredients
            modelBuilder.Entity<Ingredients>()
                .HasKey(rs => new { rs.RecipeID, rs.StockID });

            // Relationship between Ingredients and Recipe
            modelBuilder.Entity<Ingredients>()
                .HasOne(rs => rs.Recipe)
                .WithMany(r => r.Ingredients)
                .HasForeignKey(rs => rs.RecipeID);

            // Relationship between Ingredients and Stock
            modelBuilder.Entity<Ingredients>()
                .HasOne(rs => rs.Stock)
                .WithMany(s => s.Ingredients)
                .HasForeignKey(rs => rs.StockID);

            // Relationship between Allergen and Stock
            modelBuilder.Entity<Allergen>()
                .HasOne(e => e.Stock)
                .WithOne(e => e.Allergen)
                .HasForeignKey<Allergen>(e => e.StockID)
                .IsRequired();


            // Initializing data for the BakingGoods entity.
            modelBuilder.Entity<BakingGoods>().HasData(
                new BakingGoods { BakingGoodsID = 1, Name = "Citronmaane", Quantity = 232, CompanyOrdersID = 1, RecipeID = 1 },
                new BakingGoods { BakingGoodsID = 2, Name = "Baklava", Quantity = 1000, CompanyOrdersID = 2, RecipeID = 2 },
                new BakingGoods { BakingGoodsID = 3, Name = "Jalabe", Quantity = 423, CompanyOrdersID = 3, RecipeID = 3 },
                new BakingGoods { BakingGoodsID = 4, Name = "Kunefe", Quantity = 999, CompanyOrdersID = 4, RecipeID = 4 }
                );

            // Initializing data for the Batch entity.
            modelBuilder.Entity<Batch>().HasData(
                new Batch { BatchID = 1, BakingGoodsID = 1, StartTime = DateTime.Now, FinishTime = DateTime.Now.AddMinutes(40), Delay = 40 },
                new Batch { BatchID = 2, BakingGoodsID = 2, StartTime = DateTime.Now.AddMinutes(50), FinishTime = DateTime.Now.AddMinutes(70), Delay = 0 },
                new Batch { BatchID = 3, BakingGoodsID = 3, StartTime = DateTime.Now.AddMinutes(70), FinishTime = DateTime.Now.AddMinutes(100), Delay = 20 },
                new Batch { BatchID = 4, BakingGoodsID = 4, StartTime = DateTime.Now.AddMinutes(130), FinishTime = DateTime.Now.AddMinutes(150), Delay = 56 }
                );

            // Initializing data for the Dispatch entity.
            modelBuilder.Entity<Dispatch>().HasData(
                new Dispatch { TrackID = 1, CompanyOrdersID = 1, Longitude = 139, Latitude = 931 },
                new Dispatch { TrackID = 2, CompanyOrdersID = 2, Longitude = 759, Latitude = 957 },
                new Dispatch { TrackID = 3, CompanyOrdersID = 3, Longitude = 529, Latitude = 1 },
                new Dispatch { TrackID = 4, CompanyOrdersID = 4, Longitude = 329, Latitude = 923 }
                );

            // Initializing data for the CompanyOrders entity.
            modelBuilder.Entity<CompanyOrders>().HasData(
                new CompanyOrders { CompanyOrdersID = 1, DeliveryDate = "12032002 2205", DeliveryPlace = "Superbrugsen" },
                new CompanyOrders { CompanyOrdersID = 2, DeliveryDate = "02052001 2240", DeliveryPlace = "Bilka" },
                new CompanyOrders { CompanyOrdersID = 3, DeliveryDate = "16032003 1600", DeliveryPlace = "Lidl" },
                new CompanyOrders { CompanyOrdersID = 4, DeliveryDate = "03082001 2359", DeliveryPlace = "SuperBest" }
                );

            // Initializing data for the Recipe entity.
            modelBuilder.Entity<Recipe>().HasData(
                new Recipe { RecipeID = 1, Name = "Citronmaane" },
                new Recipe { RecipeID = 2, Name = "Baklava" },
                new Recipe { RecipeID = 3, Name = "Jalabe" },
                new Recipe { RecipeID = 4, Name = "Kunefe" }
            );

            // Initializing data for the Stock entity.
            modelBuilder.Entity<Stock>().HasData(
                new Stock { StockID = 1, Name = "Flour", Quantity = 123, },
                new Stock { StockID = 2, Name = "Leftover Cake", Quantity = 435 },
                new Stock { StockID = 3, Name = "Salt", Quantity = 342 },
                new Stock { StockID = 4, Name = "Milk", Quantity = 546 },
                new Stock { StockID = 5, Name = "Egg", Quantity = 234 },
                new Stock { StockID = 6, Name = "Yeast", Quantity = 457 },
                new Stock { StockID = 7, Name = "Rum", Quantity = 234 },
                new Stock { StockID = 8, Name = "Baking Powder", Quantity = 675 },
                new Stock { StockID = 9, Name = "Baking soda", Quantity = 243 }
                );
            

            modelBuilder.Entity<Ingredients>().HasData(
                //Citronmaane
                new Ingredients { Quantity = 1000, RecipeID = 1, StockID = 1 },
                new Ingredients { Quantity = 2300, RecipeID = 1, StockID = 2 },
                new Ingredients { Quantity = 3000, RecipeID = 1, StockID = 3 },
                new Ingredients { Quantity = 1230, RecipeID = 1, StockID = 4 },

                //Baklava
                new Ingredients { Quantity = 100, RecipeID = 2, StockID = 3 },
                new Ingredients { Quantity = 2400, RecipeID = 2, StockID = 4 },
                new Ingredients { Quantity = 250, RecipeID = 2, StockID = 5 },
                new Ingredients { Quantity = 300, RecipeID = 2, StockID = 2 },

                //Jalabe
                new Ingredients { Quantity = 1000, RecipeID = 3, StockID = 7 },
                new Ingredients { Quantity = 2300, RecipeID = 3, StockID = 2 },
                new Ingredients { Quantity = 230, RecipeID = 3, StockID = 6 },
                new Ingredients { Quantity = 15000, RecipeID = 3, StockID = 3 },

                //Kunefe
                new Ingredients { Quantity = 200, RecipeID = 4, StockID = 7 },
                new Ingredients { Quantity = 300, RecipeID = 4, StockID = 8 },
                new Ingredients { Quantity = 200, RecipeID = 4, StockID = 3 },
                new Ingredients { Quantity = 300, RecipeID = 4, StockID = 4 }
            );

            // Initializing data for the Allergen entity.
            modelBuilder.Entity<Allergen>().HasData(
                new Allergen { AllergenID = 1, Name = "Leavening agent", StockID = 1 },
                new Allergen { AllergenID = 2, Name = "Alcohol Free", StockID = 2 },
                new Allergen { AllergenID = 3, Name = "Gluten", StockID = 3 },
                new Allergen { AllergenID = 4, Name = "Lactose", StockID = 4 },
                new Allergen { AllergenID = 5, Name = "Gluten", StockID = 5 },
                new Allergen { AllergenID = 6, Name = "Glucose", StockID = 6 },
                new Allergen { AllergenID = 7, Name = "Sodium", StockID = 7 },
                new Allergen { AllergenID = 8, Name = "Water", StockID = 8 },
                new Allergen { AllergenID = 9, Name = "Protein", StockID = 9 }
            );
        }
    }
}
