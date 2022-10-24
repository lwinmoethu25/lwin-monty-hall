using System;
using LwinMontyHall.Models;
using LwinMontyHall.Services;
using Microsoft.AspNetCore.Mvc;

namespace LwinMontyHall.Controllers;

[ApiController]
[Route("[controller]")]
public class MontyHallController : ControllerBase
{
    private readonly ILogger<MontyHallController> _logger;

    private readonly IMontyHallService _montyHallService;

    public MontyHallController(ILogger<MontyHallController> logger, IMontyHallService montyHallService)
    {
        _logger = logger;
        _montyHallService = montyHallService;
    }

    [HttpPost("/Simulate")]
    public async Task<IActionResult> SimulateAsync([FromBody] MontyHallRequest request)
    {
       var response = await _montyHallService.startSimulation(request);
       return Ok(response);
    }
}
