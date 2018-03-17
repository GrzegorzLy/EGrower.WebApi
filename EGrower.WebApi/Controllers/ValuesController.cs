using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Egrower.Infrastructure.DAL;
using Egrower.Infrastructure.Factories;
using Egrower.Infrastructure.Factories.Interfaces;
using Egrower.Infrastructure.Factories.MailKit;
using Microsoft.AspNetCore.Mvc;

namespace EGrower.WebApi.Controllers {
    [Route ("api/[controller]")]
    public class ValuesController : Controller {
        private readonly IEmailFactory _emailFactory;
        private readonly EGrowerContext _eGrowerContext;
        public ValuesController (IEmailFactory emailFactory, EGrowerContext eGrowerContext) {
            _emailFactory = emailFactory;
            _eGrowerContext = eGrowerContext;
            

        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get () {
            // List<string> datas = Pop3.GetDownloadMessages();
             var mesages = await _emailFactory.GetEmailsIMapAsync();
            List<byte[]> list = new List<byte[]>();
            foreach (var item in mesages)
            {
                int i =+ 1;
                _eGrowerContext.EmailMessages.Add(new Core.Domain.EmailMessage(i,item.From.ToString(), item.To.ToString(), 
                    item.Date, item.Subject, item.TextBody));
                foreach (var item1 in item.Attachments)
                {
                    _eGrowerContext.Attachments.Add(new Core.Domain.Atachment(item1.ToString(),
                                                    EmailFactory.ConvertToByteArrayAsync(item1), i));
                    list.Add(EmailFactory.ConvertToByteArrayAsync(item1));
                }
            }
            // await _emailFactory.SaveAttachments (mesages);
            //var mesages = await _emailFactory.DeleteMessageAsync ();
            //var emails = await _emailFactory.ConvertMailMessagesAsync (mesages);
            await _eGrowerContext.SaveChangesAsync();

            return StatusCode(200);
        }

        // GET api/values/5
        [HttpGet ("{id}")]
        public string Get (int id) {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post ([FromBody] string value) { }

        // PUT api/values/5
        [HttpPut ("{id}")]
        public void Put (int id, [FromBody] string value) { }

        // DELETE api/values/5
        [HttpDelete ("{id}")]
        public void Delete (int id) { }


    }
}