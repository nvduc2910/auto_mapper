using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper_Demo.Models.Entities;
using AutoMapper_Demo.ViewModels;
using AutoMapper;

namespace AutoMapper_Demo.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IMapper _mapper;
        public ValuesController(IMapper mapper)
        {
            _mapper = mapper;
        }
 
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] StudentViewModel student)
        {
            var studentModel = _mapper.Map<Student>(student);


        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put([FromBody]StudentDetailViewModel studentViewModel)
        {
            var studentModel = _mapper.Map<Student>(studentViewModel);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
