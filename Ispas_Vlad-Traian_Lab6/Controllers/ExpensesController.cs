using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ispas_Vlad_Traian_Lab6.Models;

namespace Ispas_Vlad_Traian_Lab6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly ExpenseContext _context;

        public ExpensesController(ExpenseContext context)
        {
            _context = context;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseDTO>>> GetExpense()
        {
          if (_context.ExpenseDTO == null)
          {
              return NotFound();
          }
            return await _context.ExpenseDTO.ToListAsync();
        }

        // GET: api/Expenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpenseDTO>> GetExpense(int id)
        {
          if (_context.ExpenseDTO == null)
          {
              return NotFound();
          }
            var expense = await _context.ExpenseDTO.FindAsync(id);

            if (expense == null)
            {
                return NotFound();
            }

            return expense;
        }

        // PUT: api/Expenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpense(int id, ExpenseDTO expense)
        {
            if (id != expense.ID)
            {
                return BadRequest();
            }

            _context.Entry(expense).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseExists(id))
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

        // POST: api/Expenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExpenseDTO>> PostExpense(ExpenseDTO expense)
        {
          if (_context.ExpenseDTO == null)
          {
              return Problem("Entity set 'ExpenseContext.Expense'  is null.");
          }
            _context.ExpenseDTO.Add(expense);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExpense), new { id = expense.ID }, expense);
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            if (_context.ExpenseDTO == null)
            {
                return NotFound();
            }
            var expense = await _context.ExpenseDTO.FindAsync(id);
            if (expense == null)
            {
                return NotFound();
            }

            _context.ExpenseDTO.Remove(expense);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpenseExists(int id)
        {
            return (_context.ExpenseDTO?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
