using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ErrorDialogSample {
    public class LoadAllFilesCommand : UndoableCommand {
        private ImageGalleryViewModel imageGalleryViewModel;

        public LoadAllFilesCommand(ImageGalleryViewModel imageGalleryViewModel) {
            this.imageGalleryViewModel = imageGalleryViewModel;
        }

        public override bool CanExecute(object parameter) {
            return true;
        }

        public override void Execute(object parameter) {
            imageGalleryViewModel.LoadImages(ReferenceHandling.KeepNonExisting);
        }
    }
}
