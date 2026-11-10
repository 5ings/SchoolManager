using System.ComponentModel.DataAnnotations;

namespace SchoolManager.ViewModels.School
{
    public class SchoolIndexViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int RoomsCount { get; set; }
    }
}
