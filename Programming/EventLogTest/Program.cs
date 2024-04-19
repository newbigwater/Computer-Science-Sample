using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventLogTest
{
    class Program
    {
        static void DisplayEntry(object sender, EntryWrittenEventArgs e)
        {
            EventLogEntry entry = e.Entry;
            Console.WriteLine(entry.Message);
        }

        static void Main(string[] args)
        {
            string sourceName = "NBW.EventLog.Source.Application";
            #region Case 01. Write : Application Level, Default
            {
                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, "Application");
                }

                EventLog.WriteEntry(sourceName, "[EventLog.WriteEntry] 테스트로 로그를 기록합니다...", EventLogEntryType.Information);
                EventLog.WriteEntry(sourceName, "[EventLog.WriteEntry] EventLogEntryType.Error", EventLogEntryType.Error);
                EventLog.WriteEntry(sourceName, "[EventLog.WriteEntry] EventLogEntryType.Warning", EventLogEntryType.Warning);
                EventLog.WriteEntry(sourceName, "[EventLog.WriteEntry] EventLogEntryType.Information", EventLogEntryType.Information);
                EventLog.WriteEntry(sourceName, "[EventLog.WriteEntry] EventLogEntryType.SuccessAudit", EventLogEntryType.SuccessAudit);
                EventLog.WriteEntry(sourceName, "[EventLog.WriteEntry] EventLogEntryType.FailureAudit", EventLogEntryType.FailureAudit);
            }
            #endregion

            sourceName = "NBW.EventLog.Source.UserDefine";
            string machineName = Environment.MachineName;
            string logName = "NBW.EventLog.Log";
            #region Case 02. Write : NBW.Log Level
            {
//                 if (EventLog.SourceExists(sourceName))
//                     EventLog.DeleteEventSource(sourceName);

                if (EventLog.Exists(logName))
                    EventLog.Delete(logName);

                EventLog.CreateEventSource(sourceName, logName);

                var eventLogger = new EventLog(logName);
                eventLogger.Source = sourceName;
                // 로그 쓰여질 때 감시 설정
                eventLogger.EnableRaisingEvents = true;
                eventLogger.EntryWritten += DisplayEntry;

                eventLogger.WriteEntry("[eventLogger] 테스트로 로그를 기록합니다...", EventLogEntryType.Information);
                eventLogger.WriteEntry("[eventLogger] EventLogEntryType.Error", EventLogEntryType.Error);
                eventLogger.WriteEntry("[eventLogger] EventLogEntryType.Warning", EventLogEntryType.Warning);
                eventLogger.WriteEntry("[eventLogger] EventLogEntryType.Information", EventLogEntryType.Information);
                eventLogger.WriteEntry("[eventLogger] EventLogEntryType.SuccessAudit", EventLogEntryType.SuccessAudit);
                eventLogger.WriteEntry("[eventLogger] EventLogEntryType.FailureAudit", EventLogEntryType.FailureAudit);
            }
            #endregion

            Thread.Sleep(1000);

            #region Read

            if (EventLog.SourceExists(sourceName))
            {
                if (EventLog.Exists(logName))
                {
                    var eventLogger = new EventLog(logName);
                    Console.WriteLine($"└ EventLog");
                    Console.WriteLine($"    ├      Source : {eventLogger.Source}");
                    Console.WriteLine($"    ├     Machine : {eventLogger.MachineName}");
                    Console.WriteLine($"    ├    Log Name : {eventLogger.Log}");
                    Console.WriteLine($"    └ Log Display : {eventLogger.LogDisplayName}");
                    for (int j = 0; j < eventLogger.Entries.Count; j++)
                    {
                        var eventLoggerEntry = eventLogger.Entries[j];
                        if (j == eventLogger.Entries.Count - 1)
                        {
                            Console.WriteLine($"        └ [{j}] EventLogEntry");
                            Console.WriteLine("             ├          Index : " + eventLoggerEntry.Index);
                            Console.WriteLine("             ├     InstanceId : " + eventLoggerEntry.InstanceId);
                            Console.WriteLine("             ├         Source : " + eventLoggerEntry.Source);
                            Console.WriteLine("             ├      EntryType : " + eventLoggerEntry.EntryType);
                            Console.WriteLine("             ├        Message : " + eventLoggerEntry.Message);
                            Console.WriteLine("             ├    MachineName : " + eventLoggerEntry.MachineName);
                            Console.WriteLine("             ├       UserName : " + eventLoggerEntry.UserName);
                            Console.WriteLine("             ├       Category : " + eventLoggerEntry.Category);
                            Console.WriteLine("             ├ CategoryNumber : " + eventLoggerEntry.CategoryNumber);
                            Console.WriteLine("             ├    TimeWritten : " + eventLoggerEntry.TimeWritten);
                            Console.WriteLine("             └  TimeGenerated : " + eventLoggerEntry.TimeGenerated);
                        }
                        else
                        {
                            Console.WriteLine($"        ├ [{j}] EventLogEntry");

                            Console.WriteLine("        │    ├          Index : " + eventLoggerEntry.Index);
                            Console.WriteLine("        │    ├     InstanceId : " + eventLoggerEntry.InstanceId);
                            Console.WriteLine("        │    ├         Source : " + eventLoggerEntry.Source);
                            Console.WriteLine("        │    ├      EntryType : " + eventLoggerEntry.EntryType);
                            Console.WriteLine("        │    ├        Message : " + eventLoggerEntry.Message);
                            Console.WriteLine("        │    ├    MachineName : " + eventLoggerEntry.MachineName);
                            Console.WriteLine("        │    ├       UserName : " + eventLoggerEntry.UserName);
                            Console.WriteLine("        │    ├       Category : " + eventLoggerEntry.Category);
                            Console.WriteLine("        │    ├ CategoryNumber : " + eventLoggerEntry.CategoryNumber);
                            Console.WriteLine("        │    ├    TimeWritten : " + eventLoggerEntry.TimeWritten);
                            Console.WriteLine("        │    └  TimeGenerated : " + eventLoggerEntry.TimeGenerated);
                        }
                    }
                }
                else
                    Console.WriteLine("출력할 수 있는 로그가 존재하지 않습니다.");

                #region Test
                {
                    /*
                    EventLog[] eventLoggers = EventLog.GetEventLogs();
                    Console.WriteLine($"전체 로그 이벤트 수: {eventLoggers.Length}");
                    for (int i = 0; i < eventLoggers.Length; i++)
                    {
                        EventLog eventLogger = eventLoggers[i];
                        Console.WriteLine($"└ 로그 이벤트 [{i}]");
                        Console.WriteLine($"    ├      Source : {eventLogger.Source}");
                        Console.WriteLine($"    ├     Machine : {eventLogger.MachineName}");
                        Console.WriteLine($"    ├    Log Name : {eventLogger.Log}");
                        Console.WriteLine($"    └ Log Display : {eventLogger.LogDisplayName}");
                        for (int j = 0; j < eventLogger.Entries.Count; j++)
                        {
                            var eventLoggerEntry = eventLogger.Entries[i];
                            Console.WriteLine("        ├          Index : " + eventLoggerEntry.Index);
                            Console.WriteLine("        ├     InstanceId : " + eventLoggerEntry.InstanceId);
                            Console.WriteLine("        ├         Source : " + eventLoggerEntry.Source);
                            Console.WriteLine("        ├      EntryType : " + eventLoggerEntry.EntryType);
                            Console.WriteLine("        ├        Message : " + eventLoggerEntry.Message);
                            Console.WriteLine("        ├    MachineName : " + eventLoggerEntry.MachineName);
                            Console.WriteLine("        ├       UserName : " + eventLoggerEntry.UserName);
                            Console.WriteLine("        ├       Category : " + eventLoggerEntry.Category);
                            Console.WriteLine("        ├ CategoryNumber : " + eventLoggerEntry.CategoryNumber);
                            Console.WriteLine("        ├    TimeWritten : " + eventLoggerEntry.TimeWritten);
                            Console.WriteLine("        └  TimeGenerated : " + eventLoggerEntry.TimeGenerated);
                        }
                    }
                    */
                }
                #endregion
            }
            else
                Console.WriteLine("출력할 수 있는 로그 소스가 존재하지 않습니다.");

            #endregion

            Console.ReadLine();
        }
    }
}
