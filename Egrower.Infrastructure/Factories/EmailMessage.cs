using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using MimeKit;

namespace Egrower.Infrastructure.Factories {
    public class EmailMessage {

        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }
        public string Subject { get; set; }
        public string TextBody { get; set; }
        public string TextHTMLBody { get; set; }
        public IEnumerable<MimeEntity> Attachments { get; set; }
        // files

        public EmailMessage (string from, string to, DateTimeOffset date, string subject, string textBody, string textHTMLBody) {
            From = from;
            To = to;
            Date = date.DateTime;
            Subject = subject;
            TextBody = textBody;
            TextHTMLBody = textBody;
        }
        public  EmailMessage(MimeMessage message)
        {
             From = message.From.ToString();
            To = message.To.ToString();
            Date = message.Date.DateTime;
            Subject =message.Subject;
            TextBody = message.TextBody;
            TextHTMLBody = message.TextBody;
            if(message.Attachments !=null)
            Attachments = message.Attachments;
        }

		// public async Task SaveAttachments (MimeMessage message)
		// {
		// 	foreach (var attachment in message.Attachments) {
		// 		if (attachment is MessagePart) {
		// 			var fileName = attachment.ContentDisposition?.FileName +"attached.eml";
		// 				// (attachment.ContentType.Name ?? "attached.eml");
		// 			var rfc822 = (MessagePart) attachment;
        //             using (var stream = File.Create (fileName))
		// 			await rfc822.Message.WriteToAsync (stream);
		// 		} else {
		// 			var part = (MimePart) attachment;
		// 			var fileName = part.FileName;

		// 			using (var stream = File.Create (fileName))
		// 			await	part.Content.DecodeToAsync (stream);
		// 		}
		// 	}
		// }


    }
}