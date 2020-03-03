using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TDDxUnitCore.Domain._Base;
using TDDxUnitCore.Domain.Audiences;

namespace TDDxUnitCore.Domain.Students
{
    public class StudentService : ServiceBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAudienceConverter _audienceConverter;

        public StudentService(IStudentRepository studentRepository, IAudienceConverter audienceConverter, IMapper mapper) : base(mapper)
        {
            _studentRepository = studentRepository;
            _audienceConverter = audienceConverter;
        }


        public void Save(StudentDTO studentDto)
        {
            var studentAlreadySaved = _studentRepository.GetByEmail(studentDto.Email);

            RulerValidator.New()
                .When(studentAlreadySaved != null && studentDto.Id != studentAlreadySaved.Id,
                    Resources.EmailAlreadyTaken)
                .ThrowException();

            if (studentDto.Id > 0)
            {
                var studentFromDb = _studentRepository.GetById(studentDto.Id);
                studentFromDb.ChangeName(studentDto.Name);
            }
            else
            {
                var audience = _audienceConverter.Convert(studentDto.Audience);
                //var student = new Student(studentDto.Name, studentDto.Document, studentDto.Email, audience);
                var student = _mapper.Map<Student>(studentDto);
                _studentRepository.Add(student);
            }
        }

        public List<StudentDTO> GetAll()
        {
            var students = _studentRepository.Get();
            List<StudentDTO> studentsDto = _mapper.Map<List<StudentDTO>>(students);
            return studentsDto;
        }

        public StudentDTO GetByCPF(string cpf)
        {
            var student = _studentRepository.GetByCPF(cpf);
            return _mapper.Map<StudentDTO>(student);
        }

        public StudentDTO Get(int id)
        {
            var student = _studentRepository.GetById(id);
            return _mapper.Map<StudentDTO>(student);
        }
    }
}