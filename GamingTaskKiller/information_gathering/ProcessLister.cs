using System.Diagnostics;
using GamingTaskKiller.information_gathering.interfaces;
using GamingTaskKiller.information_gathering.model;

namespace GamingTaskKiller.information_gathering;

public class ProcessLister : IProcessLister
{
    private static string _usernameColumnIdentifier = "\"Benutzername\"";
    private static string _processIdColumnIdentifier = "\"PID\"";
    private static string _processNameColumnIdentifier = "\"Abbildname\"";

    public IEnumerable<ProcessInformation> GetProcesses()
    {
        var processCsv = GetProcessInformationCsv();

        var lines = processCsv.Split(Environment.NewLine);

        var (usernameIndex, processidIndex, processNameIndex) = GetColumnIndexes(lines.First());

        List<ProcessInformation> processInformation = new List<ProcessInformation>();

        foreach (var line in lines.Skip(1))
        {
            var columns = line.Split(",");
            if (columns.Length <= int.Max(usernameIndex, int.Max(processidIndex, processNameIndex)))
            {
                continue;
            }

            processInformation.Add(new ProcessInformation(columns[processNameIndex],
                columns[processidIndex],
                columns[usernameIndex]));
        }

        return processInformation;
    }

    private string GetProcessInformationCsv()
    {
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "tasklist.exe",
                Arguments = "/fi \"ImageName eq wall*\" /v /fo csv",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
            }
        };

        process.Start();
        process.WaitForExit();

        return process.StandardOutput.ReadToEnd();
    }

    private (int usernameIndex, int processidIndex, int processNameIndex) GetColumnIndexes(string firstLine)
    {
        int usernameIndex = -1;
        int processidIndex = -1;
        int processNameIndex = -1;

        var columns = firstLine.Split(",");

        for (int i = 0; i < columns.Count(); i++)
        {
            if (columns[i] == _usernameColumnIdentifier)
            {
                usernameIndex = i;
            }

            if (columns[i] == _processIdColumnIdentifier)
            {
                processidIndex = i;
            }

            if (columns[i] == _processNameColumnIdentifier)
            {
                processNameIndex = i;
            }
        }

        if (usernameIndex == -1)
        {
            throw new Exception(string.Format("Username column with Designator {0} not found",
                _usernameColumnIdentifier));
        }

        if (processidIndex == -1)
        {
            throw new Exception(string.Format("Process ID column with Designator {0} not found",
                _processIdColumnIdentifier));
        }

        if (processNameIndex == -1)
        {
            throw new Exception(string.Format("Process Name column with Designator {0} not found",
                _processNameColumnIdentifier));
        }

        return (usernameIndex, processidIndex, processNameIndex);
    }
}