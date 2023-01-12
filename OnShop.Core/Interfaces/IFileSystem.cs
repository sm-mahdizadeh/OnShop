using System.Threading.Tasks;

namespace OnShop.ApplicationServices.Interfaces
{
    public interface IFileSystem
    {
        Task<bool> SavePicture(string pictureName, string pictureBase64);
    }
}