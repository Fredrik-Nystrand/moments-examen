using backend.Data;
using backend.Models.Chore;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChoresController : ControllerBase
{

    private readonly DataContext _context;
    private ChoreEntity _entity;

    public ChoresController(DataContext context)
    {
        _context = context;
        _entity = new();
    }


    [HttpPost]
    public async Task<IActionResult> Create(ChoreRequest request)
    {
        return await _entity.Create(_entity, _context, request);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return await _entity.Get(_context, id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return await _entity.GetAll(_context);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ChoreRequest request)
    {
        return await _entity.Update(_context, request, id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await _entity.Delete(_context, id);
    }

}
