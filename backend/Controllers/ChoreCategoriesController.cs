using backend.Data;
using backend.Models.Chore.ChoreCategory;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChoreCategoriesController : ControllerBase
{

    private readonly DataContext _context;
    private ChoreCategoryEntity _entity;

    public ChoreCategoriesController(DataContext context)
    {
        _context = context;
        _entity = new ChoreCategoryEntity();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ChoreCategoryRequest request)
    {
        return await _entity.Create(_entity, _context, request);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        return await _entity.Get(_context, id);
    }

    [HttpPut]
    public async Task<IActionResult> Update(int id, ChoreCategoryRequest request)
    {
        return await _entity.Update(_context, request, id);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return await _entity.GetAll(_context);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await _entity.Delete(_context, id);
    }
}
