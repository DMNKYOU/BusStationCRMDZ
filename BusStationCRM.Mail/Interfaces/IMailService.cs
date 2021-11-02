using System.Threading.Tasks;

namespace BusStationCRM.Mail.Interfaces
{
    public interface IMailService
    {
        Task SendHtmlEmail(string[] recipients, string title, string htmlBody);

        Task SendPlainTextEmail(string[] recipients, string title, string plainTextBody);
    }
}
