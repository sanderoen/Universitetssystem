
using Xunit;
namespace Universitets_system_oppgave_2;


public class UniversitetTests
{
    [Fact]
    public void OpprettKurs_SammeKodeSkalIkkeVæreLov()
    {
        Universitet universitet = new Universitet();
        universitet.LagTestData();

        string svar = universitet.OpprettKurs("EX101", "Nytt Kurs", 10, 20, "F10");

        Assert.Equal("Kurs med samme kode eller navn finnes allerede.", svar);
    }

    [Fact]
    public void MeldStudentTilKurs_SammeStudentToGangerSkalIkkeVæreLov()
    {
        Universitet universitet = new Universitet();
        universitet.LagTestData();

        string første = universitet.MeldStudentTilKurs("1409", "EX101");
        string andre = universitet.MeldStudentTilKurs("1409", "EX101");

        Assert.Equal("Student meldt på kurs.", første);
        Assert.Equal("Studenten er allerede meldt på kurset.", andre);
    }

    [Fact]
    public void LånBok_NårIngenEksemplarerErLedige_SkalFeile()
    {
        Universitet universitet = new Universitet();
        universitet.LagTestData();

        string førsteLån = universitet.LånBok("B2", "1409");
        string andreLån = universitet.LånBok("B2", "5523");

        Assert.Equal("Bok lånt ut.", førsteLån);
        Assert.Equal("Ingen eksemplarer tilgjengelig.", andreLån);
    }

    [Fact]
    public void ReturnerBok_AktivtLån_SkalReturneres()
    {
        Universitet universitet = new Universitet();
        universitet.LagTestData();

        universitet.LånBok("B1", "1409");
        string svar = universitet.ReturnerBok("B1", "1409");

        Assert.Equal("Bok returnert.", svar);
        Assert.False(universitet.låneListe[0].Aktiv);
    }
}
