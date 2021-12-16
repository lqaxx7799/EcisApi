using EcisApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class V1Controller : ControllerBase
    {
        private readonly IV1Service v1Service;

        public V1Controller(IV1Service v1Service)
        {
            this.v1Service = v1Service;
        }
    }
}
