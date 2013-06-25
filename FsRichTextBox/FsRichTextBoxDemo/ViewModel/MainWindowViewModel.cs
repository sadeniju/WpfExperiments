using System.Windows.Input;
using System;
using System.Text;
using HTMLConverter;

namespace FsRichTextBoxDemo
{
    public class MainWindowViewModel : ViewModelBase
    {
        #region Fields

        // Property variables
        private string p_DocumentXaml;

        #endregion

        #region Constructor

        public MainWindowViewModel()
        {
            this.LoadDocument = new LoadDocumentCommand(this);  // initializing Command
        }

        #endregion

        #region Command Properties

        /// <summary>
        /// Command to simulate loading a document from the app back-end
        /// </summary>
        public ICommand LoadDocument { get; set; }

        #endregion

        #region Data Properties

        /// <summary>
        /// The text from the FsRichTextBox, as a XAML markup string.
        /// </summary>
        public string DocumentXaml
        {
            get {
                return p_DocumentXaml; 
            }

            set
            {
                p_DocumentXaml = value;
                if (p_DocumentXaml != null) {
                    DocumentHtml = HtmlFromXamlConverter.ConvertXamlToHtml(p_DocumentXaml);
                }
                base.RaisePropertyChangedEvent("DocumentXaml");
            }
        }
        #endregion

        private string documentHtml = "hullo";
        /// <summary>
        /// @EDITED html representation of the xaml document
        /// </summary>
        public string DocumentHtml {
            get {
                Console.WriteLine(documentHtml);
                return documentHtml;
            }
            set {
                documentHtml = value;
                base.RaisePropertyChangedEvent("DocumentHtml");
            }

        }
    }
}