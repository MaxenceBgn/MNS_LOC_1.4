using Abp.Web.Mvc.Models;
using AuthMVC2.Data;
using AuthMVC2.Models;
using AuthMVC2.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using ErrorViewModel = AuthMVC2.Models.ErrorViewModel;

namespace AuthMVC2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MaterialState()
        {
            try
            {
                bool searchResult = false;

                if (Request.Method == "POST")
                {
                    if (!bool.TryParse(Request.Form["searchResult"], out searchResult))
                    {
                        // La conversion a échoué, vous pouvez définir une valeur par défaut ici
                        searchResult = false; // Par exemple, définir la valeur par défaut à false
                    }
                }

                // Récupérer les valeurs depuis la session
                string searchResultString = HttpContext.Session.GetString("SearchResult");
                if (!string.IsNullOrEmpty(searchResultString) && bool.TryParse(searchResultString, out bool parsedSearchResult))
                {
                    searchResult = parsedSearchResult;
                }

                string materialListString = HttpContext.Session.GetString("MaterialList");
                List<string> materialList = new List<string>();
                if (!string.IsNullOrEmpty(materialListString))
                {
                    materialList = materialListString.Split(',').ToList();
                }

                int materialNumber = _dbContext.Materials.Count();
                int outOfOrderMaterialNumber = _dbContext.Materials.Count(m => !m.IsFunctional);
                int availableMaterial = materialNumber - outOfOrderMaterialNumber;

                var viewModel = new Models.ViewModels.MaterialSearchViewModel
                {
                    Materials = _dbContext.Materials.ToList(),
                    MaterialTypes = _dbContext.MaterialTypes.ToList(),
                    SearchResult = searchResult,
                    MaterialList = materialList ?? new List<string>()
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return RedirectToAction("Error", "Home");
            }
        }



        public IActionResult MaterialSearch(MaterialSearchViewModel model, MaterialSearchViewModel materialSearchViewModel)
        {
            bool searchResult = materialSearchViewModel.SearchResult;
            List<string> materialList = materialSearchViewModel.MaterialList;

            string typeForm = Request.Form["typeForm"];

            var query = from material in _dbContext.Materials
                        join materialType in _dbContext.MaterialTypes on material.TypeId equals materialType.TypeId
                        where materialType.TypeName == typeForm
                        select new
                        {
                            MaterialTypeName = materialType.TypeName,
                            MaterialSerialNumber = material.SerialNumber,
                            MaterialIsFunctional = material.IsFunctional
                        };

            foreach (var result in query)
            {
                string stateAvailable = result.MaterialIsFunctional ? "Disponible" : "Hors service";
                materialList.Add($"{result.MaterialTypeName} - {result.MaterialSerialNumber} - {stateAvailable}");
            }
            if (materialList.Count > 0)
            {
                searchResult = true;
            }

            HttpContext.Session.SetString("SearchResult", searchResult.ToString());
            HttpContext.Session.SetString("MaterialList", string.Join(",", materialList));

            return RedirectToAction("MaterialState");
        }
    }
}