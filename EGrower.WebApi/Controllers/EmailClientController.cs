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
    public class EmailClientController : Controller {

        private readonly IEmailClientAggregate _emailAggregate;
        

        public EmailClientController (IEmailClientAggregate emailClientAggregate) {
            _emailAggregate = emailClientAggregate;         
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var email = await _emailAggregate.GetEmailByEmailID(id);
            return Json(email);
        }

        public async Task<IActionResult> Get()
        {
            await _emailAggregate.AddNewEmailsToDBAsync();
            return StatusCode(200);
        }

        [HttpDelete ("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _emailAggregate.DeleteEmailByEmailID(id);
            return StatusCode(204);
        }


    }
}