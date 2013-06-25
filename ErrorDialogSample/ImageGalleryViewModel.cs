using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;
using System.Collections.Specialized;

namespace ErrorDialogSample {
    public enum ReferenceHandling{
        Undefined,
        RemoveNonExisting,
        KeepNonExisting
    }

    public class ImageGalleryViewModel : BaseViewModel{
        private Mediator mediator;

        #region Properties
        public LoadAllFilesCommand LoadAllFilesCommand { get; private set; }
        public LoadValidImagesCommand LoadValidImagesCommand { get; private set; }

        public ImageGallery Model { get; private set; }
        public ObservableCollection<string> ImageFileNames { get; private set; }

        public string GalleryDirectory {
            get { return Model.GalleryDirectory; }
            private set { Model.GalleryDirectory = value; }
        }
        #endregion

        #region Construction
        public ImageGalleryViewModel(Mediator mediator) {
            this.mediator = mediator;
            Model = new ImageGallery();
            LoadAllFilesCommand = new LoadAllFilesCommand(this);
            LoadValidImagesCommand = new LoadValidImagesCommand(this);
            LoadImages();
            //ImageFileNames.CollectionChanged += ImageFileNames_CollectionChanged;
        }
        #endregion

        public void LoadImages(ReferenceHandling refHandling = ReferenceHandling.Undefined) {
            ImageFileNames = new ObservableCollection<string>();
            List<string> filesToRemove = new List<string>();
            List<string> filesToAdd = new List<string>();

            if(Directory.Exists(GalleryDirectory)){
                IEnumerable<string> availableFiles = from path in Directory.GetFiles(GalleryDirectory) select Path.GetFileName(path);

                // Add files if they are located in the gallery folder and handle the others as specified
                foreach (string fileName in Model.ImageFileNames) {
                    if (availableFiles.Contains(fileName)) {
                        filesToAdd.Add(fileName);
                    }
                    else {
                        if (refHandling == ReferenceHandling.Undefined) {
                            // Let the user decide how to handle references to non-existing files
                            ErrorMessage fileNotFound = new ErrorMessage("File Not Found Error",
                                "File \"" + fileName + "\" not found. Do you want to proceed and remove all non-existent files from the gallery?",
                                new Dictionary<string, UndoableCommand> { {"Yes", LoadValidImagesCommand}, {"Keep references", LoadAllFilesCommand}, {"Cancel", null} });
                            mediator.ShowMessage(fileNotFound);
                            return;
                        }
                        else if (refHandling == ReferenceHandling.RemoveNonExisting) {
                            // Do not add the file to the view model's collection and remove its reference from the model
                            filesToRemove.Add(fileName); 
                        }
                        else { // if(refHandling == ReferenceHandling.KeepNonExisting)
                            // Add the file anyway and ignore that it does not exist
                            filesToAdd.Add(fileName); 
                        }
                    }
                }
                foreach (string file in filesToAdd) {
                    ImageFileNames.Add(file);
                    Console.WriteLine(file);
                }
                Model.ImageFileNames.RemoveAll(file => filesToRemove.Contains(file));
                RaisePropertyChanged("ImageFileNames");
            }
        }
    }
}
