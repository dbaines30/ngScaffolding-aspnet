using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ngScaffolding.database.Models;
using ngScaffolding.database;

// following required for Async Methods
using Microsoft.AspNetCore.Authorization;

namespace ngScaffolding.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class MenuItemsController : Controller
    {
        private readonly ngScaffoldingContext _context;

        public MenuItemsController(ngScaffoldingContext context)
        {
            _context = context;
        }

        // GET: api/MenuItems
        [HttpGet]
        public IEnumerable<MenuItem> GetMenuItems()
        {
            var user = HttpContext.User;

            var test = HttpContext.User.FindFirst("roles")?.Value;

            var items = _context.MenuItems.ToLookup(c => c.ParentMenuItemId);

            foreach (var c in items)
            {
                //c.items = items[c.].ToList();
            }


            //         IEnumerable<MenuItem> nodes = items.RecursiveJoin(element => element.MenuItemId,
            //element => element.ParentMenuItemId,
            //(MenuItem element, IEnumerable<MenuItem> children) => new MenuItem(element)
            //{
            //    items = children
            //});

            //var topLevelItems = items.Where(i => i.ParentMenuItemId == null).OrderBy(i => i.ItemOrder).ToList();
            //var childItems = items.Where(i => i.ParentMenuItemId != null).OrderBy(i => i.MenuItemId).ThenBy(i=> i.ItemOrder).ToList();

            //foreach(var child in childItems)
            //{
            //    var parent = topLevelItems.FirstOrDefault(i => i.MenuItemId == child.ParentMenuItemId);
            //    if(parent != null)
            //    {
            //        parent.items.Add(child);
            //    }
            //}

            return items[null].ToList();
        }

        // GET: api/MenuItems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var menuItem = await _context.MenuItems.SingleOrDefaultAsync(m => m.Id == id);

            if (menuItem == null)
            {
                return NotFound();
            }

            return Ok(menuItem);
        }

        // PUT: api/MenuItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenuItem([FromRoute] int id, [FromBody] MenuItem menuItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != menuItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(menuItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuItemExists(id))
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

        // POST: api/MenuItems
        [HttpPost]
        public async Task<IActionResult> PostMenuItem([FromBody] MenuItem menuItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MenuItems.Add(menuItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenuItem", new { id = menuItem.Id }, menuItem);
        }

        // DELETE: api/MenuItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenuItem([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var menuItem = await _context.MenuItems.SingleOrDefaultAsync(m => m.Id == id);
            if (menuItem == null)
            {
                return NotFound();
            }

            _context.MenuItems.Remove(menuItem);
            await _context.SaveChangesAsync();

            return Ok(menuItem);
        }

        private bool MenuItemExists(int id)
        {
            return _context.MenuItems.Any(e => e.Id == id);
        }
    }
}