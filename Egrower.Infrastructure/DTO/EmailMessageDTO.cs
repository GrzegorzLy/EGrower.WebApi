using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Egrower.Infrastructure.DTO
{
    public class EmailMessageDTO
    {
        public string From { get; private set; }
        public string To { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string Subject { get; private set; }
        public string TextHTMLBody { get; private set; }
        public ICollection<AtachmentDTO> Atachments { get; protected set; }

    }
}
