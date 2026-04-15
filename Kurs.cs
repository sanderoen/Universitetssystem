    namespace Universitets_system_oppgave_2;

    public class Kurs
    {
        public string kode { get; set; }
        public string Navn { get; set; }
        public int Studiepoeng { get; set; }
        public int Maksplasser { get; set; }
        public string FaglærerID { get; set; }

        public List<Student> Deltakere { get; set; } = new List<Student>();
        public List<string> pensumBøker { get; set; } = new List<string>();
        public Dictionary<string, string> karakterer { get; set; } = new Dictionary<string, string>();

        public override string ToString()
        {
            return $"{kode} - {Navn} - ({Studiepoeng}sp) {Deltakere.Count}/{Maksplasser}";
        }
    }
