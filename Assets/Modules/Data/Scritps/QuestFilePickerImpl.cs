
using UnityEngine;

namespace Xrtinkr.Data
{
    public class QuestFilePickerImpl : IFilePickerImpl
    {
        private string _mainDirectoryPath;

        public QuestFilePickerImpl()
        {
            _mainDirectoryPath = Application.persistentDataPath;
        }

        public string PickFile(string optionalFileName)
        {
            FilePicker filePicker = new FilePicker(_mainDirectoryPath);

            if(optionalFileName == null || optionalFileName == "")
            {
                return filePicker.PickFileFromOrder();
            }
            else
            {
                return filePicker.PickFileFromName(optionalFileName);
            }
        }

    }
}

