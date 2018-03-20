using EGrower.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EGrower.Core.Repositories
{
   public  interface IEmailMessageRepository
    {
        Task AddAsync(EmailMessage emailMessage);
        Task<EmailMessage> GetAsync(int id);
        Task UpdateAsync(EmailMessage emailMessage);
        Task DeleteAsync(EmailMessage emailMessage);
    }
}
