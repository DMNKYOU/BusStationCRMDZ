//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using BusStationCRM.BLL.Interfaces;
//using BusStationCRM.BLL.Models;
//using BusStationCRM.DAL.Interfaces;

//namespace CampusCRM.BLL.Services
//{
//    public class BusStopsService : IBusStopsService
//    {
//        private readonly IRepositoryAsync<BusStop> _busStopsRepository;

//        public BusStopsService(IRepositoryAsync<BusStop> stopsRepository)
//        {
//            _busStopsRepository = stopsRepository;
//        }

//        public async Task<List<BusStop>> GetAllAsync()
//        {
//            var students = await _busStopsRepository.GetAllAsync();
//            return _mapper.Map<IEnumerable<BusStop>, List<BusStop>>(students);
//        }

//        public async Task<BusStop> GetByIdAsync(int id)
//        {
//            var student = await _unitOfWork.Students.GetAsync(id);

//            return _mapper.Map<BusStop>(student);
//        }

//        public async Task AddAsync(BusStop studentDto)
//        {
//            if (studentDto == null)
//                throw new ArgumentException();

//            var student = _mapper.Map<BusStop>(studentDto);

//            await _unitOfWork.Students.CreateAsync(student);
//            // _unitOfWork.Save();
//        }

//        public async Task EditAsync(BusStop studentDto)
//        {
//            if (studentDto == null)
//                throw new ArgumentException();

//            var student = _mapper.Map<BusStop>(studentDto);

//            await _unitOfWork.Students.UpdateAsync(student);
//            //_unitOfWork.Save();
//        }

//        public async Task DeleteAsync(int id)
//        {
//            await _unitOfWork.Students.DeleteAsync(id);
//            //_unitOfWork.Save();
//        }
//        public async Task DeleteStudentFromGroupAsync(int studentId)
//        {
//            var student = await _unitOfWork.Students.GetAsync(studentId);
//            student.GroupId = null;
//            //Debug.WriteLine($"{student.GroupId.HasValue}  {student.Group}");
//            await _unitOfWork.Students.UpdateAsync(student);
//        }

//        public async Task AddStudentToGroupAsync(int studentId, int groupId)
//        {
//            var student = await _unitOfWork.Students.GetAsync(studentId);

//            if (student.GroupId != groupId)
//            {
//                student.GroupId = groupId;
//                await _unitOfWork.Students.UpdateAsync(student);
//            }
//        }

//        public void Dispose()
//        {
//            _unitOfWork.Dispose();
//        }

//    }
//}
