sing Universitets_system_oppgave_2;


public class Student : Bruker
{
    public string StudentID { get; set; }
    public List<string> kurskoder { get; set; } = new List<string>();

    public override string ToString()
    {
        return $"{StudentID} {Navn} {Epost}";
    }
}
