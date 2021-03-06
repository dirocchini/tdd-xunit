﻿using AutoMapper;
using ExpectedObjects;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using TDDxUnitCore.Domain._Base;
using TDDxUnitCore.Domain.Audiences;
using TDDxUnitCore.Domain.Courses;
using TDDxUnitCore.Domain.Enrollments;
using TDDxUnitCore.Domain.Students;
using TDDxUnitCore.Domain.Test._Builders;
using TDDxUnitCore.Domain.Test._Tooling;
using TDDxUnitCore.Domain.UnitTest._Builders;
using Xunit;

namespace TDDxUnitCore.Domain.UnitTest.Enrollments
{
    public class EnrollmentTest
    {
        [Fact]
        public void MustAdd()
        {
            var enrollmentExpected = new
            {
                Student = BuilderStudent.New().WithAudience(Audience.CTO).Build(),
                Course = BuilderCourse.New().WithAudience(Audience.CTO).Build(),
                PaidValue = 123.93
            };

            var enrollment = new Enrollment(enrollmentExpected.Student, enrollmentExpected.Course,
                enrollmentExpected.PaidValue);
            enrollmentExpected.ToExpectedObject().ShouldMatch(enrollment);
        }


        [Fact]
        public void EnrollmentMustHaveValidStudent()
        {
            Student invalidStudent = null;
            Assert.Throws<DomainCustomException>(() => BuilderEnrollment.New().WithStudent(invalidStudent).Build())
                .WithMessage(Resources.InvalidStudent);
        }


        [Fact]
        public void EnrollmentMustHaveValidCourse()
        {
            Course invalidCourse = null;
            Assert.Throws<DomainCustomException>(() => BuilderEnrollment.New().WithCourse(invalidCourse).Build())
                .WithMessage(Resources.InvalidCourse);
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void EnrollmentMustHaveValidPaidValue(double invalidPaidValue)
        {
            Assert.Throws<DomainCustomException>(() => BuilderEnrollment.New().WithPaidValue(invalidPaidValue).Build())
                .WithMessage(Resources.InvalidPaidValue);
        }


        [Fact]
        public void EnrollmentPaidCostMustBeLessOrEqualToCostValue()
        {
            Course course = BuilderCourse.New().WithCost(100).Build();
            double invalidPaidValue = course.Cost + 1;

            Assert.Throws<DomainCustomException>(() =>
                    BuilderEnrollment.New().WithCourse(course).WithPaidValue(invalidPaidValue).Build())
                .WithMessage(Resources.InvalidPaidOriginalCost);
        }


        [Fact]
        public void EnrollmentMustHasDiscount() //dont make sense
        {
            Course course = BuilderCourse.New().WithCost(100).Build();
            double paidValue = course.Cost - 1;

            var enrollment = BuilderEnrollment.New().WithPaidValue(paidValue).WithCourse(course).Build();

            Assert.True(enrollment.HasDiscount);
        }


        [Fact]
        public void AudienceMustBeEqual()
        {
            var course = BuilderCourse.New().WithAudience(Audience.CTO).Build();
            var student = BuilderStudent.New().WithAudience(Audience.Student).Build();

            Assert.Throws<DomainCustomException>(() =>
                    BuilderEnrollment.New().WithCourse(course).WithStudent(student).Build())
                .WithMessage(Resources.AudienceNotEquals);
        }


        [Fact]
        public void MustSetStudantGrade()
        {
            decimal validGrade = 5;
            var enrollment = BuilderEnrollment.New().Build();

            enrollment.SetStudentGrade(validGrade);
            Assert.Equal(validGrade, enrollment.StudentGrade);
        }


        [Theory]
        [InlineData(-1)]
        [InlineData(11)]
        public void GradeMustBeValid(decimal invalidStudentGradeValue)
        {
            var enrollment = BuilderEnrollment.New().Build();
            Assert.Throws<DomainCustomException>(() => enrollment.SetStudentGrade(invalidStudentGradeValue))
                .WithMessage(Resources.InvalidGrade);
        }


        [Fact]
        public void MustIndicatedFinishedCourse()
        {
            decimal validStudentGrade = 9.5M;

            var enrollment = BuilderEnrollment.New().Build();
            enrollment.SetStudentGrade(validStudentGrade);

            Assert.True(enrollment.CourseFinished);
        }


        [Fact]
        public void CancelEnrollment_MustCancel_Void()
        {
            var enrollment = BuilderEnrollment.New().WithCancelled(true).Build();

            Assert.True(enrollment.IsCancelled);
        }


        [Fact]
        public void CancelEnrollment_MustNotCancelIfFinished_Exception()
        {
            var enrollment = BuilderEnrollment.New().WithFinished(true).Build();

            Assert.Throws<DomainCustomException>(() => enrollment.Cancel())
                .WithMessage(Resources.ThisEnrollmentIsAlreadyFinished);
        }


        [Fact]
        public void SetStudentGrade_MustNotSetIfCancelledEnrollment_Exception()
        {
            Enrollment enrollment = BuilderEnrollment.New().WithCancelled(true).Build();

            Assert.Throws<DomainCustomException>(() => { enrollment.SetStudentGrade(6); }
            ).WithMessage(Resources.CantFinishACancelledEnrollment);
        }



    }
}