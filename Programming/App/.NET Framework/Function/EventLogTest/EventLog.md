#system #diagnostics #eventlog


> [!Quote]- Quote
> * https://learn.microsoft.com/ko-kr/dotnet/api/system.diagnostics.eventlog?view=dotnet-plat-ext-8.0

## 01. EventLog Class
### 01.01. Definition

```cs
public class EventLog : System.ComponentModel.Component, System.ComponentModel.ISupportInitialize
```
- Inheritance : Object <|- MarchalByRefObject <|- Component <|- EventLog
- Realization : ISupportInitialize

### 01.02. Explaination
- [EventLog](https://learn.microsoft.com/ko-kr/dotnet/api/system.diagnostics.eventlog?view=dotnet-plat-ext-8.0) 를 사용하면 중요한 소프트웨어 또는 하드웨어 이벤트에 대한 정보를 기록하는 Windows 이벤트 로그에 액세스하거나 사용자 지정할 수 있습니다. 
- [EventLog](https://learn.microsoft.com/ko-kr/dotnet/api/system.diagnostics.eventlog?view=dotnet-plat-ext-8.0)를 사용하여 기존 로그에서 읽고, 로그에 항목을 쓰고, 이벤트 원본을 만들거나 삭제하고, 로그를 삭제하고, 로그 항목에 응답할 수 있습니다. 이벤트 원본을 만들 때 새 로그를 만들 수도 있습니다.
	
> [!Important] 중요
> 이 형식이 구현 하는 [IDisposable](https://learn.microsoft.com/ko-kr/dotnet/api/system.idisposable?view=dotnet-plat-ext-8.0) 인터페이스입니다. 
> 형식을 사용하여 마쳤으면 직접 또는 간접적으로의 삭제 해야 있습니다. 
> 직접 형식의 dispose 호출 해당 [Dispose](https://learn.microsoft.com/ko-kr/dotnet/api/system.idisposable.dispose?view=dotnet-plat-ext-8.0) 의 메서드를 `try`/`catch` 블록입니다.


### 01.03. For example
#### 01.03.01. Source
```cs
const string sourceName = "NBW.EventTest";

if (!EventLog.SourceExists(sourceName))
{
	EventLog.CreateEventSource(sourceName, "Application");
}

EventLog.WriteEntry(sourceName, "테스트로 로그를 기록합니다...", EventLogEntryType.Information);
EventLog.WriteEntry(sourceName, "EventLogEntryType.Error", EventLogEntryType.Error);
EventLog.WriteEntry(sourceName, "EventLogEntryType.Warning", EventLogEntryType.Warning);
EventLog.WriteEntry(sourceName, "EventLogEntryType.Information", EventLogEntryType.Information);
EventLog.WriteEntry(sourceName, "EventLogEntryType.SuccessAudit", EventLogEntryType.SuccessAudit);
EventLog.WriteEntry(sourceName, "EventLogEntryType.FailureAudit", EventLogEntryType.FailureAudit);
```

#### 01.03.02. Result

![](attachments/Pasted%20image%2020240419132033.png)

![](attachments/Pasted%20image%2020240419132122.png)

---
## 02. User defined
### 02.01. For Example
```cs
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
```
### 02.02. Result

![](attachments/Pasted%20image%2020240419155820.png)

![](attachments/Pasted%20image%2020240419155752.png)
