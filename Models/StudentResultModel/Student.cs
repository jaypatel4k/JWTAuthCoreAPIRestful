namespace JWTAuthCoreAPIRestful.Models.StudentResultModel
{
    public class Student : BaseEntity
    {
        public string Name { get; set; }
        public int RollNo { get; set; }
        public int DivisionId { get; set; }
        public DateTime DOB { get; set; }
        public int StandId { get; set; }

    }
}
