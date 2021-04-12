using F1Statistics.Library.Models;

namespace F1Statistics.Library.Validators.Interfaces
{
    public interface IOptionsValidator
    {
        bool AreFastestLapYearsValid(OptionsModel options);
        bool IsLapTimesSeasonValid(int season);
        void NormalizeOptionsModel(OptionsModel options);
    }
}