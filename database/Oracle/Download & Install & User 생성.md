#database #Oracle #19C #sqld #Windows #win11

[..](Oracle.md)

## 01. Donwload

[Download](https://www.oracle.com/kr/downloads/)

![](attachments/Pasted%20image%2020240504235806.png)

![](attachments/Pasted%20image%2020240504235820.png)

## 02. Install

### 01) 설치 경로
- 만약 해당 압축 파일을 푼 곳에서 설치 시 해당 위치가 Home위치로 잡히기 때문에 원하는 위치에 옮겨서 설치하는 걸 추천한다.

![](attachments/Pasted%20image%2020240504235957.png)

### 02) Environment Configuration
- 위와 동일한 위치에 환경 구성 정보 파일들이 저장될 폴더를 미리 생성한다.

![](attachments/Pasted%20image%2020240505000154.png)

### 03) Set-up

![](attachments/Pasted%20image%2020240505000342.png)
- 구성 옵션
	- 단일 인스턴스 데이터베이스 생성 및 구성
- 시스템 클래스
	- 데이트콥 클래스
- Orcle 홈 사용자
	- 가상 계정 사용
- 기본 설치
	- Orcle Base : C:\Database\Oracle
		- 앞서 미리 생성한 폴더 선택
	- 비밀번호 : 입력
- 요약
- 제품 설치
	![](attachments/Pasted%20image%2020240505000651.png)
- 완료
	![](attachments/Pasted%20image%2020240505000735.png)

### 04) Test

![](attachments/Pasted%20image%2020240505001128.png)

```shell
Microsoft Windows [Version 10.0.22631.3447]
(c) Microsoft Corporation. All rights reserved.

C:\Users\newbi>sqlplus /nolog

SQL*Plus: Release 19.0.0.0.0 - Production on 일 5월 5 00:10:38 2024
Version 19.3.0.0.0

Copyright (c) 1982, 2019, Oracle.  All rights reserved.

SQL> conn
사용자명 입력: system
비밀번호 입력:
연결되었습니다.
SQL> select sysdate from dual
  2  ;

SYSDATE
--------
24/05/05

SQL>
```
## 03. User 생성 및 연결 정보 변경
> [!Tip] Utility
> [DBeaver](../Assist/DBeaver.md) 으로 개발 환경으로 선택
### 01) 계정 생성
- 오라클 버전 업 중 ID Prefix 'c##' 사용해야 됨
	```sql
	CREATE USER C##TEST IDENTIFIED BY 1234;
	```
- 사용하기 싫을 경우
	```sql
	ALTER SESSION SET "_ORACLE_SCRIPT"=true;
	CREATE USER TEST IDENTIFIED BY 1234;
	```
- 생성된 ID에 권한 설정
	```sql
CREATE USER NBE IDENTIFIED BY 1234;
GRANT dba TO NBE;
	```

### 02) 연결 변경

![](attachments/Pasted%20image%2020240505002715.png)

![](attachments/Pasted%20image%2020240505002826.png)

![](attachments/Pasted%20image%2020240505002800.png)


### 03) Test

![](attachments/Pasted%20image%2020240505002952.png)



