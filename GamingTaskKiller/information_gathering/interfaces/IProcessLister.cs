using GamingTaskKiller.information_gathering.model;

namespace GamingTaskKiller.information_gathering.interfaces;

public interface IProcessLister
{
    IEnumerable<ProcessInformation> GetProcesses();
}