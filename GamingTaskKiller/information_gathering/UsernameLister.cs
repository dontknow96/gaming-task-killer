using GamingTaskKiller.information_gathering.interfaces;

namespace GamingTaskKiller.information_gathering;

public class UsernameLister : IUsernameLister
{
    public string GetUsername()
    {
        var domain = Environment.GetEnvironmentVariable("USERDOMAIN");
        var username = Environment.GetEnvironmentVariable("USERNAME");

        return string.Format("{0}\\{1}", domain, username);
    }
}