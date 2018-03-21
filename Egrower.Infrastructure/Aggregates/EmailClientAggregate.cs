using Egrower.Infrastructure.Factories.Interfaces;
using EGrower.Core.Domain;
using EGrower.Core.Repositories;
using MimeKit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Egrower.Infrastructure.Aggregates
{
    public class EmailClientAggregate : IEmailClientAggregate
    {
        IEmailMessageRepository _emailRepository;
        IAtachmentRepository _atachmentRepository;
        IEmailClientFactory _clientFactorty;
        protected EmailClientAggregate()
        {               
        }

        public EmailClientAggregate(IEmailMessageRepository emailRepository, IAtachmentRepository atachmentRepository,
                                    IEmailClientFactory clientFactory)
        {
            _emailRepository = emailRepository;
            _atachmentRepository = atachmentRepository;
            _clientFactorty = clientFactory;
        }

        public async Task AddEmailsToDBAsync(IEnumerable<MimeMessage> emailsList)
        {
            IEnumerable emails = emailsList;
           // ICollection<Task<MimeMessage>> EmailsTask = new List<Task<MimeMessage>>();
            foreach (var item in emailsList)
            {
                EmailMessage emailMessage = new EmailMessage(item.From.ToString(), item.To.ToString(),
                    item.Date, item.Subject, item.HtmlBody);
                // EmailsTask.Add(Task.Run(() => _emailRepository.AddAsync(emailMessage))
                await _emailRepository.AddAsync(emailMessage);
            }
        }
    }
}
