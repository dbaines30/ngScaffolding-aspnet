using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ngScaffolding.ExtensionMethods;
using ngScaffolding.Services;

namespace ngScaffolding.Controllers
{
    [Produces("application/json")]
    [Route("api/ReferenceValues")]
    public class ReferenceValuesController : Controller
    {
        private readonly IReferenceValuesService _referenceValuesService;

        public ReferenceValuesController(IReferenceValuesService referenceValuesService)
        {
            _referenceValuesService = referenceValuesService;
        }

        // GET: api/ReferenceValues
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ReferenceValues/5
        [HttpGet("{name}/{group=}", Name = "Get")]
        public async Task<IActionResult> Get(string name, string group = null)
        {
            if (!StringChecker.IsNullOrEmpty(name) && !StringChecker.IsNullOrEmpty(group))
            {
                return Ok(_referenceValuesService.GetValue(name, group));

            }
            else if (!StringChecker.IsNullOrEmpty(name))
            {
                return Ok(_referenceValuesService.GetValue(name));
            }
            return NotFound();
        }

        //[HttpGet("{name}", Name = "Get")]
        //public string Get(string name)
        //{
        //    return $"Got By Name: {name}";
        //}


        // POST: api/ReferenceValues
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ReferenceValues/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
