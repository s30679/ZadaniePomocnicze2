using Microsoft.EntityFrameworkCore;
using ZadanieProbne2.Models;

namespace ZadanieProbne2.DAL;

public class CharacterDbContext : DbContext
{
    public DbSet<Character> Characters { get; set; }
    public DbSet<Backpack> Backpacks { get; set; }
    public DbSet<Character_Title> Character_Titles { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Title> Titles { get; set; }

    protected CharacterDbContext()
    {
    }

    public CharacterDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Backpack>().HasKey(b => new { b.CharacterId, b.ItemId});
        modelBuilder.Entity<Character_Title>().HasKey(ct => new { ct.CharacterId, ct.TitleId });
        
        //Przykładowe dane
        modelBuilder.Entity<Item>().HasData(
            new Item { ItemId = 1, Name = "I1", Weight = 100 },
            new Item { ItemId = 2, Name = "I2", Weight = 200 },
            new Item { ItemId = 3, Name = "I3", Weight = 300 }
        );
        modelBuilder.Entity<Title>().HasData(
            new Title { TitleId = 1, Name = "T1"},
            new Title { TitleId = 2, Name = "T2"},
            new Title { TitleId = 3, Name = "T3"}
        );
        modelBuilder.Entity<Character>().HasData(
            new Character
            {
                CharacterId = 1,
                FirstName = "Arthur",
                LastName = "Morgan",
                CurrentWeight = 80,
                MaxWeight = 100
            }
        );
        modelBuilder.Entity<Backpack>().HasData(
            new Backpack { CharacterId = 1, ItemId = 1, Amount = 1},
            new Backpack { CharacterId = 1, ItemId = 2, Amount = 2},
            new Backpack { CharacterId = 1, ItemId = 3, Amount = 3}
        );
        modelBuilder.Entity<Character_Title>().HasData(
            new Character_Title { CharacterId = 1, TitleId = 1, AcquiredAt = new DateTime(2020, 9,9)},
            new Character_Title { CharacterId = 1, TitleId = 2, AcquiredAt = new DateTime(2020, 8,8)},
            new Character_Title { CharacterId = 1, TitleId = 3, AcquiredAt = new DateTime(2020, 7,7)}
        );
    }
}