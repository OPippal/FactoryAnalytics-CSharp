namespace TovarniDispecink.Models;
public enum RolePracovnika
{
    Udrzbar,
    Technik,
    Manazer
}
public class Zamestnanec 
{
    public int Id {get;set;}
    public string Jmeno{get;set;}
    public RolePracovnika Role {get;set;}

    public Zamestnanec(){}

    public Zamestnanec(string jmeno, RolePracovnika role)
    {
        Jmeno = jmeno;
        Role = role;
    }
}