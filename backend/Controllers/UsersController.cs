using backend.Data;
using backend.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly DataContext _context;
    private UserEntity _entity;
    private IConfiguration _configuration;

    public UsersController(DataContext context, IConfiguration configuration)
    {
        _context = context;
        _entity = new UserEntity();
        _configuration = configuration;
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserRequest request)
    {
        return await _entity.Create(_context, _entity, request, _configuration);
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        return await _entity.Login(_context, request, _configuration);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return await _entity.GetAll(_context);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAll(int id)
    {
        return await _entity.Get(_context, id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UserRequest request)
    {
        return await _entity.Update(_context, request, id);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        return await _entity.Delete(_context, id);
    }


}
