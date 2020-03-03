using System;
using TDDxUnitCore.Domain._Base;

namespace TDDxUnitCore.Domain.Audiences
{
    public class AudienceConverter : IAudienceConverter
    {
        public Audience Convert(string audienceGiven)
        {
            RulerValidator.New().When(!Enum.TryParse<Audience>(audienceGiven, true, out var audienceConverted), Resources.InvalidAudience).ThrowException();
            return audienceConverted;
        }
    }
}