#WPF #WF

[UI](../../UI.md)

> [!Quote]- Quote
> https://learn.microsoft.com/ko-kr/dotnet/framework/windows-workflow-foundation/

## [Windows Workflow Outline]
> 워크플로는 실세계 프로세스를 설명하는 모델로 저장되는 A workflow is a set of elemental units called _활동_이라는 요소 단위의 집합입니다. 워크플로를 통해 단기 실행 작업과 장기 실행 작업의 실행 순서와 종속 관계를 설명할 수 있습니다. 이 작업은 모델을 시작부터 끝까지 통과하며 활동은 사람이 실행하거나 시스템 함수로 실행될 수 있습니다.

### Workflow Runtime Engine
> 실행 중인 모든 워크플로 인스턴스는 다음 중 하나를 통해 호스트 프로세스와 상호 작용하는 in-process 런타임 엔진에서 만들고 유지합니다.

- 메서드와 같이 워크플로를 호출하는 [WorkflowInvoker](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.workflowinvoker)
    
- 단일 워크플로 인스턴스의 실행을 명시적으로 제어하기 위한 [WorkflowApplication](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.workflowapplication)
    
- 다중 인스턴스 시나리오에서 메시지 기반 상호 작용에 사용하는 [WorkflowServiceHost](https://learn.microsoft.com/ko-kr/dotnet/api/system.servicemodel.workflowservicehost)

![호스트 프로세스의 워크플로 구성 요소](attachments/Pasted%20image%2020240412230936.png)

호스트 프로세스의 WorkFlow 구성 요소

### Workflow 구성 요소 간의 상호 작용
![](attachments/Pasted%20image%2020240412231041.png)

위의 다이어그램에서는 [Invoke](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.workflowinvoker.invoke) 클래스의 [WorkflowInvoker](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.workflowinvoker) 메서드를 사용하여 여러 워크플로 인스턴스를 호출합니다.
[WorkflowInvoker](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.workflowinvoker)는 호스트에서 관리할 필요 없는 간단한 워크플로에 사용되며, 호스트에서 관리해야 하는 워크플로(예: [Bookmark](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.bookmark) 다시 시작)는 그 대신 [Run](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.workflowapplication.run)을 사용하여 실행해야 합니다.
한 워크플로 인스턴스가 완료될 때까지 기다렸다가 다른 워크플로 인스턴스를 호출할 필요는 없습니다.

런타임 엔진은 여러 워크플로 인스턴스의 동시 실행을 지원합니다. 호출된 워크플로는 다음과 같습니다.
- [Sequence](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.statements.sequence) 자식 활동을 포함하는 [WriteLine](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.statements.writeline) 활동입니다. 부모 활동의 [Variable](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.variable)은 자식 활동의 [InArgument](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.inargument)에 바인딩됩니다. 변수, 인수 및 바인딩에 대한 자세한 내용은 [변수 및 인수](https://learn.microsoft.com/ko-kr/dotnet/framework/windows-workflow-foundation/variables-and-arguments)를 참조하세요.
    
- `ReadLine`이라는 사용자 지정 활동입니다. [OutArgument](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.outargument) 활동의 `ReadLine`가 호출 [Invoke](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.workflowinvoker.invoke) 메서드에 반환됩니다.
    
- [CodeActivity](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.codeactivity) 추상 클래스에서 파생되는 사용자 지정 활동입니다. [CodeActivity](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.codeactivity)는 [CodeActivityContext](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.codeactivitycontext) 메서드의 매개 변수로 사용되는 [Execute](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.codeactivity.execute)를 사용하여 런타임 기능(예: 추적 및 속성)에 액세스할 수 있습니다. 이러한 런타임 기능에 대한 자세한 내용은 [워크플로 추적 및 트레이싱](https://learn.microsoft.com/ko-kr/dotnet/framework/windows-workflow-foundation/workflow-tracking-and-tracing)과 [워크플로 실행 속성](https://learn.microsoft.com/ko-kr/dotnet/framework/windows-workflow-foundation/workflow-execution-properties)을 참조하세요.


---
## [Fundamental Windows Workflow Concept]
### 워크플로 및 활동

워크플로는 프로세스를 모델링하는 구조화된 동작의 컬렉션입니다. 
워크플로의 각 동작은 활동으로 모델링됩니다. 
호스트는 
[WorkflowInvoker](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.workflowinvoker)를 사용하여 워크플로를 메서드처럼 호출하고, 
[WorkflowApplication](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.workflowapplication)를 사용하여 단일 워크플로 인스턴스 실행을 명시적으로 제어하고, 
[WorkflowServiceHost](https://learn.microsoft.com/ko-kr/dotnet/api/system.servicemodel.workflowservicehost)를 사용하여 다중 인스턴스 시나리오에서 메시지 기반의 상호 작용을 수행함으로써 워크플로와 상호 작용합니다. 

워크플로 단계는 활동 계층으로 정의되기 때문에 계층의 최상위 활동이 워크플로 자체를 정의한다고 할 수 있습니다. 
이 계층 모델은 이전 버전의 명시적 `SequentialWorkflow` 및 `StateMachineWorkflow` 클래스를 대체합니다. 
활동 자체는 데이터 액세스에 런타임을 사용할 수 있게 해주는 [Activity](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.activity) 또는 활동 작성자에 워크플로 런타임을 표시하는 [CodeActivity](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.codeactivity) 클래스를 사용하여 만든 사용자 지정 활동 또는 다른 활동([NativeActivity](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.nativeactivity) 클래스를 기반으로 사용하며 일반적으로 XAML을 사용하여 정의됨)의 컬렉션으로 개발됩니다. 
[CodeActivity](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.codeactivity) 및 [NativeActivity](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.nativeactivity)를 사용하여 개발되는 활동은 C# 등과 같은 CLR 호환 언어를 통해 만들어집니다.

### 활동 데이터 모델

![](attachments/Pasted%20image%2020240412231429.png)

