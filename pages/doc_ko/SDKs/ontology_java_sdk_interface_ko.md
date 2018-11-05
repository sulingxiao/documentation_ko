---
title:
keywords: sample homepage
sidebar: SDKs_ko
permalink: ontology_java_sdk_interface_ko.html
folder: doc_ko/SDKs
giturl: https://github.com/ontio/ontology-java-sdk/blob/master/docs/cn/interface.md
---

<h1 align="center"> Ontology Java SDK 인터페이스 </h1>

<p align="center" class="version">Version 1.0.0 </p>

[English](./ontology_java_sdk_interface_en.html) / 한국어

## 소개:

이번 장에서는 Java SDK의 핵심인 SDK인터페이스를 다음과 같이 분류 및 나열합니다.

* 초기화 인터페이스
* 체인연동 인터페이스
* 지갑관리 인터페이스
* 디지털자산 인터페이스
* 디지털신분 인터페이스
* NEO스마트 컨트랙트 설치 및 호출 인터페이스
* Native계약호출

### 초기화 인터페이스: 

지갑문서 실행 및 체인연동모듈 설치를 포함합니다. 
 ```
     |                    Function                   |     Description            
 ----|-----------------------------------------------|------------------------
   1 | sdk.setRpc(rpcUrl)                            |   rpc설치           
   2 | sdk.setRestful(restUrl)                       |   restful설치
   3 | sdk.setWesocket(wsUrl, lock)                  |   websocket설치
   4 | wm.setDefaultConnect(wm.getWebSocket());      |   디폴트와 체인연동모듈 설치
   5 | wm.openWalletFile("OntAssetDemo.json");       |   지갑열기
 ```

### 체인연동 인터페이스

* 연동인터페이스 리스트：
```

      |                     Main   Function                      |           Description            
 -----|----------------------------------------------------------|---------------------------------------------   
    1 | ontSdk.getConnect().getNodeCount()                       |  노드수량 조회
    2 | ontSdk.getConnect().getBlock(15)                         |  블록조회
    3 | ontSdk.getConnect().getBlockJson(15)                     |  블록조회    
    4 | ontSdk.getConnect().getBlockJson("txhash")               |  블록조회    
    5 | ontSdk.getConnect().getBlock("txhash")                   |  블록조회     
    6 | ontSdk.getConnect().getBlockHeight()                     |  현재 블록높이 조회
    7 | ontSdk.getConnect().getTransaction("txhash")             |  트랜잭션조회                                    
    8 | ontSdk.getConnect().getStorage("contractaddress", key)   |  스마트 컨트랙트 스토리지 
    9 | ontSdk.getConnect().getBalance("address")                |  잔액조회
   10 | ontSdk.getConnect().getContractJson("contractaddress")   |  스마트컨트랙트 조회          
   11 | ontSdk.getConnect().getSmartCodeEvent(59)                |  스마트 컨트랙트 시나리오 조회 
   12 | ontSdk.getConnect().getSmartCodeEvent("txhash")          |  스마트 컨트랙트 시나리오 조회 
   13 | ontSdk.getConnect().getBlockHeightByTxHash("txhash")     |  트랜젝션이 위치한 높이 조회
   14 | ontSdk.getConnect().getMerkleProof("txhash")             |  merkle증명 조회
   15 | ontSdk.getConnect().sendRawTransaction("txhexString")    |  트랜젝션 전송
   16 | ontSdk.getConnect().sendRawTransaction(Transaction)      |  트랜젝션 전송
   17 | ontSdk.getConnect().sendRawTransactionPreExec()          |  예비 트랜젝션 전송
   18 | ontSdk.getConnect().getAllowance("ont","from","to")      |  허용된 사용치 조회
   19 | ontSdk.getConnect().getMemPoolTxCount()                  |  트랜젝션 풀의 트랜젝션 총량 조회
   20 | ontSdk.getConnect().getMemPoolTxState()                  |  트랜젝션 풀의 트랜젝션 총량 조회
   21 | ontSdk.getConnect().syncSendRawTransaction("data")       |   트랜젝션 동시 전송
```  

### 지갑관리 인터페이스:

디지털자산 및 디지털신분 관리를 포함합니다. 

* 디지털자산 계정:

