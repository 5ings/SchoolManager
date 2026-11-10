namespace SchoolManager.ViewModels.Student
{
    public class StudentDetailsViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string TeacherNames { get; set; } = string.Empty;
    }
}
