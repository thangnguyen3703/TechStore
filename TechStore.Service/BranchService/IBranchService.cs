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
        Task<List<BranchListViewModel>> GetAllBranchesAsync();
        Task<bool> AddBranchAsync(BranchFormViewModel model);
    }
}
