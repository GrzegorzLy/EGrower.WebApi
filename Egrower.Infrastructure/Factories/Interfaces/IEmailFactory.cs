using System.Collections.Generic;
using System.Threading.Tasks;
using MimeKit;

namespace Egrower.Infrastructure.Factories.Interfaces
{
    public interface IEmailFactory
    {
        //  Task GetEmailsPop3Async();
          Task<IEnumerable<MimeMessage>> GetEmailsIMapAsync();
         Task<IEnumerable<MimeMessage>> GetAllMimesIMapAsync();
         Task<IEnumerable<MimeMessage>> GetNewMimesIMapAsync();
         Task<IEnumerable<EmailMessageInfrastructure>> ConvertMailMessagesAsync(IEnumerable<MimeMessage> messages);
        //  Task SaveAttachments (IEnumerable<EmailMessage> messages);
         Task<IEnumerable<MimeMessage>>  DeleteMessageAsync ();

    }
}