using Entities.Concrete;
using Entities.DTOs.CartDTOs;

namespace WebUI.ViewModels
{
    public class CheckoutVM
    {
        public User User { get; set; }
        public List<UserCartDTO> CartItems { get; set; }
    }
}
