using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedClasses.SampleViewModels;

namespace SharedClasses.Commands {
    public class UpdateBookTitleCommand: Command {
        private BookViewModel bookViewModel;

        public UpdateBookTitleCommand(BookViewModel bookViewModel) {
            this.bookViewModel = bookViewModel;
        }

        public override bool CanExecute(object parameter) {
            return true;
        }

        public override void Execute(object parameter) {
            throw new NotImplementedException();
        }
    }
}