```  
     |                        Main   Function                                     |     Description            
 ----|----------------------------------------------------------------------------|------------------------ 
   1 | Account importAccount(String encryptedPrikey, String pwd,byte[] salt,String address)   |   자산계정 불러오기
   2 | Account createAccount(String password)                                     |   자산계정 생성
   3 | Account createAccountFromPriKey(String password, String prikey)            |   프라이빗키에 따라 생성
   4 | AccountInfo createAccountInfo(String password)                             |   프라이빗키에 따라 생성
   5 | AccountInfo createAccountInfoFromPriKey(String password, String prikey)    |   프라이빗키에 따라 생성
   6 | AccountInfo getAccountInfo(String address, String password,byte[] salt)    |   계정정보 획득
   7 | List<Account> getAccounts()                                                |   모든 계정 조회
   8 | Account getAccount(String address)                                         |   계정 획득
   9 | Account getDefaultAccount()                                                |   디폴트계정 획득
```  

* 디지털신분：
```  
     |                        Main   Function                                     |     Description            
 ----|----------------------------------------------------------------------------|------------------------ 
   1 | Identity importIdentity(String encryptedPrikey, String pwd,byte[] salt,String address) |   신분 불러오기
   2 | Identity createIdentity(String password)                                   |   신분 생성
   3 | Identity createIdentityFromPriKey(String password, String prikey)          |   프라이빗키에 따라 생성
   4 | IdentityInfo createIdentityInfo(String password)                           |   신분 생성
   5 | IdentityInfo createIdentityInfoFromPriKey(String password, String prikey)  |   프라이빗키에 따라 생성
   6 | IdentityInfo getIdentityInfo(String ontid, String password)                |   신분정보 조회  
   7 | List<Identity> getIdentitys()                                              |   모든 신분 조회 
   8 | Identity getIdentity(String ontid)                                         |   신분 획득 
   9 | Identity getDefaultIdentity()                                              |   디폴트신분 획득
  10 | Identity addOntIdController(String ontid, String key, String id)           |   관리자 추가 
```        

### 디지털자산:
1.기초 디지털자산
2.Nep-5스마트컨트랙트 디지털자산

*기초 디지털자산*

ont:
```

      |                                         Main   Function                                                     |           Description            
 -----|-------------------------------------------------------------------------------------------------------------|---------------------------------------------
    1 | String sendTransfer(Account sendAcct, String recvAddr, long amount,Account payerAcct,long gaslimit,long gasprice)   |  이체
    2 | long queryBalanceOf(String address)                                                       |  잔액조회
    3 | long queryAllowance(String fromAddr,String toAddr)                                         |  Allowance조회
    4 | String sendApprove(Account sendAcct, String recvAddr, long amount,Account payerAcct,long gaslimit,long gasprice)    |  发送Approve
    5 | String sendTransferFrom(Account sendAcct, String fromAddr, String toAddr,long amount,Account payerAcct,long gaslimit,long gasprice) |  发送TransferFrom
    6 | String queryName()                                                                          |  자산명 조회
    7 | String querySymbol()                                                                        |  자산Symbol조회
    8 | long queryDecimals()                                                                        |  정확도 조회
    9 | long queryTotalSupply()                                                                     |  총 공급량 조회
      
```
ong:
```

      |                                         Main   Function                                                     |           Description            
 -----|-------------------------------------------------------------------------------------------------------------|---------------------------------------------
    1 | String sendTransfer(Account sendAcct, String recvAddr, long amount,Account payerAcct,long gaslimit,long gasprice)   |  이체
    2 | long queryBalanceOf(String address)                                                       |  잔액조회
    3 | long queryAllowance(String fromAddr,String toAddr)                                         |  Allowance조회
    4 | String sendApprove(Account sendAcct, String recvAddr, long amount,Account payerAcct,long gaslimit,long gasprice)    |  Approve보내기
    5 | String sendTransferFrom(Account sendAcct, String fromAddr, String toAddr,long amount,Account payerAcct,long gaslimit,long gasprice) |  TransferFrom보내기
    6 | String queryName()                                                                          |  자산명 조회
    7 | String querySymbol()                                                                        |  자산Symbol조회
    8 | long queryDecimals()                                                                        |  정확도 조회
    9 | long queryTotalSupply()                                                                     |  총 공급량 조회
   10 | String claimOng(Account sendAcct, String toAddr, long amount, Account payerAcct, long gaslimit, long gasprice)             |  ong획득
   11 | String unclaimOng(String address)                                                                           |  미인출ong조회
      
```   

