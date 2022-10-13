
using System.IO;
using UnityEngine;

public class QuestFilePickerImpl : IFilePicker
{
    private string _mainDirectoryPath;

    public QuestFilePickerImpl()
    {
        _mainDirectoryPath = Application.persistentDataPath;
    }
    public string PickFile(string filename)
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(_mainDirectoryPath);

        foreach (FileInfo fileInfo in directoryInfo.GetFiles())
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
        DirectoryInfo directoryInfo = new DirectoryInfo(_mainDirectoryPath);
        foreach (FileInfo fileInfo in directoryInfo.GetFiles())
        {
            if (fileInfo.Extension == "glb" || fileInfo.Extension == ".glb")
            {
                return fileInfo.FullName;
            }
        }

        return null;
    }
}
