---
title:
keywords: sample homepage
sidebar: SDKs_ko
permalink: ontology_java_sdk_basic_ko.html
folder: doc_ko/SDKs
giturl: https://github.com/ontio/ontology-java-sdk/blob/master/docs/cn/basic.md
---

<h1 align="center"> 블록체인 상호작용의의 기본 작동 </h1>

<p align="center" class="version">Version 1.0.0 </p>

[English](./ontology_java_sdk_basic_en.html) / 한국어


다음은 SDK사용 및 블록체인 상호작용의에 관한 기본 작동 및 관련 데이터구조에 관한 정의입니다. 

Java SDK사용 전에 다음과 같은 방식으로 OntSDK예시를 초기화 하십시오.

```java
OntSdk ontSdk = OntSdk.getInstance();
ontSdk.setRpc(rpcUrl);
ontSdk.setRestful(restUrl);
ontSdk.setDefaultConnect(wm.getRestful());
ontSdk.openWalletFile("OntAssetDemo.json");
```
> Note: setRestful은 restful인터페이스를 이용해 연결을 구축함을 의미하고 setRpc는 rpc인터페이스를 이용해 연결을 구축함을 의미합니다. 또한 setDefaultConnect는 디폴트를 설치하는 링크방식을 의미합니다.  


## 체인과 인터페이스 간의 연동


* 현재 블록의 높이 획득
```java
int height = ontSdk.getConnect().getBlockHeight();
```

* 블록 획득

높이에 따른 블록 획득
```java
Block block = ontSdk.getConnect().getBlock(9757);
```

블록hash에 따른 블록 획득

```java
Block block = ontSdk.getConnect().getBlock(blockhash);
```

* Block json데이터 획득

높이에 따른 블록json획득
```java
Object block = ontSdk.getConnect().getBlockJson(9757);
```

블록hash에 따른 블록json획득

```java
Object block = ontSdk.getConnect().getBlockJson(blockhash);
```
* 계약코드 획득

계약hash에 따른 계약코드 획득

```java
Object contract =  ontSdk.getConnect().getContract(contractHash)
```

계약hash에 따른 계약코드 json데이터 획득

```java
Object contractJson = ontSdk.getConnect().getContractJson(hash)
```

* 잔액조회

계정주소에 따른 잔액조회

```java
Object  balance = ontSdk.getConnect().getBalance(address)
```

* 블록체인 노드 수 획득

```java
int count = ontSdk.getConnect().getNodeCount();
```

* 블록 높이 획득

```java
int blockheight = ontSdk.getConnect().getBlockHeight()
```

* 스마트트 건수 획득

높이에 따른 스마트 컨트랙트 건수 획득

```java
Object  event = ontSdk.getConnect().getSmartCodeEvent(height)
```

트랜젝션hash에 따른 스마트 컨트랙트 건수 획득

```java
Object  event = ontSdk.getConnect().getSmartCodeEvent(hash)
```

* 트랜젝션hash에 따른 블록높이 획득

```java
int blockheight = ontSdk.getConnect().getBlockHeightByTxHash(txhash)
```

* 스마트 컨트랙트 스토리지에 관한 데이터 획득

```java
String value = ontSdk.getConnect().getStorage(codehash,key)
```

* merkle증명 획득

트랜젝션hash에 따른 merkle증명 획득

```java
Object proof =  ontSdk.getConnect().getMerkleProof(String hash)
```

* 블록체인에서 트랜젝션 획득

트랜젝션hash에 따른 트랜젝션 대상 획득
```java
Transaction info = ontSdk.getConnect().getTransaction(txhash);
```

트랜젝션hash에 따른 트랜젝션 json데이터 획득

```java
Object info = ontSdk.getConnect().getTransactionJson(txhash);
```

* 블록체인에서 lnvokeCodeTransaction획득

```java
InvokeCodeTransaction t = (InvokeCodeTransaction) ontSdk.getConnect().getTransaction(txhash);
```
## 데이터구조 설명

* Block블록

| Field     |     Type |   Description   | 
| :--------------: | :--------:| :------: |
|    version|   int|  버전 넘버  |
|    prevBlockHash|   UInt256|  이전 블록의 해시함수|
|    transactionsRoot|   UInt256|  해당 블록에서 트랜잭션된 Merkle트리의 루트|
|    blockRoot|   UInt256| 블록루트|
|    timestamp|   int| 블록 타임스탬프, unix타임스탬프  |
|    height|   int|  블록 높이  |
|    consensusData|   long |  컨센서스 데이터 |
|    consensusPayload|   byte[] |  컨센서스payload |
|    nextBookKeeper|   UInt160 |  다음 블록 원장 계약 해시함수 |
|    sigData|   array|  서명 |
|    bookKeepers|   array|  서명인 |
|    hash|   UInt256 |  해당 블록의 hash값 |
|    transactions|   Transaction[] |  해당 블록의 트랜젝션 리스트 |


* Transaction트랜젝션

| Field     |     Type |   Description   | 
| :--------------: | :--------:| :------: |
|    version|   int|  버전넘버  |
|    txType|   TransactionType|  트랜젝션 유형|
|    nonce|   int |  랜덤 수|
| gasPrice|  long |  gas가격|
| gasLimit|  long |  gas상한선|
|    payer|   Address |  트랜젝션 비용 지불계정|
|    attributes|   Attribute[]|  트랜젝션 속성 리스트 |
|    sigs|   Sign[]|   서명 배열  |
|    payload| Payload |  payload  |


* TransactionType트랜젝션 유형

| Value     |     Type |   Description   | 
| :--------------: | :--------:| :------: |
|    208|   int |  스마트 컨트랙트 트랜젝션 설치|
|    209|   int | 스마트 컨트랙트 트랜젝션 호출 |
|      0|   int |     Bookkeeping   |
|      4|   int |     등록       |
|      5|   int |     투표 |


* 서명 필드

| Field     |     Type |   Description   | 
| :--------------: | :--------:| :------: |
|    pubKeys|   array |  퍼블릭키 배열|
|    M|   int | M |
|    sigData|   array | 서명함수 배열 |



* Attribute트랜잭션 속성

| Field    |     Type |   Description   | 
| :--------------: | :--------:| :------: |
|    usage |   AttributeUsage |  용도|
|    data|   byte[] | 속성값 |


* TransactionAttributeUsage속성용도

| Value     |     Type |   Description   | 
| :--------------: | :--------:| :------: |
|    0|   int|  Nonce|
|    32|   int | Script |
|    129|   int | DescriptionUrl |
|    144|   int | Description |
