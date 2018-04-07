using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Egrower.Infrastructure.Factories.Interfaces
{
    public interface IEmailClientFactory
    {
        Task<IEnumerable<MimeMessage>> GetNewEmailsAsync();
        Task DeleteMessageAsync(DateTime date);
    }
}
