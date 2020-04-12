using System.IO;
using System.Threading.Tasks;

namespace Covid19Data.Domain.Services
{
    public interface ISendEmailService
    {
        Task SendEmail(byte[] byteFile);
    }
}