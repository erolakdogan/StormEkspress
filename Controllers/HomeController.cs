using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using StormEkspress.Helper;
using StormEkspress.Models;
using StormEkspress.Models.Entities;
using StormEkspress.Models.UIModel;
using StormEkspress.Services;
using StormEkspress.Services.Interfaces;

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
        private readonly IFormService _formService;

        public HomeController(ILogger<HomeController> logger,IConfiguration configuration,IMemoryCache memoryCache,HttpClient httpClient, BreadcrumbService breadcrumbService, IFormService formService)
        {
            _logger = logger;
            _configuration = configuration;
            _memoryCache = memoryCache;
            _httpClient = httpClient;
            _breadcrumbService = breadcrumbService;
            _formService = formService;
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
                var services = _configuration.GetSection("services").Get<List<Service>>();

                model = new HomePageDto
                {
                    SiteSettings = siteSettings,
                    Intro = intro,
                    Menu = menu,
                    About = aboutUs,
                    References = references,
                    CurrentPath = currentPath,  // Bu kısmı her sayfaya özgü olarak ayarlayacağız
                    Services = services,
                };

                // Cache'e veriyi ekleyin (3 saat boyunca saklanacak)
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromHours(3));

                _memoryCache.Set(cacheKey, model, cacheEntryOptions);
            }

            return model;
        }
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

        [Route("hakkimizda")]
        public async Task<IActionResult> About(string language = "tr")
        {
            var currentPath = Request.Path.ToString();
            var model = GetHomePageData(language, currentPath);
            var breadcrumbs = _breadcrumbService.GetBreadcrumbs(currentPath);
            var breadcrumbJson = _breadcrumbService.GetBreadcrumbJson(breadcrumbs);
            ViewData["Breadcrumbs"] = breadcrumbs;
            ViewData["BreadcrumbJson"] = breadcrumbJson;
            ViewData["PageTitle"] = "Hakkımızda";
            return View(model);
        }


        [HttpGet("basvuru/kurye")]
        public IActionResult CourierApplicationForm(string language = "tr")
        {
            ViewData["Title"] = "Kurye Başvuru Formu";
            ViewData["Description"] = "Kurye başvuru formu ile bize katılın";
            var currentPath = Request.Path.ToString();
            var model = GetHomePageData(language, currentPath);
            var breadcrumbs = _breadcrumbService.GetBreadcrumbs(currentPath);
            var breadcrumbJson = _breadcrumbService.GetBreadcrumbJson(breadcrumbs);
            ViewData["Breadcrumbs"] = breadcrumbs;
            ViewData["BreadcrumbJson"] = breadcrumbJson;
            return View(model);
        }

        [HttpPost("basvuru/kurye")]
        public async Task<IActionResult> KuryeBasvuruFormu(CourierApplication application)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "Kurye Başvuru Formu";
                ViewData["Description"] = "Kurye başvuru formu ile bize katılın";
                return View(application); 
            }

            try
            {
                ViewData["Title"] = "Kurye Başvuru Formu";
                ViewData["Description"] = "Kurye başvuru formu ile bize katılın";
                await _formService.SubmitCourierApplication(application);
                return RedirectToAction("Success");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewData["Title"] = "Kurye Başvuru Formu";
                ViewData["Description"] = "Kurye başvuru formu ile bize katılın";
                return View(application);
            }
            catch (EmailSendException ex)
            {
                ModelState.AddModelError(string.Empty, "E-posta gönderilirken bir hata oluştu");
                ViewData["Title"] = "Kurye Başvuru Formu";
                ViewData["Description"] = "Kurye başvuru formu ile bize katılın";
                return View(application);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "E-posta gönderilirken bir hata oluştu");
                ViewData["Title"] = "Kurye Başvuru Formu";
                ViewData["Description"] = "Kurye başvuru formu ile bize katılın";
                return View(application);
            }
        }

        [HttpGet("basvuru/restoran")]
        public IActionResult RestaurantApplicationForm(string language = "tr")
        {
            ViewData["Title"] = "Restoran Başvuru Formu";
            ViewData["Description"] = "Restoran başvuru formu ile bize katılın";
            var currentPath = Request.Path.ToString();
            var model = GetHomePageData(language, currentPath);
            var breadcrumbs = _breadcrumbService.GetBreadcrumbs(currentPath);
            var breadcrumbJson = _breadcrumbService.GetBreadcrumbJson(breadcrumbs);
            ViewData["Breadcrumbs"] = breadcrumbs;
            ViewData["BreadcrumbJson"] = breadcrumbJson;
            return View(model);
        }

        [HttpPost("basvuru/restoran")]
        public async Task<IActionResult> RestoranBasvuruFormu(RestaurantApplication application)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Title"] = "Restoran Başvuru Formu";
                ViewData["Description"] = "Restoran başvuru formu ile bize katılın";
                return View(application); // Return the form with validation errors
            }

            try
            {
                await _formService.SubmitRestaurantApplication(application);
                return RedirectToAction("Success");
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ViewData["Title"] = "Restoran Başvuru Formu";
                ViewData["Description"] = "Restoran başvuru formu ile bize katılın";
                return View(application);
            }
            catch (EmailSendException ex)
            {
                ModelState.AddModelError(string.Empty, "E-posta gönderilirken bir hata oluştu.");
                ViewData["Title"] = "Restoran Başvuru Formu";
                ViewData["Description"] = "Restoran başvuru formu ile bize katılın";
                return View(application);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Beklenmeyen bir hata oluştu.");
                ViewData["Title"] = "Restoran Başvuru Formu";
                ViewData["Description"] = "Restoran başvuru formu ile bize katılın";
                return View(application);
            }
        }

        [Route("hizmetlerimiz")]
        public async Task<IActionResult> Services(string language = "tr")
        {
            var currentPath = Request.Path.ToString();
            var model = GetHomePageData(language, currentPath);
            var breadcrumbs = _breadcrumbService.GetBreadcrumbs(currentPath);
            var breadcrumbJson = _breadcrumbService.GetBreadcrumbJson(breadcrumbs);
            ViewData["Breadcrumbs"] = breadcrumbs;
            ViewData["BreadcrumbJson"] = breadcrumbJson;
            ViewData["PageTitle"] = "Hizmetlerimiz";
            return View(model);
        }

        [Route("hizmetlerimiz/{slug}")]
        public async Task<IActionResult> ServiceDetail(string slug, string language = "tr")
        {
            var currentPath = Request.Path.ToString();
            var model = GetHomePageData(language, currentPath);
            var service = model.Services.FirstOrDefault(s => s.Slug == slug);

            if (service == null)
            {
                return NotFound(); // Hizmet bulunamazsa 404 döndür
            }

            var breadcrumbs = _breadcrumbService.GetBreadcrumbs(currentPath);
            var breadcrumbJson = _breadcrumbService.GetBreadcrumbJson(breadcrumbs);
            ViewData["Breadcrumbs"] = breadcrumbs;
            ViewData["BreadcrumbJson"] = breadcrumbJson;

            model.ServiceDetail = service;
            ViewData["PageTitle"] = "Hizmet Detaylarımız";
            return View(model); // Detay sayfasını render et
        }

        [Route("iletisim")]
        public async Task<IActionResult> Contact(string language = "tr")
        {
            var currentPath = Request.Path.ToString();
            var model = GetHomePageData(language, currentPath);
            var breadcrumbs = _breadcrumbService.GetBreadcrumbs(currentPath);
            var breadcrumbJson = _breadcrumbService.GetBreadcrumbJson(breadcrumbs);
            ViewData["Breadcrumbs"] = breadcrumbs;
            ViewData["BreadcrumbJson"] = breadcrumbJson;
            ViewData["PageTitle"] = "İletişim";
            return View(model);
        }

        public IActionResult Success()
        {
            return View();
        }

        [Route("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
