using Ecom.Core.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace Ecom.Infrastructure.Repositories.Service
{
    public class IMageManagementService : IImageManagementService
    {
        private readonly IFileProvider _fileProvider;

        public IMageManagementService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }
        public async Task<List<string>> AddImgAsync(IFormFileCollection form, string foldername)
        {
            var SaveImagesSrc = new List<string>();
            var ImgDirectory = Path.Combine("wwwroot","Images",foldername);
            if (Directory.Exists(ImgDirectory) is not true) 
            {
                Directory.CreateDirectory(ImgDirectory);
            }
            foreach(var item in form) 
            {
                if(item.Length > 0)
                {
                    string? ImgName = item.FileName;
                    var ImgSrc = $"/Images/{foldername}/{ImgName}";
                    var root = Path.Combine(ImgDirectory,ImgName);
                    using (FileStream stream = new FileStream(root, FileMode.Create)) 
                    {
                        await item.CopyToAsync(stream);
                    }
                    SaveImagesSrc.Add(ImgSrc);
                }
            }
            return SaveImagesSrc;
        }

        public void DeleteImgAsync(string src)
        {
            var info = _fileProvider.GetFileInfo(src);
            var root = info.PhysicalPath;
            File.Delete(root);
        }
    }
}
