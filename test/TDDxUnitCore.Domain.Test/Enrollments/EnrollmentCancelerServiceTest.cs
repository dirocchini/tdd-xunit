using System;
using Moq;
using TDDxUnitCore.Domain._Base;
using TDDxUnitCore.Domain.Enrollments;
using TDDxUnitCore.Domain.Test._Builders;
using TDDxUnitCore.Domain.Test._Tooling;
using Xunit;

namespace TDDxUnitCore.Domain.UnitTest.Enrollments
{
    public class EnrollmentCancelerTest
    {
        private readonly Mock<IEnrollmentRepository> _enrollmentRepositoryMock;
        private readonly EnrollmentCancelerService _enrollmentCancelerService;

        public EnrollmentCancelerTest()
        {
            _enrollmentRepositoryMock = new Mock<IEnrollmentRepository>();
            _enrollmentCancelerService = new EnrollmentCancelerService(_enrollmentRepositoryMock.Object);
        }


        [Fact]
        public void CancelEnrollment_MustCancel_Void()
        {
            var enrollment = BuilderEnrollment.New().Build();
            _enrollmentRepositoryMock.Setup(r => r.GetById(enrollment.Id)).Returns(enrollment);

            _enrollmentCancelerService.CancelEnrollment(enrollment.Id);

            Assert.True(enrollment.IsCancelled);
        }

        [Fact]
        public void CancelEnrollment_MustNotCancelIfEnrollmentNotFound_Void()
        {
            Enrollment invalidEnrollment = null;

            _enrollmentRepositoryMock.Setup(r => r.GetById(It.IsAny<int>())).Returns(invalidEnrollment);

            Assert.Throws<DomainCustomException>(() =>
                    _enrollmentCancelerService.CancelEnrollment(It.IsAny<int>())
                ).WithMessage(Resources.EnrollmentNotFound);
        }
    }
}