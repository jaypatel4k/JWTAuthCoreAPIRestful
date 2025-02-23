using JWTAuthCoreAPIRestful.Models.StudentResultModel;

namespace JWTAuthCoreAPIRestful.Interface
{
    public interface IStudentReportRepository
    {

        Task BeginTransaction();
        Task RollBackTrasaction();
        Task CommitTransaction();

        //division
        Task<IEnumerable<Division>> GetAllDivisionAsync();
        Task<Division> GetDivisionByIdAsync(int divisionid);

        Task<Division> GetDivisionByNameAsync(string strDivision);
        Task AddDivisionAsync(Division division);
        Task UpdateDivisionAync(Division division);
        Task DeleteDivisionAsync(int divisionid);

        //standard
        Task<IEnumerable<Standard>> GetAllStandardAsync();
        Task<Standard> GetStandardByIdAsync(int standardid);
        Task AddStandardAsync(Standard standard);
        Task UpdateStandardAync(Standard standard);
        Task DeleteStandardAsync(int standardid);

        //StreamType
        Task<IEnumerable<StreamType>> GetAllStreamTypeAsync();
        Task<StreamType> GetStreamTypeByIdAsync(int streamTypeid);
        Task AddStreamTypeAsync(StreamType streamType);
        Task UpdateStreamTypeAync(StreamType streamType);
        Task DeleteStreamTypeAsync(int streamTypeid);

        //student
        Task<IEnumerable<Student>> GetAllStudentAsync();
        Task<IEnumerable<StudentDTO>> GetStudentByStandardAndDivisionAsync(int divisionId, int standadId);

        Task<Student?> GetStudentByNameStandardAndDivisionAsync(string studentName, int standardId, int divisionId);

        Task<Student> GetStudentByIdAsync(int studid);
        Task<Student> GetStudentByNameAsync(string studentName);
        Task AddStudentAsync(Student student);
        Task UpdateStudentAync(Student student);
        Task DeleteStudentAsync(int studid);

        //StudentMarks
        Task<IEnumerable<StudentMark>> GetAllStudentMarkAsync();
        Task<StudentMark> GetStudentMarkByIdAsync(int studentMarkid);
        Task<StudentMark> GetExistingStudentMarkIfAny(int subjectid,int studentid, int testTypeId, int monthId, int yearId, int standardId, int divisionId);
        Task AddStudentMarkAsync(StudentMark studentMark);
        Task UpdateStudentMarkAync(StudentMark studentMark);
        Task DeleteStudentMarkAsync(int studentMarkid);

        //subject
        Task<IEnumerable<Subject>> GetAllSubjectAsync();
        Task<Subject> GetSubjectByIdAsync(int subjectid);
        Task<Subject> GetSubjectByNameAsync(string subjectName);
        Task AddSubjectAsync(Subject subject);
        Task UpdateSubjectAync(Subject subject);
        Task DeleteSubjectAsync(int subjectid);

        //TestHeldOfMarks
        Task<IEnumerable<TestHeldOfMarkDTO>> GetAllTestHeldOfMarkAsync();
        Task<TestHeldOfMark> GetTestHeldOfMarkByIdAsync(int testHeldOfMarkid);
        Task AddTestHeldOfMarkAsync(TestHeldOfMark testHeldOfMark);
        Task UpdateTestHeldOfMarkAync(TestHeldOfMark testHeldOfMark);
        Task DeleteTestHeldOfMarkAsync(int testHeldOfMarkid);

        //TestType
        Task<IEnumerable<TestType>> GetAllTestTypeAsync();
        Task<TestType> GetTestTypeByIdAsync(int testTypeid);
        Task<TestType> GetTestTypeByNameAsync(string testType);
        Task AddTestTypeAsync(TestType testType);
        Task UpdateTestTypeAync(TestType testType);
        Task DeleteTestTypeAsync(int testTypeid);

        //month
        Task<IEnumerable<Month>> GetMonthAsync();
        //year
        Task<IEnumerable<Year>> GetYearAsync();

        //Rank by class
        Task<IEnumerable<TopRankInClassDTO>> GetTopThreeRankInClass(int testTypeId, int monthId, int yearId, int standardId, int divisionId, int streamId);
        Task<IEnumerable<TopRankInClassBySubjectNoRank>> GetTopRankBySubjectInClass(int testTypeId, int monthId, int yearId, int standardId, int divisionId, int streamId);

        //Rank in All Division
        Task<IEnumerable<TopThreeRankInAllDivision>> GetTopThreeRankInAllDivision(int testTypeId, int monthId, int yearId, int standardId, int streamId);
        Task<IEnumerable<TopRankInAllDivisionBySubjectNoRank>> GetTopRankBySubjectInAllDivision(int testTypeId, int monthId, int yearId, int standardId, int streamId);

        Task<IEnumerable<TopThreeRankInAllDivision>> GetFirstSecondThirdRankInAllDivision(int testTypeId, int monthId, int yearId, int standardId, int streamId);

        Task<IEnumerable<TopRankInAllDivisionBySubjectNoRank>> GetHighestInAllSubjectInAllDivision(int testTypeId, int monthId, int yearId, int standardId, int streamId);
    }
}
