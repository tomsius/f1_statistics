using F1Statistics.Library.Models;

namespace F1Statistics.Library.Validators
{
    public interface IOptionsValidator
    {
        void ValidateOptionsModel(OptionsModel options);
    }
}