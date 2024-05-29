using Expense_Tracker.Data;
using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Expense_Tracker.Services;

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
            List<Transaction> transactions = _context.Transactions.ToList();
            return View(transactions);
        }

        // GET: Category/Create
        [HttpGet]
        public IActionResult Create()
        {
            var breadcrumbs = _breadcrumbService.GetBreadcrumbs("Transaction", "Create", "Create");
            ViewBag.Breadcrumbs = breadcrumbs;

            return View(new Category());
        }

        // POST: Category/Create
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
            return View();
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
            Transaction? transaction= _context.Transactions.Find(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return View(transaction);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Update(transaction);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(transaction);
        }

        public IActionResult Delete(int id)
        {
            Transaction? transaction= _context.Transactions.Find(id);
            if (transaction == null)
            {
                return NotFound();
            }
            _context.Transactions.Remove(transaction);
            _context.SaveChanges();
            return RedirectToAction("Index", "Category");
        }
    }
}
