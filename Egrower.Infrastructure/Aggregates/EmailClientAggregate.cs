using AutoMapper;
using Egrower.Infrastructure.DTO;
using Egrower.Infrastructure.Factories.Interfaces;
using EGrower.Core.Domain;
using EGrower.Core.Repositories;
using MimeKit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egrower.Infrastructure.Aggregates
{
    public class EmailClientAggregate : IEmailClientAggregate
    {
        private readonly IEmailMessageRepository _emailRepository;
        private readonly IAtachmentRepository _atachmentRepository;
        private readonly IEmailClientFactory _emailFactory;

        protected EmailClientAggregate()
        {               
        }

        public EmailClientAggregate(IEmailMessageRepository emailRepository, IAtachmentRepository atachmentRepository,
                                    IEmailClientFactory emailFactory)
        {
            _emailRepository = emailRepository;
            _atachmentRepository = atachmentRepository;
            _emailFactory = emailFactory;
        }

        public async Task AddNewEmailsToDBAsync()
        {
            var mimeEmails = await _emailFactory.GetNewEmailsAsync();

            foreach (var item in mimeEmails)
            {
                EmailMessage emailMessage = new EmailMessage(item.From.ToString(), item.To.ToString(),
                                                              item.Date.UtcDateTime, item.Subject, item.HtmlBody);

                ICollection<Atachment> atachments = GetAttachmentsFromMmimeMessage(item);
                emailMessage.AddAtachments(atachments);
                await _emailRepository.AddAsync(emailMessage);             
            }
            await Task.CompletedTask;
        }

        private ICollection<Atachment> GetAttachmentsFromMmimeMessage(MimeMessage mime)
        {
            List<Atachment> atachments = new List<Atachment>();
            foreach (var atachment in mime.Attachments)
            {
                string name = atachment.ContentType.Name;
                byte[] data = ConvertToByteArray(atachment);
               atachments.Add( new Atachment(name, data));
            }
            return atachments;
        }

        private byte[] ConvertToByteArray(MimeEntity attachment)
        {
           
            byte[] bytes;
            using (var memory = new MemoryStream())
            {
                if (attachment is MimePart)
                    ((MimePart)attachment).Content.DecodeTo(memory);
                else
                    ((MessagePart)attachment).Message.WriteTo(memory);

                bytes = memory.ToArray();
            }
            return bytes;
        }

        public async Task<EmailMessageDTO> GetEmailByEmailID(int emailId)
        {
          var email = await  _emailRepository.GetAsync(emailId);
            return Mapper.Map<EmailMessageDTO>(email);
        }

        public Task<EmailMessageDTO> GetEmailsByUserID(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteEmailByEmailID(int emailId)
        {
            var email = await  _emailRepository.GetAsync(emailId);
            await  _emailFactory.DeleteMessageAsync(email.CreatedAt);
            await _emailRepository.DeleteAsync(email);  
        }
    }
}
