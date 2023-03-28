using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext  _storeContext;
        public BuggyController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest() {

            var thing = _storeContext.Products.Find(50);
            if (thing == null) return NotFound(new ApiResponse(404));   
            return Ok();
                
        }

        [HttpGet("servererror")]
        public IActionResult GetServerError()
        {

            var thing = _storeContext.Products.Find(50);
            if (thing == null) return NotFound();
            return Ok();

        }
        [HttpGet("badrequest")]
        public IActionResult GetBadRequest()
        {

            return BadRequest(new ApiResponse(400));

        }

        [HttpGet("badrequest/id")]
        public IActionResult GetNotFoundRequest(int id)
        {

            return Ok();

        }


    }
}
