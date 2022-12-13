using System.Threading.Tasks;
using Mastek_PostCode_API.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Mastek_PostCode_API.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostCodeController : ControllerBase
    {
        IPostCodeService _postCodeService;
        public PostCodeController(IPostCodeService postCodeService)
        {
            _postCodeService = postCodeService;
        }

        [HttpGet("GetPostCodeDetail/{pC}")]
        public async Task<ActionResult> GetPostCodeDetail(string pC)
        {
            if (string.IsNullOrWhiteSpace(pC))
                return NotFound();
            try
            {
                var ob = new string[] { "ac", "bc" };
                return Ok(await _postCodeService.GetPostCodeDetailAsync(pC));
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("GetAutoComplete/{pC}")]
        public async Task<ActionResult> GetAutoComplete(string pC)
        {
            if (string.IsNullOrWhiteSpace(pC))
                return NotFound();
            try
            {
                return Ok(await _postCodeService.GetAutoCompleteAsync(pC));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
