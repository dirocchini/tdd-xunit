using System;
using System.Collections.Generic;
using System.Linq;
using TDDxUnitCore.Domain._Base;
using TDDxUnitCore.Domain.Audiences;

namespace TDDxUnitCore.Domain.Students
{
    public class StudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IAudienceConverter _audienceConverter;

        public StudentService(IStudentRepository studentRepository, IAudienceConverter audienceConverter)
        {
            _studentRepository = studentRepository;
            _audienceConverter = audienceConverter;
        }


        public void Save(StudentDTO studentDto)
        {
            var studentAlreadySaved = _studentRepository.GetByEmail(studentDto.Email);

            RulerValidator.New()
                .When(studentAlreadySaved != null && studentDto.Id != studentAlreadySaved.Id, Resources.EmailAlreadyTaken)
                .ThrowException();

            if (studentDto.Id > 0)
            {
                var studentFromDb = _studentRepository.GetById(studentDto.Id);
                studentFromDb.ChangeName(studentDto.Name);
            }
            else
            {
                var audience = _audienceConverter.Convert(studentDto.Audience);
                var student = new Student(studentDto.Name, studentDto.Document, studentDto.Email, audience);
                _studentRepository.Add(student);
            }
        }

        public List<StudentDTO> Getall()
        {
            var students = _studentRepository.Get();

            List<StudentDTO> studentsDto = students.Select(s => new StudentDTO(s.Id, s.Name, s.Document, s.Email, s.Audience.ToString())).ToList();

            return studentsDto;
        }
    }
}
