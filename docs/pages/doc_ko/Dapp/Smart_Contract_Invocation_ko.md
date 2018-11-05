---
약itle:
keywords: sample homepage
sidebar: Dapp_ko
permalink: Smart_Contract_Invocation_ko.html
folder: doc_ko/Dapp
giturl: https://github.com/ontio/ontology-smartcontract/blob/master/smart-contract-tutorial/Smart_Contract_Invocation_cn.md
---

<h1 align="center">스마트 컨트랙트 호출</h1>

<p align="center" class="version">Version 1.0.0 </p>

[English](./Smart_Contract_Invocation_en.html) / 한국어
### 1. 트랜잭션 구성

스마트 컨트랙트가 블록체인에 배치되면 트랜잭션(Transaction) 방식를 호출하여 트랜잭션를 생성할 수 있습니다.

트랜잭션를 하기 전에, 계약abi파일과 계약 해쉬주소를 알아야합니다.

#### abi문서는 무엇인가요？

스마트 컨트랙트가 작성되면 개발자는 해당 컴파일러를 사용하여 계약을 컴파일합니다. 컴파일이 끝나면 스마트 컨트랙트의 abi파일과 avm파일이 생성됩니다. avm파일은 계약 바이트코드입니다. 계약이 블록체인에 배포되면 계약 바이트 코드는 계약서에 할당 된 저장 영역에 저장됩니다. abi파일은 엔트리 함수, 인터페이스 함수, 함수의 매개변수 리스트, 리턴 값 및 이벤트를 포함하는 계약서의 특정 구조를 설명하는 JSON파일입니다. 계약서의 abi파일에서 계약의 세부기능을 배울 수 있습니다.


예를 들어 SmartX를 사용하면 간단한 가산 계산을 할 수 있는 템플릿 계약 “Arith”가 있습니다. 계약서가 컴파일 된 후 JSON 형식의 abi 컨텐츠가 조작 패널에 표시됩니다. 사용자는 abi.json파일 다운로드를 선택 할 수 있습니다.


