using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SchoolData.Entities
{
    public class School
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Range(1, 100)]
        public int RoomsCount { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<Teacher> Teachers { get; set; } = new List<Teacher>();
    }
}
