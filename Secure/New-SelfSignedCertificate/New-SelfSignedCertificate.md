#win10 #PowerShell #SelfSignedCertificate #자체서명인증서

[Secure](../Secure.md)

> [!Quote]- Quote
> https://techexpert.tips/ko/windows-ko/windows-%EC%9E%90%EC%B2%B4-%EC%84%9C%EB%AA%85%EB%90%9C-%EC%9D%B8%EC%A6%9D%EC%84%9C-%EB%A7%8C%EB%93%A4%EA%B8%B0/
## 자습서

1. PowerShcell - 관리자 권한 실행

2. 자체 서명된 인증서 생성
![Pasted image 20240405152008.png](attachments/Pasted%20image%2020240405152008.png)
![Pasted image 20240405163055.png](attachments/Pasted%20image%2020240405163055.png)
```shell
New-SelfSignedCertificate -DnsName www.newbigwater.com -CertStoreLocation cert:\LocalMachine\My -FriendlyName "NBE" -NotAfter (Get-Date).AddYears(10) -KeyLength 4096 -KeySpec KeyExchange
```

3. 인증서 나열
![Pasted image 20240405152047.png](attachments/Pasted%20image%2020240405152047.png)
```shell
Get-ChildItem Cert:\LocalMachine\My
```

4. 인증서 배포
![Pasted image 20240405152257.png](attachments/Pasted%20image%2020240405152257.png)
![Pasted image 20240405152328.png](attachments/Pasted%20image%2020240405152328.png)
![Pasted image 20240405153230.png](attachments/Pasted%20image%2020240405153230.png)
```shell
Export-Certificate -Cert Cert:\LocalMachine\My\8EF6CDD92C14ABD2F38974CA1F5C1BFDFABA3E24 -FilePath D:\test\nbe.cer
```
5. 인증서를 신뢰할 수 있는 루트 인증 기관으로 가져옴
![Pasted image 20240405152535.png](attachments/Pasted%20image%2020240405152535.png)
![Pasted image 20240405163434.png](attachments/Pasted%20image%2020240405163434.png)
![Pasted image 20240405163408.png](attachments/Pasted%20image%2020240405163408.png)
```shell
Import-Certificate -CertStoreLocation Cert:\LocalMachine\AuthRoot -FilePath D:\test\nbe.cer
```

6. 여러 대체 이름을 가진 자체 서명된 인증서를 생성합니다.
```shell
New-SelfSignedCertificate -DnsName www.newbigwater.com,altname1.nbe.com,alternativename2.nbw.com -CertStoreLocation cert:\LocalMachine\My -FriendlyName "nbe" -NotAfter (Get-Date).AddYears(10) -KeyLength 4096
```

8. 인증서의 공개 키와 개인 키를 보호된 파일로 내보냅니다.
![Pasted image 20240405153750.png](attachments/Pasted%20image%2020240405153750.png)
```shell
$MyPassword = ConvertTo-SecureString -String "A1B2C3" -force -asplaintext 
Export-PfxCertificate -Cert Cert:\LocalMachine\My\574CBBE23A99B7C4438B1294B451C8877A06C842 -FilePath D:\Certification\nbe.pfx -Password $MyPassword
```

11. 인증서 삭제
```shell
Remove-Item -Path cert:\LocalMachine\My\5619207BCAFC85D7D2FEC7A621458A9CBA863C3A
```