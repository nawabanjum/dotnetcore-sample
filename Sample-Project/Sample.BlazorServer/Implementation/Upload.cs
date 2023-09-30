using BlazorInputFile;
using Sample.BlazorServer.Service;

namespace Sample.BlazorServer.Implementation
{
    public class Upload : IUpload
    {
        private readonly IWebHostEnvironment _env;
        public Upload(IWebHostEnvironment webHost)
        {
            _env= webHost;
        }
        public void RemoveFile(string fileName)
        {
            var path = $"{_env.WebRootPath}\\uploads\\{fileName}";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public void  UploadFile(IFileListEntry file, MemoryStream str, string fileName)
        {
            var path = $"{_env.WebRootPath}\\uploads\\{fileName}";
            using(FileStream fs= new FileStream(path,FileMode.Create))
            {
                str.WriteTo(fs);
            }
        }
    }
}
