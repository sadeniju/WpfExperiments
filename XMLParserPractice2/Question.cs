using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XMLParserPractice2
{
    class Question
    {
        string text;
        public string Text{
            set{
                if (!string.IsNullOrEmpty(value))
                    text = value;
            }
            get { return text; }
        }

        List<Answer> answers;
        public List<Answer> Answers{
            get { return answers; }
            set{
                if (value != null)
                    answers = value;
            }
        }

        string topic;
        public string Topic
        {
            set{
                if (!string.IsNullOrEmpty(value))
                    topic = value;
            }
            get { return topic; }
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text"></param>
        public Question(string text) : 
            this(text, new List<Answer>()) {}

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text"></param>
        public Question(string text, List<Answer> answers) : 
            this(text, answers, "") {}

        public Question(string text, List<Answer> answers, string topic){
            Text = text;
            Answers = answers;
            Topic = topic;
        }

        /// <summary>
        /// Add an answer.
        /// </summary>
        /// <param name="answer"></param>
        public void addAnswer(Answer answer){
            if(! answers.Contains(answer))
                answers.Add(answer);
        }

        /// <summary>
        /// Get the number of answers.
        /// </summary>
        /// <returns></returns>
        public int NumberOfAnswers(){
            return answers.Count();
        }
    }
}
