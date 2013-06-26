using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace SharedClasses.Commands {
    public abstract class Command : ICommand{
        public event EventHandler CanExecuteChanged;

        abstract public bool CanExecute(Object parameter);
        abstract public void Execute(Object parameter);

        public void RaiseCanExecuteChanged(){
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}
