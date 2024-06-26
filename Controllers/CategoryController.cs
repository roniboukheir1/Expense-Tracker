﻿using Expense_Tracker.Data;
using Expense_Tracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Expense_Tracker.Services;

namespace Expense_Tracker.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly BreadcrumbService _breadcrumbService;

        public CategoryController(ApplicationDbContext context, BreadcrumbService breadcrumbService)
        {
            _context = context;
            _breadcrumbService = breadcrumbService;
        }

        // GET: Category
        public IActionResult Index()
        {
            var breadcrumbs = _breadcrumbService.GetBreadcrumbs("Category", "Index", "Index");
            ViewBag.Breadcrumbs = breadcrumbs;
            List<Category> categories = _context.Categories.ToList();
            return View(categories);
        }

        // GET: Category/Create
        [HttpGet]
        public IActionResult Create()
        {
            var breadcrumbs = _breadcrumbService.GetBreadcrumbs("Category", "Create", "Create");
            ViewBag.Breadcrumbs = breadcrumbs;

            return View(new Category());
        }

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {

                _context.Add(category);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: Category/Edit/5
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var breadcrumbs = _breadcrumbService.GetBreadcrumbs("Category", "Edit", "Edit");
            ViewBag.Breadcrumbs = breadcrumbs;
            Category? category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Update(category);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        public IActionResult Delete(int id)
        {
            Category? category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index", "Category");
        }
    }
}
