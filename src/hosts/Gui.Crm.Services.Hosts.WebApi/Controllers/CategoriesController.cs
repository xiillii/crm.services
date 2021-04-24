using System;
using Gui.Crm.Services.Data.Entities;
using Gui.Crm.Services.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Gui.Crm.Services.Shared.Dtos;
using Gui.Crm.Services.Shared.Dtos.Requests;
using Gui.Crm.Services.Shared.Dtos.Responses;
using Microsoft.AspNetCore.Routing;

namespace Gui.Crm.Services.Hosts.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CrmDbContext _context;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public CategoriesController(CrmDbContext context, IMapper mapper, LinkGenerator linkGenerator)
        {
            _context = context;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<CategoriesResponse>> GetCategories(
            [FromQuery] string code = null,
            [FromQuery] string name = null, [FromQuery] bool? status = null, 
            [FromQuery] int? limit = null,
            [FromQuery] int? offset = null)
        {
            if ((limit != null && offset == null) || (limit == null && offset != null))
            {
                return BadRequest();
            }

            if (limit <= 0 || offset  <= 0)
            {
                return BadRequest();
            }

            List<Category> lst = null;
            ListLinks listLinks = null;

            if (limit == null)
            {
                lst = await _context.Categories.Where(c =>
                    (code == null || c.Code == code) && (name == null || c.Name == name) &&
                    (status == null || c.Status == status)).ToListAsync();
            }
            else
            {
                var myLimit = limit ?? 0;
                var myOffset = offset ?? 0;
                var count = (await _context.Categories.Where(c =>
                    (code == null || c.Code == code) && (name == null || c.Name == name) &&
                    (status == null || c.Status == status)).ToListAsync())?.Count;
                lst = await _context.Categories.Where(c =>
                    (code == null || c.Code == code) && (name == null || c.Name == name) &&
                    (status == null || c.Status == status)).Skip((myOffset - 1) * myLimit).Take(myLimit).ToListAsync();

                listLinks = new ListLinks
                {
                    Base = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}"
                };
                listLinks = CreateResponseListLinks(lst, myLimit, myOffset, count ?? 0);

            }


            var categories = _mapper.Map<List<DtoCategory>>(lst);

            var categoriesList = categories.Select(category => CreateLinksForCategory(category));
            return new CategoriesResponse
            {
                Value = categoriesList.ToList(),
                Meta = new DtoMeta
                {
                    ResponseId = Guid.NewGuid(),
                    Date = DateTimeOffset.UtcNow,
                    Status = Status.Succeeded,
                },
                Size = limit,
                Start = offset,

                _links = listLinks,
            };
        }

        private DtoCategory CreateLinksForCategory(DtoCategory category)
        {
            var objId = new {id = category.Id};
            category.Links = new List<LinkDto>();
            category.Links.Add(
                new LinkDto(_linkGenerator.GetPathByAction(nameof(this.GetCategory), "Categories", objId), "self", "GET"));
            category.Links.Add(
                new LinkDto(_linkGenerator.GetPathByAction(nameof(this.PutCategory), "Categories", objId), "update_category", "PUT"));
            category.Links.Add(
                new LinkDto(_linkGenerator.GetPathByAction(nameof(this.DeleteCategory), "Categories", objId), "delete_category", "DELETE"));

            return category;
        }

        private ListLinks CreateResponseListLinks(List<Category> lst, int limit, int offset, int count)
        {


            return new ListLinks
            {
                Base = $"{this.Request.Scheme}://{this.Request.Host}{this.Request.PathBase}",
                Context = "",
                Next = lst.Count == limit &&  offset * limit < count
                    ? this._linkGenerator.GetPathByAction(nameof(this.GetCategories), "Categories",
                        new {limit = limit, offset = offset + 1})
                    : null,
                Prev = offset > 1
                    ? _linkGenerator.GetPathByAction(nameof(this.GetCategories), "Categories",
                        new {limit = limit, offset = offset - 1})
                    : null,
                Self = _linkGenerator.GetPathByAction(nameof(this.GetCategories), "Categories",
                    new {limit = limit, offset = offset})
            };
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse>> GetCategory(long id)
        {
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            var c = _mapper.Map<DtoCategory>(category);

            c = CreateLinksForCategory(c);

            return new CategoryResponse
            {
                Value = c,
                Meta = new DtoMeta
                {
                    Date = DateTimeOffset.UtcNow,
                    ResponseId = Guid.NewGuid(),
                    Status = Status.Succeeded
                }
            };
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(long id, CategoryUpdateRequest category)
        {
            var cat = _mapper.Map<Category>(category.Data);

            cat.CategoryId = id;

            _context.Entry(cat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(CategoryAddRequest category)
        {
            var cat = _mapper.Map<Category>(category.Data);
            cat.Status = true;

            _context.Categories.Add(cat);
            await _context.SaveChangesAsync();

            var myCategory = _mapper.Map<DtoCategory>(cat);
            myCategory = CreateLinksForCategory(myCategory);
            var getResponse = new CategoryResponse
            {
                Value = myCategory,
                Meta = new DtoMeta
                {
                    Date = DateTimeOffset.UtcNow,
                    ResponseId = Guid.NewGuid(),
                    Status = Status.Succeeded
                }
            };


            return CreatedAtAction("GetCategory", new {id = cat.CategoryId}, getResponse);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(long id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
