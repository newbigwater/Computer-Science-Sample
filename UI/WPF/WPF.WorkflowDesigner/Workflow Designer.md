[UI](../../UI.md)

# WorkflowDesigner WPF 어플리케이션
System.Activities.Presentation.WorkflowDesigner를 표출하는 WPF 어플리케이션

## Windows Workflow Foundation Microsoft Learn 문서를 참고하여 작성된 프로젝트
> https://learn.microsoft.com/ko-kr/dotnet/framework/windows-workflow-foundation/rehosting-the-workflow-designer

> WorkflowDesigner가 UIElement로 구성되어있어 Forms.Control을 사용하는 WinForm에서 사용할 수는 없다.

## 프로젝트 설명
### 01.WorkflowDesignerApp
- *Workflow Designer 재호스팅* 문서 기반 프로젝트

- 좌측 Toolbox, 중앙 WorkflowDesigner, 우측 PropertyGrid로 구성
### 02.MyEditorServiceApp
*사용자 지정 식 편집기 사용* 문서 기반 프로젝트

- MyEditorService.cs: IExpressionEditorService 인터페이스, 편집기의 생성과 삭제를 관리
- MyExpressionEditorInstance.cs: IExpressionEditorInstance 인터페이스, 편집 UI용 UI를 구현
- MainWindow.xaml: 재호스팅된 워크플로 애플리케이션에 IExpressionEditorService를 게시
