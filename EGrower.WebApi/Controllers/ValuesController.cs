using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Egrower.Infrastructure.Factories.Interfaces;
using Egrower.Infrastructure.Factories.MailKit;
using Microsoft.AspNetCore.Mvc;

namespace EGrower.WebApi.Controllers {
    [Route ("api/[controller]")]
    public class ValuesController : Controller {
        private readonly IEmailFactory _emailFactory;
        public ValuesController (IEmailFactory emailFactory) {
            _emailFactory = emailFactory;

        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get () {
            // List<string> datas = Pop3.GetDownloadMessages();
            // var mesages = await _emailFactory.GetEmailsIMapAsync ();
            // await _emailFactory.SaveAttachments (mesages);
            var mesages = await _emailFactory.DeleteMessageAsync ();
            var emails = await _emailFactory.ConvertMailMessagesAsync (mesages);
            return Json (emails);
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