using System.ComponentModel.DataAnnotations;

namespace Model.ViewModel
{
    public class ProductViewModel
    {
        [StringLength(255)]
        public string Name { set; get; }

        public int Id { set; get; }
    }
}