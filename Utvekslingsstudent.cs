namespace Universitets_system_oppgave_2;

public class Utvekslingstudent : Student
{
    public string Hjemuniversitet { get; set; }
    public string Land { get; set; }
    public string fra { get; set; }
    public string til { get; set; }

    public override string ToString()
    {
        return base.ToString() + $" {Hjemuniversitet} {Land}";
    }
}