* Nep-5스마트컨트랙트 디지털자산:

```  
      |                                         Main   Function                                       |           Description            
 -----|-----------------------------------------------------------------------------------------------|---------------------------------------------
    1 | void setContractAddress(String codeHash)                                                      | 设置合约地址
    2 | String sendInit(Account acct, Account payerAcct,long gaslimit,long gasprice)                   |  초기화
    3 | long sendInitGetGasLimit()                                                                     |  사전실행 초기화
    4 | String sendTransfer(Account acct, String recvAddr, long amount,Account payerAcct, long gaslimit,long gasprice)        |  이체
    5 | long sendTransferGetGasLimit(Account acct, String recvAddr, long amount)                      |  사전실행 이체                              
    6 | String queryBalanceOf(String addr)                                                            |  잔액조회
    7 | String queryTotalSupply()                                                                     |  총 공급량 조회 
    8 | String queryName()                                                                            |  이름 조회
    9 | String queryDecimals()                                                                        |  정확도 조회
   10 | String querySymbol()                                                                          |  자산 Symbol조회

```  

### 디지털신분
1.디지털신분은 등록, 퍼블릭키, 속성, 리커버리 등 동작을 포함합니다. 
2. claim인터페이스는 배포와 검증을 포함합니다. 
3. claim증명저장 인터페이스

* ontid기능 인터페이스:
```
      |                                         Main   Function                                                     |           Description            
 -----|-------------------------------------------------------------------------------------------------------------|---------------------------------------------
    1 | String getContractAddress()                                                                                                                             |  계약 주소 조회
    2 | Identity sendRegister(Identity ident, String password,byte[] salt,Account payerAcct,long gaslimit,long gasprice)                                         |  ontid등록
    3 | Identity sendRegisterPreExec(Identity ident, String password,byte[] salt,Account payerAcct,long gaslimit,long gasprice)                                  |  ontid예비등록
    4 | Identity sendRegisterWithAttrs(Identity ident, String password,byte[] salt,Attribute[] attributes,Account payerAcct,long gaslimit,long gasprice)         |  ontid등록 및 속성추가
    5 | String sendAddPubKey(String ontid, String password,byte[] salt, String newpubkey,Account payerAcct,long gaslimit,long gasprice)                          |  퍼블릭키 추가
    6 | String sendAddPubKey(String ontid,String recoveryOntid, String password,byte[] salt, String newpubkey,Account payerAcct,long gaslimit,long gasprice)      |  퍼블릭키 추가
    7 | String sendGetPublicKeys(String ontid)                                                                                                                  |  퍼블릭키 획득
    8 | String sendRemovePubKey(String ontid, String password,byte[] salt, String removePubkey,Account payerAcct,long gaslimit,long gasprice)                    |  퍼블릭키 삭제
    9 | String sendRemovePubKey(String ontid, String recoveryOntid,String password,byte[] salt, String removePubkey,Account payerAcct,long gaslimit,long gasprice)|  퍼블릭키 삭제
   10 | String sendGetKeyState(String ontid,int index)                                                                                                          |  퍼블릭키 상태정보 획득
   11 | String sendAddAttributes(String ontid, String password,byte[] salt, Attribute[] attributes,Account payerAcct,long gaslimit,long gasprice)                |  속성추가
   12 | String sendGetAttributes(String ontid)                                                                                                                  |  속성조회
   13 | String sendRemoveAttribute(String ontid,String password,byte[] salt,String path,Account payerAcct,long gaslimit,long gasprice)                           |  속성제거
   14 | String sendAddRecovery(String ontid, String password,byte[] salt, String recoveryOntid,Account payerAcct,long gaslimit,long gasprice)                     |  리커버리 추가
   15 | String sendChangeRecovery(String ontid, String newRecovery, String oldRecovery, String password,byte[] salt,Account payerAcct, long gaslimit,long gasprice)                            |  리커버리 수정
   16 | String sendGetDDO(String ontid)                                                                                                                         |  DDO조회
   
```

