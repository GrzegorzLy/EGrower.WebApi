using System;
using System.Collections.Generic;
using System.Text;

namespace EGrower.Core.Domain
{
    public class EmailMessage
    {
        public int Id { get; private set; }
        public string From { get; private set; }
        public string To { get; private set; }
        public DateTime Date { get; private set; }
        public string Subject { get; private set; }
        public string TextHTMLBody { get;private set; }
        public ICollection<Atachment> Attachments { get; protected set; }

        protected EmailMessage()
        {
        }

        public EmailMessage(int id, string from, string to, DateTimeOffset date, string subject, string textBody)
        {
            From = from;
            To = to;
            Date = date.DateTime;
            Subject = subject;
            TextHTMLBody = textBody;
        }
    }
}
