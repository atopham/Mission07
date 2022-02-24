using Microsoft.AspNetCore.Mvc;
using Mission07.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission07.Components
{
    public class CategoriesViewComponent : ViewComponent
    {
        private IBookRepository repo { get; set; }

        public CategoriesViewComponent(IBookRepository temp)
        {
            repo = temp;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["Category"];
            var categories = repo.Books.Select(x => x.Category).Distinct().OrderBy(x => x);
            return View(categories);
        }

    }
}
