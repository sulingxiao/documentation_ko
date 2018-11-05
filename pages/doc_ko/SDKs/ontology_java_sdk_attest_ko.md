---
title:
keywords: sample homepage
sidebar: SDKs_ko
permalink: ontology_java_sdk_attest_ko.html
folder: doc_ko/SDKs
giturl: https://github.com/ontio/ontology-java-sdk/blob/master/docs/cn/attest.md
---

<h1 align="center"> 신뢰성명 및 증명저장 </h1>

<p align="center" class="version">Version 1.0.0 </p>

[English](./ontology_java_sdk_attest_en.html) / 한국어




##  1. 신뢰성명

검증 가능한 성명은 실체를 증명하는 일부 속성에 사용됩니다.

신뢰성명 증명저장계약은 신뢰성명의 저장 서비스를 제공하며 이는 신뢰성명Id증명, 발급인ONT신분, ONT 신원 등 정보 소유자 및 철회여부 등과 같은 가용성정보를 기록하는 정보를 포함합니다. 

신뢰성명 규정에 관한 설명은 다음 내용을 참고하세요 : [신뢰성명 규정](https://github.com/ontio/ontology-DID/blob/master/docs/cn/claim_spec_cn.md)


### 1.1 데이터구조와 규정

상세한 표준은 다음 내용을 참고하세요. https://github.com/kunxian-xia/ontology-DID/blob/master/docs/en/claim_spec.md

Java-sdk는 JSON Web Token의 서식을 채택하여 claim을 표시함으로써 성명발행자와 신청자 간의 전달이 원활할 수 있도록 합니다. jwt서식은 header,payload,signature을 포함합니다.  

* Claim은 다음과 같은 데이터구조를 갖고 있습니다.

```java
class Claim{
  header : Header
  payload : Payload
  signature : byte[]
}
```


```java
class Header {
    public String Alg = "ONT-ES256";
    public String Typ = "JWT-X";
    public String Kid;
    }
```

필드설명
`alg` 사용되는 서명프레임
`typ` 아래 두 값 중 하나가 될 수 있습니다. 
JWT: 블록체인 증명이 claim에 포함되지 않는 것을 의미합니다.
JWT-X: 블록체인 증명이 claim의 일부임을 의미합니다. 
`kid` 서명에 사용되는 퍼블릭 키 

```java
class Payload {
    public String Ver;
    public String Iss;
    public String Sub;
    public long Iat;
    public long Exp;
    public String Jti;
    @JSONField(name = "@context")
    public String Context;
    public Map<String, Object> ClmMap = new HashMap<String, Object>();
    public Map<String, Object> ClmRevMap = new HashMap<String, Object>();
    }
```

`ver` Claim 버전넘버
`iss` 발행자의 ontid
`sub` 신청자의 ontid
`iat` 구축시간
`exp` 기한 초가 시간
`jti` claim의 유일한 표시
`@context` 지정성명내용이 문서 URI를 정의하며 모든 필드의 의미와 값의 유형을 정의합니다. 
`clm` claim내용을 포함하는 대상
`clm-rev` 각 claim의 철회시스템을 정의

### 1.2 신뢰성명 생성

유저의 입력내용에 따라 성명대상을 조직하고, 해당 성명대상은 서명된 데이터를 포함합니다. 
claim구축:
* 1. 체인상에서 Issuer의 DDO존재여부를 조회합니다.
* 2. 서명인의 퍼블릭 키는 반드시 DDO의 Owners에 있어야 합니다. 
* 3.claimld는 claim중 Signature、Id、Proof를 삭제하는 데이터를 byte배열로 전환하고 1차 sha256 후 hexstring로 전환합니다.  
* 4. 서명을 요하는 json데이터를 Map로 전환 후 key를 배열합니다.
* 5. Signature에서의 Value값: claim에서 Signature、Proof삭제 후 byte배열로 전환 후 2번의 sha256후 얻은 byte배열입니다.   

```java
Map<String, Object> map = new HashMap<String, Object>();
map.put("Issuer", dids.get(0).ontid);
map.put("Subject", dids.get(1).ontid);
Map clmRevMap = new HashMap();
clmRevMap.put("typ","AttestContract");
clmRevMap.put("addr",dids.get(1).ontid.replace(Common.didont,""));
String claim = ontSdk.nativevm().ontId().createOntIdClaim(dids.get(0).ontid,password,salt, "claim:context", map, map,clmRevMap,System.currentTimeMillis()/1000 +100000);
```

**createOntIdClaim**

```java
String createOntIdClaim(String signerOntid, String password,byte[] salt, String context, Map<String, Object> claimMap, Map metaData,Map clmRevMap,long expire)
```


기능설명: 신뢰성명 구축

| 피라미터      | 필드   | 유형  | 서술 |             설명 |
| ----- | ------- | ------ | ------------- | ----------- |
| 파라미터 력 | signerOntid| String | 서명인ontid | 필수 |
|        | password    | String | 서명인 암호   | 필수 |
|        | salt        | byte[] | 암호해독에 필요한 파라미터|필수|
|        | context| String  |지정 성명 내용이 정의한 문서 URI,  모든 필드의 의미와 값의 유형을 정의  | 필수|
|        | claimMap| Map  |성명 내용 | 필수|
|        | metaData   | Map | 성명 발행자와 신청자의 ontid | 필수 |
|        | clmRevMap   | Map | claim의 철회 매커니즘 | 필수 |
|        | expire   | long | 성명기한 초과 시간     | 필수 |
| 피라미터 송출 | claim   | String  | 신뢰성명  |  |

   

### 1.3 신뢰성명 검증

claim검증절차
* 1. 체인상에 Metadata중Issuer의 DDO존재여부 확인
* 2. Owner가 Sgnature중의 Publickeyld에 존재하는지 확인
* 3. 서명을 요구하는 json데이터를 Map으로  전환 후 key 배열
* 4. Signature를 삭제 후 서명검증 진행 (PublicKeyId의 id값에 따라 퍼블릭 키를 조회하며, 서명은 Signature중의 Value로 base64를 해독)

```java
boolean b = ontSdk.nativevm().ontId().verifyOntIdClaim(claim);
```

**verifyOntIdClaim**

```java
boolean verifyOntIdClaim(String claim)
```

기능설명: 신뢰성명 검증

| 파라미터      | 필드   | 유형  | 서술 |             설명 |
| ----- | ------- | ------ | ------------- | ----------- |
| 파라미터 입력 | claim| String | 신뢰성명 | 필수 |
| 파라미터 송출 | true 혹은 false   | boolean  |   |  |




## 2. 신뢰성명 증명저장계약의 사용절차

해당 계약은 저장, 취소, 상태조회 등의 기능을 제공합니다.

### 2.1. 초기화 SDK

증명저장계약 사용 전 먼저 초기화 후 계약주소를 설치해야 합니다.

```java
String ip = "http://127.0.0.1";
String restUrl = ip + ":" + "20334";
String rpcUrl = ip + ":" + "20336";
String wsUrl = ip + ":" + "20335";
OntSdk wm = OntSdk.getInstance();
wm.setRpc(rpcUrl);
wm.setRestful(restUrl);
wm.setDefaultConnect(wm.getRestful());
wm.openWalletFile("RecordTxDemo.json");
wm.setCodeAddress("803ca638069742da4b6871fe3d7f78718eeee78a");
```

> Note: codeAddress증명저장계약 주소。

    
    
###  2.2 신뢰성명을 체인에 저장합니다.

**sendCommit**

```java
String sendCommit(String issuerOntid, String password,byte[] salt, String subjectOntid, String claimId, Account payerAcct, long gaslimit, long gasprice)
```

기능설명: 데이터를 체인에 저장하고 증명저장을 성명합니다. 이때 해당 성명증명은 저장된 적이 없는 것으로 간주하고, Commit함수를 committer로 호출해야 성공적으로 증명을 저장할 수 있습니다. 그렇지 않으면 저장되지 않습니다. 저장성공 후 해당 성명의 상태는 committed로 표시됩니다.  

파라미터 설명:

| 파라미터      | 필드   | 유형  | 서술 |             설명 |
| ----- | ------- | ------ | ------------- | ----------- |
| 파라미터 입력 | issuerOntid| String | 신뢰성명 발급인의 디지털신분 ontid | 필수 |
| 파라미터 입력 | password| String | 디지털신분 암호 | 필수 |
| 파라미터 입력 | subjectOntid| String | 신뢰성명 신청인ontid | 필수 |
| 파라미터 입력 | claimId| String | 신뢰성명claim중 유일한 표시인 claim안의 Jit필드 | 필수 |
| 파라미터 입력 | payerAcct| String | 트랜젝션비용 지불자 계정 | 필수 |
| 파라미터 입력 | gaslimit| String | gaslimit | 필수 |
| 파라미터 입력 | gasprice| String | gasprice | 필수 |
| 파라미터 송출 | 트랜젝션hash   | boolean  |   |  |



코드 예시

```java
String[] claims = claim.split("\\.");
JSONObject payload = JSONObject.parseObject(new String(Base64.getDecoder().decode(claims[1].getBytes())));
String commitHash = ontSdk.neovm().claimRecord().sendCommit(dids.get(0).ontid,password,dids.get(1).ontid,payload.getString("jti"),account1,ontSdk.DEFAULT_GAS_LIMIT,0)
```

###  2.3. 신뢰성명 검색 상태

**sendGetStatus**

```
String sendGetStatus(String claimId)
```

기능설명:신뢰성명 검색 상태


파라미터 설명：

```claimId```： 신뢰성명claim중 유일한 표시인 claim안의 Jit필드

리턴 값: 두 부분: 첫 번째 부분, claim상태: "Not attested", "Attested", "Attest has been revoked";두 번째 부분은 저장인의 ontid


示例代码

```java
String getstatusRes2 = ontSdk.neovm().claimRecord().sendGetStatus(payload.getString("jti"));
```


###  2.4. 신뢰성명 취소


**sendRevoke**

```java
String sendRevoke(String issuerOntid,String password,byte[] salt,String claimId,Account payerAcct,long gaslimit,long gas)
```

기능 설명: 신뢰성명 취소


| 파라미터      | 필드   | 유형  | 서술 |             설명 |
| ----- | ------- | ------ | ------------- | ----------- |
| 파라미터 입력 | issuerOntid| String | 신뢰성명 발급인의 디지털신분 ontid | 필수 |
| 파라미터 입력 | password| String | 디지털신분 암호 | 필수 |
| 파라미터 입력 | salt| String | issuer의salt | 필수 |
| 파라미터 입력 | claimId| String | 신뢰성명claim중 유일한 표시인 claim안의 Jit필드 | 필수 |
| 파라미터 입력 | payerAcct| String | 트랜젝션비용 지불자 계정 | 필수 |
| 파라미터 입력 | gaslimit| String | gaslimit | 필수 |
| 파라미터 입력 | gasprice| String | gasprice | 필수 |
| 파라미출 | 트랜젝션hash   | boolean  |   |  |


표본코드

```java
String revokeHash = ontSdk.neovm().claimRecord().sendRevoke(dids.get(0).ontid,password,salt,payload.getString("jti"),account1,ontSdk.DEFAULT_GAS_LIMIT,0);
```
