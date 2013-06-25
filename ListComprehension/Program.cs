using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListComprehension
{
    class Program
    {
        /// <summary>
        /// List Comprehension in C#:
        /// 
        /// Member aus Sequenz: 
        /// from x in y
        /// 
        /// Filter: 
        /// where ...
        /// 
        /// Auswahl:
        /// select x
        /// 
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            IEnumerable<int> numbers = Enumerable.Range(0, 10);

            #region List Comprehension
            // in Python: [num for num in numbers if num % 2 == 0]
            var evens = from num in numbers where num % 2 == 0 select num;  // gerade Zahlen aus der Liste speichern
            #endregion List Comprehension

            // ueber die geraden Zahlen iterieren (mit Enumerator)
            using (IEnumerator<int> enumerator = evens.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Console.Write(enumerator.Current);
                }
            }

            Console.WriteLine();

            // einfacher iterieren
            foreach (var no in evens)
            {
                Console.Write(no);
            }
            Console.Read();
        }
    }
}
