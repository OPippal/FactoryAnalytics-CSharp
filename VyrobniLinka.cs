namespace TovarniDispecink.Models;
public class VyrobniLinka
{
    public int Id {get;set;}
    public string Oznaceni{get;set;}
    public int VyrobenoKusu{get;set;}
    public double TeplotaCelsia{get;set;}
    public bool KritickaChyba{get;set;}

    public VyrobniLinka(){}

    public VyrobniLinka(string oznaceni, int vyrobenokusu, double teplotacelsia, bool kritickachyba)
    {
        Oznaceni = oznaceni;
        VyrobenoKusu = vyrobenokusu;
        TeplotaCelsia = teplotacelsia;
        KritickaChyba = kritickachyba;
    }
    public void ProvedUdrzbu(Zamestnanec pracovnik)
    {
        if(pracovnik.Role == RolePracovnika.Udrzbar)
        {
            TeplotaCelsia = 50.0;
            KritickaChyba = false;
        }
        else
        {
            throw new UnauthorizedAccessException($"Zaměstnanec {pracovnik.Jmeno} nemá oprávnění k údržbě!");
        }
    }
}