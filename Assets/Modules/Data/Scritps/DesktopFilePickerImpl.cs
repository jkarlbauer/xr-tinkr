
using System.IO;
namespace Xrtinkr.Data
{
    public class DesktopFilePickerImpl : IFilePicker
    {
        string mainDirectoryPath = "./";

        public string PickFile(string optionalFileName)
        {
            if (optionalFileName == null || optionalFileName == "")
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
            DirectoryInfo directoryInfo = new DirectoryInfo(mainDirectoryPath);

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
            DirectoryInfo directoryInfo = new DirectoryInfo(mainDirectoryPath);
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

