using Microsoft.AspNetCore.Mvc;
using TechStore.Service;
using TechStore.Shared_ViewModels;
namespace TechStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BranchController : Controller
    {
        private readonly IBranchService _branchService;
        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }
        public async Task<IActionResult> Index()
        {
            var branches = await _branchService.GetAllBranchesAsync();

            return View(branches);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(BranchFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _branchService.AddBranchAsync(model);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Failed to create branch.");
            }
            return View(model);
        }
    }
}
