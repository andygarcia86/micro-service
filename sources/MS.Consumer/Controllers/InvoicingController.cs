﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MS.Consumer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MS.Publisher.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvoicingController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Consumer"
        };

        private readonly ILogger<InvoicingController> _logger;

        public InvoicingController(ILogger<InvoicingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
