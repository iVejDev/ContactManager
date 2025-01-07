/* 
                                    Vad gör IMenuService?
Syfte:
Gränssnittet definierar en standard för hur menyn ska fungera i konsolapplikationen.
Det säkerställer att alla klasser som implementerar detta gränssnitt har en metod för att köra menyn.

Ansvar:
Endast en metod, Run(), som startar och kör hela menylogiken. 

Varför använda IMenuService?
Gör det möjligt att byta ut eller ändra menyimplementationen utan att påverka resten av applikationen.
Säkerställer att menylogiken följer samma struktur oavsett implementation.

 */


namespace Presentation.Console.Interfaces
{
    public interface IMenuService
    {
        void Run();
    }
}