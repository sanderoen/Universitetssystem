namespace Universitets_system_oppgave_2;


public class Bok
{
    public string ID { get; set; }
    public string tittel { get; set; }
    public string Forfatter { get; set; }
    public int år { get; set; }
    public int antalleksemplar { get; set; }

    public override string ToString()
    {
        return $"{ID} {tittel} {Forfatter} {år} Antall:{antalleksemplar}";
    }
}
