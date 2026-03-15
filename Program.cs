namespace Apllikasjon_1;

using System;
using System.Collections.Generic;

public class Program
{
    private static List<Student> studenter = new List<Student>();
    private static List<Ansatt> ansatte = new List<Ansatt>();
    private static List<Kurs> kursListe = new List<Kurs>();
    private static List<Bok> bøker = new List<Bok>();
    private static List<Lån> låneListe = new List<Lån>();
    
    
    
    public static void Main()
    {
        LagTestData();
        
        while (true)
        {
            Console.WriteLine("-------- MENY --------");
            Console.WriteLine("[1] Opprett kurs");
            Console.WriteLine("[2] Meld student til kurs");
            Console.WriteLine("[3] Print kurs og deltakere");
            Console.WriteLine("[4] Søk på kurs");
            Console.WriteLine("[5] Søk på bok");
            Console.WriteLine("[6] Lån bok");
            Console.WriteLine("[7] Returner bok");
            Console.WriteLine("[8] Registrer bok");
            Console.WriteLine("[9] Meld student av kurs");
            Console.WriteLine("[10] Vis aktive lån");
            Console.WriteLine("[11] Vis lånehistorikk");
            Console.WriteLine("[0] Avslutt");
        
            
            Console.Write("Valg: ");
            string valg = Console.ReadLine();
          
            switch (valg)
            {
                case "1":
                    OpprettKurs();
                    break;

                case "2":
                    MeldStudentTilKurs();
                    break;

                case "3":
                    PrintKursOgDeltakere();
                    break;

                case "4":
                    SøkKurs();
                    break;

                case "5":
                    SøkBok();
                    break;

                case "6":
                    LånBok();
                    break;

                case "7":
                    ReturnerBok();
                    break;

                case "8":
                    RegistrerBok();
                    break;

                case "9":
                    MeldStudentAvKurs();
                    break;

                case "10":
                    VisAktiveLån();
                    break;

                case "11":
                    VisLånehistorikk();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Ugyldig valg.");
                    break;
            }
        }
    }

    static void OpprettKurs()
    {
        Console.Write("Kurskode: ");
        string kode = Console.ReadLine();

        Console.Write("Kursnavn: ");
        string navn = Console.ReadLine();

        Console.Write("Studiepoeng: ");
        int studiepoeng = int.Parse(Console.ReadLine());

        Console.Write("Maks antall plasser: ");
        int maks = int.Parse(Console.ReadLine());

        Kurs nyttKurs = new Kurs();
        nyttKurs.kode = kode;
        nyttKurs.Navn = navn;
        nyttKurs.Studiepoeng = studiepoeng;
        nyttKurs.Maksplasser = maks;

        kursListe.Add(nyttKurs);

        Console.WriteLine("Kurs opprettet!");
    }

 
    static void MeldStudentTilKurs()
    {
        Console.Write("StudentID: ");
        string studentId = Console.ReadLine();

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

        Console.Write("Kurskode: ");
        string kode = Console.ReadLine();

        Kurs kurs = null;

        foreach (Kurs k in kursListe)
        {
            if (k.kode == kode)
            {
                kurs = k;
            }
        }

        if (kurs == null)
        {
            Console.WriteLine("Fant ikke kurs.");
            return;
        }

        if (kurs.Deltakere.Count >= kurs.Maksplasser)
        {
            Console.WriteLine("Kurset er fullt.");
            return;
        }

        if (kurs.Deltakere.Contains(student))
        {
            Console.WriteLine("Studenten er allerede meldt på kurset.");
            return;
        }

        kurs.Deltakere.Add(student);
        student.kurskoder.Add(kode);

        Console.WriteLine("Student meldt på kurs!");
    }

    static void MeldStudentAvKurs()
    {
        Console.Write("StudentID: ");
        string studentId = Console.ReadLine();

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

        Console.Write("Kurskode: ");
        string kode = Console.ReadLine();

        Kurs kurs = null;

        foreach (Kurs k in kursListe)
        {
            if (k.kode == kode)
            {
                kurs = k;
            }
        }

        if (kurs == null)
        {
            Console.WriteLine("Fant ikke kurs.");
            return;
        }

        if (!kurs.Deltakere.Contains(student))
        {
            Console.WriteLine("Studenten er ikke meldt på dette kurset.");
            return;
        }

        kurs.Deltakere.Remove(student);
        student.kurskoder.Remove(kode);

        Console.WriteLine("Student meldt av kurs!");
    }

    static void PrintKursOgDeltakere()
    {
        if (kursListe.Count == 0)
        {
            Console.WriteLine("Ingen kurs registrert.");
            return;
        }

        foreach (Kurs kurs in kursListe)
        {
            Console.WriteLine(kurs);

            if (kurs.Deltakere.Count == 0)
            {
                Console.WriteLine("Ingen deltakere.");
            }
            else
            {
                foreach (Student student in kurs.Deltakere)
                {
                    Console.WriteLine(student);
                }
            }

            Console.WriteLine();
        }
    }

