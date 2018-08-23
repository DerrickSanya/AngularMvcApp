using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestApp.API.Domain.Base;
using TestApp.API.Models;

namespace TestApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IDataService service;
        public ValuesController(IDataService dataService)
        {
            this.service = dataService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<Value>> Get()
        {
            var test = service.ValuesService.FindAll();
        var results = test.ToList();
        // new string[] { "value1", "value2" };
            return results;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("create")]
        public async void AddNew(string data)
        {
          // await service.ValuesService.CreateAsync(new Value {Name = data});
        //    service.ValuesService.Create(new Value {Name = data});
        //   await service.ValuesService.SaveAsync();

          await Task.Run(() =>{
                 this.service.ValuesService.Create(new Value {Name = data});
                 this.service.ValuesService.Save();
          });
        }
    }
}


