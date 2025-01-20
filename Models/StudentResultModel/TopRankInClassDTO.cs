namespace JWTAuthCoreAPIRestful.Models.StudentResultModel
{
    public class TopRankInClassDTO
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
        public int TotalMarks { get; set; }
        

    }
    public class TopRankInClassBySubject
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string SubjectName { get; set; }
        public int TotalMarks { get; set; }
        public int Rank { get; set; }

    }
}