* 트랜젝션 인터페이스 구성

 ```  
     |                                           Make Transaction  Function                                                |     Description            
 ----|---------------------------------------------------------------------------------------------------------------------|------------------------ 
   1 | Transaction makeRegister(String ontid,String password,byte[] salt,String payer,long gaslimit,long gasprice)                                              | 트랜젝션 등록 구성
   2 | Transaction makeRegisterWithAttrs(String ontid, String password,byte[] salt, Attribute[] attributes, String payer, long gaslimit, long gasprice)         | ontid등록 구성 및 트랜젝션 속성 추가
   3 | Transaction makeAddPubKey(String ontid,String password,byte[] salt,String newpubkey,String payer,long gaslimit,long gasprice)                            | 퍼블릭키 추가 트랜젝션 구성
   4 | Transaction makeAddPubKey(String ontid,String recoveryAddr,String password,byte[] salt,String newpubkey,String payer,long gaslimit,long gasprice)        | 퍼블릭키 추가 트랜젝션 구성
   5 | Transaction makeRemovePubKey(String ontid, String password,byte[] salt, String removePubkey,String payer,long gaslimit,long gasprice)                    | 퍼블릭키 삭제 트랜젝션 구성
   6 | Transaction makeRemovePubKey(String ontid,String recoveryAddr, String password,byte[] salt, String removePubkey,String payer,long gaslimit,long gasprice)| 퍼블릭키 삭제 트랜젝션 구성
   7 | Transaction makeAddAttributes(String ontid, String password,byte[] salt, Attribute[] attributes,String payer,long gaslimit,long gasprice)                | 속성추가 트랜젝션 구성
   8 | Transaction makeRemoveAttribute(String ontid,String password,byte[] salt,String path,String payer,long gaslimit,long gasprice)                           | 속성삭제 트랜젝션 구성
   9 | Transaction makeAddRecovery(String ontid, String password,byte[] salt, String recoveryAddr,String payer,long gaslimit,long gasprice)                     | 리커버리추가 트랜젝션 구성

  ```
  
* Claim관련 인터페이스:
  
 ```
     |                                           Claim Function                                                                      |     Description            
 ----|-------------------------------------------------------------------------------------------------------------------------------|------------------------
   1 | public Object getMerkleProof(String txhash)                                                                                   | merkle증명 획득
   2 | boolean verifyMerkleProof(String claim)                                                                                       | mekle증명 검증
   3 | String createOntIdClaim(String signerOntid, String pwd, byte[] salt,String context, Map claimMap, Map metaData,Map clmRevMap,long expire) |    claim생성
   4 | boolean verifyOntIdClaim(String claim)                                                                                        | claim검증
  
 ```
 
* Claim증명저장 인터페이스:
  
 ```
     |                                            Function                                                         |     Description
 ----|-------------------------------------------------------------------------------------------------------------|------------------------
   1 | String sendCommit(String issuerOntid,String password,byte[] salt,String subjectOntid,String claimId,Account payerAcct,long gaslimit,long gasprice) | claim저장
   2 | String sendRevoke(String issuerOntid,String password,byte[] salt,String claimId,Account payerAcct,long gaslimit,long gasprice)     | 취소
   3 | String sendGetStatus(String claimId)                                                                                               | 상태정보 획득
  
 ```
 
 ### 스마트 컨트랙트 설치 및 인터페이스 호출

설치 및 호출

  ```
      |                                            Function                                                                                                                             |     Description            
  ----|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|------------------------
    1 | DeployCode makeDeployCodeTransaction(String code, boolean needStorage, String name, String version, String author, String email, String desp, byte vmtype,String payer,long gaslimit,long gasprice)|   설치
    2 | InvokeCode makeInvokeCodeTransaction(String codeAddr,String method,byte[] params, String payer,long gaslimit,long gasprice)                                                           |   호출
   
  ```

 ### Netive 계약호출

#### 권한관리계약 

