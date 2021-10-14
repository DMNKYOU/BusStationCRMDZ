

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampusCRM.BLL.Services
{
    public class StudentService : 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<StudentDTO>> GetAllAsync()
        {
            var students = await _unitOfWork.Students.GetAllAsync();
            return _mapper.Map<IEnumerable<Student>, List<StudentDTO>>(students);
        }

        public async Task<StudentDTO> GetByIdAsync(int id)
        {
            var student =await _unitOfWork.Students.GetAsync(id);

            return _mapper.Map<StudentDTO>(student);
        }

        public async Task AddAsync(StudentDTO studentDto)
        {
            if (studentDto == null)
                throw new ArgumentException();

            var student = _mapper.Map<Student>(studentDto);

            await _unitOfWork.Students.CreateAsync(student);
           // _unitOfWork.Save();
        }

        public async Task EditAsync(StudentDTO studentDto)
        {
            if (studentDto == null)
                throw new ArgumentException();

            var student = _mapper.Map<Student>(studentDto);

            await _unitOfWork.Students.UpdateAsync(student);
            //_unitOfWork.Save();
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Students.DeleteAsync(id);
            //_unitOfWork.Save();
        }
        public async Task DeleteStudentFromGroupAsync(int studentId)
        {
            var student = await _unitOfWork.Students.GetAsync(studentId);
            student.GroupId = null;
            //Debug.WriteLine($"{student.GroupId.HasValue}  {student.Group}");
            await _unitOfWork.Students.UpdateAsync(student);
        }

        public async Task AddStudentToGroupAsync(int studentId, int groupId)
        {
            var student = await _unitOfWork.Students.GetAsync(studentId);

            if (student.GroupId != groupId)
            {
                student.GroupId = groupId;
                await _unitOfWork.Students.UpdateAsync(student);
            }
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

    }
}
