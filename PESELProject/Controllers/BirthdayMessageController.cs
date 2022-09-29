using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PESELProject.Utils;
using PESELProject.Model;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using Swashbuckle.AspNetCore.Annotations;

namespace PESELProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirthdayMessageController : ControllerBase
    {
        private const string FemaleBdayMessage = "Klientko {0} {1}! Życzymy Ci sto lat!";
        private const string MaleBdayMessage = "Kliencie {0} {1}! Życzymy Ci sto lat!";

        private const string NameRegex = "^[^0-9!@#$%^&*]{1,128}$";


        ///<summary>
        /// Get the birthday message for the customer
        ///</summary>
        ///<response code="200">Returns birthday message</response>
        ///<response code="400">Indicates bad request parameters</response>
        [SwaggerResponse(StatusCodes.Status200OK, Type = typeof(JsonResult))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public JsonResult GetBirthdayMessage(string firstName, string lastName, string pesel)
        {
            if (!PeselUtils.IsValidPesel(pesel, out _))
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return new JsonResult(BadRequest("Invalid PESEL number"));
            }

            if(!Regex.Match(firstName, NameRegex).Success || !Regex.Match(lastName, NameRegex).Success)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return new JsonResult(BadRequest("Invalid first or/and last name"));
            }

            string message = PeselUtils.GetGender(pesel) == Gender.MALE ? MaleBdayMessage : FemaleBdayMessage;


            return new JsonResult(Ok(string.Format(message, firstName, lastName)));
        }
    }
}
