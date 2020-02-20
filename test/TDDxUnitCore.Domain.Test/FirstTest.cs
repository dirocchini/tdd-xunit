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
    }
}
