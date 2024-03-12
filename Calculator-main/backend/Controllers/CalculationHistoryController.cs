using backend.Data;
using backend.DTO;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculationHistoryController : ControllerBase
    {
        private readonly CalculatorDbContext _dbContext;

        public CalculationHistoryController(CalculatorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CalculationHistoryDto>> GetHistoryById(Guid id)
        {
            var history = await _dbContext.CalculationHistories.FindAsync(id);

            if (history == null)
            {
                return NotFound();
            }

            var historyDto = new CalculationHistoryDto
            {
                Id = history.Id,
                Expression = history.Expression,
                Result = history.Result,
                DateCreated = history.DateCreated,
            };

            return Ok(historyDto);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalculationHistory>>> GetAllHistories()
        {
            var histories = await _dbContext.CalculationHistories.ToListAsync();
            return Ok(histories);
        }

        [HttpPost]
        public async Task<ActionResult<CalculationHistoryDto>> CreateHistory(CreateCalculationHistoryDto createDto)
        {
            var history = new CalculationHistory
            {
                Expression = createDto.Expression,
                Result = createDto.Result,
                DateCreated = DateTime.UtcNow
            };

            _dbContext.CalculationHistories.Add(history);
            await _dbContext.SaveChangesAsync();

            var historyDto = new CalculationHistoryDto
            {
                Id = history.Id,
                Expression = history.Expression,
                Result = history.Result,
                DateCreated = history.DateCreated,
            };

            return CreatedAtAction(nameof(GetHistoryById), new { id = history.Id }, historyDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoryById(Guid id)
        {
            var history = await _dbContext.CalculationHistories.FindAsync(id);

            if (history == null)
            {
                return NotFound();
            }

            _dbContext.CalculationHistories.Remove(history);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllHistory()
        {
            var historyRecords = _dbContext.CalculationHistories;

            _dbContext.CalculationHistories.RemoveRange(historyRecords);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }


    }
}
