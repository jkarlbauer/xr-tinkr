
using System.Threading.Tasks;
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

        public async Task<string> PickFileAsync(string optionalFileName)
        {
            TaskCompletionSource<string> taskObject = new TaskCompletionSource<string>();

            FilePicker filePicker = new FilePicker(_mainDirectoryPath);

            if(optionalFileName == null || optionalFileName == "")
            {
                taskObject.SetResult(filePicker.PickFileFromOrder());
            }
            else
            {
                taskObject.SetResult(filePicker.PickFileFromName(optionalFileName));
            }

            return await taskObject.Task;
        }

    }
}

