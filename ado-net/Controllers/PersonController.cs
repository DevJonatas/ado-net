using ado_net.DAL.Interfaces;
using ado_net.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ado_net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Person> Get([FromServices] IPersonDAL service)
        {
            var result = service.GetAll();
            return result;
        }

        [HttpPost]
        public IActionResult Post([FromServices] IPersonDAL service, [FromBody] Person person)
        {
            service.New(person);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put([FromServices] IPersonDAL service, [FromBody] Person person)
        {
            service.Update(person);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromServices] IPersonDAL service, int id)
        {
            service.Delete(id);
            return Ok();
        }

    }
}
