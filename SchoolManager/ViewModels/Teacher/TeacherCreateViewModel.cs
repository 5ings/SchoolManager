using System.ComponentModel.DataAnnotations;

namespace SchoolManager.ViewModels.Teacher
{
    public class TeacherCreateViewModel
    {
        [Required(ErrorMessage = "Името е задължително")]
        [StringLength(30, ErrorMessage = "Максималната дължина е 30.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилията е задължителна")]
        [StringLength(30, ErrorMessage = "Максималната дължина е 30")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Предметът е задължителен")]
        [StringLength(50, ErrorMessage = "Максималната дължина е 50")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Имейлът е задължителен.")]
        [EmailAddress(ErrorMessage = "Невалиден имейл.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Телефонът е задължителен.")]
        [Phone(ErrorMessage = "Невалиден телефон.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Моля, изберете училище.")]
        public int SchoolId { get; set; }
    }
}
