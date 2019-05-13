using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SwordAndFather.Data;
using Microsoft.AspNetCore.Mvc;
using SwordAndFather.Models;
using Microsoft.Extensions.Options;

namespace SwordAndFather.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TargetController : ControllerBase
    {
        readonly TargetRepository _repo;
        public TargetController(TargetRepository repo)
        {
            _repo = repo; // reading resource: ioc inversion of controll( dependency enjection) our contoller does not how to build it only asp.net needs to know it. 
            // interface ??
        }
        [HttpPost]
        public ActionResult AddTarget(CreateTargetRequest createRequest)
        {
            var repository = new TargetRepository();

            var newTarget = repository.AddTarget(createRequest.Name, 
                createRequest.Location, 
                createRequest.FitnessLevel, 
                createRequest.UserId);

            return Created($"/api/target/{newTarget.Id}", newTarget);
        }
    }
}