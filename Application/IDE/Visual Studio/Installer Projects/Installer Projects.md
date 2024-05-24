#IDE #VisualStudio2019

[..](../Visual%20Studio.md)

> [!Quote]- Quote
> https://learn.microsoft.com/ko-kr/windows/win32/msi/windows-installer-portal
> https://learn.microsoft.com/en-us/previous-versions/visualstudio/visual-studio-2010/wx3b589t(v=vs.100)
> https://m.blog.naver.com/terry7990/221978905502
> https://tjstory.tistory.com/120


## 01. Ready
### 01.01. 테스트 프로그램 준비
![Pasted image 20240401144745.png](attachments/Pasted%20image%2020240401144745.png)
![Pasted image 20240401144319.png](attachments/Pasted%20image%2020240401144319.png)

> [!danger] 중요!
> 모든 프로젝트는 Release Mode에서 빌드가 에러 없이 되어야 한다.

- CalculatorLib -> Calculate.cs
```
public static class Calculate
{
	public static double Add(double n1, double n2)
	{
		double result = n1 + n2;
		Console.WriteLine("Received Add({0},{1})", n1, n2);
		Console.WriteLine("Return: {0}", result);
		return result;
	}

	public static double Subtract(double n1, double n2)
	{
		double result = n1 - n2;
		Console.WriteLine("Received Subtract({0},{1})", n1, n2);
		Console.WriteLine("Return: {0}", result);
		return result;
	}

	public static double Multiply(double n1, double n2)
	{
		double result = n1 * n2;
		Console.WriteLine("Received Multiply({0},{1})", n1, n2);
		Console.WriteLine("Return: {0}", result);
		return result;
	}

	public static double Divide(double n1, double n2)
	{
		double result = n1 / n2;
		Console.WriteLine("Received Divide({0},{1})", n1, n2);
		Console.WriteLine("Return: {0}", result);
		return result;
	}
}
```
- Calculator -> MainForm
```
public partial class MainForm : Form
{
	public MainForm()
	{
		InitializeComponent();
	}

	private void btn_add_Click(object sender, EventArgs e)
	{
		double result = Calculate.Add(Double.Parse(edt_fir.Text), Double.Parse(edt_sec.Text));
		lbl_result.Text = result.ToString();
		Console.WriteLine($"Add({edt_fir.Text},{edt_sec.Text}) = {result}");
	}

	private void btn_sub_Click(object sender, EventArgs e)
	{
		double result = Calculate.Subtract(Double.Parse(edt_fir.Text), Double.Parse(edt_sec.Text));
		lbl_result.Text = result.ToString();
		Console.WriteLine($"Add({edt_fir.Text},{edt_sec.Text}) = {result}");
	}

	private void btn_mul_Click(object sender, EventArgs e)
	{
		double result = Calculate.Multiply(Double.Parse(edt_fir.Text), Double.Parse(edt_sec.Text));
		lbl_result.Text = result.ToString();
		Console.WriteLine($"Add({edt_fir.Text},{edt_sec.Text}) = {result}");
	}

	private void btn_div_Click(object sender, EventArgs e)
	{
		double result = Calculate.Divide(Double.Parse(edt_fir.Text), Double.Parse(edt_sec.Text));
		lbl_result.Text = result.ToString();
		Console.WriteLine($"Add({edt_fir.Text},{edt_sec.Text}) = {result}");
	}
}
```
- Output 경로 통합
	- .csprj![Pasted image 20240401144917.png](attachments/Pasted%20image%2020240401144917.png)
### 01.02. Microsoft Visual Studio Installer Project
- 확장 -> 확장 관리 -> 온라인 -> Installer Projects 설치
	![Pasted image 20240401144127.png](attachments/Pasted%20image%2020240401144127.png)
- 솔루션 -> 추가 -> 새 프로젝트 -> Setup Project
	![Pasted image 20240401145222.png](attachments/Pasted%20image%2020240401145222.png)
## 02. Configuration
![Pasted image 20240401150118.png](attachments/Pasted%20image%2020240401150118.png)
- Application Folder : 프로그램이 설치되었을 때 필요한 파일들이 들어가는 폴더
- User's Desktop : 바탕 화면
- User's Programs Menu : 시작 메뉴
### 02.01. Description
- Set-up 속성
	![Pasted image 20240401160351.png](attachments/Pasted%20image%2020240401160351.png)
	- Author
	- Manufacturer
		- 제조사 명
		- 설치 중에 표시되는 기본 설치 경로(C:\Program Files\Manufacturer\Product Name)의 일부로도 사용
	- ProductName
	- Title
