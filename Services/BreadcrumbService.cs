using Expense_Tracker.Models;

namespace Expense_Tracker.Services
{
    public class BreadcrumbService
    {
        public List<BreadcrumbItem> GetBreadcrumbs(string controller, string action, string title)
        {
            var breadcrumbs = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Title = "Home", Action = "Index", Controller = "Home", IsActive = false },
                new BreadcrumbItem { Title = controller, Action = "Index", Controller = controller, IsActive = false },
                new BreadcrumbItem { Title = title, Action = action, Controller = controller, IsActive = true }
            };

            return breadcrumbs;
        }
    }
}
