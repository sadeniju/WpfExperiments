using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XMLParserPractice2
{
    class Program
    {
        static string filename = "Fragen1.xml";
        static string topic = "astrophysik";
        static List<Question> questions;

        static void Main(string[] args)
        {
            questions = new List<Question>();
            ParseFile();
            PlayQuiz();
            Console.Read();
        }

        /// <summary>
        /// Parse a xml file containing questions for the quiz.
        /// Select questions from a specific topic and add them to the questions list.
        /// </summary>
        static void ParseFile(){
            XmlReader reader = XmlReader.Create(filename);
            string qText;
            List<Answer> answers;
            bool correct;

            while (reader.Read())
            {
                qText = "";
                answers = new List<Answer>();

                // <frage> Elemente verarbeiten
                if (reader.NodeType == XmlNodeType.Element && reader.Name.Equals("frage") && reader.GetAttribute("gebiet") != null){

                    // nur Fragen mit dem gewuenschten Gebiet betrachten
                    if (reader.GetAttribute("gebiet").Equals(topic)){

                        // alle Infos aus der Frage zwischenspeichern (bis zum Endtag der Frage)
                        // INFO: Der Name des Enddtags /frage ist nicht "/frage" sondern "frage" => zusaetlich auf Knotentyp EndElement pruefen
                        while (!(reader.NodeType == XmlNodeType.EndElement && reader.Name.Equals("frage"))){
                            reader.Read();  // den naechsten Knoten lesen

                            switch (reader.Name){
                                case "antwort":
                                    if (reader.GetAttribute("korrekt").Equals("ja"))
                                        correct = true;
                                    else
                                        correct = false;
                                    //Console.WriteLine(correct + " answer");
                                    answers.Add(new Answer(reader.ReadElementContentAsString(), correct));
                                    break;
                                case "fragetext":
                                    qText = reader.ReadElementContentAsString();
                                    //Console.Write("frage: "+qText + "\r\n");
                                    break;
                                default:
                                    break;
                            }
                        }
                        // neue Frage zur Liste hinzufuegen
                        //Console.WriteLine("----------------------------- end of question -----------------------------");
                        questions.Add(new Question(qText, answers, topic));
                    }
                }
            } 
        }

        /// <summary>
        /// Play the quiz.
        /// There are five turns during which questions from a specific topic will be asked.
        /// After each question has been answered, the player will be notified if his choice was correct.
        /// At the end of the game the number of correctly given answers will be displayed.
        /// </summary>
        static void PlayQuiz(){
            int answer; // Nummer der gegebenen Antwort
            int correctIndex = 0;   // Nummer der richtigen Antwort
            Question currentQ;
            int aIndex = 1;
            int correctAnswers = 0;
            Random random = new Random();

            XmlWriterSettings settings = new XmlWriterSettings();   // Settings fuer den Writer
            settings.Indent = true; // Zeilen sollen eingerueckt werden
            XmlWriter writer = XmlWriter.Create("quiz.xml", settings);

            Console.WriteLine("Willkommen zum Quiz.\r\nBeantworte fuenf {0}-Fragen.\r\n", topic);

            // Einzelne Runden des Quiz
            for (int i = 0; i < 5; i++){
                aIndex = 1;
                currentQ = questions[random.Next() % questions.Count()];   // zufaellige Frage auswaehlen

                // Frage stellen
                Console.WriteLine("{0}. Frage:\r\n", i+1);
                Console.WriteLine(currentQ.Text);

                // Antworten ausgeben
                foreach (Answer a in currentQ.Answers)
                {
                    Console.WriteLine((aIndex)+") "+a.Text);
                    if (a.Correct)
                        correctIndex = aIndex;
                    aIndex += 1;
                    
                }

                // Antwort einlesen
                answer = Convert.ToInt32(Console.ReadLine());

                // Aufloesung
                if (answer == correctIndex)
                {
                    Console.WriteLine("Richtig!");
                    correctAnswers += 1;
                }
                else
                {
                    Console.WriteLine("Falsch.");
                    Console.WriteLine("Richtig gewesen waere Antwort {0} ({1}).",correctIndex, currentQ.Answers[correctIndex - 1].Text);
                }
                Console.WriteLine();
                questions.Remove(currentQ); // gestellte Frage aus der Liste entfernen
            }
            Console.WriteLine("Du hast "+correctAnswers+" Fragen richtig beantwortet.");

            // @todo Gestellte Frage und gegebene Antwort in einem neuen XML-File speichern
        }
    }
}
