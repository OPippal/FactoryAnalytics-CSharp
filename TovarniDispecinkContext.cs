using Microsoft.EntityFrameworkCore;
namespace TovarniDispecink.Data;
using TovarniDispecink.Models;
public class TovarniDispecinkContext : DbContext
{
    // DbSet reprezentuje jednu tabulku v SQL databázi. 
    // Říkáme tím: "Vytvoř v databázi tabulku a ukládej do ní objekty VyrobniLinka."
    public DbSet<VyrobniLinka> Linky { get; set; }
    
    // Zde můžeme přidat i zaměstnance
    public DbSet<Zamestnanec> Zamestnanci { get; set; }

    public DbSet<ZaznamUdrzby> HistorieUdrzby { get; set; }

    // Tato metoda říká Entity Frameworku, kam má databázi uložit
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=tovarna.db");
    }
}