using Microsoft.AspNetCore.Mvc;
using Sample.WebApi.Contract;
using Sample.WebApi.Data.DTO;
using Sample.WebApi.Data.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Sample.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {

        // GET: api/<BrandsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
         
            return Ok();
        }

        // GET api/<BrandsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BrandsController>
        [HttpPost]
        public async Task<IActionResult> Create(BrandDTO dto)
        {
            if (dto == null)
            {
                return BadRequest(ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Brand model = new Brand();
            model.BrandName = dto.BrandName;
            model.BrandDesc = dto.BrandDesc;
            var isuccess = await _brandRepository.Create(model);
            if (!isuccess)
            {
                return BadRequest();
            }
            return Created("Created", new { model });
        }

        // PUT api/<BrandsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BrandsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
