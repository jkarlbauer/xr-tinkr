
using System.IO;
using UnityEngine;

namespace Xrtinkr.Data
{
    public class QuestFilePickerImpl : IFilePicker
    {
        private string _mainDirectoryPath;

        public QuestFilePickerImpl()
        {
            _mainDirectoryPath = Application.persistentDataPath;
        }

        public string PickFile(string optionalFileName)
        {
            if(optionalFileName == null || optionalFileName == "")
            {
                return PickFileFromOrder();
            }
            else
            {
                return PickFileFromName(optionalFileName);
            }
        }
        private string PickFileFromName(string filename)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_mainDirectoryPath);

            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                if (fileInfo.Name.Equals(filename))
                {
                    return fileInfo.FullName;
                }
            }

            throw new FileNotFoundException();
        }

        private string PickFileFromOrder()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_mainDirectoryPath);
            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                if (fileInfo.Extension == "glb" || fileInfo.Extension == ".glb")
                {
                    return fileInfo.FullName;
                }
            }

            throw new FileNotFoundException();
        }
    }
}

