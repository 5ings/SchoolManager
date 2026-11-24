using System.ComponentModel.DataAnnotations;

namespace SchoolManager.ViewModels.Student
{
    public class StudentEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Името е задължително.")]
        [StringLength(30, ErrorMessage = "Максималната дължина е 30.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Фамилията е задължителна.")]
        [StringLength(30, ErrorMessage = "Максималната дължина е 30.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Имейлът е задължителен.")]
        [EmailAddress(ErrorMessage = "Невалиден имейл.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Телефонът е задължителен.")]
        [Phone(ErrorMessage = "Невалиден телефон.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Моля, изберете учител.")]
        public List<int> TeacherIds { get; set; }
    }
}
