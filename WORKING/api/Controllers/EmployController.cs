using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Service;

namespace api.Controllers
{
    [Route("[controller]")]
    public class EmployController : Controller
    {
        private readonly IEmployService _employService;
        //IEmployService _employService;

     
        public EmployController(IEmployService employService)
        {
            _employService = employService;
        }
        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(
                _employService.GetAll()
                );
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(
                _employService.Getid(id)
                );
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Empoy model)
        {
            //return Ok(
            //    _employService.Add(model));
            return Ok(
                _employService.Add(model));

        }

        // PUT api/values/5
        [HttpPut]
        public IActionResult Put([FromBody]Empoy model)
        {
            return Ok(
                _employService.Update(model));
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(
                _employService.Delete(id));
        }
    }
}
