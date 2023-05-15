using backend.Data;
using backend.Models.Chore.ChoreCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models.Chore;

public class ChoreEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    [Required]
    public int BaseXp { get; set; }
    [Required]
    [Column(TypeName = "tinyint")]
    public bool IsTimebased { get; set; }
    [Required]
    public int ChoreCategoryId { get; set; }
    public virtual ChoreCategoryEntity ChoreCategory { get; set; }

    public async Task<IActionResult> Create(ChoreEntity entity, DataContext context, ChoreRequest request)
    {


        try
        {
            var _category = await context.ChoreCategories.FindAsync(request.ChoreCategoryId);

            if (_category == null)
            {
                return new BadRequestObjectResult("could not find category");
            }

            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.BaseXp = request.BaseXp;
            entity.IsTimebased = request.IsTimebased;
            entity.ChoreCategoryId = request.ChoreCategoryId;

            context.Chores.Add(entity);
            await context.SaveChangesAsync();


            ChoreResponse _response = new()
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                BaseXp = entity.BaseXp,
                IsTimebased = entity.IsTimebased,
                ChoreCategoryId = entity.ChoreCategoryId,
                ChoreCategoryName = _category.Name,

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
            var _entity = await context.Chores.FindAsync(id);

            if (_entity == null)
            {
                return new BadRequestObjectResult("could not find the chore");
            }


            var _category = await context.ChoreCategories.FindAsync(_entity.ChoreCategoryId);

            if (_category == null)
            {
                return new BadRequestObjectResult("could not find category");
            }

            ChoreResponse _response = new()
            {
                Id = _entity.Id,
                Name = _entity.Name,
                Description = _entity.Description,
                BaseXp = _entity.BaseXp,
                IsTimebased = _entity.IsTimebased,
                ChoreCategoryId = _entity.ChoreCategoryId,
                ChoreCategoryName = _category.Name,

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
        List<ChoreResponse> _choreResponseList = new List<ChoreResponse>();
        List<ChoreEntity> _entities;

        try
        {
            _entities = await context.Chores.ToListAsync();

            foreach (var _entity in _entities)
            {
                var _category = await context.ChoreCategories.FindAsync(_entity.ChoreCategoryId);

                ChoreResponse _response = new()
                {
                    Id = _entity.Id,
                    Name = _entity.Name,
                    Description = _entity.Description,
                    BaseXp = _entity.BaseXp,
                    IsTimebased = _entity.IsTimebased,
                    ChoreCategoryId = _entity.ChoreCategoryId,
                    ChoreCategoryName = _category.Name,

                };

                _choreResponseList.Add(_response);

            }

            return new OkObjectResult(_choreResponseList);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e);
        }
    }

    public async Task<IActionResult> Update(DataContext context, ChoreRequest request, int id)
    {


        try
        {
            var _entity = await context.Chores.FindAsync(id);

            if (_entity == null)
            {
                return new BadRequestObjectResult("could not find the chore");
            }


            var _category = await context.ChoreCategories.FindAsync(request.ChoreCategoryId);

            if (_category == null)
            {
                return new BadRequestObjectResult("could not find the category");
            }


            _entity.Name = request.Name;
            _entity.Description = request.Description;
            _entity.BaseXp = request.BaseXp;
            _entity.IsTimebased = request.IsTimebased;
            _entity.ChoreCategoryId = request.ChoreCategoryId;

            context.Chores.Update(_entity);
            await context.SaveChangesAsync();


            ChoreResponse _response = new()
            {
                Id = _entity.Id,
                Name = _entity.Name,
                Description = _entity.Description,
                BaseXp = _entity.BaseXp,
                IsTimebased = _entity.IsTimebased,
                ChoreCategoryId = _entity.ChoreCategoryId,
                ChoreCategoryName = _category.Name,

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
            var _entity = await context.Chores.FindAsync(id);

            if (_entity == null)
            {
                return new BadRequestObjectResult("could not find the chore");
            }

            context.Chores.Remove(_entity);
            var result = await context.SaveChangesAsync();


            return new OkObjectResult($"{result} total chores deleted");
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e);
        }
    }
}
