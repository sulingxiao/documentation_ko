---
title:
keywords: sample homepage
sidebar: SDKs_ko
permalink: ontology_java_sdk_auth_ko.html
folder: doc_ko/SDKs
giturl: https://github.com/ontio/ontology-java-sdk/blob/master/docs/cn/auth.md
---

<h1 align="center"> 권한관리 </h1>

<p align="center" class="version">Version 1.0.0 </p>

[English](./ontology_java_sdk_auth_en.html) / 한국어


현재 모든 유저가 스마트 컨트랙트 함수를 호출할 수 있는데, 이는 현실적인 요구에 부합하지 않습니다. 역할에 따른 권한관리는 본래 모든 역할은 일부 함수를 호출할 수 있고 각 개체는 여러 역할을 부여 받을 수 있어야 합니다. (개체는 해상 ONT ID로 식별됩니다.)

만일 스마트트에 권한관리 기능을 추가해야 하다면, 반드시 계약에서 분배한 역할, 역할이 호출 가능한 함수는 무엇인지, 어떤 개체가 해당 역할을 맡았는지 등 정보를 기록해야 합니다, 이 작업은 비교적 복잡하므로 하나의 시스템계약으로 관리하는 것이 좋습니다. 

자세한 사항은 [권한계약]을 참고하세요.(https://github.com/ontio/ontology-smartcontract/blob/master/smartcontract/native/auth/auth.md)


* 권한계약 관리

* 인터페이스 리스트

* 인터페이스 리스트


### 권한계약 관리 

Auth 계약은 어플리케이션 계약 함수의 호출권한을 관리하며, 기능으로는 계약관리자가 계약관리 권한 양도하기, 계약 관리자의 역할로써 함수 분배하기, 계약관리자가 역할과 개체신분 연동하기, 계약함수 호출권을 가진 개체가 계약호출권을 타인에게 위임하기, 계약관리자가 계약 호출권을 회수하기, 개체가 계약호출token의 유효성 검증하기 등이 있습니다.    

Auth 계약은 역할에 따른 권한관리를 체계적으로 구현했으며 모든 역할은 호출 가능한 함수와 대응됩니다. 관리자는 역할을 ONT ID에 분배함으로써 해당 역할의 함수를 호출할 수 있도록 합니다. 동시에 역할은 전달이 가능하여 A가 어떤 역할을 B에 위임하고 위임시간을 지정할 수도 있습니다. 이렇게 되면 B는 일정기간 동안 대응하는 함수를 호출할 수 있습니다.  
   
### 사례 예시

Ontology 스마트 컨트랙트은 설치 시 초기화를 지원하지 않기 때문에 계약관리자는 계약코드에 하드코딩해야 합니다. 즉, 계약관리자 ONT ID를 계약에서 하나의 상수로 정의합니다. 자세한 사항은 하단의 계약 예시를 참고하십시오. Ontology스마트 컨트랙트은 verify Token함수를 호출하여 권한검증을 진행하고 동시에 개발자가 어떤 OntID이 어떤 역할인지를 쉽게 검증할 수 있도록 Java-SDK 역시 verify Token인터페이스를 제공하므로 실시간으로 역할의 매칭상황을 조회할 수 있습니다.  

사용 프로세스:

```
1. 스마트 컨트랙트을 체인에 설치합니다.
2. 스마트 컨트랙트의 init방식을 호출하고 계약코드에서 initContranctAdmin방식을 호출하여 계약에서 사전정의한 관리자 ONT ID를 해당 계약의 관리자로 설정합니다. (주의: 먼저 관리자 ONT ID를 체인에 등록시켜야 함)
3. 계약관리자가 사용해야 할 역할을 설계하고 역할과 스마트 컨트랙트 중의 함수를 연동합니다. 이 절차에서 Java-SDK의 assignFuncsTokRole인터페이스를 호출하여 설치할 수 있습니다.  
4. 계약관리자는 역할을 ONT ID에 분배하고 해당 역할을 부여 받은 ONT ID는 해당 역할과 대응하는 함수를 호출할 수 있는 권한을 갖게 됩니다. 이 절자에서 Java-SDK의 assignFuncsTokRole인터페이스를 호출하여 설치할 수 있습니다.
5. 어떤 역할을 가진 ONT ID가 해당 역할과 대응하는 함수를 호출하기 전, Java-SDK의 verifyToken인터페이스를 통해 해다아 ontid가 상응하는 함수를 검증할 수 있는 권한이 있는지 검증할 수 있습니다. 
```

다음 예시를 통해 사용프로세스를 참고하세요.

A. 스마트 컨트랙트을 체인에 설치합니다.
B. 해당 계약 중의 init방식을 호출합니다. 

```java
AbiInfo abiInfo = JSON.parseObject(abi,AbiInfo.class);
String name = "init";
AbiFunction function = abiInfo.getFunction(name);
function.setParamsValue();
String txhash = (String) sdk.neovm().sendTransaction(Helper.reverse(codeAddress),account,account,sdk.DEFAULT_GAS_LIMIT,0,function,false);
```

C. 계약관리자가 role1과 role2의 역할을 설계하고 역할role1과 함수foo1를 연동하고 역할role2와 함수foo1, foo3를 연동합니다. 

```java
String txhash = sdk.nativevm().auth().assignFuncsToRole(adminIdentity.ontid, password, adminIdentity.controls.get(0).getSalt(),
1, Helper.reverse(codeAddress), "role1", new String[]{"foo1"}, account, sdk.DEFAULT_GAS_LIMIT, 0);

String txhash = sdk.nativevm().auth().assignFuncsToRole(adminIdentity.ontid, password, adminIdentity.controls.get(0).getSalt(), 1,
Helper.reverse(codeAddress), "role2", new String[]{"foo2","foo3"}, account, sdk.DEFAULT_GAS_LIMIT, 0);

```

D. 계약관리자가 역할role1을 ontld에 분배하고 역할role2를 ontld2에 분배합니다. 이렇게 되면 ontld1은 함수foo1을 호출할 수 있는 권한을 가지며 ontld2는 함수foo2와 함수foo3를 호출할 수 있는 권한을 가집니다. 

```java
String txhash = sdk.nativevm().auth().assignOntIdsToRole(adminIdentity.ontid, password, adminIdentity.controls.get(0).getSalt(), 1,
Helper.reverse(codeAddress), "role1", new String[]{identity1.ontid}, account, sdk.DEFAULT_GAS_LIMIT, 0);

String txhash = sdk.nativevm().auth().assignOntIdsToRole(adminIdentity.ontid, password, adminIdentity.controls.get(0).getSalt(), 1,
Helper.reverse(codeAddress), "role2", new String[]{identity2.ontid}, account, sdk.DEFAULT_GAS_LIMIT, 0);


```

E. ontld1의 역할은 계약관리자가 분배한 것이기 때문에 그 권한level의 디폴트는 2이며, 즉 ontld1은 권한을 기타 ontldX에 위임할 수 있고 대행하는 Java-SDK인터페이스는 delegate입니다. 구체적인 인터페이스 정보는 하단 인터페이스 정보를 참고하세요. 

```java
sdk.nativevm().auth().delegate(identity1.ontid,password,identity1.controls.get(0).getSalt(),1,Helper.reverse(codeAddress),
identityX.ontid,"role1",60*5,1,account,sdk.DEFAULT_GAS_LIMIT,0);
```

F. verifyToken인터페이스 조회를 통해 어떤 ontld가 어떤 함수를 호출할 수 있는 권한이 있는지 검증할 수 있습니다. 

```java
String result = sdk.nativevm().auth().verifyToken(identityX.ontid, password, identityX.controls.get(0).getSalt(), 1, Helper.reverse(codeAddress), "foo1");

返回值: "01"表示有权限，"00"表示没有权限。
```

G. 대행인의 권한시간이 종료되지 않은 경우에라도 위임자는 미리 권한을 회수하여 또 다른 이에게 권한을 위임할 수 있습니다.  

```java
sdk.nativevm().auth().withdraw(identity1.ontid,password,identity1.controls.get(0).getSalt(),1,Helper.reverse(codeAddress),identityX.ontid,"role1",account,sdk.DEFAULT_GAS_LIMIT,0);
```

H. 계약관리자는 자신의 관리권한을 기타 ontld에 양도할 수 있습니다. 

```java
String txhash = sdk.nativevm().auth().sendTransfer(adminIdentity.ontid,password,adminIdentity.controls.get(0).getSalt(),1,Helper.reverse(codeAddress),adminIdentity.ontid,
account,sdk.DEFAULT_GAS_LIMIT,0);
```



Ontology계약예시：
```c#
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.ComponentModel;
using System.Numerics;

namespace Example
{
    public class AppContract : SmartContract
    {
        public struct initContractAdminParam
        {
            public byte[] adminOntID;
        }

        public struct verifyTokenParam
        {
            public byte[] contractAddr; //계약주소
            public byte[] calllerOntID; // 호출자ontld
            public string funcName;     // 호줄한 함수명
            public int keyNo;           // 호출자ontld를 사용한 몇 번째 퍼블릭키

        }

        //the admin ONT ID of this contract must be hardcoded.
        public static readonly byte[] adminOntID = "did:ont:AazEvfQPcQ2GEFFPLF1ZLwQ7K5jDn81hve".AsByteArray();

        public static Object Main(string operation,object[] args)
        {
            if (operation == "init") return init();

            if (operation == "foo")
            {
                //we need to check if the caller is authorized to invoke foo
                if (!verifyToken(operation, args)) return "no auth";

                return foo();
            }
            if (operation == "foo2")
            {
                //we need to check if the caller is authorized to invoke foo
                if (!verifyToken(operation, args)) return "no auth";

                return foo2();
            }
            if (operation == "foo3")
            {
                //we need to check if the caller is authorized to invoke foo
                if (!verifyToken(operation, args)) return "no auth";

                return foo3();
            }

            return "over";
        }

        public static string foo()
        {
            return "A";
        }
        public static string foo2()
        {
            return "B";
        }
        public static string foo3()
        {
            return "C";
        }

        //this method is a must-defined method if you want to use native auth contract.
        public static bool init()
        {
            byte[] authContractAddr = {
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x06 };
            byte[] ret = Native.Invoke(0, authContractAddr, "initContractAdmin", adminOntID);
            return ret[0] == 1;
        }

        internal static bool verifyToken(string operation, object[] args)
        {
            verifyTokenParam param = new verifyTokenParam{};
            param.contractAddr = ExecutionEngine.ExecutingScriptHash;
            param.funcName = operation;
            param.calllerOntID = (byte[])args[0];
            param.keyNo = (int)args[1];

            byte[] authContractAddr = {
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x06 };
            byte[] ret = Native.Invoke(0, authContractAddr, "verifyToken", param);
            return ret[0] == 1;
        }
    }
}
```


### 계약 인터페이스 권한

Java-SDK는 이미 권한계약의 호출인터페이스를 팩킹했기 때문에 Java-SDK를 통해 권한을 관리할 수 있습니다. 

#### 1. 계약관리자의 계약관리 권한 양도

```java
String sendTransfer(String adminOntId, String password, byte[] salt, String contractAddr, String newAdminOntID, long keyNo, Account payerAcct, long gaslimit, long gasprice)
```
|설명||서술|
|:--|:--|:--|
|기능설명|계약관리자의 계약관리 권한 양도|해당 함수는 반드시 계약관리자에 의해 호출되어야 함. 즉 adminOntID명의하에 keyNo로 넘버링된 퍼블릭 키로 트랜잭션서명의 합법여부 검증.  |
|파라미터|필드|서술|
||adminOntId|계약 관리자 ontid|
||password|合约管理员密码|
||salt |계약관리자 암호|
||contractAddr|계약 주소|
||newAdminOntID|새 관리자|
||keyNo|계약 관리자의 퍼블릭키 넘버|
||payerAcct|지불계정|
||gaslimit|gas가격|
||gasprice|gas가격|
|리턴치 설명|트랜잭션hash||

#### 2. 역할분담 함수
```java
String assignFuncsToRole(String adminOntID,String password,byte[] salt,String contractAddr,String role,String[] funcName,long keyNo,Account payerAcct,long gaslimit,long gasprice)
```
|설명||서술|
|:--|:--|:--|
|기능설명|역할분담 함수|반드시 계약관리자에 의해 호출되어야 하며 모든 함수가 자동으로 role과 연동됨. 만일 이미 연동된 상태라면 자동으로 패스하고 마지막에 true로 되돌아감.|
|파라미터 설명|필드|서술|
||adminOntId|계약관리자ontid|
||password|계약관리자 암호|
||salt|프라이빗키 해독 파라미터|
||contractAddr|계약 주소|
||role|역할|
||funcName|함수명 배열|
||keyNo|계약 관리자의 퍼블릭키 넘버|
||payerAcct|지불 계정|
||gaslimit|gas가격|
||gasprice|gas가격|
|리턴치 설명|트랜잭션hash||

#### 3. 역할과 실제신분 연동
```java
String assignOntIDsToRole(String adminOntId,String password,byte[] salt, String contractAddr,String role,String[] ontIDs,long keyNo,Account payerAcct,long gaslimit,long gasprice)
```
 |설명||설명|
 |:--|:--|:--|
 |기능설명|역할과 실제신분 연동|반드시 계약관리자에 의해 호출되어야 하며 ointIDs배열 중의 ONTID가 role역할로 배분되고 마지막에 true로 되돌아감. 현재 상황에서 권한token 레벨인 level디폴트는 2와 같음. |
 |파라미터설명|드|설명|
 ||adminOntId|계약 관리자ontid|
 ||password|계약 관리자 암호|
 ||salt|프라이빗키 해독 파라미터|
 ||contractAddr|계약 주소|
 ||role|역할|
 ||ontIDs|ontid배열|
 ||keyNo|계약 관리자의 퍼블릭키 넘버|
 ||payerAcct|지불 계정|
 ||gaslimit|gas가격|
 ||gasprice|gas가격|
 |리턴 값 설명|트랜잭션hash||

#### 4. 계약호출권을 타인에게 위임하기
```java
String delegate(String ontid,String password,byte[] salt,String contractAddr,String toOntId,String role,long period,long level,long keyNo,Account payerAcct,long gaslimit,long gasprice)
```
 역할자는 역할을 타인에게 위임할 수 있음. From은 양도인의 ONT ID이고 to는 대행인의 ONT ID임. Role은 대행역할을 의미하며 period파라미터는 대행기간을 지정함. (second를 단위로 함)

대행인은 해당 역할을 다시 더 많은 사람에게 위임할 수 있으며 level파라미터로 위탁단계를 표시함. 예를 들어,
   
    Level = 1: 이 때 대행인은 해당 역할을 다시 타인에게 위임할 수 없음. 현재로써는 해당 시나리오만 지원함. 

 |설명||설명|
 |:--|:--|:--|
 |기능설명|계약호출권을 타인에게 위임||
 |파라터설명|필드|설명|
 ||ontid|계약 중의 함수 호출권을 가진 ontid|
 ||password|ontid|
 ||salt|프라이빗키 해독 파라미터|
 ||contractAddr|계약주소|
 ||toOntId|계약호출권 수신 ontid|
 ||role|역할|
 ||period|초 단위의 시간|
 ||keyNo|ontid의 퍼블릭키 넘버|
 ||payerAcct|지불계정|
 ||gaslimit|gas가격|
 ||gasprice|gas가격|
 |리턴치 설명|트랜잭션hash||

#### 5. 계약 호출권 회수
```java
String withdraw(String initiatorOntid,String password,byte[] salt,String contractAddr,String delegate, String role,long keyNo,Account payerAcct,long gaslimit,long gasprice)
```
 역할자는 역할의 위임을 기한 전에 철회할 수 있으며 initiatorOntid는 발기인이고 delegate는 역할대행인임. Initiator는 delegate에 위임한 역할을 기한 전에 철회함. 

 |설명||설명|
 |:--|:--|:--|
 |기능설명|계약호출권 회수(delegate사용과 매칭)||
 |파라미터설명|필드|설명|
 ||initiatorOntid|계약호출권을 기타ontid에 위임|
 ||password|ontid암호|
 ||salt|프라이빗키 해독 파라미터|
 ||contractAddr|계약주소|
 ||delegate|대리인ontid|
 ||role|역할|
 ||keyNo|ontid의 퍼블릭키 넘버|
 ||payerAcct|지불계정|
 ||gaslimit|gas가격|
 ||gasprice|gas가격|
 |리턴치설명|트랜잭션hash||

#### 6. 계약호출token의 유효성 검증
```java
String verifyToken(String ontid,String password,byte[] salt,String contractAddr,String funcName,long keyNo)
```
|설명||설명|
|:--|:--|:--|
|기능설명|계약호출 token의 유효성 검증||
|파라미터설명|필드|설명|
||ontid|검증한 ontid|
||password|ontid암호|
||salt|프라이빗 키 해독 파라미터|
||contractAddr|계약주소|
||funcName|함수명|
||keyNo|ontid의 퍼블릭키 넘버|
|리턴치설명|트랜잭션hash||