* 권한관리기능 인터페이스:
 ```
       |                                            Function                                                                                                                               |     Description
   ----|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|------------------------
     1 | String sendTransfer(String adminOntId,String password,byte[] salt,String contractAddr, String newAdminOntID,int key,Account payerAcct,long gaslimit,long gasprice)                  | 계약관리자가 계약관리권한을 양도
     2 | String assignFuncsToRole(String adminOntID,String password,byte[] salt,String contractAddr,String role,String[] funcName,int key,Account payerAcct, long gaslimit,long gasprice) | 역할에 함수 매칭
     3 | String assignOntIDsToRole(String adminOntId,String password,byte[] salt,String contractAddr,String role,String[] ontIDs, int key,Account payerAcct, long gaslimit,long gasprice)  | 실제신분에 역할 매칭
     4 | String delegate(String ontid,String password,byte[] salt,String contractAddr,String toOntId,String role,int period,int level,int key,Account payerAcct, long gaslimit,long gasprice)|                                        계약호출권을 타인에게 위임
     5 | String withdraw(String initiatorOntid,String password,byte[] salt,String contractAddr,String delegate, String role,int key,Account payerAcct, long gaslimit,long gasprice)          | 계약호출권 회수
 ```

* 트랜젝션 인터페이스 구성
```
       |                                            Function                                                                                                                               |     Description
   ----|-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|------------------------
     1 | Transaction makeTransfer(String adminOntID,String contractAddr, String newAdminOntID,int key,String payer,long gaslimit,long gasprice)                    | 계약관리자가 계약관리권한을 양도
     2 | Transaction makeAssignFuncsToRole(String adminOntID,String contractAddr,String role,String[] funcName,int key,String payer,long gaslimit,long gasprice)   | 역할에 함수 매칭
     3 | Transaction makeAssignOntIDsToRole(String adminOntId,String contractAddr,String role,String[] ontIDs, int key,String payer,long gaslimit,long gasprice)   | 실제신분에 역할 매칭
     4 | Transaction makeDelegate(String ontid,String contractAddr,String toAddr,String role,int period,int level,int key,String payer,long gaslimit,long gasprice)| 계약호출권을 타인에게 위임
     5 | Transaction makeWithDraw(String ontid,String contractAddr,String delegate, String role,int key,String payer,long gaslimit,long gasprice)                  | 계약호출권 회수
 ```
 ```

#### 거버넌스 계약

         |   Function                                                                                                                                                                         |   Description
    -----|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
              1 | String registerCandidate(Account account, String peerPubkey, long initPos, String ontid,String ontidpwd,byte[] salt,  long keyNo, Account payerAcct, long gaslimit, long gasprice) | 일정량의 ONT를 담보로 일정량의 별도 ONG를 소모하고 후보노드 신청
       2 | String unRegisterCandidate(Account account, String peerPubkey,Account payerAcct, long gaslimit, long gasprice)                  | ONT 후보노드 신청을 취소하면 담보한 ONT를 반환
       3 | String withdrawOng(Account account,Account payerAcct,long gaslimit,long gasprice)                                               | 반환된ong 인출하기
       4 | String getPeerInfo(String peerPubkey)                                                                                           | 노드정보 조회
       5 | String getPeerInfoAll()                                                                                                         | 모든 노드 조회
       6 | String getAuthorizeInfo(String peerPubkey,Address addr)                                                                  | 주소와 노드 간 권한위임정보 조회 
       7 | String withdraw(Account account,String peerPubkey[],long[] withdrawList,Account payerAcct,long gaslimit,long gasprice)          | 미동결 상태의 디파짓ONT인출
       8 | String quitNode(Account account,String peerPubkey,Account payerAcct,long gaslimit,long gasprice)                                | 노드 탈퇴
       9 | String addInitPos(Account account,String peerPubkey,int pos,Account payerAcct,long gaslimit,long gasprice)                      | 
노드가 추가한 initPos인터페이스는 노드소유자만 호출 가능
       10| String reduceInitPos(Account account,String peerPubkey,int pos,Account payerAcct,long gaslimit,long gasprice)                   | 노드가 initPod인터페이스를 감축하면 노드소유자만 호출 가능하며 initPos는 약정치 보다 낮을 수 없고 이미 위임 받은 권한 수의 1/10보다 적을 수 없습니다.  
       11| String setPeerCost(Account account,String peerPubkey,int peerCost,Account payerAcct,long gaslimit,long gasprice)                | 노드 자신이 독점하는 보상비율 설정
       12| String changeMaxAuthorization(Account account,String peerPubkey,int maxAuthorize,Account payerAcct,long gaslimit,long gasprice) | 노드 자신이 위임받을 최대 권한 ONT수량 수정
       13| String getPeerAttributes(String peerPubkey)                                                                                     | 노드속성정보 조회
       14| String getSplitFeeAddress(String address)                                                                                       | 불특정 주소로부터 받은 보상 조회