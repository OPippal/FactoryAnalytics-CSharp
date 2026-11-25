using Microsoft.EntityFrameworkCore;
namespace TovarniDispecink.Data;
using TovarniDispecink.Models;
public class TovarniDispecinkContext : DbContext
{
    // DbSet reprezentuje jednu tabulku v SQL databázi
    public DbSet<VyrobniLinka> Linky { get; set; }
    
    public DbSet<Zamestnanec> Zamestnanci { get; set; }

    public DbSet<ZaznamUdrzby> HistorieUdrzby { get; set; }

    // Tato metoda říká Entity Frameworku, kam má databázi uložit
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=tovarna.db");
    }
}