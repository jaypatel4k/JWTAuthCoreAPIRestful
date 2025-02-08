using System.ComponentModel.DataAnnotations;

namespace JWTAuthCoreAPIRestful.Models.StudentResultModel
{
    public class UploadStudentModel
    {
        [Required]    
        public int divisionid { get; set; }
        [Required]
        public int standardid { get; set; }
        
        [Required(ErrorMessage = "Please select file")]
        public IFormFile file { get; set; }
    }
}
