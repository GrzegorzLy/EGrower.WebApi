using Egrower.Infrastructure.DAL;
using EGrower.Core.Domain;
using EGrower.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egrower.Infrastructure.Repositories
{
    public class EmailMessageRepository : IEmailMessageRepository
    {
        EGrowerContext _context;
        protected EmailMessageRepository()
        {
        }

        public EmailMessageRepository(EGrowerContext context)
        {
            _context = context;
        }

        public async Task<EmailMessage> GetAsync(int id)
          =>await Task.FromResult(_context.EmailMessages.Include(x => x.Atachments).SingleOrDefault(x => x.Id == id));


        public async Task AddAsync(EmailMessage emailMessage)
        {
            await _context.EmailMessages.AddAsync(emailMessage);
            await _context.SaveChangesAsync();

            Console.WriteLine(emailMessage.Subject);
        }
        public async Task AddRangeAsync(ICollection<EmailMessage> emailMessage)
        {
            _context.EmailMessages.AddRange(emailMessage);
            await _context.SaveChangesAsync();
            Console.WriteLine("aaaa");
        }


        public async Task UpdateAsync(EmailMessage emailMessage)
        {
            _context.EmailMessages.Update(emailMessage);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(EmailMessage emailMessage)
        {
            _context.EmailMessages.Remove(emailMessage);
            await _context.SaveChangesAsync();
        }

    }
}
