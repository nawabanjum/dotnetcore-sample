using BlazorInputFile;

namespace Sample.BlazorServer.Service
{
    public interface IUpload
    {
        public void UploadFile(IFileListEntry file, MemoryStream str, string fileName);
        public void RemoveFile(string fileName);
    }
}
