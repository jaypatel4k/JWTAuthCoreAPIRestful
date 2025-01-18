namespace JWTAuthCoreAPIRestful.Models.StudentResultModel
{
    public class TopRankInClassDTO
    {
        public string Name { get; set; }
        public int RollNo { get; set; }
        public int TotalMarks { get; set; }
        public int Rank { get; set; }

    }
    public class TopRankInClassBySubject
    {
        public string Name { get; set; }
        public int RollNo { get; set; }
        public string SubjectName { get; set; }
        public int TotalMarks { get; set; }
        public int Rank { get; set; }

    }
}
