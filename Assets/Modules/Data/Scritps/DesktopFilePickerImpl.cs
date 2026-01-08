
using System.Threading.Tasks;

namespace Xrtinkr.Data
{
    public class DesktopFilePickerImpl : IFilePickerImpl
    {
        string mainDirectoryPath = "./";
        string glbFileType = NativeFilePicker.ConvertExtensionToFileType( ".glb" );

        public async Task<string> PickFileAsync(string optionalFileName)
        {
            TaskCompletionSource<string> taskObject = new TaskCompletionSource<string>();

            NativeFilePicker.PickFile((path) => {
                if (string.IsNullOrEmpty(path))
                {
                    taskObject.SetResult(null);
                }
                else
                {
                    taskObject.SetResult(path);
                }

            }, new string[] { glbFileType });

            return await taskObject.Task;
        }    
    }
}

