using static Entities.DTOs.CategoryDTOs.CategoryDTO;
using static Entities.DTOs.ProductDTOs.ProductDTO;

namespace WebUI.ViewModels
{
    public class HomeVM
    {
        public List<ProductFeaturedDTO> ProductFeaturedDTOs { get; set; }
        public List<ProductRecentDTO> ProductRecentDTOs { get; set; }
        public List<CategoryFeaturedDTO> CategoryFeaturedDTOs { get; set; }
    }
}
