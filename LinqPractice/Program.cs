using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinqPractice
{
    class Program
    {
        // Linq kann mit allem was IEnumerable implementiert benutzt werden.
        // Original wird nicht veraendert => kann auch mit immutable objects verwendet werden.
        // Die meisten LINQ-Operatoren nutzen deferred evaluation => faengt erst an zu arbeiten wenn ueber das Result iteriert wird (yields results) => macht keine unnoetige Arbeit
        static void Main(string[] args)
        {
            IEnumerable<int> numbers = Enumerable.Range(0, 20);

            #region Query Expression
            // ########################
            // ### QUERY EXPRESSION ###
            // ########################
            // from [range variable] in [source of information] where [filter expression] select/group [output variable/expression]
            var evens = from num in numbers
                        where num % 2 == 0
                        select num; 
            // evens ist vom Typ IEnumerable<int>

            Console.Write("Query Expression: ");
            foreach (var e in evens)
                Console.Write(e+" ");

            // for-Schleife mit Query (Quadratzahlen)
            var quads = from i in Enumerable.Range(0, 10) // die ersten fuenf Elemente der Zahlenliste testen
                        select i*i;
            Console.Write("\r\nQuadratzahlen: ");
            foreach (var q in quads)
                Console.Write(q + " ");

            #endregion Query Expression

            #region Query Expression with let-clause
            // ##################
            // ### LET-clause ### 
            // ##################
            // um Variablen zu definieren (Mehrfachverwendung)
            var unevens = from num in numbers
                          let numString = num.ToString()
                          where numString.Contains("1")
                          select numString;

            Console.Write("\r\nLet clause: ");
            foreach (var u in unevens)
                Console.Write(u + " ");
            #endregion Query Expression with let-clause

            #region Filtering
            // #################
            // ### FILTERING ###
            // #################
            // mit der Methode TypeOf<T>
            List<object> list = new List<object> {"Hello", 1, "world", 3, 5, "!" };
            var strings = list.OfType<string>();    // alle string-Objekte herausfiltern

            Console.Write("\r\nFiltering: ");
            foreach (var s in strings)
                Console.Write(s+" ");
            #endregion Filtering

            #region Ordering
            // ################
            // ### ORDERING ###
            // ################
            // orderby [attribute] [, attribute] [,...] [descending/ascending] => mehrere Sortierkriterien moeglich
            var reverseNumbers = from n in numbers
                                 orderby n descending
                                 select n;

            Console.Write("\r\nOrdering: ");
            foreach (var n in reverseNumbers)
                Console.Write(n+ " ");
            #endregion Ordering

            #region Concatenation
            // ##############
            // ### CONCAT ###
            // ##############
            // sequence1.Concat(sequence2)
            // im Gegensatz zur AddRange-Methode von Listen veraendert Concat das Originalobjekt nicht!
            var concatTest = numbers.Concat(reverseNumbers);

            Console.Write("\r\nConcat: ");
            foreach (var n in concatTest)
                Console.Write(n + " ");
            #endregion Concatenation

            #region Grouping
            // #############
            // ### GROUP ###
            // #############
            
            // s. Buch Seite 280
            // z.B. Events nach Tagen sortieren (Mo: work, study; Di: sleep;..)
            // pro Tag eine neue Liste mit Events (mehrdim Liste)

            #endregion Grouping

            Console.Read();
        }
    }
}
