namespace Apllikasjon_1;

public class Student
{
    public string  StudentID { get; set; }
    public string Navn { get; set; }
    public string Epost { get; set; }

    public List<string> kurskoder { get; set;}
    public override string ToString()
    => $"{StudentID} {Navn} {Epost}";
}

