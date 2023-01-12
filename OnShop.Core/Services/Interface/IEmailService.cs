using System.Threading.Tasks;

namespace OnShop.ApplicationServices.Services.Interface
{
    public interface IEmailService
    {
        Task Execute(string userEmail, string body, string subject);
    }
}