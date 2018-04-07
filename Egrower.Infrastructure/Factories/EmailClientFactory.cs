using Egrower.Infrastructure.Factories.Interfaces;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Egrower.Infrastructure.Factories
{
    public class EmailClientFactory : IEmailClientFactory
    {
        public string AttachmentPath { get; private set; }
        public string ServerPath { get; private set; }
        public int Port { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public EmailClientFactory()
        {
            AttachmentPath = "..\\Egrower.Infrastructure\\EmailsFiles\\Attachments\\";
            ServerPath = "imap.gmail.com";
            Port = 993;
            Email = "ehodowcatest@gmail.com";
            Password = "ehodowca";
        }

        public EmailClientFactory(string serverPath, int port, string email,string password)
        {
            ServerPath = serverPath;
            Port = port;
            Email = email;
            Password = password;
            AttachmentPath = "..\\Egrower.Infrastructure\\EmailsFiles\\Attachments\\";
        }

        public async Task<IEnumerable<MimeMessage>> GetNewEmailsAsync()
        {
            using (var client = new ImapClient())
            {
                await client.ConnectAsync(ServerPath, Port, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(Email, Password);
                await client.Inbox.OpenAsync(FolderAccess.ReadWrite);
                var uids = await client.Inbox.SearchAsync(SearchQuery.NotSeen);
                List<MimeMessage> messages = new List<MimeMessage>();

                foreach (var uid in uids.Reverse())
                {
                    var message = await client.Inbox.GetMessageAsync(uid);
                    if (message != null && message.Date < DateTime.Now && message.Date > DateTime.Now.AddDays(-30))
                    {
                        messages.Add(message);
                        
                        await client.Inbox.SetFlagsAsync(uid, MessageFlags.Seen,false).ConfigureAwait(false);
                    }
                    else
                        break;
                }
                await client.DisconnectAsync(true);

                return messages;
            }
        }

        public async Task DeleteMessageAsync(DateTime date) 
        {
            using (var client = new ImapClient())
            {
                await client.ConnectAsync(ServerPath, Port, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(Email, Password);
                await client.Inbox.OpenAsync(FolderAccess.ReadWrite);
                var uids = await client.Inbox.SearchAsync(SearchQuery.DeliveredOn(date));
                await client.Inbox.SetFlagsAsync(uids[0], MessageFlags.Deleted, false).ConfigureAwait(false);                
                await client.DisconnectAsync(true);
            }
        }
    }
}
