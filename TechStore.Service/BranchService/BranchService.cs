using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechStore.Data;
using TechStore.Shared_ViewModels;
namespace TechStore.Service
{
    public class BranchService : IBranchService
    {
        private readonly TechStoreDBEntities _dbContext;
        public BranchService(TechStoreDBEntities dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<PagedResult<BranchListViewModel>> GetAllBranchesAsync(int page = 1, int pageSize = 10)
        {
            var query = _dbContext.Branches
                    .AsNoTracking()
                    .OrderBy(x => x.BranchID)
                    .Select(x => new BranchListViewModel
                    {
                        BranchId = x.BranchID,
                        Code = x.Code,
                        Name = x.Name,
                        Address = x.Address,
                        Phone = x.Phone,
                        Email = x.Email,
                        IsActive = x.IsActive
                    });

            return await query.ToPagedResultAsync(page, pageSize);
        }
        public async Task<bool> AddBranchAsync(BranchFormViewModel model)
        {
            var branch = new Branches
            {
                Code = model.Code,
                Name = model.Name,
                Address = model.Address,
                Phone = model.Phone,
                Email = model.Email,
                IsActive = model.IsActive,
                CreatedAt = DateTime.Now
            };

            _dbContext.Branches.Add(branch);

            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<BranchFormViewModel?> GetBranchByID(int id)
        {
            return await _dbContext.Branches
                .Where(x => x.BranchID == id)
                .Select(x => new BranchFormViewModel
                {
                    BranchId = x.BranchID,
                    Code = x.Code,
                    Name = x.Name,
                    Address = x.Address,
                    Phone = x.Phone,
                    Email = x.Email,
                    IsActive = x.IsActive
                })
                .FirstOrDefaultAsync();
        }
        public async Task<bool> UpdateBranch(BranchFormViewModel model)
        {
            var branch = await _dbContext.Branches
                .FirstOrDefaultAsync(x => x.BranchID == model.BranchId);

            if (branch == null)
                return false;

            branch.Code = model.Code;
            branch.Name = model.Name;
            branch.Address = model.Address;
            branch.Phone = model.Phone;
            branch.Email = model.Email;
            branch.IsActive = model.IsActive;
            branch.UpdatedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return true;
        }
        public async Task<bool> DeleteBranch(int id)
        {
            var branch = await _dbContext.Branches
                .FirstOrDefaultAsync(x => x.BranchID == id);
            if (branch == null)
                return false;
            _dbContext.Branches.Remove(branch);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ToggleStatus(int id)
        {
            var branch = await _dbContext.Branches
                .FirstOrDefaultAsync(x => x.BranchID == id);
            if (branch == null) 
                return false;
            if (branch.IsActive)
                branch.IsActive = false;
            else branch.IsActive = true;
                await _dbContext.SaveChangesAsync();
            return true;
        }

    }
}
