﻿
namespace Expense_Tracker.Models
{
    public class BreadcrumbItem
    {
        public string Title { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public bool IsActive { get; set; }

    }
}
