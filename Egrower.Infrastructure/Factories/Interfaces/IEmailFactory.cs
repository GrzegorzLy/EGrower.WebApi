using System.Collections.Generic;
using System.Threading.Tasks;
using MimeKit;

namespace Egrower.Infrastructure.Factories.Interfaces
{
    public interface IEmailFactory
    {
         Task DownloadEmailsPop3Async();
         Task<IEnumerable<EmailMessage>> DownloadEmailsIMapAsync();
         Task SaveAttachments (IEnumerable<EmailMessage> messages);

    }
}