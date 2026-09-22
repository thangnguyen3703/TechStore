using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var branches = await _branchService.GetAllBranchesAsync(page, pageSize);

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
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var branch= await _branchService.GetBranchByID(id);
            if (branch == null)
                return NotFound();
            return View(branch);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BranchFormViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _branchService.UpdateBranch(model);

            if (!result)
                return NotFound();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _branchService.DeleteBranch(id);

            if (!result)
                return NotFound();

            TempData["SuccessMessage"] = "Xóa chi nhánh thành công.";

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            await _branchService.ToggleStatus(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
