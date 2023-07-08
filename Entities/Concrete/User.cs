using Core.Entities;

namespace Entities.Concrete
{
    public class User : AppUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PhotoUrl { get; set; }
        public string Address { get; set; }
    }
}
