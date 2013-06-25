using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace XmlDocumentPractice{

    /// <summary>
    /// Beispiel zum XML Parsen mit XmlDocument (DOM) und XDocument (LINQ to XML API).
    /// Funktionalitaet der beiden APIs ist sehr aehnlich. 
    /// Vorteile DOM: XPath oder ByName Navigation
    /// Vorteile LINQ to XML: kuerzer zu schreiben, performanter (weniger Overhead), einfacher/intuitiver, XML-Baumstruktur erkennbar (beim Schreiben von XML-Files)
    /// </summary>
    class Program{
        static string filename = "Fragen1.xml";

        /// <summary>
        /// XmlDocument Parser Uebung.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args){
            if (File.Exists(filename)){
                ReadXmlDocument();    // mit XmlDocument lesen (DOM)
                ReadXDocument();    // mit XDocument (LINQ to XML API) lesen
            }
            CreateXmlDocument();   // Schreiben mit XmlDocument
            CreateXDocument();  // Schreiben mit XDocument

            Console.Read();
        }

        /// <summary>
        /// Parsen eines XML Dokuments mit XmlDocument (DOM).
        /// => Wie bei Java mit der DocumentBuilderFactory.
        /// </summary>
        static void ReadXmlDocument(){
            string output;
            Console.WriteLine("------------------------- XmlDocument Parsing -------------------------");

            // XML Dokument laden
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            // Elemente selektieren (mit einem bestimmten Attributwer)
            // 1. Ueber den Tagnamen der Elemente
            XmlNodeList questionNodes = doc.GetElementsByTagName("frage");
            // 2. Mit XPath (nur Ele eines bestimmten Gebiets)
            questionNodes = doc.SelectNodes("//frage[@gebiet='astrophysik']");

            // <frage>-Elemente verarbeiten (die ersten drei)
            for (int i = 0; i < 3; i++ ){
                XmlNode n = questionNodes[i]; // aktueller Node

                // Zugriff auf Attribute
                XmlAttribute topic = n.Attributes["gebiet"];
                output = topic.Value;
                Console.WriteLine(output);

                // Zugriff auf das <fragetext>-Tag
                XmlNode qText = n.FirstChild;   // erstes Child von <frage> ist immer der <fragetext>
                // Elementname ausgeben
                Console.Write(qText.Name + ": ");
                // Textinhalt des Elements ausgeben
                output = qText.InnerText;
                Console.WriteLine(output + "\r\n");
            }

            //// Zugriff auf die 2. Antwortmoeglichkeit der Antwortmoeglichkeiten der 3. Frage)
            //XmlNode node = doc.DocumentElement.ChildNodes[2].ChildNodes[1].ChildNodes[1];
            //Console.WriteLine(node.InnerText);

            Console.WriteLine("----------------------- End Of XmlDocument Parsing -----------------------\r\n");
            #region Info
            /*
             * Attribute-Properties:
             * Name: Bezeichnung des Attributs
             * Value: Wert des Attributs
             * 
             * Element-Properties:
             * Name: Bezeichnung des Elements
             * InnerText: Textinhalt eines Elements
             * Attributes["name"]: Attribut mit dem angegebenen Namen (null wenn es das Attribut nicht gibt)
             * InnerXml: Enthaltener Text und XML Tags eines Elements
             * OuterXml: Wie InnerXml inkl. XML Tags des Elements selbst
             * FirstChild/LastChild
             * NodeType
             * NextSibling/PreviousSibling
             * ParentNode
             * Item["name"]: Erstes untergeordnete Ele mit dem angegebenen Namen
             * 
             * Sonstiges:
             * SelectNodes("XPath Expression")
             * ChildNodes: alle untergeordneten Knoten
             * ChildNodes[i]: i-tes Child
             * DocumentElement: Wurzelelement
             * GetElementById/ByTagName-Methode
             * 
             * XPath:
             * //   Element irgendwo unterhalb des vorangehenden Knotens
             * /    Child-Element des vorangehenden Knotens
             * .    Knoten selbst
             * @    Attribut
             * 
             * */
            #endregion Info
        }

        /// <summary>
        /// XML-Datei mit XDocument (LINQ to XML API) lesen.
        /// </summary>
        static void ReadXDocument(){
            Console.WriteLine("--------------------------- XDocument Parsing ----------------------------");
            string output = "";

            //XDocument doc = XDocument.Load(filename); // XML Dokument mit Instanzmethode laden
            //XElement root = doc.Root;
            
            // vom Wurzelelement ausgehend..
            XElement root = XElement.Load(filename);    // XML-Dokument mit statischer Methode laden

            // ..alle <frage>-Elemente selektieren
            List<XElement> questions = root.Elements("frage").ToList<XElement>();    // note: Descendants statt Elements => alle Children

            // Frage-Elemente mit einem bestimmten Gebiet selektiereb
            List<XElement> topicQuestions = (from q in questions
                         where q.Attribute("gebiet").Value.Equals("astrophysik")    // alternativ mit System.Xml.XPath: where (bool)q.XPathEvaluate("@gebiet='astrophysik'")
                         select q).ToList<XElement>();

            // <frage>-Elemente verarbeiten (die ersten drei ausgeben)
            for (int i = 0; i < 3; i++ ){
                XElement q = topicQuestions[i];  // aktuelles Element

                // Bezeichnung eines Attrbitus
                output = q.Attribute("gebiet").Name + ": ";

                // Wert eines Attributs
                output += q.Attribute("gebiet").Value;
                Console.WriteLine(output);

                // Textinhalt eines Elements
                output = q.Element("fragetext").Value;
                Console.WriteLine(output + "\r\n");
            }

            // NOTE: Nach _einem_ Element suchen: Mit der Methode .SingleOrDefault() bekommt man das _eine_ Ergebnis eines LINQ-Queries (null wenn es kein Match gibt)
            Console.WriteLine("----------------------- End Of XDocument Parsing -------------------------\r\n");
        }

        /// <summary>
        /// XML-Datei mit XmlDocument erstellen.
        /// </summary>
        static void CreateXmlDocument()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration declaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "no");  // XML Deklaration
            xmlDoc.AppendChild(declaration);

            XmlNode rootNode = xmlDoc.CreateElement("books");   // Wurzelelement
            xmlDoc.AppendChild(rootNode);   // Wurzelelement zum Dokument hinzufuegen

            // <book> Element
            XmlNode userNode = xmlDoc.CreateElement("book");
            // mit einem Attribut
            XmlAttribute attribute = xmlDoc.CreateAttribute("release");
            attribute.Value = "2013/02/15";
            userNode.Attributes.Append(attribute);  // Attribut zum <book> Ele hinzufuegen
            // und Textinhalt
            userNode.InnerText = "Elizabeth Corley - Pretty Little Things";
            rootNode.AppendChild(userNode); // Textinhalt zum <book> Ele hinzufuegen

            // und noch ein <book>
            userNode = xmlDoc.CreateElement("book");
            attribute = xmlDoc.CreateAttribute("release");
            attribute.Value = "2011/11/11";
            userNode.Attributes.Append(attribute);
            userNode.InnerText = "Stephen Hawking - A Brief History Of Time";
            rootNode.AppendChild(userNode);

            // XML Dokument speichern
            xmlDoc.Save("xmlDocument_books.xml");   // in ../projectname/bin/debug/
        }

        /// <summary>
        /// XML-Datei mit XDocument erzeugen.
        /// </summary>
        static void CreateXDocument(){
            // Struktur des XML Dokuments ist aus dem Code ersichtlich
            XDocument doc = new XDocument(
                new XDeclaration("1.0", "UTF-8", "no"),
                new XElement("books",
                    new XElement("book", "Elizabeth Corley - Pretty Little Things",     // <book> mit InnerText..
                        new XAttribute("release", "2013/02/15")                         // ..und einem Attribut
                    ),
                    new XElement("book", "Stephen Hawking - A Brief History Of Time",   // noch ein <book>
                        new XAttribute("release", "2011/11/11")                         
                    )
                )
            );

            // Dokument speichern
            doc.Save("xDocument_books.xml");    // in ../projectname/bin/debug/
        }    
    }
}
