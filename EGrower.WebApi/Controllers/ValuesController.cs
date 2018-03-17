using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Egrower.Infrastructure.DAL;
using Egrower.Infrastructure.Factories;
using Egrower.Infrastructure.Factories.Interfaces;
using Egrower.Infrastructure.Factories.MailKit;
using EGrower.Core.Domain;
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
            foreach (var item in mesages)
            {
                EmailMessage emailMessage = new EmailMessage(item.From.ToString(), item.To.ToString(), 
                    item.Date, item.Subject, item.HtmlBody);
                foreach (var item1 in item.Attachments)
                {
                    emailMessage.Atachments.Add(new Atachment(item1.ToString(), EmailFactory.ConvertToByteArray(item1)));
                }

                _eGrowerContext.EmailMessages.Add(emailMessage);
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