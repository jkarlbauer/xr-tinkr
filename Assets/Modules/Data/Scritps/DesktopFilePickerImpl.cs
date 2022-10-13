
using System.IO;

public class DesktopFilePickerImpl : IFilePicker
{
    string mainDirectoryPath = "./";

    public string PickFile(string filename)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(mainDirectoryPath);
;

        foreach(FileInfo fileInfo in directoryInfo.GetFiles())
        {
            if (fileInfo.Name.Equals(filename))
            {
               return fileInfo.FullName;
            }
        }

        return null;
    }

    public string PickFile()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(mainDirectoryPath);
        foreach(FileInfo fileInfo in directoryInfo.GetFiles())
        {
            if(fileInfo.Extension == "glb" || fileInfo.Extension == ".glb")
            {
                return fileInfo.FullName;
            }
        }

        return null;
    }
}
