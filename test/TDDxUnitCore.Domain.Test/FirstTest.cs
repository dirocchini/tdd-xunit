using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TDDxUnitCore.Domain.Test
{
    public class FirstTest
    {
        [Fact(DisplayName = "Test")]
        public void Test()
        {
            var a = 1;
            var b = 1;

            Assert.Equal(a, b);
        }
    }
}
