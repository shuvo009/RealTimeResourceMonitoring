using System;
using Microsoft.AspNetCore.Mvc;

namespace ResourceServiceTwo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorController : ControllerBase
    {
        public IActionResult Get()
        {
            var info = new
            {
                Cpu = new Random().Next(10, 100) +" Service Two",
                Ram = new Random().Next(30, 100) + " Service Two",
            };
            return Ok(info);
        }
    }
}