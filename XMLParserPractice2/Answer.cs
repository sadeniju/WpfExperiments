using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLParserPractice2
{
    class Answer
    {
        string text;
        public string Text
        {
            get { return text; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    text = value;
            }
        }

        public bool Correct
        {
            get;
            set;
        }

        /// <summary>
        /// Konstruktor. Die Antwort wird default auf falsch gesetzt.
        /// </summary>
        /// <param name="text">Textinhalt der Antwort</param>
        public Answer(string text)
        {
            Text = text;
            Correct = false;
        }

        /// <summary>
        /// Konstruktor.
        /// </summary>
        /// <param name="text"></param>
        public Answer(string text, bool correct)
        {
            Text = text;
            Correct = correct;
        }

        public Answer() { }
    }
}
