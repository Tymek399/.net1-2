using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

public class SystemMonitor
{
    private PerformanceCounter cpuCounter;
    private PerformanceCounter ramCounter;

    public SystemMonitor()
    {
        cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        ramCounter = new PerformanceCounter("Memory", "Available MBytes");
    }

    public float GetCpuUsage()
    {
        return cpuCounter.NextValue();
    }

    public float GetAvailableRam()
    {
        return ramCounter.NextValue();
    }
}

public class SystemEventLogger
{
    private EventLog eventLog;

    public SystemEventLogger(string sourceName)
    {
        if (!EventLog.SourceExists(sourceName))
        {
            EventLog.CreateEventSource(sourceName, "Application");
        }
        eventLog = new EventLog("Application", ".", sourceName);
    }

    public void LogEvent(string message, EventLogEntryType eventType)
    {
        eventLog.WriteEntry(message, eventType);
    }
}

[DataContract]
public class Configuration
{
    [DataMember]
    public string LogFilePath { get; set; }

    [DataMember]
    public string EventSourceName { get; set; }

    public Configuration()
    {
        LogFilePath = "log.txt";
        EventSourceName = "SystemMonitor";
    }
}

public class ConfigurationManager
{
    private const string configFileName = "config.json";

    public static Configuration LoadConfiguration()
    {
        Configuration config;
        if (File.Exists(configFileName))
        {
            using (FileStream fs = new FileStream(configFileName, FileMode.Open))
            {
                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Configuration));
                config = (Configuration)jsonSerializer.ReadObject(fs);
            }
        }
        else
        {
            config = new Configuration();
            SaveConfiguration(config);
        }
        return config;
    }

    public static void SaveConfiguration(Configuration config)
    {
        using (FileStream fs = new FileStream(configFileName, FileMode.Create))
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Configuration));
            jsonSerializer.WriteObject(fs, config);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Wczytaj konfigurację
        Configuration config = ConfigurationManager.LoadConfiguration();
        SystemEventLogger eventLogger = new SystemEventLogger(config.EventSourceName);
        SystemMonitor systemMonitor = new SystemMonitor();

        while (true)
        {
            float cpuUsage = systemMonitor.GetCpuUsage();
            float availableRam = systemMonitor.GetAvailableRam();

            LogDataToFile(config.LogFilePath, $"CPU Usage: {cpuUsage}%\tAvailable RAM: {availableRam} MB");
          
            if (cpuUsage > 90)
            {
                eventLogger.LogEvent($"High CPU Usage: {cpuUsage}%", EventLogEntryType.Warning);
            }
            if (availableRam < 100)
            {
                eventLogger.LogEvent($"Low Available RAM: {availableRam} MB", EventLogEntryType.Error);
            }

            System.Threading.Thread.Sleep(5000);
        }
    }

    static void LogDataToFile(string filePath, string data)
    {
        using (StreamWriter writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine($"{DateTime.Now}: {data}");
        }
    }
}
