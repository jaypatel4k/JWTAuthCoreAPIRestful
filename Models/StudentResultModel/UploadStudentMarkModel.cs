using System.ComponentModel.DataAnnotations;

namespace JWTAuthCoreAPIRestful.Models.StudentResultModel
{
    public class UploadStudentMarkModel
    {
        [Required]
        public int TestTypeId { get; set; }
        [Required]
        public int DivisionId { get; set; }
        [Required]
        public int StandardId { get; set; }
        [Required]
        public int YearId { get; set; }
        [Required]
        public int MonthId { get; set; }
        [Required]
        public int StreamId { get; set; }

        [Required(ErrorMessage = "Please select file")]
        public IFormFile file { get; set; }

    }
}
