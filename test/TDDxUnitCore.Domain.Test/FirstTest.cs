using TDDxUnitCore.CrossCutting.Extensions;
using Xunit;

namespace TDDxUnitCore.Domain.Test
{
    public class FirstTest
    {
        [Fact(DisplayName = "Test")]
        public void Test()
        {
            //Organization
            var a = 1;
            var b = 1;


            //Action
            //a = b;


            //Assert
            Assert.Equal(a, b);
        }


        [Theory]
        [InlineData("Diego@d iego.com", "diegodiegocom")]
        [InlineData("D@! - ie/go@d iego.com", "diegodiegocom")]
        public void ComparableString_ValidComparison_True(string input, string expectedValue)
        {
            var newInput = input.ComparableString();
            

            Assert.Equal(expectedValue, newInput);
        }
    }
}
