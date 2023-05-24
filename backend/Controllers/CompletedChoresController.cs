using backend.Data;
using backend.Models.Chore.CompletedChore;
using backend.Models.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CompletedChoresController : ControllerBase
{
    private readonly DataContext _context;
    private CompletedChoreEntity _entity;
    private IConfiguration _config;

    public CompletedChoresController(DataContext context, IConfiguration config)
    {
        _context = context;
        _entity = new();
        _config = config;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CompletedChoreRequest request)
    {
        var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        TokenHandler _tokenHandler = new(token, _config);

        if (_tokenHandler.UserValidated)
        {

            return await _entity.Create(_context, _entity, request);
        }
        else
        {
            return Unauthorized();
        }

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return await _entity.Get(_context, id);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAllForUser(int id)
    {
        return await _entity.GetAllForUser(_context, id);
    }
}
