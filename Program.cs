namespace Universitets_system_oppgave_2;
    




public class Program
{
    static Universitet universitet = new Universitet();

    public static void Main()
    {
        universitet.LagTestData();

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("----- UNIVERSITETSSYSTEM -----");
            Console.WriteLine("[1] Logg inn");
            Console.WriteLine("[2] Registrer ny bruker");
            Console.WriteLine("[0] Avslutt");
            Console.Write("Valg: ");
            string valg = Console.ReadLine();

            switch (valg)
            {
                case "1":
                    LoggInn();
                    break;

                case "2":
                    RegistrerBruker();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Ugyldig valg.");
                    break;
            }
        }
    }

    static void LoggInn()
    {
        Console.Write("Brukernavn: ");
        string brukernavn = Console.ReadLine();

        Console.Write("Passord: ");
        string passord = Console.ReadLine();

        Bruker bruker = universitet.LoggInn(brukernavn, passord);

        if (bruker == null)
        {
            Console.WriteLine("Feil brukernavn eller passord.");
            return;
        }

        Console.WriteLine($"Innlogget som {bruker.Navn} ({bruker.Rolle})");

        if (bruker.Rolle == "student")
        {
            StudentMeny((Student)bruker);
        }
        else if (bruker.Rolle == "faglærer")
        {
            FaglærerMeny((Faglærer)bruker);
        }
        else if (bruker.Rolle == "bibliotekansatt")
        {
            BibliotekMeny((Bibliotekansatt)bruker);
        }
    }

    static void RegistrerBruker()
    {
        Console.WriteLine("[1] Student");
        Console.WriteLine("[2] Faglærer");
        Console.WriteLine("[3] Bibliotekansatt");
        Console.Write("Velg rolle: ");
        string rolleValg = Console.ReadLine();

        Console.Write("ID: ");
        string id = Console.ReadLine();

        Console.Write("Navn: ");
        string navn = Console.ReadLine();

        Console.Write("Epost: ");
        string epost = Console.ReadLine();

        Console.Write("Brukernavn: ");
        string brukernavn = Console.ReadLine();

        Console.Write("Passord: ");
        string passord = Console.ReadLine();

        bool registrert = false;

        if (rolleValg == "1")
        {
            Student student = new Student();
            student.StudentID = id;
            student.Navn = navn;
            student.Epost = epost;
            student.Brukernavn = brukernavn;
            student.Passord = passord;
            student.Rolle = "student";

            registrert = universitet.RegistrerStudent(student);
        }
        else if (rolleValg == "2")
        {
            Faglærer lærer = new Faglærer();
            lærer.AnsattID = id;
            lærer.Navn = navn;
            lærer.Epost = epost;
            lærer.Brukernavn = brukernavn;
            lærer.Passord = passord;
            lærer.Rolle = "faglærer";
            lærer.Stilling = "Faglærer";
            lærer.Avdeling = "Undervisning";

            registrert = universitet.RegistrerFaglærer(lærer);
        }
        else if (rolleValg == "3")
        {
            Bibliotekansatt ansatt = new Bibliotekansatt();
            ansatt.AnsattID = id;
            ansatt.Navn = navn;
            ansatt.Epost = epost;
            ansatt.Brukernavn = brukernavn;
            ansatt.Passord = passord;
            ansatt.Rolle = "bibliotekansatt";
            ansatt.Stilling = "Bibliotekansatt";
            ansatt.Avdeling = "Bibliotek";

            registrert = universitet.RegistrerBibliotekansatt(ansatt);
        }
        else
        {
            Console.WriteLine("Ugyldig rollevalg.");
            return;
        }

        if (registrert)
        {
            Console.WriteLine("Bruker registrert.");
        }
        else
        {
            Console.WriteLine("Kunne ikke registrere bruker.");
        }
    }

    static void StudentMeny(Student student)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("---- STUDENT MENY ----");
            Console.WriteLine("[1] Meld på kurs");
            Console.WriteLine("[2] Meld av kurs");
            Console.WriteLine("[3] Se mine kurs");
            Console.WriteLine("[4] Se mine karakterer");
            Console.WriteLine("[5] Søk bok");
            Console.WriteLine("[6] Lån bok");
            Console.WriteLine("[7] Returner bok");
            Console.WriteLine("[0] Logg ut");
            Console.Write("Valg: ");
            string valg = Console.ReadLine();

            switch (valg)
            {
                case "1":
                    Console.Write("Kurskode: ");
                    Console.WriteLine(universitet.MeldStudentTilKurs(student.StudentID, Console.ReadLine()));
                    break;

                case "2":
                    Console.Write("Kurskode: ");
                    Console.WriteLine(universitet.MeldStudentAvKurs(student.StudentID, Console.ReadLine()));
                    break;

                case "3":
                    universitet.VisStudentKurs(student.StudentID);
                    break;

                case "4":
                    universitet.VisKarakterer(student.StudentID);
                    break;

                case "5":
                    Console.Write("Søk etter bok: ");
                    universitet.SøkBok(Console.ReadLine());
                    break;

                case "6":
                    Console.Write("Bok-ID: ");
                    Console.WriteLine(universitet.LånBok(Console.ReadLine(), student.StudentID));
                    break;

                case "7":
                    Console.Write("Bok-ID: ");
                    Console.WriteLine(universitet.ReturnerBok(Console.ReadLine(), student.StudentID));
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Ugyldig valg.");
                    break;
            }
        }
    }

    static void FaglærerMeny(Faglærer lærer)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("---- FAGLÆRER MENY ----");
            Console.WriteLine("[1] Opprett kurs");
            Console.WriteLine("[2] Søk kurs");
            Console.WriteLine("[3] Søk bok");
            Console.WriteLine("[4] Lån bok");
            Console.WriteLine("[5] Returner bok");
            Console.WriteLine("[6] Sett karakter");
            Console.WriteLine("[7] Registrer pensum");
            Console.WriteLine("[0] Logg ut");
            Console.Write("Valg: ");
            string valg = Console.ReadLine();

            switch (valg)
            {
                case "1":
                    try
                    {
                        Console.Write("Kurskode: ");
                        string kode = Console.ReadLine();

                        Console.Write("Kursnavn: ");
                        string navn = Console.ReadLine();

                        Console.Write("Studiepoeng: ");
                        int studiepoeng = int.Parse(Console.ReadLine());

                        Console.Write("Maks antall plasser: ");
                        int maks = int.Parse(Console.ReadLine());

                        Console.WriteLine(universitet.OpprettKurs(kode, navn, studiepoeng, maks, lærer.AnsattID));
                    }
                    catch
                    {
                        Console.WriteLine("Feil input. Studiepoeng og maksplasser må være tall.");
                    }
                    break;

                case "2":
                    Console.Write("Søk etter kurskode eller navn: ");
                    universitet.SøkKurs(Console.ReadLine());
                    break;

                case "3":
                    Console.Write("Søk etter tittel eller forfatter: ");
                    universitet.SøkBok(Console.ReadLine());
                    break;

                case "4":
                    Console.Write("Bok-ID: ");
                    Console.WriteLine(universitet.LånBok(Console.ReadLine(), lærer.AnsattID));
                    break;

                case "5":
                    Console.Write("Bok-ID: ");
                    Console.WriteLine(universitet.ReturnerBok(Console.ReadLine(), lærer.AnsattID));
                    break;

                case "6":
                    Console.Write("Kurskode: ");
                    string kurskode = Console.ReadLine();

                    Console.Write("StudentID: ");
                    string studentId = Console.ReadLine();

                    Console.Write("Karakter: ");
                    string karakter = Console.ReadLine();

                    Console.WriteLine(universitet.SettKarakter(kurskode, studentId, karakter, lærer.AnsattID));
                    break;

                case "7":
                    Console.Write("Kurskode: ");
                    string kursKodePensum = Console.ReadLine();

                    Console.Write("Bok-ID: ");
                    string bokId = Console.ReadLine();

                    Console.WriteLine(universitet.RegistrerPensum(kursKodePensum, bokId, lærer.AnsattID));
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Ugyldig valg.");
                    break;
            }
        }
    }

    static void BibliotekMeny(Bibliotekansatt ansatt)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("---- BIBLIOTEK MENY ----");
            Console.WriteLine("[1] Registrer bok");
            Console.WriteLine("[2] Vis aktive lån");
            Console.WriteLine("[3] Vis lånehistorikk");
            Console.WriteLine("[0] Logg ut");
            Console.Write("Valg: ");
            string valg = Console.ReadLine();

            switch (valg)
            {
                case "1":
                    try
                    {
                        Console.Write("Bok-ID: ");
                        string id = Console.ReadLine();

                        Console.Write("Tittel: ");
                        string tittel = Console.ReadLine();

                        Console.Write("Forfatter: ");
                        string forfatter = Console.ReadLine();

                        Console.Write("År: ");
                        int år = int.Parse(Console.ReadLine());

                        Console.Write("Antall eksemplarer: ");
                        int antall = int.Parse(Console.ReadLine());

                        Console.WriteLine(universitet.RegistrerBok(id, tittel, forfatter, år, antall));
                    }
                    catch
                    {
                        Console.WriteLine("Feil input. År og antall må være tall.");
                    }
                    break;

                case "2":
                    universitet.VisAktiveLån();
                    break;

                case "3":
                    universitet.VisLånehistorikk();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Ugyldig valg.");
                    break;
            }
        }
    }
}
