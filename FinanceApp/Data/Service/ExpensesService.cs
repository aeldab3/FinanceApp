using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Data.Service
{
    public class ExpensesService : IExpensesService
    {
        private readonly FinanceAppContext _context;

        public ExpensesService(FinanceAppContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Expense>> GetAllExpensesAsync()
        {
            var expenses = await _context.Expenses.ToListAsync();
            return expenses;
        }

        public async Task AddExpenseAsync(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
        }

        public IQueryable GetChartData()
        {
            var chartData = _context.Expenses
                .GroupBy(e => e.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    TotalAmount = g.Sum(e => e.Amount)
                });
            return chartData;
        }

        public Task<Expense?> GetExpenseByIdAsync(int id)
        {
            var expense = _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
            return expense;
        }
        public Task UpdateExpenseAsync(Expense expense)
        {
            if (expense == null)
            {
                throw new ArgumentNullException(nameof(expense));
            }
            _context.Expenses.Update(expense);
            return _context.SaveChangesAsync();
        }

        public async Task DeleteExpenseAsync(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense == null)
            {
                throw new KeyNotFoundException($"Expense with ID {id} not found.");
            }
            _context.Expenses.Remove(expense);
            await _context.SaveChangesAsync();
        }

    }
}
