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
        public async Task<List<BranchListViewModel>> GetAllBranchesAsync()
        {
            return await _dbContext.Branches
                .Select(b => new BranchListViewModel
                {
                    BranchId = b.BranchID,
                    Code = b.Code,
                    Name = b.Name,
                    Address = b.Address,
                    Phone = b.Phone,
                    Email = b.Email,
                    IsActive = b.IsActive
                })
                .ToListAsync();
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
    }
}
