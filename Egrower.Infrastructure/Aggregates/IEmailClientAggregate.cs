using Egrower.Infrastructure.DTO;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Egrower.Infrastructure.Aggregates
{
    public interface IEmailClientAggregate
    {
        Task AddEmailsToDBAsync(IEnumerable<MimeMessage> emailsList);
        Task<EmailMessageDTO> GetEmailByEmailID(int emailId);
        Task<EmailMessageDTO> GetEmailsByUserID(int userId);
    }
}
