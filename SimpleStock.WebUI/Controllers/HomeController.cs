using Microsoft.AspNetCore.Mvc;
using SimpleStock.Application.Interfaces;
using SimpleStock.WebUI.Models;
using System.Diagnostics;

namespace SimpleStock.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(IProductService productService, ICategoryService categoryService,ILogger<HomeController> logger)
        {
            _productService = productService;
            _categoryService = categoryService; 
            _logger = logger;
        } 
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();
             
            ViewBag.TotalProducts = products.Count;
            ViewBag.TotalCategories = categories.Count;           

            return View();
        }
     

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
  

}