- File System on Target Machine
	![Pasted image 20240401151935.png](attachments/Pasted%20image%2020240401151935.png)
	- Application Folder
		> 프로그램이 설치되었을 때 필요한 파일들이 들어가는 폴더
		![Pasted image 20240401152115.png](attachments/Pasted%20image%2020240401152115.png)
		- DefaultLocation
			- [ProgramFilesFolder] : 설치할 경로
			- [Manufacturer] : 항목은 Set-up에서 설정한 정보가 설정된다.
			- [ProductName] : 설치할 폴더명
			
> [!Tip]-  **Windows에서 사용하는 환경변수**
> %HomeDrive% – 로그인한 계정의 정보가 들어있는 드라이브  
%HomePath% – 로그인한 계정의 폴더  
%SystemDrive% – 윈도우가 부팅된 드라이브  
%SystemRoot% – 부팅된 운영체제가 들어있는 폴더  
%ProgramFiles% – 기본 프로그램 설치 폴더  
%TEMP%, %TMP% – 임시 파일이 저장되는 폴더  
%ComSpec% – 기본 명령 프롬프트 프로그램  
%USERDOMAIN% – 로그인한 시스템의 도메인 명  
%USERNAME% – 로그인한 계정 이름  
%USERPROFILE% – 로그인한 유저의 프로필이 들어있는 폴더명  
%ALLUSERPROFILE% – 모든 사용자 프로필이 저장된 폴더  
%APPDATA% – 설치된 프로그램의 필요 데이터가 저장된 폴더  
%LOGONSERVER% – 로그인한 계정이 접속한 서버명  
%Path% – 실행 참조용 폴더 지정 목록  
%PathEXT% – 참조용 폴더에서 검색한 파일들의 확장자 목록
### 02.02. Set-up File 설정
- 프로젝트 출력
	- 해당 산출물에 참조하는 Library가 있을 경우 자동 추가됨
	![Pasted image 20240401153105.png](attachments/Pasted%20image%2020240401153105.png)
	![Pasted image 20240401153158.png](attachments/Pasted%20image%2020240401153158.png)
	![Pasted image 20240401153313.png](attachments/Pasted%20image%2020240401153313.png)
	- 이미지와 같이 CalculatorLib.dll을 참조하니 자동 추가됨
- 어셈블리
	- 개별적 추가 시에는 종속성을 생각하며 추가 한다.
	![Pasted image 20240401152913.png](attachments/Pasted%20image%2020240401152913.png)
	![Pasted image 20240401153027.png](attachments/Pasted%20image%2020240401153027.png)
- 조건부 File Setting
	- User Interface 추가
		- 조건을 입력 받을 공간 마련
			- UI 추가
				![Pasted image 20240402115851.png](attachments/Pasted%20image%2020240402115851.png)
			- 변수 설정
				![Pasted image 20240402115944.png](attachments/Pasted%20image%2020240402115944.png)
				- Edit4Property = 변수명
			- 조건 설정
				- 다중 설정 파일
					![Pasted image 20240402130827.png](attachments/Pasted%20image%2020240402130827.png)
				- 설정 파일 마다 Condition 설정
					![Pasted image 20240402130816.png](attachments/Pasted%20image%2020240402130816.png)
## 03. Environment
### 03.01. Set-up Project Environment
![Pasted image 20240401162531.png](attachments/Pasted%20image%2020240401162531.png)
- Output file name : set-up file 생성 경로
- Package Files : Packege 생성 타입
	![Pasted image 20240401162738.png](attachments/Pasted%20image%2020240401162738.png)
- Prerequirsites : 필수 구성 요소
	![Pasted image 20240401162835.png](attachments/Pasted%20image%2020240401162835.png)
### 03.02. Launch Conditions
![Pasted image 20240408135331.png](attachments/Pasted%20image%2020240408135331.png)
![Pasted image 20240408135400.png](attachments/Pasted%20image%2020240408135400.png)
![Pasted image 20240408135432.png](attachments/Pasted%20image%2020240408135432.png)
## 04. 테스트
![Pasted image 20240401162916.png](attachments/Pasted%20image%2020240401162916.png)
![Pasted image 20240401162944.png](attachments/Pasted%20image%2020240401162944.png)
- C:\Program Files (x86)\CALCULATOR FACTORY\CALCULATOR\
![Pasted image 20240401163007.png](attachments/Pasted%20image%2020240401163007.png)
![Pasted image 20240401163044.png](attachments/Pasted%20image%2020240401163044.png)
