namespace JWTAuthCoreAPIRestful.Models.StudentResultModel
{
    public class StudentMark : BaseEntity
    {
        public int TestTypeId { get; set; }
        public int MonthId { get; set; }
        public int YearId { get; set; }
        public int StandardId { get; set; }
        public int DivisionId { get; set; }
        public int StreamId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int TestHeldOfMarkId { get; set; }
        public decimal Marks { get; set; }
        public string? Remarks { get; set; }
    }
}
