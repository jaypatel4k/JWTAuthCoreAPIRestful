using JWTAuthCoreAPIRestful.Data;
using JWTAuthCoreAPIRestful.Interface;
using JWTAuthCoreAPIRestful.Models.StudentResultModel;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthCoreAPIRestful.Repository
{
    public class StudentReportRepository : IStudentReportRepository
    {
        private readonly JWTAuthCRUDContext _dbcontext;
        public StudentReportRepository(JWTAuthCRUDContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task BeginTransaction()
        {
            await _dbcontext.Database.BeginTransactionAsync();
        }
        public async Task RollBackTrasaction()
        {
            await _dbcontext.Database.RollbackTransactionAsync();
        }

        public async Task CommitTransaction()
        {
            await _dbcontext.Database.CommitTransactionAsync();
        }


        public async Task<IEnumerable<Division>> GetAllDivisionAsync()
        {
            return await _dbcontext.Division.ToListAsync();
        }

        public async Task<Division> GetDivisionByIdAsync(int divisionid)
        {
            return await _dbcontext.Division.FindAsync(divisionid);
        }

        public async Task AddDivisionAsync(Division division)
        {
            await _dbcontext.Division.AddAsync(division);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateDivisionAync(Division division)
        {
            _dbcontext.Update(division);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteDivisionAsync(int divisionid)
        {
            var division = await _dbcontext.Division.FindAsync(divisionid);
            if (division != null)
            {
                _dbcontext.Division.Remove(division);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Standard>> GetAllStandardAsync()
        {
            return await _dbcontext.Standard.ToListAsync();
        }

        public async Task<Standard> GetStandardByIdAsync(int standardid)
        {
            return await _dbcontext.Standard.FindAsync(standardid);
        }

        public async Task AddStandardAsync(Standard standard)
        {
            await _dbcontext.Standard.AddAsync(standard);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateStandardAync(Standard standard)
        {
            _dbcontext.Update(standard);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteStandardAsync(int standardid)
        {
            var standard = await _dbcontext.Standard.FindAsync(standardid);
            if (standard != null)
            {
                _dbcontext.Standard.Remove(standard);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<StreamType>> GetAllStreamTypeAsync()
        {
            return await _dbcontext.StreamType.ToListAsync();
        }

        public async Task<StreamType> GetStreamTypeByIdAsync(int streamTypeid)
        {
            return await _dbcontext.StreamType.FindAsync(streamTypeid);
        }

        public async Task AddStreamTypeAsync(StreamType streamType)
        {
            await _dbcontext.StreamType.AddAsync(streamType);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateStreamTypeAync(StreamType streamType)
        {
            _dbcontext.Update(streamType);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteStreamTypeAsync(int streamTypeid)
        {
            var streamType = await _dbcontext.StreamType.FindAsync(streamTypeid);
            if (streamType != null)
            {
                _dbcontext.StreamType.Remove(streamType);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Student>> GetAllStudentAsync()
        {
            return await _dbcontext.Student.ToListAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int studentid)
        {
            return await _dbcontext.Student.FindAsync(studentid);
        }

        public async Task<Student> GetStudentByNameAsync(string studentName)
        {
            return await _dbcontext.Student.FirstOrDefaultAsync(s => s.Name == studentName);
        }

        public async Task AddStudentAsync(Student student)
        {
            await _dbcontext.Student.AddAsync(student);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateStudentAync(Student student)
        {
            _dbcontext.Update(student);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int studentid)
        {
            var student = await _dbcontext.Student.FindAsync(studentid);
            if (student != null)
            {
                _dbcontext.Student.Remove(student);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<StudentMark>> GetAllStudentMarkAsync()
        {
            return await _dbcontext.StudentMark.ToListAsync();
        }

        public async Task<StudentMark> GetStudentMarkByIdAsync(int studentMarkid)
        {
            return await _dbcontext.StudentMark.FindAsync(studentMarkid);
        }

        public async Task AddStudentMarkAsync(StudentMark studentMark)
        {
            await _dbcontext.StudentMark.AddAsync(studentMark);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateStudentMarkAync(StudentMark studentMark)
        {
            _dbcontext.Update(studentMark);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteStudentMarkAsync(int studentMarkid)
        {
            var studentMark = await _dbcontext.StudentMark.FindAsync(studentMarkid);
            if (studentMark != null)
            {
                _dbcontext.StudentMark.Remove(studentMark);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Subject>> GetAllSubjectAsync()
        {
            return await _dbcontext.Subject.ToListAsync();
        }

        public async Task<Subject> GetSubjectByIdAsync(int subjectid)
        {
            return await _dbcontext.Subject.FindAsync(subjectid);
        }

        public async Task<Subject> GetSubjectByNameAsync(string subjectName)
        {
            return await _dbcontext.Subject.FirstOrDefaultAsync(s => s.SubjectName == subjectName);
        }

        public async Task AddSubjectAsync(Subject subject)
        {
            await _dbcontext.Subject.AddAsync(subject);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateSubjectAync(Subject subject)
        {
            _dbcontext.Update(subject);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteSubjectAsync(int subjectid)
        {
            var subject = await _dbcontext.Subject.FindAsync(subjectid);
            if (subject != null)
            {
                _dbcontext.Subject.Remove(subject);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TestHeldOfMark>> GetAllTestHeldOfMarkAsync()
        {
            return await _dbcontext.TestHeldOfMark.ToListAsync();
        }

        public async Task<TestHeldOfMark> GetTestHeldOfMarkByIdAsync(int testHeldOfMarkid)
        {
            return await _dbcontext.TestHeldOfMark.FindAsync(testHeldOfMarkid);
        }

        public async Task AddTestHeldOfMarkAsync(TestHeldOfMark testHeldOfMark)
        {
            await _dbcontext.TestHeldOfMark.AddAsync(testHeldOfMark);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateTestHeldOfMarkAync(TestHeldOfMark testHeldOfMark)
        {
            _dbcontext.Update(testHeldOfMark);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteTestHeldOfMarkAsync(int testHeldOfMarkid)
        {
            var testHeldOfMark = await _dbcontext.TestHeldOfMark.FindAsync(testHeldOfMarkid);
            if (testHeldOfMark != null)
            {
                _dbcontext.TestHeldOfMark.Remove(testHeldOfMark);
                await _dbcontext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TestType>> GetAllTestTypeAsync()
        {
            return await _dbcontext.TestType.ToListAsync();
        }

        public async Task<TestType> GetTestTypeByIdAsync(int testTypeid)
        {
            return await _dbcontext.TestType.FindAsync(testTypeid);
        }

        public async Task AddTestTypeAsync(TestType testType)
        {
            await _dbcontext.TestType.AddAsync(testType);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task UpdateTestTypeAync(TestType testType)
        {
            _dbcontext.Update(testType);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task DeleteTestTypeAsync(int testTypeid)
        {
            var testType = await _dbcontext.TestType.FindAsync(testTypeid);
            if (testType != null)
            {
                _dbcontext.TestType.Remove(testType);
                await _dbcontext.SaveChangesAsync();
            }
        }

        
    }
}
