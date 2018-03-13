using System.Collections.Generic;
using System.Threading.Tasks;
using MimeKit;

namespace Egrower.Infrastructure.Factories.Interfaces
{
    public interface IEmailFactory
    {
        //  Task GetEmailsPop3Async();
        //  Task<IEnumerable<EmailMessage>> GetEmailsIMapAsync();
         Task<IEnumerable<MimeMessage>> GetAllMimesIMapAsync();
         Task<IEnumerable<MimeMessage>> GetNewMimesIMapAsync();
         Task<IEnumerable<EmailMessage>> ConvertMailMessagesAsync(IEnumerable<MimeMessage> messages);
        //  Task SaveAttachments (IEnumerable<EmailMessage> messages);
         Task<IEnumerable<MimeMessage>>  DeleteMessageAsync ();

    }
}