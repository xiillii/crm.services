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

namespace Gui.Crm.Services.Hosts.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly CrmDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(CrmDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<CategoriesResponse>> GetCategories([FromQuery] string code = null,
            [FromQuery] string name = null, [FromQuery] bool? status = null)
        {
            var lst = await _context.Categories.Where(c =>
                (code == null || c.Code == code) && (name == null || c.Name == name) &&
                (status == null || c.Status == status)).ToListAsync();
            

            var categories = _mapper.Map<List<DtoCategory>>(lst);
            return new CategoriesResponse
            {
                Data = categories,
                ResponseId = Guid.NewGuid(),
                Date = DateTimeOffset.UtcNow,
                Status = Status.Succeeded
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


            return new CategoryResponse
            {
                Data = c,
                Date = DateTimeOffset.UtcNow,
                ResponseId = Guid.NewGuid(),
                Status = Status.Succeeded
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

            var getResponse = new CategoryResponse
            {
                Data = _mapper.Map<DtoCategory>(cat),
                Date = DateTimeOffset.UtcNow,
                ResponseId = Guid.NewGuid(),
                Status = Status.Succeeded
            };

            return CreatedAtAction("GetCategory", new { id = cat.CategoryId }, getResponse);
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
