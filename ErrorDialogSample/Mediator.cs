using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ErrorDialogSample {
    public class Mediator : IMediator{
        public void ShowMessage(ErrorMessage error){
            ErrorDialog errorDialog = new ErrorDialog(error);
            errorDialog.Show();
        }
    }
}
