using backend.Data;
using backend.Models.Chore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.Models.Chore.ChoreCategory;

public class ChoreCategoryEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public virtual ICollection<ChoreEntity> Chores { get; set; }


    public async Task<IActionResult> Create(ChoreCategoryEntity entity, DataContext context, ChoreCategoryRequest request)
    {
        ChoreCategoryResponse response = new ChoreCategoryResponse();

        try
        {
            entity.Name = request.Name;

            context.ChoreCategories.Add(entity);
            await context.SaveChangesAsync();

            response.Id = entity.Id;
            response.Name = entity.Name;

            return new OkObjectResult(response);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e);
        }
    }

    public async Task<IActionResult> Get(DataContext context, int id)
    {
        ChoreCategoryResponse _response = new();
        ChoreCategoryEntity? _entity;

        try
        {
            _entity = await context.ChoreCategories.FirstOrDefaultAsync(x => x.Id == id);

            if (_entity == null)
            {
                return new BadRequestObjectResult("could not find category");
            }
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e);
        }

        _response.Id = _entity.Id;
        _response.Name = _entity.Name;

        return new OkObjectResult(_response);
    }

    public async Task<IActionResult> GetAll(DataContext context)
    {

        List<ChoreCategoryResponse> _responseList = new List<ChoreCategoryResponse>();
        List<ChoreCategoryEntity> _entities;

        try
        {
            _entities = await context.ChoreCategories.ToListAsync();


            foreach (ChoreCategoryEntity _entity in _entities)
            {
                ChoreCategoryResponse response = new()
                {
                    Id = _entity.Id,
                    Name = _entity.Name
                };


                _responseList.Add(response);
            }

            return new OkObjectResult(_responseList);
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e);
        }
    }

    public async Task<IActionResult> Update(DataContext context, ChoreCategoryRequest request, int id)
    {
        try
        {
            ChoreCategoryEntity? _entity = await context.ChoreCategories.FindAsync(id);

            if (_entity == null)
            {
                return new BadRequestObjectResult("could not find category");
            }

            _entity.Name = request.Name;

            context.ChoreCategories.Update(_entity);

            ChoreCategoryResponse response = new()
            {
                Id = _entity.Id,
                Name = _entity.Name
            };

            var result = await context.SaveChangesAsync();

            return new OkObjectResult(response);
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
            ChoreCategoryEntity? _entity = await context.ChoreCategories.FindAsync(id);

            if (_entity == null)
            {
                return new BadRequestObjectResult("could not find category");
            }

            context.ChoreCategories.Remove(_entity);
            var result = await context.SaveChangesAsync();

            return new OkObjectResult($"{result} total categories deleted");
        }
        catch (Exception e)
        {
            return new BadRequestObjectResult(e);
        }
    }
}
