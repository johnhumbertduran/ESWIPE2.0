using System;
using System.Collections.Generic;
using System.Text;

using ESWIPE.Models;
using System.Threading.Tasks;

namespace ESWIPE.Services.Interfaces
{
    public interface IStudentService
    {
        Task<bool> AddorUpdateStudent(StudentModel studentModel);
        Task<bool> DeleteStudent(string key);
        //Task<TeacherModel> GetById(int teacherNumber);
        Task<List<StudentModel>> GetAllStudent();
        Task<List<StudentModel>> GetAllStudentForAdmin();
    }
}
