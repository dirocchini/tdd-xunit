using System;
using System.Linq;
using TDDxUnitCore.Domain._Base;
using Xunit;

namespace TDDxUnitCore.Domain.Test._Tooling
{
    public static class AssertExtension
    {
        public static void WithMessage(this DomainCustomException exception, string message)
        {
            if (exception.ErrorMessages.Contains(message))
                Assert.True(true);
            else
                Assert.False(true, $"Message expected -> {message}  |  Received message -> {exception.Message}");
        }
    }
}
