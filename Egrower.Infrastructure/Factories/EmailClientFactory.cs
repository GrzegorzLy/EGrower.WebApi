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
        public string PathImap { get; private set; }
        public string PathImapLog { get; private set; }
        public string AttachmentPath { get; private set; }
        public string ServerPath { get; private set; }
        public int Port { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }

        public EmailClientFactory()
        {
            PathImap = "..\\Egrower.Infrastructure\\EmailsFiles\\IMap\\";
            PathImapLog = "..\\Egrower.Infrastructure\\EmailsFiles\\IMapLogs\\";
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
            PathImap = "..\\Egrower.Infrastructure\\EmailsFiles\\IMap\\";
            PathImapLog = "..\\Egrower.Infrastructure\\EmailsFiles\\IMapLogs\\";
            AttachmentPath = "..\\Egrower.Infrastructure\\EmailsFiles\\Attachments\\";
        }

        public async Task<IEnumerable<MimeMessage>> GetEmailsIMapAsync()
        {
            using (var client = new ImapClient(new ProtocolLogger(string.Format("{0}{1}_imap.log", PathImapLog, Email))))
            {
                await client.ConnectAsync(ServerPath, Port, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(Email, Password);
                await client.Inbox.OpenAsync(FolderAccess.ReadOnly);
                var uids = await client.Inbox.SearchAsync(SearchQuery.All);
                List<MimeMessage> messages = new List<MimeMessage>();
                foreach (var uid in uids.Reverse())
                {
                    var message = await client.Inbox.GetMessageAsync(uid);
                    if (message != null && message.Date < DateTime.Now && message.Date > DateTime.Now.AddDays(-30))
                    {
                        messages.Add(message);
                        //await message.WriteToAsync(string.Format(@"{0}" + "{1}_{2}.eml", PathImap, Email, uid));
                    }
                    else
                        break;
                }
                await client.DisconnectAsync(true);

                return messages;
            }
        }
    }
}
