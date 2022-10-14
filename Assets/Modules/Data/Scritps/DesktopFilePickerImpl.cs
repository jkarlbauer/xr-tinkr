
namespace Xrtinkr.Data
{
    public class DesktopFilePickerImpl : IFilePickerImpl
    {
        string mainDirectoryPath = "./";

        public string PickFile(string optionalFileName)
        {
            FilePicker filePicker = new FilePicker(mainDirectoryPath);

            if (optionalFileName == null || optionalFileName == "")
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

