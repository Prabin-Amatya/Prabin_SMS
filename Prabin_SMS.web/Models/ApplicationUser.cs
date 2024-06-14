using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prabin_SMS.web.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }

        public int CourseId { get; set; }
        public string UserRoleId {  get; set; }
        public string? profileUrl {  get; set; }
        [NotMapped]
        public IFormFile? profilePicture {  get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }


    }
}
