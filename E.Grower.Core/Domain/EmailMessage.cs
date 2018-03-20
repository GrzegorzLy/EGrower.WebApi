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
        public DateTime CreatedAt { get; private set; }
        public DateTime DateAddedToTheDataBase { get; private set; }
        public string Subject { get; private set; }
        public string TextHTMLBody { get;private set; }
        public ICollection<Atachment> Atachments { get; protected set; }

        protected EmailMessage()
        {
        }

        public EmailMessage(string from, string to, DateTimeOffset createdAt, string subject, string textBody)
        {
            From = from;
            To = to;
            CreatedAt = createdAt.DateTime;
            DateAddedToTheDataBase = DateTime.UtcNow;
            Subject = subject;
            TextHTMLBody = textBody;
            Atachments = new List<Atachment>();
        }
    }
}
