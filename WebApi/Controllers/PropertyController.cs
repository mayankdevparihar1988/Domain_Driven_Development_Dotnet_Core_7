using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Dto;
using Application.Features.Properties.Commands;
using Application.Features.Properties.Queries;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : Controller
    {
        private readonly ISender _mediatrSender;

        public PropertyController(ISender mediatrSender)
        {
            _mediatrSender = mediatrSender;
        }



        // GET: api/values
        [HttpGet("all")]
        public async Task<ActionResult<List<PropertyResponseDto>>> GetProperties()
        {
            List<PropertyResponseDto> propertiesRetrived = await _mediatrSender.Send(new GetPropertiesRequest());

            if (propertiesRetrived is null) return NotFound("No properties found");

            return Ok(propertiesRetrived);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyResponseDto>> Get(int id)
        {
            var retrievedProperty = await _mediatrSender.Send(new GetPropertyByIdRequest(id));

            if (retrievedProperty is null) return NotFound("The Property not Found!");

            return Ok(retrievedProperty);
        }

        // POST api/values
        [HttpPost("add")]
        public async Task<IActionResult> AddNewProperty([FromBody]NewPropertyRequest newPropertyRequest)
        {

            var isSuccess = await _mediatrSender.Send(new CreatePropertyRequest(newPropertyRequest));

            if (!isSuccess)
            {
                return BadRequest();
            }

            return Ok("New Property Created");

        }

        // PUT api/values/5
        [HttpPut("update")]
        public async Task<IActionResult> UpdateProperty([FromBody]UpdatePropertyRequestDto updateProperty)
        {
            bool isSuccessful = await _mediatrSender.Send(new UpdatePropertyRequest(updateProperty));

            if (isSuccessful) return Ok("Property is succefully updated");

            return BadRequest("Unable to update property");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(int id)
        {
            var isSuccessful = await _mediatrSender.Send(new DeletePropertyRequest(id));
            if (isSuccessful > 0)
            {
                return Ok("Property deleted successfully.");
            }
            return NotFound("Property doest not exists.");
        }
    }
}

