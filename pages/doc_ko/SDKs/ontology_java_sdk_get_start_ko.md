---
title:
keywords: sample homepage
sidebar: SDKs_ko
permalink: ontology_java_sdk_get_start_ko.html
folder: doc_ko/SDKs
giturl: https://github.com/ontio/ontology-java-sdk/blob/master/docs/cn/sdk_get_start.md
---

<h1 align="center"> Java sdk 사용설명 </h1>

<p align="center" class="version">Version 1.0.0 </p>

[English](./ontology_java_sdk_get_start_en.html) / 한국어

ONT에는 두 가지 자산이 존재합니다: 기초자산과 계약자산입니다. Ont와 ong이 바로 기초자산입니다. 거래소와 연동 시 주로 이 두 유형의 자산으로 충전 및 인출 등이 가능합니다. 

sdk문서：[sdk문서](https://github.com/ontio/ontology-java-sdk/tree/master/docs/cn) 

본문의 요약은 다음과 같습니다. 
* [java-sdk-사용설명](# java-sdk-사용설명)
	* [1. 퍼블릭 및 프라이빗키와 주소](#1-퍼블릭 및 프라이빗키와 주소)
		* [1.1퍼블릭 및 프라이빗키 저장](#11-퍼블릭 및 프라이빗키 저장)
			* [1.1.1자체저장：](#111-자체저장)
				* [계정 랜덤 생성：](# 계정 랜덤 생성)
				* [프라이빗키로 계정 생성](# 프라이빗키로 계정 생성)
			* [1.1.2지갑 규정에 따른 저장：](#112-지갑 규정에 따른 저장)
		* [1.2주소](#12-주소)
	* [2. 기초자산 이체](#2-기초자산 이체)
		* [2.1초기화](#21-초기화)
		* [2.2조회](#22-조회)
			* [ ont，ong잔액 조회](#ontong잔액 조회)
			* [트랜젝션이 트랜젝션 풀에 있는지 여부 조회](# 트랜젝션이 트랜젝션 풀에 있는지 여부 조회)
			* [트랜젝션이 성공적으로 호출되었는지 조회](# 트랜젝션이 성공적으로 호출되었는지 조회)
			* [스마트event통합검색](# 스마트 컨트랙트event통합검색)
			* [체인과 연동된 기타 인터페이스 리스트：](# 체인과 연동된 기타 인터페이스 리스트)
		* [2.3트랜젝션 리버스 오더](#23-트랜젝션 리버스 오더)	
		* [2.4 ONT이체](#24-ONT이체)
2.4 ONT이체 $24- ONT이체
			* [이체 트랜젝션 구성 및 전송](# 이체 트랜젝션 구성 및 전송)
			* [연속서명](#연속서명)
			* [1회 고액이체 또는 수 차례 고액이체](# 1회 고액이체 또는 수 차례 고액이체)
			* [서명디바이스를 이용한 서명](# 서명디바이스를 이용한 서명)
		* [2.5 ONG이체](#25-ONG이체)
			* [ ONG이체](#ONG이체)
			* [ ong인출](#ong인출)
	* [3. N](#3-nep5이체
)
		* [3.1조회](#31-조회)
		* [3.2이체](#32-조회)


## 1. 퍼블릭 및 프라이빗키 주소

계정은 퍼블릭 및 프라이빗키를 기반으로 생성되며 주소는 퍼블릭키를 전환하여 생성됩니다. 

###  1.1 ** 퍼블릭 및 프라이빗키 저장

퍼블릭 및 프라이빗키는 데이터베이스에 저장 가능하며 지갑 규정에 따라 문서에도 저장 가능합니다. 

#### 1.1.1 자체저장: 

자체저장이란 계정정보를 지갑규정을 따르는 문서가 아닌 유저의 데이터베이스 및 다른 곳에 저장하는 것을 의미합니다.  
#####  랜덤 계정 생성:
```java
com.github.ontio.account.Account acct = new com.github.ontio.account.Account(ontSdk.defaultSignScheme);
acct.serializePrivateKey();//프라이빗키
acct.serializePublicKey();//퍼블릭키
acct.getAddressU160().toBase58();//base58 주소
##### 프라이빗키에 따른 계정생성
```java     
com.github.ontio.account.Account acct0 = new com.github.ontio.account.Account(Helper.hexToBytes(privatekey0), ontSdk.defaultSignScheme);
com.github.ontio.account.Account acct1 = new com.github.ontio.account.Account(Helper.hexToBytes(privatekey1), ontSdk.defaultSignScheme);
com.github.ontio.account.Account acct2 = new com.github.ontio.account.Account(Helper.hexToBytes(privatekey2), ontSdk.defaultSignScheme);

```

#### 1.1.2 지갑규정에 따른 저장

계정과 신분정보는 지갑규정을 따르는 문서에 저장됩니다. [예시]/ontology-java-sdk/blob/master/src/main/java/demo/WalletDemo.java) 




#### 지갑에서 다량의 계정생성

여러 계정을 생성하는 방법
```java
ontSdk.getWalletMgr().createAccounts(10, "passwordtest");
ontSdk.getWalletMgr().writeWallet();

랜덤 생성:
AccountInfo info0 = ontSdk.getWalletMgr().createAccountInfo("passwordtest");

프라이빗키를 통한 생성:
AccountInfo info = ontSdk.getWalletMgr().createAccountInfoFromPriKey("passwordtest","e467a2a9c9f56b012c71cf2270df42843a9d7ff181934068b4a62bcdd570e8be");

계정 획득:
com.github.ontio.account.Account acct0 = ontSdk.getWalletMgr().getAccount(info.addressBase58,"passwordtest",salt);

```




###  1.2 **주소**


싱글서명 주소와 멀티서명 주소를 포함하며 생성방식은 NEO주소와 동일합니다. 

싱글서명 주소는 퍼블릭키를 전환하여 생성되며 멀티서명 주소는 여러 퍼블릭키를 전환하여 생성됩니다.  

```java
싱글서명 주소 생성:
String privatekey0 = "c19f16785b8f3543bbaf5e1dbb5d398dfa6c85aaad54fc9d71203ce83e505c07";
String privatekey1 = "49855b16636e70f100cc5f4f42bc20a6535d7414fb8845e7310f8dd065a97221";
String privatekey2 = "1094e90dd7c4fdfd849c14798d725ac351ae0d924b29a279a9ffa77d5737bd96";

//계정을 생성하고 주소를 획득합니다. 
com.github.ontio.account.Account acct0 = new com.github.ontio.account.Account(Helper.hexToBytes(privatekey0), ontSdk.defaultSignScheme);
Address sender = acct0.getAddressU160();

//base58주소 해독
sender = Address.decodeBase58("AVcv8YBABi9m6vH7faq3t8jWNamDXYytU2")；

멀티서명 주소 생성:
Address recvAddr = Address.addressFromMultiPubKeys(2, acct1.serializePublicKey(), acct2.serializePublicKey());


```


|방법이름 | 파라미터 | 파라미터 서술 |
| :--- | :--- | :--- |
| addressFromMultiPubkeys | int m,byte\[\]... pubkeys | 최소 서명 개수(<=퍼블릭키 개수)，퍼블릭키 |

## 2. 기초자산 이체

참고예시:[예시](https://github.com/ontio/ontology-java-sdk/blob/master/src/main/java/demo/MakeTxWithoutWalletDemo.java)


2.1 초기화

이체 전에 SDK예시를 구축하고 노드IP를 매칭해야 합니다. 

```java

String ip = "http://polaris1.ont.io";
String rpcUrl = ip + ":" + "20336";
OntSdk ontSdk = OntSdk.getInstance();
ontSdk.setRpc(rpcUrl);
ontSdk.setDefaultConnect(wm.getRpc());

또는 restful 사용:
String restUrl = ip + ":" + "20334";
ontSdk.setRestful(restUrl);
ontSdk.setDefaultConnect(wm.getRestful());

也可以选择websocket：
String wsUrl = ip + ":" + "20335";
ontSdk.setWesocket(wsUrl, lock);
ontSdk.setDefaultConnect(wm.getWebSocket());

```


### 2.2 조회

트랜젝션 전송 후 트랜젝션이 정상적으로 원장에 기입되었는지 확인해야 할 수도 있으며 계정잔액을 조회해야 할 수도 있습니다. 
####  **ont，ong잔액조회**

```java
ontSdk.getConnect().getBalance("AVcv8YBABi9m6vH7faq3t8jWNamDXYytU2");
ontSdk.nativevm().ont().queryBalanceOf("AVcv8YBABi9m6vH7faq3t8jWNamDXYytU2")
ontSdk.nativevm().ong().queryBalanceOf("AVcv8YBABi9m6vH7faq3t8jWNamDXYytU2")

ont정보 조회:
System.out.println(ontSdk.nativevm().ont().queryName());
System.out.println(ontSdk.nativevm().ont().querySymbol());
System.out.println(ontSdk.nativevm().ont().queryDecimals());
System.out.println(ontSdk.nativevm().ont().queryTotalSupply());

ont정보 조회:
System.out.println(ontSdk.nativevm().ong().queryName());
System.out.println(ontSdk.nativevm().ong().querySymbol());
System.out.println(ontSdk.nativevm().ong().queryDecimals());
System.out.println(ontSdk.nativevm().ong().queryTotalSupply());



```

#### **트랜젝션이 트랜젝션 풀에 있는지 여부 조회**

인터페이스를 통해 트랜젝션이 트랜젝션 풀에 있는지 여부를 조회합니다.
```json

ontSdk.getConnect().getMemPoolTxState("d441a967315989116bf0afad498e4016f542c1e7f8605da943f07633996c24cc")


response 트랜젝션 풀에 해당 트랜젝션이 존재합니다:

{
    "Action": "getmempooltxstate",
    "Desc": "SUCCESS",
    "Error": 0,
    "Result": {
        "State":[
            {
              "Type":1,
              "Height":744,
              "ErrCode":0
            },
            {
              "Type":0,
              "Height":0,
              "ErrCode":0
            }
       ]
    },
    "Version": "1.0.0"
}

또는 트랜젝션 풀에 해당 트랜젝션이 존재하지 않습니다.

{
    "Action": "getmempooltxstate",
    "Desc": "UNKNOWN TRANSACTION",
    "Error": 44001,
    "Result": "",
    "Version": "1.0.0"
}

```


#### **트랜젝션 호출 성공여부 조회**

스마트 컨트랙트의 전송내용이 조회 가능하면 트랜젝션이 성공적으로 진행되었음을 의미하며 만일 성공하지 못했을 경우에는 States에 transfer이 발생하지 않습니다. 

```json
ontSdk.getConnect().getSmartCodeEvent("d441a967315989116bf0afad498e4016f542c1e7f8605da943f07633996c24cc")


response:
{
    "Action": "getsmartcodeeventbyhash",
    "Desc": "SUCCESS",
    "Error": 0,
    "Result": {
        "TxHash": "20046da68ef6a91f6959caa798a5ac7660cc80cf4098921bc63604d93208a8ac",
        "State": 1,
        "GasConsumed": 0,
        "Notify": [
            {
                "ContractAddress": "ff00000000000000000000000000000000000001",
                "States": [
                    "transfer",
                    "T9yD14Nj9j7xAB4dbGeiX9h8unkKHxuWwb",
                    "TA4WVfUB1ipHL8s3PRSYgeV1HhAU3KcKTq",
                    1000000000
                ]
            }
        ]
    },
    "Version": "1.0.0"
}

```

블록 높이에 따라 스마트 컨트랙트 시나리오를 조회하면 시나리오가 있는 트랜젝션 hash가 리턴됩니다. 


```json

ontSdk.getConnect().getSmartCodeEvent(10)

response:
{
    "Action": "getsmartcodeeventbyheight",
    "Desc": "SUCCESS",
    "Error": 0,
    "Result": [{
	"GasConsumed": 0,
	"Notify": [{
		"States": ["transfer", "AFmseVrdL9f9oyCzZefL9tG6UbvhPbdYzM", "APrfMuKrAQB5sSb5GF8tx96ickZQJjCvwG", 1000000000],
		"ContractAddress": "0100000000000000000000000000000000000000"
	}],
	"TxHash": "b8a4f77e19fcae04faa576fbc71fa5a9775166d4485ce13f1ba5ff30ce264c52",
	"State": 1
     }, {
	"GasConsumed": 0,
	"Notify": [{
		"States": ["transfer", "AFmseVrdL9f9oyCzZefL9tG6UbvhPbdYzM", "AFmseVrdL9f9oyCzZefL9tG6UbvhUMqNMV", 1000000000000000000],
		"ContractAddress": "0200000000000000000000000000000000000000"
	}],
	"TxHash": "7e8c19fdd4f9ba67f95659833e336eac37116f74ea8bf7be4541ada05b13503e",
	"State": 1
     }, {
	"GasConsumed": 0,
	"Notify": [],
	"TxHash": "80617b4a97eb4266e5e38886f234f324d57587362b5039a01c45cf413461f53b",
	"State": 1
     }, {
	"GasConsumed": 0,
	"Notify": [],
	"TxHash": "ede7ecc6e4e7e699b8ba1f07f2e5f8af3b65e70f126d82f7765d20a506080d2d",
	"State": 0
}],
    "Version": "1.0.0"
}

```

#### **스마트 컨트랙트 event 동기화 조회**

트랜젝션 전송 후 트랜젝션을 조회해야 리턴됩니다. 

```json
//트랜젝션 전송 후 3초마다 요청되며 최장 60초까지 대기합니다. 

Object object = ontSdk.getConnect().waitResult(tx.hash().toString());
System.out.println(object);

response success:
{
	"GasConsumed": 0,
	"Notify": [],
	"TxHash": "cb9e0d4a7a4aea0518bb39409613b8ef76798df3962feb8f8040e05329674890",
	"State": 1
}

response fail,reject by txpool:
com.github.ontio.sdk.exception.SDKException: {"Action":"getmempooltxstate","Desc":"UNKNOWN TRANSACTION","Error":44001,"Result":"","Version":"1.0.0"}

```

#### 체인과 연동되는 기타 인터페이스 리스트:

체인과 연동되는 또 다른 인터페이스는 다음과 같습니다. 
```

      |                     Main   Function                      |           Description            
 -----|----------------------------------------------------------|---------------------------------------------
    1 | ontSdk.getConnect().getNodeCount()                       |  노드수량 조회
    2 | ontSdk.getConnect().getBlock(15)                         |  블록조회
    3 | ontSdk.getConnect().getBlockJson(15)                     |  블록조회    
    4 | ontSdk.getConnect().getBlockJson("txhash")               |  블록조회    
    5 | ontSdk.getConnect().getBlock("txhash")                   |  블록조회     
    6 | ontSdk.getConnect().getBlockHeight()                     |  현재 블록높이 조회
    7 | ontSdk.getConnect().getTransaction("txhash")             |  트랜젝션조회                                                                     
    8 | ontSdk.getConnect().getStorage("contractaddress", key)   |  스마트 컨트랙트 스토리지 조회
    9 | ontSdk.getConnect().getBalance("address")                |  잔액 조회
   10 | ontSdk.getConnect().getContractJson("contractaddress")   |  스마트 컨트랙트 조회              
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
```  

### 2.3 트랜젝션 리버스오더

json서식의 트랜젝션 데이터 획득

```json  
http://polaris1.ont.io:20334/api/v1/transaction/8f4ab5db768e41e56643eee10ad9749be0afa54a891bcd8e5c45543a8dd0cf7d?raw=0

{
    "Action": "gettransaction",
    "Desc": "SUCCESS",
    "Error": 0,
    "Result": {
        "Version": 0,
        "Nonce": 391455426,
        "GasPrice": 500,
        "GasLimit": 20000,
        "Payer": "ASyx6be9APCR6BzcM81615FgBU26gqr1JL",
        "TxType": 209,
        "Payload": {
            "Code": "00c66b147af216ff3da82b999b26f5efe165de5f944ac5496a7cc814d2c124dd088190f709b684e0bc676d70c41b37766a7cc80800ca9a3b000000006a7cc86c51c1087472616e736665721400000000000000000000000000000000000000010068164f6e746f6c6f67792e4e61746976652e496e766f6b65"
        },
        "Attributes": [],
        "Sigs": [
            {
                "PubKeys": [
                    "0369d1e9a5a1d83fa1798bbd162e8d8d8ef8e4e1a0e03aa2753b472943e235e219"
                ],
                "M": 1,
                "SigData": [
                    "017b80d5f0826b52b2037ee564be55f0ada1d0cb714a80967deb2d04b49a59f6c4358c57d06ee8f7666aec3fc570c5251c30be1cd134acb791775de9e11cacd22c"
                ]
            }
        ],
        "Hash": "8f4ab5db768e41e56643eee10ad9749be0afa54a891bcd8e5c45543a8dd0cf7d",
        "Height": 95796
    },
    "Version": "1.0.0"
}

```  

hex서식의 트랜젝션 데이터 획득
```json  
http://polaris1.ont.io:20334/api/v1/transaction/8f4ab5db768e41e56643eee10ad9749be0afa54a891bcd8e5c45543a8dd0cf7d?raw=1


{
    "Action": "gettransaction",
    "Desc": "SUCCESS",
    "Error": 0,
    "Result": "00d1c2225517f401000000000000204e0000000000007af216ff3da82b999b26f5efe165de5f944ac5497900c66b147af216ff3da82b999b26f5efe165de5f944ac5496a7cc814d2c124dd088190f709b684e0bc676d70c41b37766a7cc80800ca9a3b000000006a7cc86c51c1087472616e736665721400000000000000000000000000000000000000010068164f6e746f6c6f67792e4e61746976652e496e766f6b6500014241017b80d5f0826b52b2037ee564be55f0ada1d0cb714a80967deb2d04b49a59f6c4358c57d06ee8f7666aec3fc570c5251c30be1cd134acb791775de9e11cacd22c23210369d1e9a5a1d83fa1798bbd162e8d8d8ef8e4e1a0e03aa2753b472943e235e219ac",
    "Version": "1.0.0"
}

``` 

트랜젝션 중의 데이터 내용을 이해하려면 트랜젝션 데이터를 리버스오더 해야 트랜젝션의 유형과 상세한 정보를 판단해 낼 수 있습니다. 
``` 
//버전넘버 트랜젝션 유형 랜덤수  gasprice    gaslimit              네트워크비용 지불인   트랜젝션 데이터
(version(1) type(1) nonce(4) gasprice(8) gaslimit(8))22 bytes + (payer)21 bytes + payload code bytes( any bytes)

claim ong 
//             claim address                                                 ont contract address                         to   address                                 amount                       "transferFrom"                           ong                   SYSCALL         "Ontology.Native.Invoke"
//00 c66b 14bb2d5b718efeac060ac825338ca440216da4d8dc 6a7cc8 140000000000000000000000000000000000000001 6a7cc8 14bb2d5b718efeac060ac825338ca440216da4d8dc 6a7cc8 08 806a735501000000 6a7cc8 6c 0c7472616e7366657246726f6d 140000000000000000000000000000000000000002 0068 164f6e746f6c6f67792e4e61746976652e496e766f6b65
ont and ong transfer
//                     from                                           to                                        amount                                 "transfer"                                                                       ont or ong                SYSCALL           "Ontology.Native.Invoke"
//00 c66b 147af216ff3da82b999b26f5efe165de5f944ac549 6a7cc8 14d2c124dd088190f709b684e0bc676d70c41b3776 6a7cc8 08 00ca9a3b00000000 6a7cc8 6c 51c1 087472616e73666572                                                      140000000000000000000000000000000000000001 0068 164f6e746f6c6f67792e4e61746976652e496e766f6b65

For amount ：   1-16  is  0x51-0x60  .     >=16 is  long,  08 is the total amount bytes .  
Example: 1000 is  0xe803000000000000 -> 0x00000000000003e8   change from little endian to big endian if print.

```  

### 2.4 ONT이체

ONT와 ONG이체는 일 대 일 또는 일 대 다수, 다수 대 다수, 다수 대 일로 이루어 질 수 있습니다.  

#### **이체 트랜잭션 구성 및 전송**

```java
송금인과 수취인 주소 :
Address sender = acct0.getAddressU160();
Address recvAddr = acct1;
//
멀티서명 주소 생성
//Address recvAddr = Address.addressFromMultiPubKeys(2, acct1.serializePublicKey(), acct2.serializePublicKey());

이체 트랜젝션 구성
long amount = 1000;
Transaction tx = ontSdk.nativevm().ont().makeTransfer(sender.toBase58(),recvAddr.toBase58(), amount,sender.toBase58(),30000,0);
String hash = tx.hash().toString()

트랜젝션에 서명하기:
ontSdk.signTx(tx, new com.github.ontio.account.Account[][]{{acct0}});
//멀티서명 주소에서의 서명 방법：
ontSdk.signTx(tx, new com.github.ontio.account.Account[][]{{acct1, acct2}});
//만약 송금인이 네트워크비용 지불인 주소와 다른 경우에는 네트워크비용 지불인의 서명을 추가해야 합니다.


사전 집행 전송(옵션):
Object obj = ontSdk.getConnect().sendRawTransactionPreExec(tx.toHexString());
System.out.println(obj);
리턴 성공:
{"State":1,"Gas":30000,"Result":"01"}
잔액 부족으로 리턴 오류 :
com.github.ontio.network.exception.RestfulException: {"Action":"sendrawtransaction","Desc":"SMARTCODE EXEC ERROR","Error":47001,"Result":"","Version":"1.0.0"}


트랜젝션 전송:
ontSdk.getConnect().sendRawTransaction(tx.toHexString());


트랜젝션 동기화 전송:
Object obj = ontSdk.getConnect().syncSendRawTransaction(tx.toHexString());

response success:
{
	"GasConsumed": 0,
	"Notify": [],
	"TxHash": "cb9e0d4a7a4aea0518bb39409613b8ef76798df3962feb8f8040e05329674890",
	"State": 1
}

response fail,reject by txpool:
com.github.ontio.sdk.exception.SDKException: {"Action":"getmempooltxstate","Desc":"UNKNOWN TRANSACTION","Error":44001,"Result":"","Version":"1.0.0"}

```



| 방법 | 파라미터 | 파라미터 서술 |
| :--- | :--- | :--- |
| makeTransfer | String sender，String recvAddr,long amount,String payer,long gaslimit,long gasprice | 전송인 주소, 수신인 주소, 금액, 네트워크비용 지불인 주소，gaslimit，gasprice |
| makeTransfer | State\[\] states,String payer,long gaslimit,long gasprice | 1회 트랜젝션에 다수의 이체거래가 포함 |


####**멀티서명**

만약 송금인이 네트워크비용 지불인 주소와 다른 경우에는 네트워크비용 지불인의 서명을 추가해야 합니다.

```java

1.싱글서명 추가
ontSdk.addSign(tx,acct0);

2.멀티서명 추가
ontSdk.addMultiSign(tx,2,new byte[][]{acct.serializePublicKey(),acct2.serializePublicKey()},acct);
ontSdk.addMultiSign(tx,2,new byte[][]{acct.serializePublicKey(),acct2.serializePublicKey()},acct2);

3.멀티서명을 여러 회 나눠서 서명
acct0서명：
ontSdk.addMultiSign(tx,2,new byte[][]{acct.serializePublicKey(),acct2.serializePublicKey()},acct);

acct1서명：
ontSdk.addMultiSign(tx,2,new byte[][]{acct.serializePublicKey(),acct2.serializePublicKey()},acct2);

```


 
#### **1회 고액이체 또는 수 차례 고액이체**

1. 다수의 state트랜젝션 구성
2. 서명
3. 1회 트랜젝션의 상한선은 이체 1024회


```java

Address sender1 = acct0.getAddressU160();
Address sender2 = Address.addressFromMultiPubKeys(2, acct1.serializePublicKey(), acct2.serializePublicKey());
int amount = 10;
int amount2 = 20;

State state = new State(sender1, recvAddr, amount);
State state2 = new State(sender2, recvAddr, amount2);
Transaction tx = ontSdk.nativevm().ont().makeTransfer(new State[]{state1,state2},sender1.toBase58(),30000,0);

//첫 번째 송금인은 싱글서명이고 두 번째 송금인은 멀티서명 입니다. 
ontSdk.signTx(tx, new com.github.ontio.account.Account[][]{{acct0}});
ontSdk.addMultiSign(tx,2,new byte[][]{acct1.serializePublicKey(),acct2.serializePublicKey()},acct1);
ontSdk.addMultiSign(tx,2,new byte[][]{acct1.serializePublicKey(),acct2.serializePublicKey()},acct2);
```

#### 서명디바이스를 이용해 서명하기

** 트랜젝션 구성 및 서명**

1. 트랜젝션을 구성하고 트랜젝션을 서열화 한 후 트랜젝션을 서명디바이스에 전송합니다. 
2. 서명디바이스가 트랜젝션을 수신하면 서열화 후 트랜젝션을 검사하고 서명을 추가합니다. 
3. 트랜젝션을 전송합니다.

```java

서열화한 트랜젝션을 서명디바이스에 전송:
Transaction tx = ontSdk.nativevm().ont().makeTransfer(sender.toBase58(),recvAddr.toBase58(), amount,sender.toBase58(),30000,0);
String txHex = tx.toHexString();

수신인이 트랜젝션을 리버스오더 후 서명:

Transaction txRx = Transaction.deserializeFrom(Helper.hexToBytes(txHex));


서명：
ontSdk.addSign(txRx,acct0);
```

**SDK와 서명디바이스 연동**:

[예시](https://github.com/ontio/ontology-java-sdk/blob/master/src/main/java/demo/SignServerDemo.java)

```java
노드 부팅 시 서명디바이스 서비스 켜기: 
go run SigSvr.go


서명디바이스URL 설치:
String url = ip + ":" + "20000/cli";
OntSdk ontSdk = OntSdk.getInstance();
ontSdk.setSignServer(url);
        

String txHex = tx.toHexString();

싱글서명 트랜젝션 요청 : 
ontSdk.getSignServer().sendSigRawTx(txHex);
 
멀티서명 트랜젝션 요청: 
String[] signs = new String[]{"02039b196d5ed74a4d771ade78752734957346597b31384c3047c1946ce96211c2a7",
                    "0203428daa06375b8dd40a5fc249f1d8032e578b5ebb5c62368fc6c5206d8798a966"};
ontSdk.getSignServer().sendMultiSigRawTx(txHex,2,signs);

이체 트랜젝션 구성 및 서명 요청:
ontSdk.getSignServer().sendSigTransferTx("ont","TU5exRFVqjRi5wnMVzNoWKBq9WFncLXEjK","TA5SgQXTeKWyN4GNfWGoXqioEQ4eCDFMqE",10,30000,0);
            

```

 **데이터에 서명하기**

SDK가 데이터에 직접 서명하는 인터페이스를 제공합니다. [예시](https://github.com/ontio/ontology-java-sdk/blob/master/src/main/java/demo/SignatureDemo.java) 


```java
com.github.ontio.account.Account acct = new com.github.ontio.account.Account(ontSdk.defaultSignScheme);

byte[] data = "12345".getBytes();
byte[] signature = ontSdk.signatureData(acct, data);

System.out.println(ontSdk.verifySignature(acct.serializePublicKey(), data, signature));

```



### 2.5 ONG이체

ONG이체방법은 ONT이체와 비슷하지만 ONG의 정확도는 9입니다. 
####  **ONG이체**


```json
ontSdk.nativevm().ong().makeTransfer(sender.toBase58(),recvAddr.toBase58(), amount,sender.toBase58(),30000,0);
```

####  **ong인출**

1. ong가 있는지 조회 후 인출 가능합니다. 
2. ong인출 트랜젝션을 전송합니다. 

```json
미인출ong조회:
String addr = acct0.getAddressU160().toBase58();
String ong = sdk.nativevm().ong().unboundOng(addr);

//ong인출
com.github.ontio.account.Account account = new com.github.ontio.account.Account(Helper.hexToBytes(privatekey0), ontSdk.signatureScheme);
String hash = sdk.nativevm().ong().withdrawOng(account,toAddr,64000L,payerAcct,30000,500);

```



## 3. NEP5이체

참고예시: [예시](https://github.com/ontio/ontology-java-sdk/blob/master/src/main/java/demo/Nep5Demo.java)

### 3.1 조회

```json
String balance = ontSdk.neovm().nep5().queryBalanceOf(acct.address);
System.out.println(new BigInteger(Helper.reverse(Helper.hexToBytes(balance))).longValue());

String totalSupply = ontSdk.neovm().nep5().queryTotalSupply();
System.out.println(new BigInteger(Helper.reverse(Helper.hexToBytes(totalSupply))).longValue());

String decimals = ontSdk.neovm().nep5().queryDecimals();
System.out.println(decimals);

String name = ontSdk.neovm().nep5().queryName();
System.out.println(new String(Helper.hexToBytes(name)));

String symbol = ontSdk.neovm().nep5().querySymbol();
System.out.println(new String(Helper.hexToBytes(symbol)));

System.out.println(Address.decodeBase58(acct.address).toHexString());
```

### 3.2 이체

```json
ontSdk.neovm().nep5().sendTransfer(acct,"AVcv8YBABi9m6vH7faq3t8jWNamDXYytU2",46440000L,acct,gasLimit,0);
```

