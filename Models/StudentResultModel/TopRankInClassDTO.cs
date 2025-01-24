namespace JWTAuthCoreAPIRestful.Models.StudentResultModel
{
    public class TopRankInClassDTO
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public int Rank { get; set; }
        public int Total { get; set; }
        

    }
    public class TopRankInClassBySubject
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public int Total { get; set; }
        public int Rank { get; set; }

    }
    public class TopRankInClassBySubjectNoRank
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public int Marks { get; set; }

    }
    public class TopThreeRankInAllDivision
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string Div { get; set; }
        public int Rank { get; set; }
        public int Total { get; set; }
        

    }
    public class TopRankInAllDivisionBySubject
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string Div { get; set; }
        public string Subject { get; set; }
        public int Marks { get; set; }
        public int Rank { get; set; }
    }
    public class TopRankInAllDivisionBySubjectNoRank
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string Div { get; set; }
        public string Subject { get; set; }
        public int Marks { get; set; }
    }
}
