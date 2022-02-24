using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Mission07.Models;
using Mission07.Models.Infrastructure;

namespace Mission07.Pages
{
    public class PurchaseModel : PageModel
    {

        private IBookRepository repo { get; set; }

        public PurchaseModel (IBookRepository temp)
        {
            repo = temp;
        }

        public Cart cart { get; set; }
        public string ReturnUrl { get; set; }
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }

        public IActionResult OnPost(int BookId, string returnUrl)
        {
            Book b = repo.Books.FirstOrDefault(x => x.BookId == BookId);
            cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            cart.AddItem(b, 1, b.Price);
            HttpContext.Session.SetJson("cart", cart);
            return RedirectToPage(new { ReturnUrl = returnUrl });
        }
    }
}