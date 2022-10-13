
using System.IO;
namespace Xrtinkr.Data
{
    public class FilePicker
    {
        private string _directorypath;
        public FilePicker(string directorypath)
        {
            _directorypath = directorypath;
        }
        public string PickFileFromName(string filename)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_directorypath);

            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                if (fileInfo.Name.Equals(filename))
                {
                    return fileInfo.FullName;
                }
            }

            throw new FileNotFoundException();
        }

        public string PickFileFromOrder()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(_directorypath);
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
