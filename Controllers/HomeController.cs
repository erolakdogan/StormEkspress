using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StormEkspress.Helper;
using StormEkspress.Models;
using StormEkspress.Models.UIModel;
using StormEkspress.Services;

namespace StormEkspress.Controllers
{
    [SeoMetaDataFilter]
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _memoryCache;
        private readonly HttpClient _httpClient;
        private readonly BreadcrumbService _breadcrumbService;

        public HomeController(ILogger<HomeController> logger,IConfiguration configuration,IMemoryCache memoryCache,HttpClient httpClient, BreadcrumbService breadcrumbService)
        {
            _logger = logger;
            _configuration = configuration;
            _memoryCache = memoryCache;
            _httpClient = httpClient;
            _breadcrumbService = breadcrumbService;
        }
        private HomePageDto GetHomePageData(string language, string currentPath)
        {
            string cacheKey = $"HomePageData_{language}_{currentPath}";

            if (!_memoryCache.TryGetValue(cacheKey, out HomePageDto model))
            {
                // Cache'de veri yoksa, veriyi alıp cache'e ekleyin
                ViewBag.SiteKey = _configuration["ReCaptchaSettings:SiteKey"];
                var siteSettings = _configuration.GetSection("siteSettings").Get<SiteSettings>();
                var intro = _configuration.GetSection("intro").Get<List<Intro>>();
                var menu = _configuration.GetSection("menu").Get<List<Menu>>();
                var aboutUs = _configuration.GetSection("aboutUs").Get<About>();
                var references = _configuration.GetSection("references").Get<List<Models.Reference>>();

                model = new HomePageDto
                {
                    SiteSettings = siteSettings,
                    Intro = intro,
                    Menu = menu,
                    About = aboutUs,
                    References = references,
                    CurrentPath = currentPath,  // Bu kısmı her sayfaya özgü olarak ayarlayacağız
                };

                // Cache'e veriyi ekleyin (3 saat boyunca saklanacak)
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(3));

                _memoryCache.Set(cacheKey, model, cacheEntryOptions);
            }

            return model;
        }
        // Index Action'ı
        public async Task<IActionResult> Index(string language = "tr")
        {
            var currentPath = Request.Path.ToString();
            var model = GetHomePageData(language, currentPath);
            var breadcrumbs = _breadcrumbService.GetBreadcrumbs(currentPath);
            var breadcrumbJson = _breadcrumbService.GetBreadcrumbJson(breadcrumbs);
            ViewData["Breadcrumbs"] = breadcrumbs;
            ViewData["BreadcrumbJson"] = breadcrumbJson;
            return View(model);
        }
        // About Action'ı
        [Route("hakkimizda")]
        public async Task<IActionResult> About(string language = "tr")
        {
            var currentPath = Request.Path.ToString();
            var model = GetHomePageData(language, currentPath);
            var breadcrumbs = _breadcrumbService.GetBreadcrumbs(currentPath);
            var breadcrumbJson = _breadcrumbService.GetBreadcrumbJson(breadcrumbs);
            ViewData["Breadcrumbs"] = breadcrumbs;
            ViewData["BreadcrumbJson"] = breadcrumbJson;
            return View(model);
        }
        // Services Action'ı
        [Route("hizmetlerimiz")]
        public async Task<IActionResult> Services(string language = "tr")
        {
            var currentPath = Request.Path.ToString();
            var model = GetHomePageData(language, currentPath);
            var breadcrumbs = _breadcrumbService.GetBreadcrumbs(currentPath);
            var breadcrumbJson = _breadcrumbService.GetBreadcrumbJson(breadcrumbs);
            ViewData["Breadcrumbs"] = breadcrumbs;
            ViewData["BreadcrumbJson"] = breadcrumbJson;
            return View(model);
        }
        // Contact Action'ı
        [Route("iletisim")]
        public async Task<IActionResult> Contact(string language = "tr")
        {
            var currentPath = Request.Path.ToString();
            var model = GetHomePageData(language, currentPath);
            var breadcrumbs = _breadcrumbService.GetBreadcrumbs(currentPath);
            var breadcrumbJson = _breadcrumbService.GetBreadcrumbJson(breadcrumbs);
            ViewData["Breadcrumbs"] = breadcrumbs;
            ViewData["BreadcrumbJson"] = breadcrumbJson;
            return View(model);
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
