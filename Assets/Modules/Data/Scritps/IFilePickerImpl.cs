using System.Threading.Tasks;

namespace Xrtinkr.Data
{
    public interface IFilePickerImpl
    {
        public Task<string> PickFileAsync(string optionalFileName);

    }
}

