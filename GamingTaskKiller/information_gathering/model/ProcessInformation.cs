namespace GamingTaskKiller.information_gathering.model;

public class ProcessInformation
{
    public ProcessInformation(string processName, string pid, string username)
    {
        ProcessName = processName;
        Pid = pid;
        Username = username;
    }

    public string ProcessName { get; set; }
    public string Pid { get; set; }
    public string Username { get; set; }
}