namespace Apllikasjon_1;

public class Lån
{
    public string BokID { get; set; }
    public string BrukerID { get; set; }
    public bool Aktiv { get; set; } = true;
    public DateTime Låndato{  get; set; } =  DateTime.Now;
    
    public DateTime? Returdato { get; set; }
    public override string ToString()
    {
        string status = Aktiv ? "AKTIV" : "RETURNERT";

        return $"{status} Bok:{BokID} Bruker:{BrukerID} {Låndato}";
    }

}
