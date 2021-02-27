using F1Statistics.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace F1Statistics.Library.DataAggregation.Interfaces
{
    public interface IPodiumsAggregator
    {
        List<PodiumsModel> GetDriversPodiums(int from, int to);
        List<PodiumsModel> GetConstructorsPodiums(int from, int to);
        List<SamePodiumsModel> GetSameDriversPodiums(int from, int to);
        List<SamePodiumsModel> GetSameConstructorsPodiums(int from, int to);
    }
}
