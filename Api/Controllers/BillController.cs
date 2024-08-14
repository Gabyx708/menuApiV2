using Application.Common.Models;
using Application.Interfaces.IBills;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IGetBillsMonth _getBillsMonth;

        public BillController(IGetBillsMonth getBillsMonth)
        {
            this._getBillsMonth = getBillsMonth;
        }

        [HttpGet("year/{year}/month/{month}")]
        public IActionResult GetBillinMonth(int year,int month)
        {
            var result = _getBillsMonth.GetBillsInMonth(year, month);

            if(result.Success) return Ok(result.Data);

            return new JsonResult(new SystemResponse
            {
                StatusCode = result.StatusCode,
                Message = result.ErrorMessage
            })
            { StatusCode = result.StatusCode };
        }
    }
}
