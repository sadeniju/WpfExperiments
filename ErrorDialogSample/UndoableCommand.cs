using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace ErrorDialogSample {
    public abstract class UndoableCommand : ICommand {
        public abstract bool CanExecute(object parameter);

        public event EventHandler CanExecuteChanged;

        public abstract void Execute(object parameter);
    }

}
