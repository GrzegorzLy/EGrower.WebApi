using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Egrower.Infrastructure.Aggregates;
using Egrower.Infrastructure.DAL;
using Egrower.Infrastructure.Factories;
using Egrower.Infrastructure.Factories.Interfaces;
using Egrower.Infrastructure.Factories.MailKit;
using EGrower.Core.Domain;
using EGrower.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EGrower.WebApi.Controllers {
    [Route ("api/[controller]")]
    public class ValuesController : Controller {
        private readonly IEmailClientFactory _emailFactory;
        private readonly IEmailClientAggregate _emailAggregate;
        

        public ValuesController (IEmailClientFactory emailFactory, IEmailClientAggregate emailClientAggregate) {
            _emailFactory = emailFactory;
            _emailAggregate = emailClientAggregate;         
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var email = await _emailAggregate.GetEmailByEmailID(id);
            return Json(email);
        }

        //public async Task<IActionResult> Get()
        //{
        //    var mimeEmails = await _emailFactory.GetEmailsIMapAsync();
        //   await _emailAggregate.AddEmailsToDBAsync(mimeEmails);

        //    return StatusCode(200);
        //}

        // GET api/values
        //[HttpGet]
        //public async Task<IActionResult> Get () {

        //    //Test
        //    // List<string> datas = Pop3.GetDownloadMessages();
        //     var mesages = await _emailFactory.GetEmailsIMapAsync();
        //    foreach (var item in mesages)
        //    {
        //        EmailMessage emailMessage = new EmailMessage(item.From.ToString(), item.To.ToString(), 
        //            item.Date, item.Subject, item.HtmlBody);
        //        foreach (var item1 in item.Attachments)
        //        {

        //            emailMessage.Atachments.Add(new Atachment(item1.ContentType.Name, EmailFactory.ConvertToByteArray(item1)));
        //        }

        //        _eGrowerContext.EmailMessages.Add(emailMessage);
        //    }
        //    // await _emailFactory.SaveAttachments (mesages);
        //    //var mesages = await _emailFactory.DeleteMessageAsync ();
        //    //var emails = await _emailFactory.ConvertMailMessagesAsync (mesages);
        //    await _eGrowerContext.SaveChangesAsync();

        //    return StatusCode(200);
        //}

        // GET api/values/5
       

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