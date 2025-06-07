using FinanceApp.Models;

namespace FinanceApp.Data.Service
{
    public interface IExpensesService
    {
        Task<IEnumerable<Expense>> GetAllExpensesAsync();
        Task AddExpenseAsync(Expense expense);
        //Task<Expense?> GetExpenseByIdAsync(int id);
        //Task UpdateExpenseAsync(Expense expense);
        //Task DeleteExpenseAsync(int id);
    }
}