    static void SøkKurs()
    {
        Console.Write("Søk etter kurskode eller navn: ");
        string søk = Console.ReadLine();

        bool fant = false;

        foreach (Kurs kurs in kursListe)
        {
            if (kurs.kode.Contains(søk) || kurs.Navn.Contains(søk))
            {
                Console.WriteLine (kurs);
                fant = true;
            }
        }

        if (!fant)
        {
            Console.WriteLine("Ingen kurs funnet.");
        }
    }

    static void SøkBok()
    {
        Console.Write("Søk etter tittel eller forfatter: ");
        string søk = Console.ReadLine();

        bool fant = false;

        foreach (Bok bok in bøker)
        {
            if (bok.tittel.Contains(søk) || bok.Forfatter.Contains(søk))
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

    static void LånBok()
    {
        Console.Write("Bok-ID: ");
        string bokId = Console.ReadLine();

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
            Console.WriteLine("Fant ikke bok.");
            return;
        }

        Console.Write("BrukerID: ");
        string brukerId = Console.ReadLine();

        bool brukerFinnes = false;

        foreach (Student s in studenter)
        {
            if (s.StudentID == brukerId)
            {
                brukerFinnes = true;
            }
        }

        foreach (Ansatt a in ansatte)
        {
            if (a.AnsattID == brukerId)
            {
                brukerFinnes = true;
            }
        }

        if (!brukerFinnes)
        {
            Console.WriteLine("Fant ikke bruker.");
            return;
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
            Console.WriteLine("Ingen eksemplarer tilgjengelig.");
            return;
        }

        Lån nyttLån = new Lån();
        nyttLån.BokID = bokId;
        nyttLån.BrukerID = brukerId;
        nyttLån.Aktiv = true;
        nyttLån.Låndato = DateTime.Now;

        låneListe.Add(nyttLån);

        Console.WriteLine("Bok lånt ut.");
    }

    static void ReturnerBok()
    {
        Console.Write("Bok-ID: ");
        string bokId = Console.ReadLine();

        Console.Write("BrukerID: ");
        string brukerId = Console.ReadLine();

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
            Console.WriteLine("Fant ikke aktivt lån.");
            return;
        }

        lånSomSkalReturneres.Aktiv = false;

        try
        {
            lånSomSkalReturneres.Returdato = DateTime.Now;
        }
        catch
        {
        }

        Console.WriteLine("Bok returnert.");
    }

    static void RegistrerBok()
    {
        Console.Write("Bok-ID: ");
        string id = Console.ReadLine();

        foreach (Bok b in bøker)
        {
            if (b.ID == id)
            {
                Console.WriteLine("Bok med denne ID-en finnes allerede.");
                return;
            }
        }

        Console.Write("Tittel: ");
        string tittel = Console.ReadLine();

        Console.Write("Forfatter: ");
        string forfatter = Console.ReadLine();

        Console.Write("År: ");
        int år = int.Parse(Console.ReadLine());

        Console.Write("Antall eksemplarer: ");
        int antall = int.Parse(Console.ReadLine());

        Bok nyBok = new Bok();
        nyBok.ID = id;
        nyBok.tittel = tittel;
        nyBok.Forfatter = forfatter;
        nyBok.år = år;
        nyBok.antalleksemplar = antall;

        bøker.Add(nyBok);

        Console.WriteLine("Bok registrert!");
    }

    static void VisAktiveLån()
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

    static void VisLånehistorikk()
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


    static void LagTestData()
    {
        Student s1 = new Student();
        s1.StudentID = "1409";
        s1.Navn = "Jens Nordmann";
        s1.Epost = "JensNord@uia.no";

        Student s2 = new Student();
        s2.StudentID = "5523";
        s2.Navn = "Hilde Hansen";
        s2.Epost = "HildHans@uia.no";
        
        Utvekslingstudent u1 = new Utvekslingstudent();
        u1.StudentID = "9001";
        u1.Navn = "Anna Müller";
        u1.Epost = "anna@exchange.no";
        u1.Hjemuniversitet = "University of Berlin";
        u1.Land = "Tyskland";
        
        studenter.Add(u1);
        studenter.Add(s1);
        studenter.Add(s2);

        Ansatt a1 = new Ansatt();
        a1.AnsattID = "A24";
        a1.Navn = "peter fram";
        a1.Epost = "ola@uia.no";

        ansatte.Add(a1);

        Kurs k1 = new Kurs();
        k1.kode = "EX101";
        k1.Navn = "Exfil";
        k1.Studiepoeng = 10;
        k1.Maksplasser = 30;

        kursListe.Add(k1);

        Bok b1 = new Bok();
        b1.ID = "B1";
        b1.tittel = "";
        b1.Forfatter = "Per Hansen";
        b1.år = 2020;
        b1.antalleksemplar = 2;

        Bok b2 = new Bok();
        b2.ID = "B2";
        b2.tittel = "Objektorientert programmering";
        b2.Forfatter = "Kari Nilsen";
        b2.år = 2022;
        b2.antalleksemplar = 1;

        bøker.Add(b1);
        bøker.Add(b2);
    }
}

