using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedClasses.SampleViewModels;

namespace SharedClasses.Commands {
    public class RemovePageCommand: Command {
        private BookViewModel bookViewModel;

        public RemovePageCommand(BookViewModel bookViewModel) {
            this.bookViewModel = bookViewModel;
        }

        public override bool CanExecute(object parameter) {
            if (bookViewModel.SelectedPage != null && bookViewModel.PageViewModels.Count > 0) {
                return true;
            }
            return false;
        }

        public override void Execute(object parameter) {
            bookViewModel.PageViewModels.Remove(bookViewModel.SelectedPage);
        }
    }
}
