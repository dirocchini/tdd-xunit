using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDDxUnitCore.Domain._Base
{
    public class RulerValidator
    {
        private readonly List<string> _errorMessages;

        private RulerValidator()
        {
            _errorMessages = new List<string>();
        }

        public static RulerValidator New()
        {
            return new RulerValidator();
        }

        public RulerValidator When(bool hasError, string errorMessage)
        {
            if (hasError)
                _errorMessages.Add(errorMessage);

            return this;
        }

        public void ThrowException()
        {
            if (_errorMessages.Any())
            {
                throw new DomainCustomException(_errorMessages);
            }
        }
    }

    public class DomainCustomException : Exception
    {
        public List<string> ErrorMessages { get; set; }

        public DomainCustomException(List<string> messages)
        {
            ErrorMessages = messages;
        }
    }
}
