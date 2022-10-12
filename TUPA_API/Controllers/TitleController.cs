using Data.Model.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedInterfaces;
using SharedInterfaces.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TUPA_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TitleController : ControllerBase
    {

        private readonly ITitle _title;

        public TitleController(ITitle title)
        {
            _title = title;
        }

        [HttpPost]
        public async Task<IActionResult> PostTitle(TitleList param)
        {

            var response = await _title.PostTitle(param);
            try
            {
                if (response.ReturnStatus)
                {

                    return Ok(response.ReturnMessage);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                response.ReturnStatus = false;
                response.ReturnMessage = ex.Message;
                return BadRequest(response);
            }
        }

    }
}
