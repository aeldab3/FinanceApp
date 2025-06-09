using FinanceApp.Data;
using FinanceApp.Data.Service;
using FinanceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly IExpensesService _expensesService;
        public ExpensesController(IExpensesService expensesService)
        {
            _expensesService = expensesService;
        }
        public async Task<IActionResult> Index()
        {
            var expenses = await _expensesService.GetAllExpensesAsync();
            return View(expenses);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Expense expense)
        {
            if (ModelState.IsValid)
            {
                await _expensesService.AddExpenseAsync(expense);
                return RedirectToAction("Index");
            }
            return View(expense);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var expense = await _expensesService.GetExpenseByIdAsync(id);
            if (expense == null)
            {
                return NotFound();
            }
            return View(expense);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Expense expense)
        {
            if (expense == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _expensesService.UpdateExpenseAsync(expense);
                return RedirectToAction("Index");
            }
            return View(expense);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var expense = await _expensesService.GetExpenseByIdAsync(id);

            if (expense == null)
                return NotFound();

            return View(expense);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _expensesService.DeleteExpenseAsync(id);
                return RedirectToAction("Index");
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        public IActionResult GetChart()
        {
            var chartData = _expensesService.GetChartData();
            return Json(chartData);
        }
    }
}
