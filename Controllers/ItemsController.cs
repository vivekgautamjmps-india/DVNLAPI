using DVNLAPI.Models;
using DVNLAPI.Services;
using Microsoft.AspNetCore.Mvc;
using DVNLAPI;

namespace DVNLAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        // GET: api/items
        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetAll()
        {
            return Ok(_itemService.GetAll());
        }

        // GET: api/items/5
        [HttpGet("{id:int}")]
        public ActionResult<Item> GetById(int id)
        {
            var item = _itemService.GetById(id);
            if (item is null)
                return NotFound(new { message = $"Item with id {id} not found." });

            return Ok(item);
        }

        // POST: api/items
        [HttpPost]
        public ActionResult<Item> Create([FromBody] ItemCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest(new { message = "Name is required." });

            var created = _itemService.Add(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/items/5
        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] ItemUpdateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest(new { message = "Name is required." });

            var updated = _itemService.Update(id, dto);
            if (!updated)
                return NotFound(new { message = $"Item with id {id} not found." });

            return NoContent();
        }

        // DELETE: api/items/5
        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var deleted = _itemService.Delete(id);
            if (!deleted)
                return NotFound(new { message = $"Item with id {id} not found." });

            return NoContent();
        }
    }
}
