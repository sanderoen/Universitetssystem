namespace Universitets_system_oppgave_2;


public class Universitet
{
    public List<Student> studenter { get; set; } = new List<Student>();
    public List<Faglærer> faglærere { get; set; } = new List<Faglærer>();
    public List<Bibliotekansatt> bibliotekansatte { get; set; } = new List<Bibliotekansatt>();
    public List<Kurs> kursListe { get; set; } = new List<Kurs>();
    public List<Bok> bøker { get; set; } = new List<Bok>();
    public List<Lån> låneListe { get; set; } = new List<Lån>();

    public Bruker LoggInn(string brukernavn, string passord)
    {
        foreach (Student s in studenter)
        {
            if (s.Brukernavn == brukernavn && s.Passord == passord)
            {
                return s;
            }
        }

        foreach (Faglærer f in faglærere)
        {
            if (f.Brukernavn == brukernavn && f.Passord == passord)
            {
                return f;
            }
        }

        foreach (Bibliotekansatt b in bibliotekansatte)
        {
            if (b.Brukernavn == brukernavn && b.Passord == passord)
            {
                return b;
            }
        }

        return null;
    }

    public bool RegistrerStudent(Student student)
    {
        if (student == null) return false;
        if (string.IsNullOrWhiteSpace(student.StudentID)) return false;
        if (string.IsNullOrWhiteSpace(student.Brukernavn)) return false;
        if (BrukernavnFinnes(student.Brukernavn)) return false;

        studenter.Add(student);
        return true;
    }

    public bool RegistrerFaglærer(Faglærer lærer)
    {
        if (lærer == null) return false;
        if (string.IsNullOrWhiteSpace(lærer.AnsattID)) return false;
        if (string.IsNullOrWhiteSpace(lærer.Brukernavn)) return false;
        if (BrukernavnFinnes(lærer.Brukernavn)) return false;

        faglærere.Add(lærer);
        return true;
    }

    public bool RegistrerBibliotekansatt(Bibliotekansatt ansatt)
    {
        if (ansatt == null) return false;
        if (string.IsNullOrWhiteSpace(ansatt.AnsattID)) return false;
        if (string.IsNullOrWhiteSpace(ansatt.Brukernavn)) return false;
        if (BrukernavnFinnes(ansatt.Brukernavn)) return false;

        bibliotekansatte.Add(ansatt);
        return true;
    }

    private bool BrukernavnFinnes(string brukernavn)
    {
        foreach (Student s in studenter)
        {
            if (s.Brukernavn == brukernavn)
            {
                return true;
            }
        }

        foreach (Faglærer f in faglærere)
        {
            if (f.Brukernavn == brukernavn)
            {
                return true;
            }
        }

        foreach (Bibliotekansatt b in bibliotekansatte)
        {
            if (b.Brukernavn == brukernavn)
            {
                return true;
            }
        }

        return false;
    }

    public string OpprettKurs(string kode, string navn, int studiepoeng, int maksplasser, string lærerId)
    {
        if (string.IsNullOrWhiteSpace(kode) || string.IsNullOrWhiteSpace(navn))
        {
            return "Kurskode og kursnavn må fylles ut.";
        }

        if (studiepoeng <= 0 || maksplasser <= 0)
        {
            return "Studiepoeng og maksplasser må være større enn 0.";
        }

        foreach (Kurs k in kursListe)
        {
            if (k.kode == kode || k.Navn == navn)
            {
                return "Kurs med samme kode eller navn finnes allerede.";
            }
        }

        Kurs nyttKurs = new Kurs();
        nyttKurs.kode = kode;
        nyttKurs.Navn = navn;
        nyttKurs.Studiepoeng = studiepoeng;
        nyttKurs.Maksplasser = maksplasser;
        nyttKurs.FaglærerID = lærerId;

        kursListe.Add(nyttKurs);

        return "Kurs opprettet.";
    }

