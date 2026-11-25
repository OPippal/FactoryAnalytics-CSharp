using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using TovarniDispecink.Models;
using TovarniDispecink.Data;

// 1. Založení webového serveru
var builder = WebApplication.CreateBuilder(args);

// 2. Dependency Injection (DI) - Naučíme server, že existuje naše databáze
builder.Services.AddDbContext<TovarniDispecinkContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TovarniDispecinkContext>();
    db.Database.EnsureCreated();

    if (!db.Linky.Any())
    {
        db.Linky.AddRange(
            new VyrobniLinka("L-01", 1200, 65.5, false),
            new VyrobniLinka("L-02", 850, 108.0, false),
            new VyrobniLinka("L-03", 400, 115.0, true)
        );
    }

    if (!db.Zamestnanci.Any())
    {
        db.Zamestnanci.AddRange(
            new Zamestnanec("Karel", RolePracovnika.Udrzbar),
            new Zamestnanec("Petr", RolePracovnika.Technik),
            new Zamestnanec("Jana", RolePracovnika.Manazer)
        );
    }

    db.SaveChanges();

    if (!db.HistorieUdrzby.Any())
    {
        var prvniLinka = db.Linky.First();
        var udrzbar = db.Zamestnanci.First(z => z.Role == RolePracovnika.Udrzbar);

        db.HistorieUdrzby.Add(new ZaznamUdrzby(prvniLinka.Id, udrzbar.Id, "Vstupní revize linky"));
        db.SaveChanges();
    }
}

app.UseSwagger();
app.UseSwaggerUI();

// 3. API ENDPOINTY

// Když někdo do prohlížeče zadá adresu /api/linky, spustí se tento kód:
app.MapGet("/api/linky", async (TovarniDispecinkContext db) => 
{
    // Získáme všechny linky z databáze a server je automaticky převede na JSON
    var vsechnyLinky = await db.Linky.ToListAsync();
    return Results.Ok(vsechnyLinky);
});

app.MapGet("/api/zamestnanci", async (TovarniDispecinkContext db) => 
{
    var vsichniZamestnanci = await db.Zamestnanci.ToListAsync();
    return Results.Ok(vsichniZamestnanci);

});
// Když někdo zadá /api/historie, uvidí záznamy o údržbě:
app.MapGet("/api/historie", async (TovarniDispecinkContext db) => 
{
    return Results.Ok(await db.HistorieUdrzby.ToListAsync());
});


app.MapGet("/api/vyroba/{oznaceni}", async (string oznaceni, TovarniDispecinkContext db) => 
{
    // Místo natvrdo napsaného "L-01" hledáme to, co přišlo z URL adresy
    var hledanaLinka = await db.Linky.FirstOrDefaultAsync(l => l.Oznaceni == oznaceni);

    if (hledanaLinka == null) return Results.NotFound($"Linka {oznaceni} neexistuje!");

    if (hledanaLinka.KritickaChyba)
    {
        return Results.Ok($"[INFO]: Linka {oznaceni} momentálně stojí a čeká na zásah údržby.");
    }
    else if (hledanaLinka.TeplotaCelsia >= 110.0)
    {
        hledanaLinka.KritickaChyba = true;
        await db.SaveChangesAsync();
        return Results.Ok($"[EMERGENCY STOP]: Linka {oznaceni} odstavena! Nebezpečí požáru.");
    }
    else
    {
        hledanaLinka.VyrobenoKusu += 150;
        hledanaLinka.TeplotaCelsia += 5.5;
        await db.SaveChangesAsync();
        return Results.Ok($"Výroba na lince {oznaceni} proběhla. Aktuální teplota: {hledanaLinka.TeplotaCelsia}°C");
    }
});

app.MapGet("/api/oprav/{oznaceniLinky}/{jmenoUdrzbare}", async (string oznaceniLinky, string jmenoUdrzbare, TovarniDispecinkContext db) => 
{
    var linka = await db.Linky.FirstOrDefaultAsync(l => l.Oznaceni == oznaceniLinky);
    var udrzbar = await db.Zamestnanci.FirstOrDefaultAsync(z => z.Jmeno == jmenoUdrzbare);

    if (linka == null) return Results.NotFound($"Linka {oznaceniLinky} neexistuje.");
    if (udrzbar == null) return Results.NotFound($"Zaměstnanec {jmenoUdrzbare} neexistuje.");

    try
    {
        // Voláme metodu z modelu!
        linka.ProvedUdrzbu(udrzbar);
        
        // Zápis do historie
        db.HistorieUdrzby.Add(new ZaznamUdrzby(linka.Id, udrzbar.Id, "Běžná oprava na vyžádání z tabletu"));
        
        await db.SaveChangesAsync();
        return Results.Ok($"[ÚDRŽBA]: Linka {linka.Oznaceni} byla úspěšně opravena technikem {udrzbar.Jmeno}.");
    }
    catch (UnauthorizedAccessException ex)
    {
        return Results.BadRequest($"[CHYBA PRÁV]: {ex.Message}");
    }
});

// 4. Spuštění serveru do nekonečné smyčky
app.Run();