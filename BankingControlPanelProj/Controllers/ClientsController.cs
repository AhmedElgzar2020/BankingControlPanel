using BankingControlPanelProj.Infrastructure;
using BankingControlPanelProj.Infrastructure.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingControlPanelProj.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            if (_context.Clients == null)
            {
                return NotFound();
            }
            return await _context.Clients.Include("Accounts").ToListAsync();
        }

        // GET: api/Clients/Search
        [HttpPost]
        [Route("Search")]
        public async Task<ActionResult<IEnumerable<Client>>> Search([FromBody]FilterModel filter)
        {
            if (_context.Clients == null)
            {
                return NotFound();
            }
            return await _context.Clients.Include("Accounts")
                .Where(x =>string.IsNullOrEmpty(filter.key)
                    ||x.FirstName == filter.key
                    || x.LastName == filter.key
                    || x.Email == filter.key
                    || x.PersonalId == filter.key)
                .Skip(filter.PageNumber * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public ActionResult<Client> GetClient(Guid id)
        {
            if (_context.Clients == null)
            {
                return NotFound();
            }
            var client = _context.Clients.Include("Accounts").FirstOrDefault(x => x.Id == id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(Guid id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }

            _context.Entry(client).State = EntityState.Modified;
            foreach (var acc in client.Accounts)
            {
                if (acc.Id == Guid.Empty)
                {
                    _context.Entry(acc).State = EntityState.Added;
                }
                else
                {
                    _context.Entry(acc).State = EntityState.Modified;
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
            if (_context.Clients == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Clients'  is null.");
            }
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.Id }, client);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(Guid id)
        {
            if (_context.Clients == null)
            {
                return NotFound();
            }
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(Guid id)
        {
            return (_context.Clients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
