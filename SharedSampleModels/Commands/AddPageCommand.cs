using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharedClasses.SampleViewModels;

namespace SharedClasses.Commands {
    public class AddPageCommand: Command {
        private BookViewModel bookViewModel;

        public AddPageCommand(BookViewModel bookViewModel) {
            this.bookViewModel = bookViewModel;
        }

        public override bool CanExecute(object parameter) {
            return true;
        }

        public override void Execute(object parameter) {
            bookViewModel.PageViewModels.Add(new PageViewModel());
        }
    }
}
