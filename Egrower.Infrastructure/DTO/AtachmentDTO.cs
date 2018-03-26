using System;
using System.Collections.Generic;
using System.Text;

namespace Egrower.Infrastructure.DTO
{
    public class AtachmentDTO
    {
        public string Name { get; protected set; }
        public byte[] Data { get; protected set; }
    }
}
