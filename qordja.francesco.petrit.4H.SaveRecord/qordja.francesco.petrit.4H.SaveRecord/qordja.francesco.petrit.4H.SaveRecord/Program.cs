using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace qordja.francesco.petrit._4H.SaveRecord
{
    class Program
    {
        static void Main(string[] args)
        {
            const int verifica = 8;
            Console.WriteLine("SaveRecord");
            
            
            // Leggo un file CSV con i comuni e lo trasformo in una list<Comune>
            Comuni c = new Comuni( "Comuni.csv" );
            Console.WriteLine($"Ho letto {c.Count} righe...");

            Console.WriteLine($"Ecco la riga {verifica}: {c[verifica]}");

            
            // Scrivo la List<Comune> in un file binario
            c.Save();
            
            
            // Rileggere il file binario in una List<Comune>
            c.Load();
            Console.WriteLine($"Ho letto {c.Count} righe dal file binario...");
        
            Console.WriteLine($"Ecco la riga {verifica}: {c[verifica]}");
        }
    }
}
