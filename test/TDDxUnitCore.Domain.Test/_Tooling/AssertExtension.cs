using System;
using Xunit;

namespace TDDxUnitCore.Domain.Test._Tooling
{
    public static class AssertExtension
    {
        public static void WithMessage(this ArgumentException exception, string message)
        {
            if (exception.Message.ToLower().Trim() == message.ToLower().Trim())
                Assert.True(true);
            else
                Assert.False(true, $"Message expected -> {message}  |  Received message -> {exception.Message}");
        }
    }
}
