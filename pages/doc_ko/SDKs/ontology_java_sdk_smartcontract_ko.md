---
title:
keywords: sample homepage
sidebar: SDKs_ko
permalink: ontology_java_sdk_smartcontract_ko.html
folder: doc_ko/SDKs
giturl: https://github.com/ontio/ontology-java-sdk/blob/master/docs/cn/smartcontract.md
---

<h1 align="center"> Java sdk 스마트컨트랙트 </h1>

<p align="center" class="version">Version 1.0.0 </p>

[English](./ontology_java_sdk_smartcontract_en.html) / 한국어

## 소개

이번 페이지는 Java SDK를 통해 어떻게 스마트 컨트랙트를 실행하는지 설명합니다.

## 스마트컨트랙트 배치, 호출, 이벤트 푸쉬

> Note:현재까지java-sdk는neo스마트 컨트랙트 설치와 용을 지지하며, WASM컨트랙트를 지원하지 않습니다. NEO와WASM의 컨트랙트 설치방법은 같으면 조작시 다른 몇 가지 부분을 아래에서 설명해드리겠습니다.


### 설치

[SmartX](https://smartx.ont.io/)를 통해 스마트 컨트랙트를 컴파일하면, SmartX에서 직접 계약을 설치할 수 있고 java sdk를 통해서도 계약설치가 가능합니다. 

```java
InputStream is = new FileInputStream("/Users/sss/dev/ontologytest/IdContract/IdContract.avm");
byte[] bys = new byte[is.available()];
is.read(bys);
is.close();
code = Helper.toHexString(bys);
ontSdk.setCodeAddress(Address.AddressFromVmCode(code).toHexString());

//계약 설치하기
Transaction tx = ontSdk.vm().makeDeployCodeTransaction(code, true, "name",
                    "v1.0", "author", "email", "desp", account.getAddressU160().toBase58(),ontSdk.DEFAULT_DEPLOY_GAS_LIMIT,500);
String txHex = Helper.toHexString(tx.toArray());
ontSdk.getConnect().sendRawTransaction(txHex);
//블록 나오기 기다리기
Thread.sleep(6000);
DeployCodeTransaction t = (DeployCodeTransaction) ontSdk.getConnect().getTransaction(txHex);
```

**makeDeployCodeTransaction**

```java
public DeployCode makeDeployCodeTransaction(String codeStr, boolean needStorage, String name, String codeVersion, String author, String email, String desp,String payer,long gaslimit,long gasprice) 

```

| 파라미터      | 필드   | 유형  | 서술 |             설명 |
| ----- | ------- | ------ | ------------- | ----------- |
| 파라미터 입력 | codeHexStr| String | 계약 코드 16진수 문자열 | 필수 |
|        | needStorage    | Boolean | 저장 필요 여부   | 필수 |
|        | name    | String  | 이름       | 필수|
|        | codeVersion   | String | 버전       |  필수 |
|        | author   | String | 작가     | 필수 |
|        | email   | String | emal     | 필수 |
|        | desp   | String | 서술정보     | 필수 |
|        | VmType   | byte | 버추얼머신 유형     | 필수 |
|        | payer   | String | 트랜젝션 비용 지불계정 주소     | 필수 |
|        | gaslimit   | long | gaslimit    | 필수 |
|        | gasprice   | long | gas가격   | 필수 |
| 파라미터 송출 | tx   | Transaction  | 트랜젝션 예시  |  |

### 호출

#### NEO스마트 컨트랙트 호출

* 기본 프로세스

 1. 스마트 컨트랙트의 abi문서 불러오기
 2. 스마트 컨트랙트함수 호출 구성
 3. 트랜젝션 구성
 4. 트랜젝션 서명 (사전집행은 서명 불필요)
 5. 트랜젝션 전송

* 예시

```java
// 스마트 컨트랙트의 abi문서 불러오기
InputStream is = new FileInputStream("C:\\ZX\\NeoContract1.abi.json");
byte[] bys = new byte[is.available()];
is.read(bys);
is.close();
String abi = new String(bys);

//abi문서 해석
AbiInfo abiinfo = JSON.parseObject(abi, AbiInfo.class);
System.out.println("codeHash:"+abiinfo.getHash());
System.out.println("Entrypoint:"+abiinfo.getEntrypoint());
System.out.println("Functions:"+abiinfo.getFunctions());
System.out.println("Events"+abiinfo.getEvents());

//스마트 컨트랙트codeAddress설정
ontSdk.setCodeAddress(abiinfo.getHash());

//계정정보 획득
Identity did = ontSdk.getWalletMgr().getIdentitys().get(0);
AccountInfo info = ontSdk.getWalletMgr().getAccountInfo(did.ontid,"passwordtest");

//스마트 컨트랙트함수 구성
AbiFunction func = abiinfo.getFunction("AddAttribute");
System.out.println(func.getParameters());
func.setParamsValue(did.ontid.getBytes(),"key".getBytes(),"bytes".getBytes(),"values02".getBytes(),Helper.hexToBytes(info.pubkey));
System.out.println(func);
//사전집행
Object obj =  ontSdk.neovm().sendTransaction(Helper.reverse("872a56c4583570e46dde1346137b78fdb9fd3ce1"),null,null,0,0,func, true);
System.out.println(obj);
//집행
String hash = ontSdk.neovm().sendTransaction(Helper.reverse("872a56c4583570e46dde1346137b78fdb9fd3ce1"), acct1, acct1, 20060313, 500, func, true);
```

* AbiInfo구조(NEO컨트랙트 호출 시 필요함, WASM계약에는 불필요)

```java
public class AbiInfo {
    public String hash;
    public String entrypoint;
    public List<AbiFunction> functions;
    public List<AbiEvent> events;
}
public class AbiFunction {
    public String name;
    public String returntype;
    public List<Parameter> parameters;
}
public class Parameter {
    public String name;
    public String type;
    public String value;
}
```

#### WASM스마트 컨트랙트호출 – 현재는 WASM를 지원하지 않습니다. 

* 기본 프로세스
1. 계약 내에서 호출구성 방법으로 필요한 파라미터
2. 트랜젝션 구성
3. 트랜젝션 서명 (사전집행일 경우 서명 불필요)
4. 트랜젝션 전송

* 예시

```java
//설정 시 호출해야 하는 계약주소 codeAddress
ontSdk.getSmartcodeTx().setCodeAddress(codeAddress);
String funcName = "add";
//계약함수 구성에 필요한 파라미터
String params = ontSdk.vm().buildWasmContractJsonParam(new Object[]{20,30});
//버추얼머신 유형을 지정하여 트랜젝션 구성
Transaction tx = ontSdk.vm().makeInvokeCodeTransaction(ontSdk.getSmartcodeTx().getCodeAddress(),funcName,params.getBytes(),VmType.WASMVM.value(),payer,gas);
//트랜젝션 전송
ontSdk.getConnect().sendRawTransaction(tx.toHexString());
```

#### 스마트 컨트랙트 호출 예시

계약 내 방식
```c#

public static bool Transfer(byte[] from, byte[] to, object[] param)
{
    StorageContext context = Storage.CurrentContext;

    if (from.Length != 20 || to.Length != 20) return false;

    for (int i = 0; i < param.Length; i++)
    {

        TransferPair transfer = (TransferPair)param[i];
        byte[] hash = GetContractHash(transfer.Key);
        if (hash.Length != 20 || transfer.Amount < 0) throw new Exception();
        if (!TransferNEP5(from, to, hash, transfer.Amount)) throw new Exception();

    }
    return true;
}
struct TransferPair
{
     public string Key;
     public ulong Amount;
}
```

Java-SDK에서 Transfer함수를 호출하는 방법

분석: 계약에서 Transfer방법은 3가지 파라미터를 필요로 합니다. 2가지 파라미터 모두 바이트 유형의 파라미터이고, 마지막 하나의 파라미터는 대상체 배열입니다. 두 유형의 모든 요소구성에 대해서는 TransferPair를 통해 각 속성데이터 유형을 파악할 수 있습니다.  

```java
String functionName = "Transfer";
//Transfer구성에 필요한 pram배열
List list = new ArrayList();
List list2 = new ArrayList();
list2.add("Atoken");
list2.add(100);
list.add(list2);
List list3 = new ArrayList();
list3.add("Btoken");
list3.add(100);
list.add(list3);
//함수설정에 필요한 파라미터
func.setParamsValue(account999.getAddressU160().toArray(),Address.decodeBase58("AacHGsQVbTtbvSWkqZfvdKePLS6K659dgp").toArray(),list);
String txhash = ontSdk.neovm().sendTransaction(Helper.reverse("44f1f4ee6940b4f162d857411842f2d533892084"),acct,acct,20000,500,func,false);
Thread.sleep(6000);
System.out.println(ontSdk.getConnect().getSmartCodeEvent(tx.hash().toHexString()));
```



> 만일 전송결과에 대한 모니터링이 필요하면 다음 내용을 참고하십시오.

### 스마트 컨트랙트 이벤트 푸쉬

websocket스레드를 생성하고 전송결과를 해석합니다. 

#### 1. websocket링크 설정

```java
//lock글로벌 변수, 동기화 lock
public static Object lock = new Object();

//ont예시 획득
String ip = "http://127.0.0.1";
String wsUrl = ip + ":" + "20335";
OntSdk wm = OntSdk.getInstance();
wm.setWesocket(wsUrl, lock);
wm.setDefaultConnect(wm.getWebSocket());
wm.openWalletFile("OntAssetDemo.json");

```


#### 2. websocket스레드 실행


```java
//false는 미출력 콜백함수정보를 의미합니다. 
ontSdk.getWebSocket().startWebsocketThread(false);

```


#### 3. 결과처리 스레드 실행



```java
Thread thread = new Thread(
                    new Runnable() {
                        @Override
                        public void run() {
                            waitResult(lock);
                        }
                    });
            thread.start();
            //MSgqUeue중의 데이터에서 추출하여 인쇄
            public static void waitResult(Object lock) {
                    try {
                        synchronized (lock) {
                            while (true) {
                                lock.wait();
                                for (String e : MsgQueue.getResultSet()) {
                                    System.out.println("RECV: " + e);
                                    Result rt = JSON.parseObject(e, Result.class);
                                    //TODO
                                    MsgQueue.removeResult(e);
                                    if (rt.Action.equals("getblockbyheight")) {
                                        Block bb = Serializable.from(Helper.hexToBytes((String) rt.Result), Block.class);
                                        //System.out.println(bb.json());
                                    }
                                }
                            }
                        }
                    } catch (Exception e) {
                        e.printStackTrace();
                    }
                }
```


#### 4. 6초마다 Heartbeat 프로그램을 전송하여 socket링크를 유지합니다. 


```java
for (;;){
                    Map map = new HashMap();
                    if(i >0) {
                        map.put("SubscribeEvent", true);
                        map.put("SubscribeRawBlock", false);
                    }else{
                        map.put("SubscribeJsonBlock", false);
                        map.put("SubscribeRawBlock", true);
                    }
                    //System.out.println(map);
                    ontSdk.getWebSocket().setReqId(i);
                    ontSdk.getWebSocket().sendSubscribe(map);     
                Thread.sleep(6000);
            }
```


#### 5. 사례분석 전송


증명저장계약의 put함수호출을 예시로 들어보겠습니다. 

//증명저장계약 abi.jaon문서내용은 다음과 같습니다. 

```json
{
    "hash":"0x27f5ae9dd51499e7ac4fe6a5cc44526aff909669",
    "entrypoint":"Main",
    "functions":
    [

    ],
    "events":
    [
        {
            "name":"putRecord",
            "parameters":
            [
                {
                    "name":"arg1",
                    "type":"String"
                },
                {
                    "name":"arg2",
                    "type":"ByteArray"
                },
                {
                    "name":"arg3",
                    "type":"ByteArray"
                }
            ],
            "returntype":"Void"
        }
    ]
}
```


put함수를 호출하여 데이터를 저장할 때 putRecord시나리오를 유발하여 websocket이 전송하는 결과는 {"putRecord", "arg1", "arg2", "arg3"}의 16진수 문자열입니다.  

예시는 다음과 같습니다. 

```json
RECV: 
{
	"Action": "Log",
	"Desc": "SUCCESS",
	"Error": 0,
	"Result": {
		"Message": "Put",
		"TxHash": "8cb32f3a1817d88d8562fdc0097a0f9aa75a926625c6644dfc5417273ca7ed71",
		"ContractAddress": "80f6bff7645a84298a1a52aa3745f84dba6615cf"
	},
	"Version": "1.0.0"
}
RECV: {
      	"Action": "Notify",
      	"Desc": "SUCCESS",
      	"Error": 0,
      	"Result": [{
      		"States": ["7075745265636f7264", "507574", "6b6579", "7b2244617461223a7b22416c6772697468656d223a22534d32222c2248617368223a22222c2254657874223a2276616c75652d7465737431222c225369676e6174757265223a22227d2c2243416b6579223a22222c225365714e6f223a22222c2254696d657374616d70223a307d"],
      		"TxHash": "8cb32f3a1817d88d8562fdc0097a0f9aa75a926625c6644dfc5417273ca7ed71",
      		"ContractAddress": "80f6bff7645a84298a1a52aa3745f84dba6615cf"
      	}],
      	"Version": "1.0.0"
      }
```


## FAQ


* contractAdderss란?

contractAddress는 스마트 컨트랙트의 유일한 표식입니다. 

* contractAddress획득방법

```java
InputStream is = new FileInputStream("IdContract.avm");
byte[] bys = new byte[is.available()];
is.read(bys);
is.close();
code = Helper.toHexString(bys);
System.out.println("Code:" + Helper.toHexString(bys));
System.out.println("CodeAddress:" + Address.AddressFromVmCode(code).toHexString());
```

> Note: codeAddress획득 시, 해당 계약이 작동되는 버추얼머신을 설치해야합니다. 현재 지원하고 있는 버추얼머신은 NEO와 WASM입니다. 

* 스마트 컨트랙트 invokeTransaction을 호출하는 과정 및 sdk에서의 구체적인 역할

```java
//step1 : 트랜젝션 구성
//스마트 컨트랙트 파라미터를 vm이 식별 가능한 opcode로 변환해야 합니다. 
Transaction tx = ontSdk.vm().makeInvokeCodeTransaction(ontContractAddr, null, contract.toArray(), VmType.Native.value(), sender.toBase58(),gaslimit，gasprice);

//step2. 트랜젝션 서명
ontSdk.signTx(tx, info1.address, password);

//step3: 트랜젝션 전송
ontSdk.getConnectMgr().sendRawTransaction(tx.toHexString());
```

* invoke시 계정과 암호를 전송해야 하는 이유

스마트 컨트랙트을 호출할 때 유저서명이 필요합니다. 사전집행 시에는 서명이 필요하지 않습니다. 지갑에는 암호화된 유저 프라이빗키가 보관되어 있으며 암호가 있어야 프라이빗키를 해독하고 호출할 수 있습니다. 

* 자산을 조회할 때 스마트 컨트랙트의 사전집행은 무엇이며 어떻게 호출하나요?

스마트 컨트랙트 get과 관련된 작업은 스마트 컨트랙트 스토리지 공간에서 데이터를 처리하기 때문에 노드 컨센서스가 불필요하며, 해당 노드에서 작업하면 바로 결과가 전송됩니다. 트랜젝션 전송 시 집행인터페이스를 호출합니다.   
```java
String result = (String) sdk.getConnect().sendRawTransactionPreExec(txHex);
```
