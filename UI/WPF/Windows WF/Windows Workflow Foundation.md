#WPF #WF

[UI](../../UI.md)

> [!Quote]- Quote
> https://learn.microsoft.com/ko-kr/dotnet/framework/windows-workflow-foundation/

## Windows Workflow Outline
> 워크플로는 실세계 프로세스를 설명하는 모델로 저장되는 A workflow is a set of elemental units called _활동_이라는 요소 단위의 집합입니다. 워크플로를 통해 단기 실행 작업과 장기 실행 작업의 실행 순서와 종속 관계를 설명할 수 있습니다. 이 작업은 모델을 시작부터 끝까지 통과하며 활동은 사람이 실행하거나 시스템 함수로 실행될 수 있습니다.

---
### Workflow Runtime Engine
> 실행 중인 모든 워크플로 인스턴스는 다음 중 하나를 통해 호스트 프로세스와 상호 작용하는 in-process 런타임 엔진에서 만들고 유지합니다.

- 메서드와 같이 워크플로를 호출하는 [WorkflowInvoker](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.workflowinvoker)
    
- 단일 워크플로 인스턴스의 실행을 명시적으로 제어하기 위한 [WorkflowApplication](https://learn.microsoft.com/ko-kr/dotnet/api/system.activities.workflowapplication)
    
- 다중 인스턴스 시나리오에서 메시지 기반 상호 작용에 사용하는 [WorkflowServiceHost](https://learn.microsoft.com/ko-kr/dotnet/api/system.servicemodel.workflowservicehost)

호스트 프로세스의 WorkFlow 구성 요소

![호스트 프로세스의 워크플로 구성 요소](attachments/Pasted%20image%2020240412223827.png)



