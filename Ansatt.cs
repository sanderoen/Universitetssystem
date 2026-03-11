namespace Apllikasjon_1;

public class Ansatt
{
    public string AnsattID { get; set; }
    public string Navn { get; set; }
    public string Epost { get; set; }
    public string Stilling { get; set; }
    public string Avdeling { get; set; }

    public override string ToString()
        => $"{Navn} {Epost} {Stilling} {Avdeling}";
}
