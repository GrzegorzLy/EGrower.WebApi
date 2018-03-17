using System;
using System.Collections.Generic;
using System.Text;

namespace EGrower.Core.Domain
{
    public class Atachment
    {
        public int Id { get; protected set;}
        public string Name { get; protected set; }
        public byte[] Data { get; protected set; }
        public int EmailMessageId { get; protected set; }
        public EmailMessage EmailMessage { get; protected set; }

        protected Atachment()
        {
        }
        public Atachment(string name, byte[] data, int emailMessageId)
        {
            Name = name;
            Data = data;
            EmailMessageId = emailMessageId;
        }
    }
}
