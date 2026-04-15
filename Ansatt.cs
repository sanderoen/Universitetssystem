namespace Universitets_system_oppgave_2;

public class Ansatt : Bruker
{
    public string AnsattID { get; set; }
    public string Stilling { get; set; }
    public string Avdeling { get; set; }

    public override string ToString()
    {
        return $"{AnsattID} {Navn} {Epost} {Stilling} {Avdeling}";
    }
}
