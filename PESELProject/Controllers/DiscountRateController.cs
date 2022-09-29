using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PESELProject.Utils;
using Swashbuckle.AspNetCore.Annotations;

namespace PESELProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountRateController : ControllerBase
    {
        ///<summary>
        /// Returns discount rate based on the customer PESEL
        ///</summary>
        ///<response code="200">Returns discount rate</response>
        ///<response code="400">Indicates bad request parameters</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(JsonResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public JsonResult getDiscountRate (string pesel){
            DateTime? dateOfBirth;

            if (!PeselUtils.IsValidPesel(pesel, out dateOfBirth))
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return new JsonResult(BadRequest("Invalid PESEL number"));
            }

            return new JsonResult(Ok(TimeUtils.GetDiscountRate(dateOfBirth.Value)));
        }
    }
}
