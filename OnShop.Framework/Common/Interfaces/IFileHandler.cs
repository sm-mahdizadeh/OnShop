using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OnShop.Framework.Common.File;

namespace OnShop.Framework.Common.Interfaces
{
    public interface IFileHandler
    {
        UploadDto UploadFile(IFormFile file, string pathFolder);
        Task<UploadDto> UploadFileAsync(IFormFile file, string pathFolder);
        Task<List<UploadDto>> UploadFile(List<IFormFile> files, string pathFolder);
    }
}