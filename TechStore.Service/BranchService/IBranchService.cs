using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Shared_ViewModels;

namespace TechStore.Service
{
    public interface IBranchService
    {
        Task<PagedResult<BranchListViewModel>> GetAllBranchesAsync(int page = 1, int pageSize = 10);
        Task<bool> AddBranchAsync(BranchFormViewModel model);
        Task<BranchFormViewModel?> GetBranchByID(int id);
        Task<bool> UpdateBranch(BranchFormViewModel model);
        Task<bool> DeleteBranch(int id);
        Task<bool> ToggleStatus(int id);

    }
}
