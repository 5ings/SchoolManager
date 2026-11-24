using System.ComponentModel.DataAnnotations;

namespace SchoolManager.Models
{
    public class School
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името е задължително.")]
        [StringLength(50, ErrorMessage = "Максимум 50 символа.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Адресът е задължителен.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Имейлът е задължителен.")]
        [EmailAddress(ErrorMessage = "Невалиден имейл.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Телефонът е задължителен.")]
        [Phone(ErrorMessage = "Невалиден телефон.")]
        public string PhoneNumber { get; set; }

        [Range(1, 100, ErrorMessage = "Броят стаи трябва да е между 1 и 100.")]
        public int RoomsCount { get; set; }
    }
}
