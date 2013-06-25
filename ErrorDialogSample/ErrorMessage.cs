using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ErrorDialogSample {
    public class ErrorMessage {
        #region Properties
        public string Title { get; private set; }
        public string Message { get; private set; }
        public Dictionary<string, UndoableCommand> Actions {get; private set;}
        #endregion

        public ErrorMessage(string title, string message, Dictionary<string, UndoableCommand> actions) {
            Title = title;
            Message = message;
            Actions = actions;
        }
    }
}
