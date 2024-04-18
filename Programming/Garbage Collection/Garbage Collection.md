#GC 

## 01. KeepAlive
#KeepAlive

> [!Quote]- Quote
> https://learn.microsoft.com/ko-kr/dotnet/api/system.gc.keepalive?view=netframework-4.0&devlangs=csharp&f1url=%3FappId%3DDev16IDEF1%26l%3DKO-KR%26k%3Dk(System.GC.KeepAlive)%3Bk(SolutionItemsProject)%3Bk(TargetFrameworkMoniker-.NETFramework%2CVersion%253Dv4.0)%3Bk(DevLang-csharp)%26rd%3Dtrue

```CS
public static void KeepAlive (object obj);
```
- Description
	- 지정된 개체를 참조하여 현재 루틴이 시작된 지점에서 이 메서드가 호출된 지점까지 가비지 컬렉션이 불가능하도록 합니다.
- Argument
	- Object obj : 참조할 개체	

## 02. Test
> [!Quote]- Quote
> https://www.sysnet.pe.kr/2/0/13053?pageno=0

### 02.01. Test Class
```cs
public class StringClass : IDisposable
{
	private byte[] bytes = new byte[10];

	public byte[] Bytes
	{
		get => bytes;
		private set => bytes = value;
	}

	public string Value
	{
		get => Encoding.ASCII.GetString(Bytes);
		set => Bytes = Encoding.ASCII.GetBytes(value);
	}
	

	public void Dispose()
	{
		this.bytes = null;
	}
}

public class KeepAliveTestClass
{
	private string funcName = "";
	private StringClass strClass = new StringClass();

	public StringClass Value
	{
		get => strClass;
		set => strClass = value;
	}

	public KeepAliveTestClass(string funcName = "")
	{
		this.funcName = funcName;
		strClass.Value = "0123456789";
	}

	~KeepAliveTestClass()
	{
		strClass.Dispose();
		Console.WriteLine($"[{this.funcName}] GC");
	}

	public void MyMethod_Before()
	{
		Console.WriteLine($"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] Start");

		KeepAliveTestClass obj = new KeepAliveTestClass(System.Reflection.MethodBase.GetCurrentMethod().Name);
		StringClass obj2 = obj.Value;// 이 코드 이후로는 obj 개체를 참조하지 않음!

		Task.Run(() =>
		{
			try
			{
				Console.WriteLine($"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] Task.Run Start");

				// ... 만약, 바로 이 시점에 GC가 호출된다면?
				Thread.Sleep(1000);

				// obj still alive here? Possibly not.
				TestMethod(obj2);
				Console.WriteLine($"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] Task.Run End");
			}
			catch (Exception exp)
			{
				Console.WriteLine($"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] {exp.Message}");
			}
		});

		GC.Collect();
		Console.WriteLine($"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] end");
	}

	public void MyMethod_After()
	{
		Console.WriteLine($"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] Start");

		KeepAliveTestClass obj = new KeepAliveTestClass(System.Reflection.MethodBase.GetCurrentMethod().Name);
		StringClass obj2 = obj.Value;// 이 코드 이후로는 obj 개체를 참조하지 않음!
		GC.KeepAlive(obj2);

		Task.Run(() =>
		{
			try
			{
				Console.WriteLine($"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] Task.Run Start");

				// ... 만약, 바로 이 시점에 GC가 호출된다면?
				Thread.Sleep(1000);

				// obj still alive here? Possibly not.
				TestMethod(obj2);
				Console.WriteLine($"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] Task.Run End");
			}
			catch (Exception exp)
			{
				Console.WriteLine($"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] {exp.Message}");
			}
		});

		GC.Collect();
		Console.WriteLine($"[{System.Reflection.MethodBase.GetCurrentMethod().Name}] end");
	}

	void TestMethod(StringClass obj2, [CallerMemberName] string funcName = "")
	{
		Console.WriteLine($"[{funcName}] Value : {obj2.Value}");
	}
}
```

### 02.02. Main
```cs
class Program
{
	static void Main(string[] args)
	{
		KeepAliveTestClass keepAliveTestObj1 = new KeepAliveTestClass();
		keepAliveTestObj1.MyMethod_Before();

		KeepAliveTestClass keepAliveTestObj2 = new KeepAliveTestClass();
		keepAliveTestObj2.MyMethod_After();

		while (true)
		{
			for(int i = 0; i < 10; i++)
				Thread.Sleep(100);

			Console.Write($"Input : ");
			string input = Console.ReadLine();
			bool bCheck = int.Parse(input) == 0 ? true : false;
			if (bCheck)
				break;

			GC.Collect();
		}
	}
}
```

### 02.03. Result

![](attachments/Pasted%20image%2020240418144349.png)