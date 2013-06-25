using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ErrorDialogSample {
    public interface IMediator {
        void ShowMessage(ErrorMessage errorMessage);
    }
}
