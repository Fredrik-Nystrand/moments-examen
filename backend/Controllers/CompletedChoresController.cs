using backend.Data;
using backend.Models.Chore.CompletedChore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class CompletedChoresController : ControllerBase
{
    private readonly DataContext _context;
    private CompletedChoreEntity _entity;

    public CompletedChoresController(DataContext context)
    {
        _context = context;
        _entity = new();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CompletedChoreRequest request)
    {
        return await _entity.Create(_context, _entity, request);
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