    public string MeldStudentTilKurs(string studentId, string kurskode)
    {
        Student student = null;
        Kurs kurs = null;

        foreach (Student s in studenter)
        {
            if (s.StudentID == studentId)
            {
                student = s;
            }
        }

        foreach (Kurs k in kursListe)
        {
            if (k.kode == kurskode)
            {
                kurs = k;
            }
        }

        if (student == null)
        {
            return "Fant ikke student.";
        }

        if (kurs == null)
        {
            return "Fant ikke kurs.";
        }

        if (kurs.Deltakere.Count >= kurs.Maksplasser)
        {
            return "Kurset er fullt.";
        }

        foreach (Student s in kurs.Deltakere)
        {
            if (s.StudentID == studentId)
            {
                return "Studenten er allerede meldt på kurset.";
            }
        }

        kurs.Deltakere.Add(student);
        student.kurskoder.Add(kurskode);

        return "Student meldt på kurs.";
    }

    public string MeldStudentAvKurs(string studentId, string kurskode)
    {
        Student student = null;
        Kurs kurs = null;

        foreach (Student s in studenter)
        {
            if (s.StudentID == studentId)
            {
                student = s;
            }
        }

        foreach (Kurs k in kursListe)
        {
            if (k.kode == kurskode)
            {
                kurs = k;
            }
        }

        if (student == null)
        {
            return "Fant ikke student.";
        }

        if (kurs == null)
        {
            return "Fant ikke kurs.";
        }

        bool finnes = false;
        foreach (Student s in kurs.Deltakere)
        {
            if (s.StudentID == studentId)
            {
                finnes = true;
            }
        }

        if (!finnes)
        {
            return "Studenten er ikke meldt på dette kurset.";
        }

        kurs.Deltakere.Remove(student);
        student.kurskoder.Remove(kurskode);

        return "Student meldt av kurs.";
    }

