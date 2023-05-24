using backend.Data;
using backend.Models.Chore.CompletedChore;
using backend.Models.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace backend.Models.User;

public class UserEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Firstname { get; set; } = string.Empty;
    [Required]
    public string Lastname { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public byte[]? PasswordHash { get; set; }
    [Required]
    public byte[]? PasswordSalt { get; set; }
    public int Xp { get; set; } = 0;
    public int Currency { get; set; } = 0;
    public int Level { get; set; } = 1;
    public virtual ICollection<CompletedChoreEntity>? CompletedChores { get; set; }

    public async Task<IActionResult> Create(DataContext context, UserEntity entity, UserRequest request, IConfiguration configuration)
    {
        var password = new SecurePassword(request.Password);


        entity.Firstname = request.Firstname;
        entity.Lastname = request.Lastname;
        entity.Email = request.Email;
        entity.PasswordHash = password.PasswordHash;
        entity.PasswordSalt = password.PasswordSalt;
        entity.Xp = Xp;
        entity.Currency = Currency;
        entity.Level = Level;


        try
        {
            var _existingUser = await context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (_existingUser != null)
            {
                return new BadRequestObjectResult("email already exists");
            }

            context.Users.Add(entity);
            await context.SaveChangesAsync();

            UserResponseWithToken _response = new()
            {
                Id = entity.Id,
                Firstname = entity.Firstname,
                Lastname = entity.Lastname,
                Email = entity.Email,
                Xp = entity.Xp,
                Currency = entity.Currency,
                Level = entity.Level,
                Token = new TokenHandler($"{entity.Firstname} {entity.Lastname}", entity.Email, configuration).Token
            };


            return new OkObjectResult(_response);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e);
        }
    }

    public async Task<IActionResult> GetAll(DataContext context)
    {
        List<UserResponse> _userResponseList = new List<UserResponse>();
        List<UserEntity> _entities;

        try
        {
            _entities = await context.Users.ToListAsync();

            foreach (var _entity in _entities)
            {
                UserResponse _response = new()
                {
                    Id = _entity.Id,
                    Firstname = _entity.Firstname,
                    Lastname = _entity.Lastname,
                    Email = _entity.Email,
                    Xp = _entity.Xp,
                    Currency = _entity.Currency,
                    Level = _entity.Level
                };

                _userResponseList.Add(_response);

            }

            return new OkObjectResult(_userResponseList);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e);
        }
    }

    public async Task<IActionResult> Get(DataContext context, int id)
    {

        try
        {
            var _entity = await context.Users.FindAsync(id);

            if (_entity == null)
            {
                return new BadRequestObjectResult("could not find the user");
            }

            var _xpHandler = new XpHandler();
            var _calculatedXp = await _xpHandler.CalculateXpValues(context, _entity.Id);

            UserResponse _response = new()
            {
                Id = _entity.Id,
                Firstname = _entity.Firstname,
                Lastname = _entity.Lastname,
                Email = _entity.Email,
                Xp = _entity.Xp,
                Currency = _entity.Currency,
                Level = _entity.Level,
                XpToday = _calculatedXp.XpToday,
                XpLastMonth = _calculatedXp.XpLastMonth,
                XpThisMonth = _calculatedXp.XpThisMonth,

            };

            return new OkObjectResult(_response);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e);
        }
    }

    public async Task<IActionResult> Login(DataContext context, LoginRequest request, IConfiguration configuration)
    {
        try
        {
            var _entity = await context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (_entity == null)
            {
                return new BadRequestObjectResult("could not find the user");
            }

            SecurePassword securePassword = new(_entity.PasswordHash, _entity.PasswordSalt, request.Password);

            if (!securePassword.Authorized)
            {
                return new BadRequestObjectResult("wrong password");
            }


            string _fullName = $"{_entity.Firstname} {_entity.Lastname}";

            var _xpHandler = new XpHandler();
            var _calculatedXp = await _xpHandler.CalculateXpValues(context, _entity.Id);

            UserResponseWithToken _response = new()
            {
                Id = _entity.Id,
                Firstname = _entity.Firstname,
                Lastname = _entity.Lastname,
                Email = _entity.Email,
                Xp = _entity.Xp,
                Currency = _entity.Currency,
                Level = _entity.Level,
                XpToday = _calculatedXp.XpToday,
                XpLastMonth = _calculatedXp.XpLastMonth,
                XpThisMonth = _calculatedXp.XpThisMonth,
                Token = new TokenHandler(_fullName, _entity.Email, configuration).Token,

            };


            return new OkObjectResult(_response);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e);
        }
    }

    public async Task<IActionResult> Update(DataContext context, UserRequest request, int id)
    {
        var password = new SecurePassword(request.Password);


        try
        {
            var _entity = await context.Users.FindAsync(id);

            if (_entity == null)
            {
                return new BadRequestObjectResult("could not find the user");
            }

            var _existingUser = await context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (_existingUser != null)
            {
                return new BadRequestObjectResult("email already exists");
            }

            _entity.Firstname = request.Firstname;
            _entity.Lastname = request.Lastname;
            _entity.Email = request.Email;
            _entity.PasswordHash = password.PasswordHash;
            _entity.PasswordSalt = password.PasswordSalt;


            context.Users.Update(_entity);
            await context.SaveChangesAsync();

            var _xpHandler = new XpHandler();
            var _calculatedXp = await _xpHandler.CalculateXpValues(context, _entity.Id);

            UserResponse _response = new()
            {
                Id = _entity.Id,
                Firstname = _entity.Firstname,
                Lastname = _entity.Lastname,
                Email = _entity.Email,
                Xp = _entity.Xp,
                Currency = _entity.Currency,
                Level = _entity.Level,
                XpToday = _calculatedXp.XpToday,
                XpLastMonth = _calculatedXp.XpLastMonth,
                XpThisMonth = _calculatedXp.XpThisMonth,

            };


            return new OkObjectResult(_response);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e);
        }
    }

    public async Task<IActionResult> Delete(DataContext context, int id)
    {

        try
        {
            var _entity = await context.Users.FindAsync(id);

            if (_entity == null)
            {
                return new BadRequestObjectResult("could not find the user");
            }

            context.Users.Remove(_entity);
            var result = await context.SaveChangesAsync();

            return new OkObjectResult($"{result} total users deleted");
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e);
        }
    }


}
