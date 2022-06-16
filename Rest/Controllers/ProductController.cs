using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rest.Data;
using Rest.Models;

namespace Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [CostumException]
    public class ProductController : ControllerBase
    {
        private ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }
        

        
        /*[ProducesResponseType(200, Type = typeof(IEnumerable<Product>))]*/

        /*[HttpGet]

        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        */

        [HttpPost]
        public void Create([FromForm] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(long id, [FromBody] Product product)
        {
            if (id != product.id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();


            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.id))
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

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(404)]
        public IActionResult Details(int id)
        {
            Product product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        //TO FIND HOW MANY CHARACTERS ARE IN DESCRIPTION
        //JUST A TASK HAH
        /*[HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(404)]
        public IActionResult Details(int id, char name)
        {

            Product product = _context.Products.Find(id);

            var ch=name;

            int e=product.Description.Count(name => (name == ch));

            return Ok(e);
        }
        */

        private bool ProductExists(long id)
        {
            return _context.Products.Any(p => p.id == id);
        }


    }
}
