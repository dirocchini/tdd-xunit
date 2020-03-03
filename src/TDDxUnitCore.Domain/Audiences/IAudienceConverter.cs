namespace TDDxUnitCore.Domain.Audiences
{
    public interface IAudienceConverter
    {
        Audience Convert(string audienceGiven);
    }
}