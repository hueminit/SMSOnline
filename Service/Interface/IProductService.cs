using System.Collections.Generic;
using Model.ViewModel;

namespace Services.Interface
{
    public interface IProductService
    {
        void Add(ProductViewModel ProductViewModel);

        void Update(ProductViewModel ProductViewModel);

        void Delete(int id);

        List<ProductViewModel> GetAll();

        ProductViewModel GetById(int id);

        void SaveChanges();
    }
}