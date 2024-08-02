using Microsoft.AspNetCore.Mvc;
using CEPAW.NewsApp.Services;
using System.Threading.Tasks;
using CEPAW.NewsApp.Models;

namespace CEPAW.NewsApp.Controllers
{
    public class HomeController : Controller
    {
        // SOLID: Dependency Injection (DIP)
        // DESIGN_PATTERN: Dependency Injection Pattern
        private readonly INewsService _newsService;
        private readonly IClimateService _climateService;
        private readonly IStocksService _stocksService;

        public HomeController(INewsServiceFactory serviceFactory)
        {
            _newsService = serviceFactory.CreateNewsService();
            _climateService = serviceFactory.CreateClimateService();
            _stocksService = serviceFactory.CreateStocksService();
        }

        public async Task<IActionResult> Index()
        {
            var newsArticles = await _newsService.GetLatestNewsAsync();
            var climateData = await _climateService.GetClimateDataAsync();
            var stockData = await _stocksService.GetStockDataAsync();

            var viewModel = new HomeViewModel
            {
                NewsArticles = newsArticles,
                ClimateData = climateData,
                StockData = stockData
            };

            return View(viewModel);
        }

        public class HomeViewModel
        {
            public List<NewsArticle> NewsArticles { get; set; }
            public ClimateData ClimateData { get; set; }
            public StockData StockData { get; set; }
        }
    }
}
