using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PESELProject.Utils;
using Swashbuckle.AspNetCore.Annotations;

namespace PESELProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientAgeController : ControllerBase
    {

        ///<summary>
        /// Get age of the customer based on its PESEL
        ///</summary>
        ///<response code="200">Returns age of the customer</response>
        ///<response code="400">Indicates bad request parameters</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(JsonResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public JsonResult getClientAge(string pesel)
        {
            DateTime? dateOfBirth;

            if(!PeselUtils.IsValidPesel(pesel, out dateOfBirth))
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return new JsonResult(BadRequest("Invalid PESEL number"));
            }
                

            int age = TimeUtils.GetPersonAge(dateOfBirth.Value);

            return new JsonResult(Ok(age));

        }
    }
}
