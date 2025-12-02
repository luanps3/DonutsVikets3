using System.Diagnostics;
using DonutsVikets3.Models;
using Microsoft.AspNetCore.Mvc;
using DonutsVikets3.Data;
using DonutsVikets.Models;
using Microsoft.EntityFrameworkCore;

namespace DonutsVikets3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DonutsVikets3Context _context;

        public HomeController(ILogger<HomeController> logger, DonutsVikets3Context context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var produtos = await _context.Produto.Include(p => p.Categoria).ToListAsync();
            return View(produtos);
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
