using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.Services.Interfaces
{
    public interface IPodiumsService
    {
        List<PodiumsModel> AggregateDriversPodiums(OptionsModel options);
        List<PodiumsModel> AggregateConstructorsPodiums(OptionsModel options);
        List<SamePodiumsModel> AggregateSameDriverPodiums(OptionsModel options);
        List<SamePodiumsModel> AggregateSameConstructorsPodiums(OptionsModel options);
    }
}
