using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using SharedClasses.SampleModels;

namespace XMLParserPractice
{
    class Program
    {
        static void Main(string[] args)
        {
            //string output = "";
            double result = 0;
            string filename = "UE_10_messwerte.xml";
            string daytime = "abend";

//            string input = @"<bookstore>
//                                <book genre='autobiography' publicationdate='1981-03-22' ISBN='1-861003-11-0'>
//                                    <title>The Autobiography of Benjamin Franklin</title>
//                                    <author>
//                                        <first-name>Benjamin</first-name>
//                                        <last-name>Franklin</last-name>
//                                    </author>
//                                    <price>8.99</price>
//                                </book>
//                            </bookstore>";  // @ symbol: escape sequences (e.g. \n) are not processed

            // XML Reader
            //output = DoStuff(input);   // MSDN-Beispiel
            result = CalcArithmeticMean(filename, daytime); // Lesen eines XML-Files ueben
            WriteXmlFile("xmlWriterTest.xml"); // Schreiben eines XML-Files ueben
            SerializeBook(new Book("Elizabeth Corley", "Pretty Little Things"));    // Object serialization

            Console.WriteLine("Durchschnittlicher Messwert {0}s: {1}", daytime, result);
            Console.Read();
        }

        /// <summary>
        /// XML Writer Beispiel: Erzeugen einer XML-Datei.
        /// </summary>
        public static void WriteXmlFile(string filename){
            XmlWriterSettings settings = new XmlWriterSettings();   // Settings fuer den Writer
            settings.Indent = true; // Zeilen sollen eingerueckt werden

            XmlWriter writer = XmlWriter.Create(filename, settings);    // Dateiname angeben

            writer.WriteStartDocument();    // fuegt die XML-Deklaration ein

            // <books> Wurzelelement
            writer.WriteStartElement("books");

            #region first book
            // <book releaseDate="2012/09/30">
            writer.WriteStartElement("book");
            writer.WriteAttributeString("releaseDate", "2012/09/30");

            // <autor>Elizabeth Corley</autor>
            writer.WriteStartElement("author");
            writer.WriteString("Elizabeth Corley");
            writer.WriteEndElement();
            
            // <title>Pretty little things</title>
            writer.WriteStartElement("title");
            writer.WriteString("Pretty Little Things");
            writer.WriteEndElement();

            // </book>
            writer.WriteEndElement();
            #endregion first book

            #region second book
            // <book releaseDate="2012/09/30">
            writer.WriteStartElement("book");
            writer.WriteAttributeString("releaseDate", "2013/02/15");

            // <autor>Elizabeth Corley</autor>
            writer.WriteStartElement("author");
            writer.WriteString("Stephen Hawking");
            writer.WriteEndElement();

            // <title>Pretty little things</title>
            writer.WriteStartElement("title");
            writer.WriteString("A Brief History Of Time");
            #endregion secondbook

            // </title></book></books>
            writer.WriteEndDocument();  // schliesst automatisch alle noch offenen Tags 
            writer.Close(); // Dokument wird gespeichert (im directory der .exe des Programms => projectname/bin/debug/)
        }

        /// <summary>
        /// XML Reader Beispiel zum Lesen eines XML-Files.
        /// Berechnung des arithmetischen Mittels der Messwerte. 
        /// Nur Messwerte einer bestimmten Tageszeit (mittag|morgen|abend) werden beruecksichtigen (Tageszeit steht im Attribut "gueltig").
        /// Zugriff auf Elementname, Elementinhalt, Attributname und Attributinhalt.
        /// </summary>
        /// <param name="filename">Dateiname</param>
        /// <param name="daytime">Tageszeit, zu welcher die relevanten Werte gemessen wurden</param>
        /// <returns></returns>
        public static double CalcArithmeticMean(string filename, string daytime)
        {
            List<int> values = new List<int>();

            XmlReader reader = XmlReader.Create(filename);
            // Nodes durchgehen und alle Elementknoten mit dem gueltig-Attribut, das den Wert daytime hat verarbeiten
            while (reader.Read())
            {
                // Nur Knoten vom Typ Element mit dem Namen "wert" und dem Attribut "gueltig" betrachten
                if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("wert") && reader.GetAttribute("gueltig") != null)
                {
                    // Wenn die Tageszeit mit der gegebenen uebereinstimmt wird der Messwert zur Liste hinzugefuegt
                    if (reader.GetAttribute("gueltig").Equals(daytime))
                    {
                        // Messwert zur richtigen Tageszeit gefunden
                        values.Add(reader.ReadElementContentAsInt());
                    }
                }
            }

            return values.Average();
        }

        /// <summary>
        /// XML Reader MSDN-Beispiel.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string DoStuff(string input)
        {
            // StringBuilder:
            // Zeichenfolge aendern ohne neues Objekt anzulesen (sinnvoll bei wiederholter Aenderung)
            // (da string immutable ist wird bei Aenderung immer ein neues Objekt im Speicher angelegt)
            StringBuilder output = new StringBuilder();

            // XML Reader
            using (XmlReader reader = XmlReader.Create(new StringReader(input)))
            {
                XmlWriterSettings ws = new XmlWriterSettings();
                ws.Indent = true;

                // XML Writer
                using (XmlWriter writer = XmlWriter.Create(output, ws))
                {
                    // Parsing the file, reading node after node
                    while (reader.Read())
                    {
                        // examining the current node and deciding how to proceed
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                writer.WriteStartElement(reader.Name);
                                break;
                            case XmlNodeType.Text:
                                writer.WriteString(reader.Value);
                                break;
                            case XmlNodeType.XmlDeclaration:    // wendet die Anweisung des naechsten cases an
                            case XmlNodeType.ProcessingInstruction:
                                writer.WriteProcessingInstruction(reader.Name, reader.Value);
                                break;
                            case XmlNodeType.Comment:
                                writer.WriteComment(reader.Value);
                                break;
                            case XmlNodeType.EndElement:
                                writer.WriteFullEndElement();
                                break;
                        }
                    }
                }
            }
            return output.ToString();
        }

        /// <summary>
        /// XML Serialization of a book.
        /// </summary>
        /// <param name="book"></param>
        public static void SerializeBook(Book book)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Book));
            TextWriter textWriter = new StreamWriter("serializedBook.xml");
            serializer.Serialize(textWriter, book);
            textWriter.Close();
        }
    }
}
