using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOUQ2.Models;
using SOUQ2.ViewModels;

namespace SOUQ2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        SouqContext db = new SouqContext();

        var model = new HomeViewModel
        {
            Categories = db.Categories.ToList(),
            Services = db.Services.ToList(),
            Products = db.Products.Include(p => p.Cat).ToList()
        };

        return View(model);
    }
    public IActionResult Services()
    {
        SouqContext db = new SouqContext();
        var serv = db.Services.ToList();
        return View(serv);
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Portfolio()
    {

        SouqContext db = new SouqContext();

        var cat = db.Categories.ToList();

        var products = db.Products.Include(p => p.Cat).ToList();

        ViewBag.Products = products;

        return View(cat);
    }
    public IActionResult Categories(int id)
    {
        SouqContext db = new SouqContext();
        var cate = db.Categories.ToList();
        return View(cate);
    }
    public IActionResult Product(int id)
    {
        SouqContext db = new SouqContext();
        var products = db.Products
                     .Where(p => p.Catid == id)
                     .Include(p => p.Cat)
                     .ToList();

        return View(products);

    }
    [HttpGet]
    public IActionResult ProductSearch(String xname)
    {
        SouqContext db = new SouqContext();
        var products = db.Products
                     .Where(x => x.Name.Contains(xname))
                     .ToList();

        return View(products);


    }
    [HttpPost]
    public IActionResult Sendreview(Review model)
    {
        SouqContext db = new SouqContext();
        db.Reviews.Add(new Review { Name = model.Name, Email = model.Email, Subject = model.Subject, Description = model.Description });
        db.SaveChanges();

        return RedirectToAction("index");


    }
    public IActionResult ProductDetails(int id)
    {
        SouqContext db = new SouqContext();


        //var product = db.Products.FirstOrDefault(x => x.Id == id);

        //return View(product);
       
            var product = db.Products
                    .Include(p => p.Cat)
                    .Include(p => p.Productimages)
                    .FirstOrDefault(p => p.Id == id);
        if (product == null)
            {
                return Content("المنتج مش موجود 😥");
            }

            return View(product);
        }
    public IActionResult Cart()
    {
        SouqContext db = new SouqContext();
        string userId = "1"; // مؤقت

        var cartItems = db.Carts
            .Include(c => c.Product)
            .Where(c => c.Userid == userId)
            .ToList();

        return View(cartItems); // رجع Cart العادية
    }

    [HttpPost]
    public IActionResult AddToCart(int productId, int qty)
    {
        SouqContext db = new SouqContext();
        string userId = "1"; // مؤقت لحد ما تعملي login system

        var existingItem = db.Carts.FirstOrDefault(c => c.Userid == userId && c.Productid == productId);

        if (existingItem != null)
        {
            existingItem.Qty += qty;
        }
        else
        {
            var newCartItem = new Cart
            {
                Userid = userId,
                Productid = productId,
                Qty = qty
            };
            db.Carts.Add(newCartItem);
        }

        db.SaveChanges();  // لازم تتنفذ قبل رجوع النتيجة

        int totalQuantity = db.Carts.Where(c => c.Userid == userId).Sum(c => c.Qty) ?? 0;

        return Json(new { cartCount = totalQuantity });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult RemoveFromCart(int id)
    {
        SouqContext db = new SouqContext();

        var cartItem = db.Carts.FirstOrDefault(c => c.Id == id);
        if (cartItem != null)
        {
            db.Carts.Remove(cartItem);
            db.SaveChanges();
        }

        return NoContent();
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}