using System.Diagnostics;
using GamingTaskKiller.information_gathering;

var lister = new ProcessLister();

var processes = lister.GetProcesses();

var i = 0;