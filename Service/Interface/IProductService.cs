using Model.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IProductService
    {
        Task<bool> AddAsync(ProductViewModel productViewModel);

        Task<bool> UpdateAsync(ProductViewModel productViewModel);

        Task<bool> DeleteAsync(int id);

        Task<List<ProductViewModel>> GetAllAsync();

        Task<ProductViewModel> GetByIdAsync(int id);

        Task<int> SaveChangesAsync();
    }
}