![](https://upload-images.jianshu.io/upload_images/150344-297f0b59eb7b3e94.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

#### 계약해시 란？

계약해시는 계약 avm내용에 대해 일부 해시 연산작업을 수행하여 얻은 값을 말합니다. 이 값은 다른 계약을 구별하는데 사용됩니다. Abi파일은 해시값도 포함됩니다

#### SDK를 통한 트랜잭션구성

SDK를 통해 트랜잭션를 쉽게 할 수 있습니다. TS SDK를 예로 들어, 우리가 호출하고자 하는 계약은 템플릿 계약 “Arith”이며, 호출될 방식는 “Add”기능의 계약입니다.

````
import {Parameter, ParameterType, Address, reverseHex, TransactionBuilder} from 'Ont'
//set the function name to call
const funcName = 'Add';

//set the parameters of the function
const p1 = new Parameter('a', ParameterType.Integer, 1);
const p2 = new Parameter('b', ParameterType.Integer, 2);

//set the contract address
const contractAddr = new Address(reverseHex('c76d6764213612597cb815b6d444047e91a576bd));

//set gasPrice and gasLimit
const gasPrice = '0';
const gasLimit = '30000';

//make transaction
const tx = TransactionBuilder.makeInvokeTransaction(funcName, [p1, p2], contractAddr, gasPrice, gasLimit)
````

위 단계에 따라 수행하여 트랜잭션객체를 생성합니다. 구축 프로세스가 진행되는 동안 메서드 이름과 매개변수가 abi파일의 설명과 일치하여야합니다. 그렇지 않을경우 오류가 발생합니다.

일반적으로 스마트 컨트랙트를 실행하기 위해서는 gas가 필요합니다. 따라서 gasPrice와 gasLimit를 트랜잭션객체에 설정해야합니다. 이 두 값이 너무 작으면 계약이 블록에 채워지지 않고, 계약이 실행되지 않습니다. TestNet에서 gasPrice를 0으로 설정할 수 있어 gas를 소비할 필요가 없습니다. 메인넷에서는 일정량의 gas가 있어야 계약이 진행됩니다.

정확한 트랜잭션에는 서명이 있어야 블록체인으로 배포됩니다. 계정과 프라이빗 키를 가지고 있다고 가정합니다.

````
//assume we have an account and the private key
tx.payer = account.address;
signTransaction(tx, privateKey);
````

이제 서명된 트랜잭션를 블록체인에서 실행 할 수 있습니다.

### 2. 트랜잭션발송

여러가지 방법으로 트랜잭션를 보낼 수 있습니다：

1. rpc 인터페이스를 통해 트랜잭션 보내기
2. restful인터페이스를 통해 트랜잭션 보내기
3. websocket를 통해 트랜잭션보내기

rpc와 restful 인터페이스를 통해 트랜잭션를 발송하면 트랜잭션 상태 및 트랜잭션 해시가 리턴됩니다.；

websocket를 통해 트랜잭션을 보내면 계약 방식에 이벤트 푸쉬가 있는 경우 일반적으로 계약 실행 성공의 결과를 모니터링 할 수 있습니다.  

위의 인터페이스를 통해 사전 실행된 트랜잭션를 체인에 보낼 수도 있습니다. 사전 실행은 이 트랜잭션을 받은 노드에서만 트랜잭션가 실행된다는 것을 의미하며 블록체인의 컨센서스 후 실행의 결과를 얻을 때까지 기다리지 않아도 됩니다. 트랜잭션를 미리 실행함으로서 트랜잭션가 올바르게 구성되었는지 확인 할 수 있으며 소비될 gas를 예측할 수 있습니다.

TS SDK의 restful 인터페이스를 예로들어 트랜잭션를 어떻게 간단히 보내는지 설명해드리겠습니다.

````
import {RestClient} from 'Ont'
//construct the restful client
const client = new RestClient();

//we use the tx made in last step
client.sendRawTransaction(tx.serialize()).then(res => {
	//here is the result
    console.log(res);
})
````

### 3. 트랜잭션결과 받기

위에서 우리는 
restful 인터페이스를 통해 트랜잭션를 블록체인에 보냈습니다. 결과는 아래와 같습니다：

````
{
	"Action": "sendrawtransaction",
	"Desc": "SUCCESS",
	"Error": 0,
	"Id": null,
	"Result": "886b2cd35af7ea65e502077b70966652f4cf281244868814b8f3b2cf82776214",
	"Version": "1.0.0"
}
````

결과 필드의 값은 트랜잭션 해시입니다. 우리는 
restful 인터페이스를 통해 트랜잭션 실행 결과를 찾아볼 수 있습니다.

````
import {RestClient} from 'Ont'
const client = new RestClient();
client.getSmartCodeEvent('886b2cd35af7ea65e502077b70966652f4cf281244868814b8f3b2cf82776214').then(res => {
    console.log(res)
})
````

TS SDK의 안정적인 스를 통해 트랜잭션 결과를 찾아 볼 수 있고, postman등 네트워크를 통해 쿼리 할 수 있습니다. 검색된 url은 다음과 같습니다：

````
http://{{NODE_URL}}/api/v1/smartcode/event/txhash/03295a1b38573f3a40cf75ae2bdda7e7fb5536f067ff5e47de44aeaf5447259b
````

여기의 NODE_URL은 TestNet의 노드 혹은 로컬 노드일 수 있습니다.

쿼리 결과는 다음과 같습니다：

````
{
    "Action": "getsmartcodeeventbyhash",
    "Desc": "SUCCESS",
    "Error": 0,
    "Result": {
        "TxHash": "03295a1b38573f3a40cf75ae2bdda7e7fb5536f067ff5e47de44aeaf5447259b",
        "State": 1,
        "GasConsumed": 0,
        "Notify": [
            {
                "ContractAddress": "bd76a5917e0444d4b615b87c5912362164676dc7",
                "States": [
                    "02"
                ]
            }
        ]
    },
    "Version": "1.0.0"
}
````

결과에 있는 데이터를 통해 트랜잭션의 실행 성공 여부를 확인 할 수 있습니다.

```State``` 1은 실행 성공; 0은 실패

````Notify```` 은 계약 식 실행시 메시지 푸시입니다.
