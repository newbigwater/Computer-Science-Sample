[UI](../../UI.md)

# WorkflowDesigner WPF 어플리케이션
System.Activities.Presentation.WorkflowDesigner를 표출하는 WPF 어플리케이션

## Windows Workflow Foundation Microsoft Learn 문서를 참고하여 작성된 프로젝트
> https://learn.microsoft.com/ko-kr/dotnet/framework/windows-workflow-foundation/rehosting-the-workflow-designer

WorkflowDesigner가 UIElement로 구성되어있어 Forms.Control을 사용하는 WinForm에서 사용할 수는 없다.

## 프로젝트 설명
### 01.WorkflowDesignerApp
- *Workflow Designer 재호스팅* 문서 기반 프로젝트

- 좌측 Toolbox, 중앙 WorkflowDesigner, 우측 PropertyGrid로 구성
### 02.MyEditorServiceApp
*사용자 지정 식 편집기 사용* 문서 기반 프로젝트

- MyEditorService.cs: IExpressionEditorService 인터페이스, 편집기의 생성과 삭제를 관리
- MyExpressionEditorInstance.cs: IExpressionEditorInstance 인터페이스, 편집 UI용 UI를 구현
- MainWindow.xaml: 재호스팅된 워크플로 애플리케이션에 IExpressionEditorService를 게시

### 03.ExpressionTextBox
Custom Activity 생성 프로젝트

- MultiAssign
- MyDynamicActivity

### 99.WorkflowConsoleApplication
워크플로 콘솔 애플리케이션
Workflow1.xaml에서 워크플로 작업을 하는 프로젝트
실행 시 작성된 워크플로를 수행한다.

## WorkflowDesignerApp 상세
### MainWindow 구성
![](attachments/Pasted%20image%2020240418165052.png)
![](attachments/Pasted%20image%2020240419144458.png)
3개의 Grid로 구성되어있다.
- 좌측 Grid는 Toolbox,
- 중앙 Grid는 WorkflowDesigner,
- 우측 Grid는 Property가 그려진다.

우측 하단에 file 관련 버튼이 있다.
- Load - 저장한 워크플로 xaml 파일을 Load WorkflowDesigner에 표출한다.
- Run - 저장한 워크플로 xaml 파일을 실행한다.
- Save  - 작성한 워크플로를  xaml 파일로 저장한다.

### WorkflowDesigner
중앙 Grid에 그려지는 Workflow 편집 뷰이다.
AddDesigner() 메서드에서 할당한다.
> ActivityBuilder를 Load해야 인수를 활성화하여 사용할 수 있다.

