using System;
using System.Collections.Generic;
using System.Text;

using ESWIPE.Models;
using System.Threading.Tasks;

namespace ESWIPE.Services.Interfaces
{
    public interface ITeacherService
    {
        Task<bool> AddorUpdateTeacher(TeacherModel teacherModel);
        Task<bool> DeleteTeacher(string key);
        //Task<TeacherModel> GetById(int teacherNumber);
        Task<List<TeacherModel>> GetAllTeacher();
    }
}
