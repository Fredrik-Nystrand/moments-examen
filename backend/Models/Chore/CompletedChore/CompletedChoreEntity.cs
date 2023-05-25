using backend.Data;
using backend.Models.User;
using backend.Models.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace backend.Models.Chore.CompletedChore;

public class CompletedChoreEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int CompletedXp { get; set; }
    [Required]
    public DateTime DateCompleted { get; set; }
    [Required]
    public string? Amount { get; set; } = null;
    [Required]
    public int ChoreId { get; set; }
    public virtual ChoreEntity Chore { get; set; }
    [Required]
    public int UserId { get; set; }
    public virtual UserEntity User { get; set; }



    public async Task<IActionResult> Create(DataContext context, CompletedChoreEntity newEntity, CompletedChoreRequest request)
    {

        try
        {
            var _chore = await context.Chores.FindAsync(request.ChoreId);
            if (_chore == null)
            {
                return new BadRequestObjectResult("could not find the chore");
            }

            if (_chore.IsTimebased)
            {
                if (request.Amount != null && request.Amount != "" && double.Parse(request.Amount.Replace(".", ",")) > 0)
                {
                    newEntity.Amount = request.Amount;

                    double amount = double.Parse(request.Amount.Replace(".", ","));
                    var calcXP = amount * _chore.BaseXp;
                    var rounded = (int)Math.Round(calcXP);

                    newEntity.CompletedXp = rounded;
                }


            }
            else
            {
                newEntity.Amount = "";
                newEntity.CompletedXp = _chore.BaseXp;
            }


            newEntity.DateCompleted = DateTime.Parse(request.DateCompleted);
            newEntity.ChoreId = request.ChoreId;
            newEntity.UserId = request.UserId;

            context.CompletedChores.Add(newEntity);
            await context.SaveChangesAsync();

            var _entity = await context.CompletedChores.Include(x => x.User).Include(x => x.Chore).FirstOrDefaultAsync(x => x.Id == newEntity.Id);
            var _user = _entity.User;

            int newXp = _user.Xp + _entity.CompletedXp;

            LevelHandler levelHandler = new LevelHandler(newXp, _user.Level);

            _user.Xp = levelHandler.XpRemainder;
            _user.Level = levelHandler.Level;

            if (levelHandler.LeveledUp)
            {
                _user.Currency = _user.Currency + 1;
            }

            context.Users.Update(_user);
            await context.SaveChangesAsync();

            CompletedChoreResponse _response = new()
            {
                Id = _entity.Id,
                CompletedXp = _entity.CompletedXp,
                DateCompleted = _entity.DateCompleted,
                ChoreId = _entity.ChoreId,
                ChoreName = _entity.Chore.Name,
                UserId = _entity.UserId,
                UserName = $"{_entity.User.Firstname} {_entity.User.Lastname}",
                Amount = _entity.Amount,
                IsTimebased = _entity.Chore.IsTimebased


            };

            return new OkObjectResult(_response);
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
            var _entity = await context.CompletedChores.Include(x => x.User).Include(x => x.Chore).FirstOrDefaultAsync(x => x.Id == id);

            if (_entity == null)
            {
                return new BadRequestObjectResult("could not find the completed chore");
            }

            if (_entity.User == null)
            {
                return new BadRequestObjectResult("could not find the user");
            }

            if (_entity.Chore == null)
            {
                return new BadRequestObjectResult("could not find the Chore");
            }

            CompletedChoreResponse _response = new()
            {
                Id = _entity.Id,
                CompletedXp = _entity.CompletedXp,
                DateCompleted = _entity.DateCompleted,
                ChoreId = _entity.ChoreId,
                ChoreName = _entity.Chore.Name,
                UserId = _entity.UserId,
                UserName = $"{_entity.User.Firstname} {_entity.User.Lastname}",
                Amount = _entity.Amount,
                IsTimebased = _entity.Chore.IsTimebased
            };

            return new OkObjectResult(_response);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e);
        }
    }

    public async Task<IActionResult> GetAllForUser(DataContext context, int id)
    {
        List<CompletedChoreResponse> _responseList = new List<CompletedChoreResponse>();
        List<CompletedChoreEntity> _completedChores;
        try
        {
            var _user = await context.Users.FindAsync(id);

            if (_user == null)
            {
                return new BadRequestObjectResult("could not find the user");
            }

            _completedChores = await context.CompletedChores.ToListAsync();

            foreach (CompletedChoreEntity _entity in _completedChores)
            {
                if (_entity.UserId != _user.Id) continue;
                var _chore = await context.Chores.FindAsync(_entity.ChoreId);
                CompletedChoreResponse _response = new()
                {
                    Id = _entity.Id,
                    CompletedXp = _entity.CompletedXp,
                    DateCompleted = _entity.DateCompleted,
                    ChoreId = _entity.ChoreId,
                    ChoreName = _chore.Name,
                    UserId = _user.Id,
                    UserName = $"{_user.Firstname} {_user.Lastname}",
                    Amount = _entity.Amount,
                    IsTimebased = _chore.IsTimebased

                };

                _responseList.Add(_response);

            }


            return new OkObjectResult(_responseList);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e);
        }
    }
}
