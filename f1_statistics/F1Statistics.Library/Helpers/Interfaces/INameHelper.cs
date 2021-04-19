using F1Statistics.Library.Models.Responses;

namespace F1Statistics.Library.Helpers.Interfaces
{
    public interface INameHelper
    {
        string GetConstructorName(ConstructorDataResponse constructor);
        string GetDriverName(DriverDataResponse driver);
    }
}