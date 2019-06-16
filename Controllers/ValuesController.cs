using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodinovaTask.Entities;
using CodinovaTask.Helper;
using CodinovaTask.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodinovaTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
    //    private readonly IBaseRepository<EmployeeTesting> _context;
        private readonly CodinovaTaskcontext _dbContext;

      public ValuesController( CodinovaTaskcontext dbContext)  
        {
        //    _context = context;
            _dbContext = dbContext;
        }  

        // GET api/values
        [HttpGet]
        [Route("GetValues")]
        public async Task<IActionResult> Get()
        {
            var employee = await _dbContext.Employees.ToListAsync();       
            var data = "Ahmad Nabeel Rahmani";
            var resultReturnModel = new ReturnModel(enumReturnStatus.Success, "Detail_not_found", "Details details not found", employee);     
            return Ok(resultReturnModel);    
        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public ActionResult<string> Get(int id)
        //{
        //    return "value";
        //}

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
    }
}
