namespace JWTAuthCoreAPIRestful.Models.StudentResultModel
{
    public class Marks5Percent1DTO
    {
        public string SubjectName { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public string TestTypeName { get; set; }
        public decimal Marks { get; set; }
    }
    public class Marks5Test1DTO
    {
        public string SubjectName { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public decimal Unit1 { get; set; }
        public decimal Unit2 { get; set; }
        public decimal Unit3 { get; set; }
        public decimal Unit4 { get; set; }
        public decimal Best { get; set; }
        
    }
    public class Marks5Test2DTO
    {
        public string SubjectName { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public decimal Unit5 { get; set; }
        public decimal Unit6 { get; set; }
        public decimal Unit7 { get; set; }
        public decimal Unit8 { get; set; }
        public decimal Best1 { get; set; }
    }
    public class Marks5PercentFinalDTO
    {
        public string sheetsNames { get; set; }
        public string typeNames { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public decimal Unit1 { get; set; }
        public decimal Unit2 { get; set; }
        public decimal Unit3 { get; set; }
        public decimal Unit4 { get; set; }
        public decimal Best { get; set; }
        public decimal Unit5 { get; set; }
        public decimal Unit6 { get; set; }
        public decimal Unit7 { get; set; }
        public decimal Unit8 { get; set; }
        public decimal Best1 { get; set; }
        public decimal First_UNIT_5Percent { get; set; }
        public decimal Second_UNIT_5Percent { get; set; }
        public decimal CW { get; set; }
        public decimal HW { get; set; }

        public decimal Total { get; set; }
        public decimal TOTAL_ROUND_OFF { get; set; }

    }

    public class FinalMarks5Subject
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public decimal Subject1 { get; set; }
        public decimal Subject2 { get; set; }
        public decimal Subject3 { get; set; }
        public decimal Subject4 { get; set; }
        public decimal Subject5 { get; set; }

    }
    public class FinalMarks6Subject
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public decimal Subject1 { get; set; }
        public decimal Subject2 { get; set; }
        public decimal Subject3 { get; set; }
        public decimal Subject4 { get; set; }
        public decimal Subject5 { get; set; }
        public decimal Subject6 { get; set; }

    }
    public class FinalMarks7Subject
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public decimal Subject1 { get; set; }
        public decimal Subject2 { get; set; }
        public decimal Subject3 { get; set; }
        public decimal Subject4 { get; set; }
        public decimal Subject5 { get; set; }
        public decimal Subject6 { get; set; }
        public decimal Subject7 { get; set; }

    }

    public class FinalMarks8Subject
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public decimal Subject1 { get; set; }
        public decimal Subject2 { get; set; }
        public decimal Subject3 { get; set; }
        public decimal Subject4 { get; set; }
        public decimal Subject5 { get; set; }
        public decimal Subject6 { get; set; }
        public decimal Subject7 { get; set; }
        public decimal Subject8 { get; set; }
    }
    public class FinalMarks9Subject
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public decimal Subject1 { get; set; }
        public decimal Subject2 { get; set; }
        public decimal Subject3 { get; set; }
        public decimal Subject4 { get; set; }
        public decimal Subject5 { get; set; }
        public decimal Subject6 { get; set; }
        public decimal Subject7 { get; set; }
        public decimal Subject8 { get; set; }
        public decimal Subject9 { get; set; }

    }
    public class FinalMarks10Subject
    {
        public int RollNo { get; set; }
        public string Name { get; set; }
        public decimal Subject1 { get; set; }
        public decimal Subject2 { get; set; }
        public decimal Subject3 { get; set; }
        public decimal Subject4 { get; set; }
        public decimal Subject5 { get; set; }
        public decimal Subject6 { get; set; }
        public decimal Subject7 { get; set; }
        public decimal Subject8 { get; set; }
        public decimal Subject9 { get; set; }
        public decimal Subject10 { get; set; }

    }
    public class Marks5Percent
    {
        public int RollNo { get; set; }
        public string Name { get; set; }

    }
    public class subjectDTO
    {
        public string strAllSubject { get; set; }

    }
    public class SheetnameDTO
    {
        public string strFinalSheetname { get; set; }

    }
}