    public void SøkKurs(string søk)
    {
        bool fant = false;

        foreach (Kurs kurs in kursListe)
        {
            if ((kurs.kode != null && kurs.kode.Contains(søk, StringComparison.OrdinalIgnoreCase)) ||
                (kurs.Navn != null && kurs.Navn.Contains(søk, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine(kurs);
                fant = true;
            }
        }

        if (!fant)
        {
            Console.WriteLine("Ingen kurs funnet.");
        }
    }

    public void SøkBok(string søk)
    {
        bool fant = false;

        foreach (Bok bok in bøker)
        {
            if ((bok.tittel != null && bok.tittel.Contains(søk, StringComparison.OrdinalIgnoreCase)) ||
                (bok.Forfatter != null && bok.Forfatter.Contains(søk, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine(bok);
                fant = true;
            }
        }

        if (!fant)
        {
            Console.WriteLine("Ingen bøker funnet.");
        }
    }

    public string LånBok(string bokId, string brukerId)
    {
        Bok bok = null;

        foreach (Bok b in bøker)
        {
            if (b.ID == bokId)
            {
                bok = b;
            }
        }

        if (bok == null)
        {
            return "Fant ikke bok.";
        }

        bool brukerFinnes = false;

        foreach (Student s in studenter)
        {
            if (s.StudentID == brukerId)
            {
                brukerFinnes = true;
            }
        }

        foreach (Faglærer f in faglærere)
        {
            if (f.AnsattID == brukerId)
            {
                brukerFinnes = true;
            }
        }

        foreach (Bibliotekansatt b in bibliotekansatte)
        {
            if (b.AnsattID == brukerId)
            {
                brukerFinnes = true;
            }
        }

        if (!brukerFinnes)
        {
            return "Fant ikke bruker.";
        }

        int aktiveLån = 0;

        foreach (Lån lån in låneListe)
        {
            if (lån.BokID == bokId && lån.Aktiv)
            {
                aktiveLån++;
            }
        }

        if (aktiveLån >= bok.antalleksemplar)
        {
            return "Ingen eksemplarer tilgjengelig.";
        }

        Lån nyttLån = new Lån();
        nyttLån.BokID = bokId;
        nyttLån.BrukerID = brukerId;
        nyttLån.Aktiv = true;
        nyttLån.Låndato = DateTime.Now;

        låneListe.Add(nyttLån);

        return "Bok lånt ut.";
    }

    public string ReturnerBok(string bokId, string brukerId)
    {
        Lån lånSomSkalReturneres = null;

        foreach (Lån lån in låneListe)
        {
            if (lån.BokID == bokId && lån.BrukerID == brukerId && lån.Aktiv)
            {
                lånSomSkalReturneres = lån;
            }
        }

        if (lånSomSkalReturneres == null)
        {
            return "Fant ikke aktivt lån.";
        }

        lånSomSkalReturneres.Aktiv = false;
        lånSomSkalReturneres.Returdato = DateTime.Now;

        return "Bok returnert.";
    }

    public string RegistrerBok(string id, string tittel, string forfatter, int år, int antall)
    {
        if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(tittel) || string.IsNullOrWhiteSpace(forfatter))
        {
            return "Alle bokfeltene må fylles ut.";
        }

        if (år <= 0 || antall <= 0)
        {
            return "År og antall må være større enn 0.";
        }

        foreach (Bok b in bøker)
        {
            if (b.ID == id)
            {
                return "Bok med denne ID-en finnes allerede.";
            }
        }

        Bok nyBok = new Bok();
        nyBok.ID = id;
        nyBok.tittel = tittel;
        nyBok.Forfatter = forfatter;
        nyBok.år = år;
        nyBok.antalleksemplar = antall;

        bøker.Add(nyBok);

        return "Bok registrert.";
    }

    public string RegistrerPensum(string kurskode, string bokId, string lærerId)
    {
        Kurs kurs = null;
        Bok bok = null;

        foreach (Kurs k in kursListe)
        {
            if (k.kode == kurskode)
            {
                kurs = k;
            }
        }

        foreach (Bok b in bøker)
        {
            if (b.ID == bokId)
            {
                bok = b;
            }
        }

        if (kurs == null)
        {
            return "Fant ikke kurs.";
        }

        if (bok == null)
        {
            return "Fant ikke bok.";
        }

        if (kurs.FaglærerID != lærerId)
        {
            return "Du underviser ikke dette kurset.";
        }

        foreach (string id in kurs.pensumBøker)
        {
            if (id == bokId)
            {
                return "Boken er allerede registrert som pensum.";
            }
        }

        kurs.pensumBøker.Add(bokId);
        return "Pensum registrert.";
    }

    public string SettKarakter(string kurskode, string studentId, string karakter, string lærerId)
    {
        Kurs kurs = null;

        foreach (Kurs k in kursListe)
        {
            if (k.kode == kurskode)
            {
                kurs = k;
            }
        }

        if (kurs == null)
        {
            return "Fant ikke kurs.";
        }

        if (kurs.FaglærerID != lærerId)
        {
            return "Du underviser ikke dette kurset.";
        }

        bool studentFinnes = false;

        foreach (Student s in kurs.Deltakere)
        {
            if (s.StudentID == studentId)
            {
                studentFinnes = true;
            }
        }

        if (!studentFinnes)
        {
            return "Studenten er ikke meldt på dette kurset.";
        }

        kurs.karakterer[studentId] = karakter;
        return "Karakter satt.";
    }

    public void VisStudentKurs(string studentId)
    {
        Student student = null;

        foreach (Student s in studenter)
        {
            if (s.StudentID == studentId)
            {
                student = s;
            }
        }

        if (student == null)
        {
            Console.WriteLine("Fant ikke student.");
            return;
        }

        if (student.kurskoder.Count == 0)
        {
            Console.WriteLine("Ingen kurs.");
            return;
        }

        foreach (string kode in student.kurskoder)
        {
            Console.WriteLine(kode);
        }
    }

    public void VisKarakterer(string studentId)
    {
        bool fant = false;

        foreach (Kurs kurs in kursListe)
        {
            if (kurs.karakterer.ContainsKey(studentId))
            {
                Console.WriteLine($"{kurs.kode} {kurs.Navn}: {kurs.karakterer[studentId]}");
                fant = true;
            }
        }

        if (!fant)
        {
            Console.WriteLine("Ingen karakterer registrert.");
        }
    }

    public void VisAktiveLån()
    {
        bool fant = false;

        foreach (Lån lån in låneListe)
        {
            if (lån.Aktiv)
            {
                Console.WriteLine(lån);
                fant = true;
            }
        }

        if (!fant)
        {
            Console.WriteLine("Ingen aktive lån.");
        }
    }

    public void VisLånehistorikk()
    {
        if (låneListe.Count == 0)
        {
            Console.WriteLine("Ingen lån registrert.");
            return;
        }

        foreach (Lån lån in låneListe)
        {
            Console.WriteLine(lån);
        }
    }

    public void LagTestData()
    {
        Student s1 = new Student();
        s1.StudentID = "1409";
        s1.Navn = "Jens Nordmann";
        s1.Epost = "JensNord@uia.no";
        s1.Brukernavn = "jens";
        s1.Passord = "1234";
        s1.Rolle = "student";

        Student s2 = new Student();
        s2.StudentID = "5523";
        s2.Navn = "Hilde Hansen";
        s2.Epost = "HildHans@uia.no";
        s2.Brukernavn = "hilde";
        s2.Passord = "1234";
        s2.Rolle = "student";

        Utvekslingstudent u1 = new Utvekslingstudent();
        u1.StudentID = "9001";
        u1.Navn = "Fredrik Müller";
        u1.Epost = "fredrik@uob.com";
        u1.Brukernavn = "fredrik";
        u1.Passord = "1234";
        u1.Rolle = "student";
        u1.Hjemuniversitet = "University of Berlin";
        u1.Land = "Tyskland";
        u1.fra = "01.08.2025";
        u1.til = "01.06.2026";

        studenter.Add(s1);
        studenter.Add(s2);
        studenter.Add(u1);

        Faglærer f1 = new Faglærer();
        f1.AnsattID = "F10";
        f1.Navn = "Per strøm";
        f1.Epost = "perstrøm@uia.no";
        f1.Brukernavn = "perstrøm";
        f1.Passord = "1234";
        f1.Rolle = "faglærer";
        f1.Stilling = "Faglærer";
        f1.Avdeling = "IT";

        faglærere.Add(f1);

        Bibliotekansatt bA1 = new Bibliotekansatt();
        bA1.AnsattID = "B20";
        bA1.Navn = "Lise Birkeland";
        bA1.Epost = "lisebirkeland@uia.no";
        bA1.Brukernavn = "lisebirkeland";
        bA1.Passord = "1234";
        bA1.Rolle = "bibliotekansatt";
        bA1.Stilling = "Bibliotekansatt";
        bA1.Avdeling = "Bibliotek";

        bibliotekansatte.Add(bA1);

        Kurs k1 = new Kurs();
        k1.kode = "EX101";
        k1.Navn = "Exphil";
        k1.Studiepoeng = 10;
        k1.Maksplasser = 30;
        k1.FaglærerID = "F10";

        Kurs k2 = new Kurs();
        k2.kode = "PROG100";
        k2.Navn = "Programmering";
        k2.Studiepoeng = 10;
        k2.Maksplasser = 25;
        k2.FaglærerID = "F10";

        kursListe.Add(k1);
        kursListe.Add(k2);

        Bok bok1 = new Bok();
        bok1.ID = "B1";
        bok1.tittel = "Objektorientert programmering";
        bok1.Forfatter = "Kari Nilsen";
        bok1.år = 2022;
        bok1.antalleksemplar = 2;

        Bok bok2 = new Bok();
        bok2.ID = "B2";
        bok2.tittel = "Databaser";
        bok2.Forfatter = "Per Strøm";
        bok2.år = 2020;
        bok2.antalleksemplar = 1;

        bøker.Add(bok1);
        bøker.Add(bok2);
    }
}
