namespace JWTAuthCoreAPIRestful.Models.StudentResultModel
{
    public class Marks5PercentTotalDTO
    {
        public string SubjectName { get; set; }
        public int RollNo { get; set; }
        public string Name { get; set; }
        public decimal JULY_UNIT { get; set; }
        public decimal AUG_UNIT { get; set; }
        public decimal SEP_UNIT { get; set; }
        public decimal Best { get; set; }
        public decimal DEC_UNIT { get; set; }
        public decimal JAN_UNIT { get; set; }
        public decimal Best1 { get; set; }
        public decimal First_UNIT_5_Percent { get; set; }
        public decimal Second_UNIT_5_Percent { get; set; }
        public decimal CW { get; set; }
        public decimal HW { get; set; }
        public decimal ROUND_Total { get; set; }

    }

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
        public decimal Forst_UNIT_5_Percent { get; set; }
        public decimal Second_UNIT_5_Percent { get; set; }
        public decimal CW { get; set; }
        public decimal HW { get; set; }
        public decimal TOTAL_ROUND_OFF { get; set; }

    }

}
