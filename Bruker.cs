namespace Universitets_system_oppgave_2;


public class Bruker
{
    public string Navn { get; set; }
    public string Epost { get; set; }
    public string Brukernavn { get; set; }
    public string Passord { get; set; }
    public string Rolle { get; set; }

    public override string ToString()
    {
        return $"{Navn} {Epost} {Rolle}";
    }
}
        
