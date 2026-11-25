namespace TovarniDispecink.Models;
public class ZaznamUdrzby
{
    public int Id { get; set; }
    public DateTime CasOpravy { get; set; }
    public string PopisZavady { get; set; }

    // Foreign Key
    // Databáze díky nim ví, ke které lince a komu záznam patří
    public int VyrobniLinkaId { get; set; }
    public int ZamestnanecId { get; set; }

    public ZaznamUdrzby() { }

    public ZaznamUdrzby(int linkaId, int zamestnanecId, string popis)
    {
        CasOpravy = DateTime.Now; // Automaticky dosadí aktuální čas
        VyrobniLinkaId = linkaId;
        ZamestnanecId = zamestnanecId;
        PopisZavady = popis;
    }
}