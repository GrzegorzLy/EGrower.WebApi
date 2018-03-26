using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Egrower.Infrastructure.Factories.Interfaces;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Search;
using MailKit.Security;
using MimeKit;

namespace Egrower.Infrastructure.Factories {
    public class EmailFactory : IEmailFactory {

        public static string PathPop3 { get; private set; }
        public string PathImap { get; private set; }
        public string PathPop3Log { get; private set; }
        public string PathImapLog { get; private set; }
        public string AttachmentPath { get; private set; }

        public string email { get; private set; }
        public string password { get; private set; }
        public EmailFactory() {
            PathPop3 = "..\\Egrower.Infrastructure\\EmailsFiles\\Pop3\\";
            PathImap = "..\\Egrower.Infrastructure\\EmailsFiles\\IMap\\";
            PathPop3Log = "..\\Egrower.Infrastructure\\EmailsFiles\\Pop3Logs\\";
            PathImapLog = "..\\Egrower.Infrastructure\\EmailsFiles\\IMapLogs\\";
            email = "ehodowcatest@gmail.com";
            password = "ehodowca";
            AttachmentPath = "..\\Egrower.Infrastructure\\EmailsFiles\\Attachments\\";
        }
        // public async Task<IEnumerable<EmailMessage>> GetEmailsIMapAsync () {
        //     using (var client = new ImapClient (new ProtocolLogger (string.Format ("{0}{1}_imap.log", PathImapLog, email)))) {
        //         await client.ConnectAsync ("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
        //         await client.AuthenticateAsync (email, password);
        //         await client.Inbox.OpenAsync (FolderAccess.ReadOnly);
        //         var uids = await client.Inbox.SearchAsync (SearchQuery.All);
        //         List<EmailMessage> messages = new List<EmailMessage> ();
        //         foreach (var uid in uids.Reverse ()) {
        //             var message = await client.Inbox.GetMessageAsync (uid);
        //             if (message != null && message.Date < DateTime.Now && message.Date > DateTime.Now.AddDays (-30)) {

        //                 messages.Add (new EmailMessage (message));
        //                 // write the message to a file
        //                 await message.WriteToAsync (string.Format (@"{0}" + "{1}_{2}.eml", PathImap, email, uid));

        //             } else
        //                 break;
        //         }

        //         await client.DisconnectAsync (true);

        //         return messages;
        //     }
        // }

        //}
        //public async Task<IEnumerable<MimeMessage>> GetEmailsIMapAsync()
        //{
        //    using (var client = new ImapClient(new ProtocolLogger(string.Format("{0}{1}_imap.log", PathImapLog, email))))
        //    {
        //        await client.ConnectAsync(Uri );
        //        await client.AuthenticateAsync(email, password);
        //        await client.Inbox.OpenAsync(FolderAccess.ReadOnly);
        //        var uids = await client.Inbox.SearchAsync(SearchQuery.All);
        //        List<MimeMessage> messages = new List<MimeMessage>();
        //        foreach (var uid in uids.Reverse())
        //        {
        //            var message = await client.Inbox.GetMessageAsync(uid);
        //            if (message != null && message.Date < DateTime.Now && message.Date > DateTime.Now.AddDays(-30))
        //            {
        //                messages.Add(message);
        //                // write the message to a file
        //                // await message.WriteToAsync(string.Format(@"{0}" + "{1}_{2}.eml", PathImap, email, uid));
        //            }
        //            else
        //                break;
        //        }
        //        await client.DisconnectAsync(true);

        //        return messages;
        //    }
        //}

        // public async Task GetEmailsPop3Async () {
        //     using (var client = new Pop3Client (new ProtocolLogger ("pop3.log"))) {
        //         await client.ConnectAsync ("pop.gmail.com", 995, SecureSocketOptions.SslOnConnect);

        //         //test acount on gmail
        //         await client.AuthenticateAsync (email, password);
        //         for (int i = 0; i < client.Count; i++) {
        //             var message = await client.GetMessageAsync (i);
        //             // DisplayMessageCW (message);
        //             // write the message to a file
        //             await message.WriteToAsync (string.Format (@"{0}" + "{1}_{2}.msg", email, i));

        //             // mark the message for deletion
        //             await client.DeleteMessageAsync (i);
        //         }

        //         await client.DisconnectAsync (true);
        //     }
        // }

        // private void DisplayMessageCW (MimeMessage message) {
        //     string a = string.Format ("MAIL: body : {0} From: {1} , To : {2}  , Date : {3} , Sender: {4}, Subject: {5}, Text Body: {6} ", "message.Body", message.From, message.To, message.Date, message.Sender, message.Subject, message.TextBody);

        //     string b = string.Format ("MAILs datas:  Attachments: {0} ResentFrom: {1} , Cc : {2}  , Headers : {3} , Importance: {4}, References: {5}, ResentBcc: {6} , ResentCc: {7} ,  ReplyTo: {8} , ResentFrom: {9} ,  ResentSender: {10}  ,  ResentTo: {11}, XPriority {12} ", message.Attachments, message.ResentFrom, message.Cc, message.Headers, message.Importance, message.References, message.ResentBcc, message.ResentCc, message.ReplyTo, message.ResentFrom, message.ResentSender, message.ResentTo, message.XPriority);

        //     System.Console.WriteLine (a);
        //     System.Console.WriteLine ("-------------------------");
        //     System.Console.WriteLine (b);

