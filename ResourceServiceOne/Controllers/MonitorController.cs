using System;
using Microsoft.AspNetCore.Mvc;

namespace ResourceServiceOne.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitorController : ControllerBase
    {
        public IActionResult Get()
        {
            var info = new
            {
                Cpu = new Random().Next(10, 100) + " Service One",
                Ram = new Random().Next(30, 100) + " Service One",
            }; 
            return Ok(info);
        }
    }
}