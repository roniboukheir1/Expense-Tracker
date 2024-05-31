using Expense_Tracker.Data;
using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Expense_Tracker.Services;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly BreadcrumbService _breadcrumbService;

        public TransactionController(ApplicationDbContext context, BreadcrumbService breadcrumbService)
        {
            _context = context;
            _breadcrumbService = breadcrumbService;
        }

        public IActionResult Index()
        {
            var breadcrumbs = _breadcrumbService.GetBreadcrumbs("Transaction", "Index", "Index");
            ViewBag.Breadcrumbs = breadcrumbs;
            var transactions = _context.Transactions.Include(t => t.Category).ToList();
            return View(transactions);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var breadcrumbs = _breadcrumbService.GetBreadcrumbs("Transaction", "Create", "Create");
            ViewBag.Breadcrumbs = breadcrumbs;
            GetCategories();
            return View(new Transaction());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var breadcrumbs = _breadcrumbService.GetBreadcrumbs("Transaction", "Edit", "Edit");
            ViewBag.Breadcrumbs = breadcrumbs;

            var transaction = _context.Transactions.Find(id);
            if (transaction == null)
            {
                return NotFound();
            }
            GetCategories();
            return View(transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Transaction transaction)
        {

            if(id != transaction.TransactionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingTransaction = _context.Transactions.Find(id);
                if (existingTransaction == null)
                    return NotFound();
                existingTransaction.CategoryId = transaction.CategoryId;
                existingTransaction.Amount = transaction.Amount;
                existingTransaction.Note = transaction.Note;
                existingTransaction.Date = transaction.Date;
                _context.Update(existingTransaction);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            GetCategories();
            return View(transaction);
            /*            if(ModelState.IsValid)
                        {
                            _context.Update(transaction);
                            _context.SaveChanges();
                            return RedirectToAction(nameof(Index));
                        }
                        return View(transaction);
            */
        }

        public IActionResult Delete(int id)
        {
            var transaction = _context.Transactions.Find(id);
            if (transaction == null)
            {
                return NotFound();
            }
            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [NonAction]
        private void GetCategories()
        {
            List<Category> categories = _context.Categories.ToList();
            ViewBag.Categories = categories;
        }
    }
}