        // }

        //private async Task SaveAttachment(EmailMessage message)
        //{
        //    foreach (var attachment in message.Attachments)
        //    {
        //        if (attachment is MessagePart)
        //        {
        //            var fileName = attachment.ContentDisposition?.FileName + "attached.eml";
        //            // (attachment.ContentType.Name ?? "attached.eml");
        //            var rfc822 = (MessagePart)attachment;
        //            using (var stream = File.Create(AttachmentPath + email + "_" + fileName))
        //                await rfc822.Message.WriteToAsync(stream);
        //        }
        //        else
        //        {
        //            var part = (MimePart)attachment;
        //            var fileName = part.FileName;

        //            using (var stream = File.Create(string.Format(AttachmentPath + email + "_" + fileName)))
        //                await part.Content.DecodeToAsync(stream);
        //        }
        //    }
        //}

        public static byte[] ConvertToByteArray(MimeEntity attachment)
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

        // public async Task SaveAttachments (IEnumerable<EmailMessage> messages) {
        //     foreach (var message in messages) {
        //         await SaveAttachment (message);
        //     }
        // }

        // public Task DeleteMessageAsync (EmailMessage messages) {
        //     throw new NotImplementedException ();
        // }
        // "imap.gmail.com", 993
        public async Task<IEnumerable<MimeMessage>> GetAllMimesIMapAsync () {
            // new ProtocolLogger (string.Format ("{0}{1}_imap.log", PathImapLog, email))
            using (var client = new ImapClient ()) {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await client.ConnectAsync ("poczta.interia.pl", 143, SecureSocketOptions.Auto);
                await client.AuthenticateAsync (email, password);
                await client.Inbox.OpenAsync (FolderAccess.ReadOnly);
                var uids = await client.Inbox.SearchAsync (SearchQuery.All);
                List<MimeMessage> messages = new List<MimeMessage> ();
                foreach (var uid in uids.Reverse ()) {
                    var message = await client.Inbox.GetMessageAsync (uid);
                    if (message != null && message.Date < DateTime.Now && message.Date > DateTime.Now.AddMonths (-1)) {

                        messages.Add (message);
                        // write the message to a file
                        await message.WriteToAsync (string.Format (@"{0}" + "{1}_{2}.eml", PathImap, email, uid));

                    } else
                        break;
                }

                await client.DisconnectAsync (true);

                return messages;
            }
        }

        public async Task<IEnumerable<MimeMessage>> GetNewMimesIMapAsync () {
            // new ProtocolLogger (string.Format ("{0}{1}_imap.log", PathImapLog, email))
            using (var client = new ImapClient ()) {
                await client.ConnectAsync ("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync (email, password);
                await client.Inbox.OpenAsync (FolderAccess.ReadWrite);
                var uids = await client.Inbox.SearchAsync (SearchQuery.NotRecent);
                List<MimeMessage> messages = new List<MimeMessage> ();
                foreach (var uid in uids.Reverse ()) {
                    var message = await client.Inbox.GetMessageAsync (uid);
                    if (message != null && message.Date < DateTime.Now && message.Date > DateTime.Now.AddDays (-1)) {

                        messages.Add (message);
                        // await client.Inbox.AddFlagsAsync(uid,MessageFlags.Seen,false);
                        // write the message to a file
                        //await message.WriteToAsync (string.Format (@"{0}" + "{1}_{2}.eml", PathImap, email, uid));
                        
                    } else
                        break;
                }

                await client.DisconnectAsync (true);

                return messages;
            }
        }
        public async Task<IEnumerable<EmailMessageInfrastructure>> ConvertMailMessagesAsync (IEnumerable<MimeMessage> messages) {
            List<EmailMessageInfrastructure> emailMessages = new List<EmailMessageInfrastructure> ();
            int i =1;
            foreach (var message in messages) {
                emailMessages.Add (new EmailMessageInfrastructure (++i,message.From.ToString (), message.To.ToString (), message.Date, message.Subject, message.Body.ToString ()));
            }
            return await Task.FromResult (emailMessages);
        }

        public async Task<IEnumerable<MimeMessage>> DeleteMessageAsync () {
            using (var client = new ImapClient ()) {
                await client.ConnectAsync ("poczta.interia.pl", 143, SecureSocketOptions.Auto);
                await client.AuthenticateAsync (email, password);
                await client.Inbox.OpenAsync (FolderAccess.ReadWrite);
                var uids = await client.Inbox.SearchAsync (SearchQuery.All);
                List<MimeMessage> messages = new List<MimeMessage> ();
                foreach (var uid in uids.Reverse ()) {
                    var message = await client.Inbox.GetMessageAsync (uid);
                    if (message != null && message.Date < DateTime.Now && message.Date > DateTime.Now.AddMinutes (-30)) {

                        messages.Add (message);
                        await client.Inbox.AddFlagsAsync (uid, MessageFlags.Deleted, false);
                        // write the message to a file
                        // await message.WriteToAsync (string.Format (@"{0}" + "{1}_{2}.eml", PathImap, email, uid));

                    } else
                        break;
                }

                await client.DisconnectAsync (true);

                return await Task.FromResult (messages);
            }
        }

        public Task<IEnumerable<MimeMessage>> GetEmailsIMapAsync()
        {
            throw new NotImplementedException();
        }
    }
}