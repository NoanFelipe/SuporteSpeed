using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuporteSpeed.API.Data;
using SuporteSpeed.API.DTOs.SupportTicket;
using SuporteSpeed.API.Services.AI;
using SuporteSpeed.API.Static;
using System.Security.Claims;

namespace SuporteSpeed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupportTicketsController : ControllerBase
    {
        private readonly SuporteSpeedDbContext _context;
        private readonly IMapper mapper;
        private readonly IGeminiService _geminiService;

        public SupportTicketsController(SuporteSpeedDbContext context, IMapper mapper, IGeminiService geminiService)
        {
            _context = context;
            this.mapper = mapper;
            this._geminiService = geminiService;
        }

        // GET: api/SupportTickets
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<SupportTicketReadOnlyDto>>> GetSupportTickets()
        {
            var userId = User.FindFirstValue(CustomClaimTypes.Uid);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var query = _context.SupportTickets.AsQueryable();

            if (!User.IsInRole("Administrator"))
            {
                query = query.Where(t => t.UserId == userId);
            }

            var supportTickets = await query
                .Include(q => q.User)
                .ProjectTo<SupportTicketReadOnlyDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(supportTickets);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<SupportTicketDetailsDto>> GetSupportTicket(int id)
        {
            var userId = User.FindFirstValue(CustomClaimTypes.Uid);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var query = _context.SupportTickets.AsQueryable();

            if (!User.IsInRole("Administrator"))
            {
                query = query.Where(t => t.UserId == userId);
            }

            var supportTicket = await query
                .Include(q => q.User)
                .ProjectTo<SupportTicketDetailsDto>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (supportTicket == null)
            {
                return NotFound();
            }

            return supportTicket;
        }

        // PUT: api/SupportTickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupportTicket(int id, [FromBody] SupportTicketUpdateDto supportTicketDto)
        {
            if (id != supportTicketDto.Id)
            {
                return BadRequest("Wrong Id!");
            }

            var supportTicket = await _context.SupportTickets.FindAsync(id);

            if (supportTicket == null) { return NotFound(); }

            mapper.Map(supportTicketDto, supportTicket);
            _context.Entry(supportTicket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupportTicketExists(id))
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

        // POST: api/SupportTickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<SupportTicketCreateDto>> PostSupportTicket([FromBody] SupportTicketCreateDto supportTicketDto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            supportTicketDto.UserId = userId;

            if (userId == null)
            {
                return BadRequest("UserId from token is NULL — the user is not authenticated or token missing NameIdentifier claim.");
            }

            var supportTicket = mapper.Map<SupportTicket>(supportTicketDto);
            _context.SupportTickets.Add(supportTicket);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSupportTicket), new { id = supportTicket.Id }, supportTicket);
        }

        // DELETE: api/SupportTickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupportTicket(int id)
        {
            var supportTicket = await _context.SupportTickets.FindAsync(id);
            if (supportTicket == null)
            {
                return NotFound();
            }

            _context.SupportTickets.Remove(supportTicket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SupportTicketExists(int id)
        {
            return _context.SupportTickets.Any(e => e.Id == id);
        }

        [HttpGet]
        [Route("ai-view")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<SupportTicketAiViewDto>>> GetTicketsWithAi()
        {
            var userId = User.FindFirstValue(CustomClaimTypes.Uid);

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var query = _context.SupportTickets.AsQueryable();

            if (!User.IsInRole("Administrator"))
            {
                query = query.Where(t => t.UserId == userId);
            }

            const string ErrorSignature = "Erro ao comunicar com a IA:";

            var ticketsFromDb = await query
                .Include(t => t.Airesponses)
                .ToListAsync();

            var results = new List<SupportTicketAiViewDto>();
            bool changesMade = false;

            foreach (var ticket in ticketsFromDb)
            {
                var lastAiResponseEntity = ticket.Airesponses
                    .OrderByDescending(a => a.RespondedAt)
                    .FirstOrDefault();

                string aiResponseText = lastAiResponseEntity?.Message;

                if (string.IsNullOrEmpty(aiResponseText) || aiResponseText.StartsWith(ErrorSignature))
                {
                    string newResponseText = await _geminiService.GenerateTicketResponseAsync(
                        ticket.Title,
                        ticket.Field,
                        ticket.Description
                    );

                    var newAiResponseEntity = new Airesponse
                    {
                        TicketId = ticket.Id,
                        ModelName = "gemini-2.0-flash",
                        Message = newResponseText,
                        RespondedAt = DateTime.UtcNow
                    };

                    ticket.Airesponses.Add(newAiResponseEntity);

                    aiResponseText = newResponseText;
                    changesMade = true;
                }

                var dto = new SupportTicketAiViewDto
                {
                    TicketId = ticket.Id,
                    Title = ticket.Title,
                    Description = ticket.Description ?? string.Empty,
                    Category = ticket.Field,
                    AiResponse = aiResponseText,
                    LastResponseDate = lastAiResponseEntity?.RespondedAt
                };

                results.Add(dto);
            }

            if (changesMade)
            {
                await _context.SaveChangesAsync();
            }

            return Ok(results);
        }
    }
}