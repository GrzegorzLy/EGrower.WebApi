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
        IEmailMessageRepository _emailRepository;
        IAtachmentRepository _atachmentRepository;

        protected EmailClientAggregate()
        {               
        }

        public EmailClientAggregate(IEmailMessageRepository emailRepository, IAtachmentRepository atachmentRepository)
        {
            _emailRepository = emailRepository;
            _atachmentRepository = atachmentRepository;
        }

        public async Task AddEmailsToDBAsync(IEnumerable<MimeMessage> emailsList)
        {
            List<EmailMessage> emails = new List<EmailMessage>();
            foreach (var item in emailsList)
            {
                EmailMessage emailMessage = new EmailMessage(item.From.ToString(), item.To.ToString(),
                                                              item.Date.UtcDateTime, item.Subject, item.HtmlBody);

                ICollection<Atachment> atachments = GetAttachmentsFromMmimeMessage(item);
                emailMessage.AddAtachments(atachments);
                await _emailRepository.AddAsync(emailMessage);
                //emails.Add(emailMessage);              
            }

            await Task.CompletedTask;
            //await Task.FromResult(_emailRepository.AddRangeAsync(emails));
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
    }
}
