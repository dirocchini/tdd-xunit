using TDDxUnitCore.Domain._Base;
using TDDxUnitCore.Domain.Audiences;
using Xunit;
using TDDxUnitCore.Domain.Test._Tooling;


namespace TDDxUnitCore.Domain.UnitTest.Audiences
{
    public class AudienceConverterTest
    {
        private readonly AudienceConverter _converter;
        public AudienceConverterTest()
        {
            _converter = new AudienceConverter();
        }

        [Theory]
        [InlineData(Audience.Student, "Student")]
        [InlineData(Audience.NonDev, "NonDev")]
        [InlineData(Audience.Employee, "employee")]
        [InlineData(Audience.CTO, "CTO")]
        public void Convert_MustConvert_ValidAudience(Audience audienceExpected, string audienceGiven)
        {
            Audience audienceConverted = _converter.Convert(audienceGiven);
            Assert.Equal(audienceExpected, audienceConverted);
        }

        [Fact]
        public void Convert_MustNotConvertInvalid_Exception()
        {
            var invalidAudience = "Invalid";
            Assert.Throws<DomainCustomException>(() => _converter.Convert(invalidAudience)).WithMessage(Resources.InvalidAudience);
        }
    }
}
