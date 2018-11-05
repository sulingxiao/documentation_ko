---
title:
keywords: sample homepage
sidebar: Dapp_ko
permalink: Smart_Contract_Deployment_ko.html
folder: doc_ko/Dapp
giturl: https://github.com/ontio/ontology-smartcontract/blob/master/smart-contract-tutorial/Smart_Contract_Deployment_cn.md
---

<h1 align="center">스마트 컨트랙트 배치</h1>

<p align="center" class="version">Version 1.0.0 </p>

[English](./Smart_Contract_Deployment_en.html) / 한국어

## 준비작업

컨트랙트을 배치하기 전에 개발자는 배치할 컨트랙트을 준비해야 합니다. 또한 이 컨트랙트은 정확히 .avm 문서로 컴파일 되어있어야 합니다.

개발자는 Ontology의 지갑계정이 있어야 하며 계정에 충분한 ONG를 보유하여 컨트랙트 배치 비용을 지불할 수 있도록 해야 합니다.  

현재, 테스트넷에서 컨트랙트을 배치할 때 비용을 0으로 설정할 수 있습니다. 

## 컨트랙트배치방법

컨트랙트을 배치하려면 유저가 특정 트랜젝션을 생성하고 블록체인에 전송하여 실행해야 합니다. 트랜젝션 집행이 완료된 후 컨트랙트배치가 완료됩니다.  

Ontology는 서로 다른 SDK와 컨트랙트개발도구인 SmartX를 제공하여 유저가 편리하게 컨트랙트을 배치할 수 있도록 합니다.   

## 컨트랙트배치에 필요한 gas지불계획

스마트 컨트랙트의 행을 통해 현재 컨트랙트 집행에 필요한 `Gaslimit`을 획득하여 스마트 컨트랙트의 실제집행 시의 `Gaslimit`설정에 참고할 수 있도록 하고 있습니다. 이를 통해 **ONG**잔액부족으로 인한 집행실패를 방지할 수 있습니다.

```
$ ./ontology contract invoke --address 79102ef95cf75cab8e7f9a583c9d2b2b16fbce70 --params string:Hello,[string:Hello] --prepare --return bool
Invoke:70cefb162b2b9d3c589a7f8eab5cf75cf92e1079 Params:["Hello",["Hello"]]
Contract invoke successfully
  Gaslimit:20000
  Return:true
```

## SDK를 통한 컨트랙트배치

Ontology는 각각의 SDK를 제공합니다. 여기에서는 [Typescript SDK](https://github.com/ontio/ontology-ts-sdk)를 예로 들어 컨트랙트배치과정을 설명하겠습니다.  

Ts sdk는 컨트랙트배치 인터페이스를 제공합니다. 해당 인터페이스 파라미터는 다음과 같습니다. 

```avmCode``` 컨트랙트의 avm code. 필수값.

```name``` 컨트랙트 명칭. 옵션값. 디폴트는 공백 문자열.

```version``` 컨트랙트 버전. 옵션값. 디폴트는 공백 문자열.

```author``` 컨트랙트 작성자. 옵션값. 디폴트는 공백 문자열.

```email``` 컨트랙트 작성자의 메일. 옵션값. 디폴트는 공백 문자열.

```desc``` 컨트랙트에 대한 서술. 옵션값. 디폴트는 공백 문자열.

```needStorage``` 컨트랙트 저장필요 여부. 옵션값. 디폴트는 true.

```gasPrice``` 컨트랙트배치 시 지불하는 gas price. 필수값. 해당 값이 너무 작으면 트랜젝션 집행이 실패할 수 있음. 

```gasLimit``` 컨트랙트배치 시 지불하는 gas limit. 필수값. 해당 값이 너무 작으면 트랜젝션 집행이 실패할 수 있음.

```payer``` 배치비용을 지불하는 계정주소. 필수값.

````
import { makeDeployCodeTransaction } from 'Ont'
const avmCode = '5ac56b6c766b00527ac46c766b51527ac4616c766b00c.........';
const name = 'test_contract';
const version = '1.0';
const author = 'Alice';
const email = 'alice@onchain.com'
const desc  = 'a test contract';
const needStorage = true;
const gasPrice = '0'; //set 0 for test in testnet
const gasLimit = '30000000'; // should be big enough
const payer = new Address('AazEvfQPcQ2GEFFPLF1ZLwQ7K5jDn81hve')
const privateKey = new PrivateKey('75de8489fcb2dcaf2ef3cd607feffde18789de7da129b5e97c81e001793cb7cf')
// construct transaction for deploy
const tx = makeDeployCodeTransaction(avmCode, name, version, author, email, desc, needStorage, gasPrice, gasLimit, payer);
//sign transaction with privateKey
signTransaction(tx, privateKey)
````

상기 절차에 따라 트랜젝션 대상 구성 후, 트랜젝션을 블록체인에 전송해야 합니다. 여러 가지 방식으로 트랜젝션을 전송할 수 있습니다. 자세한 정보는 [스마트 컨트랙트호출]()을 참고하십시오.    

여기에서는 TS SDK의 방법을 예로 들어 트랜젝션 전송 과정을 살펴보겠습니다. 

````
import { RestClient } from 'Ont''
const restClient = new RestClient('http://polaris1.ont.io');
restClient.sendRawTransaction(tx.serialize()).then(res => {
    console.log(res);
})
````

해당 요청이 리턴한 결과는 다음과 비슷합니다. 

````
{
	"Action": "sendrawtransaction",
	"Desc": "SUCCESS",
	"Error": 0,
	"Result": "70b81e1594afef4bb0131602922c28f47273e1103e389441a2e18ead344f4bd0",
	"Version": "1.0.0"
}
````

```Result```는 해당 회차 트랜잭션의 hash 입니다. 트랜젝션의 집행 성공여부를 조회하는데 사용할 수 있으며 만약 집행성공이면 컨트랙트배치가 완료되었음을 의미합니다. 

restful인터페이스를 통해서도 트랜젝션의 집행결과를 조회할 수 있습니다.

````
restClient.getSmartCodeEvent('70b81e1594afef4bb0131602922c28f47273e1103e389441a2e18ead344f4bd0').then(res => {
    console.log(res);
})
````

해당 요청이 리턴한 결과는 다음과 비슷합니다. 

````
{
    "Action": "getsmartcodeeventbyhash",
    "Desc": "SUCCESS",
    "Error": 0,
    "Result": {
        "TxHash": "70b81e1594afef4bb0131602922c28f47273e1103e389441a2e18ead344f4bd0",
        "State": 1,
        "GasConsumed": 0,
        "Notify": []
    },
    "Version": "1.0.0"
}
````

```State```값이 1일때는 트랜젝션 집행에 성공하여 컨트랙트배치가 완료되었음을 의미합니다. 값이 0일때는 트랜젝션 집행 실패로 컨트랙트배치 역시 실패하였음을 의미합니다. 

또한 컨트랙트hash값을 통해서도 체인상에 컨트랙트 존재여부를 조회할 수 있습니다. 컨트랙트hash값은 컨트랙트 avm내용에 따른 hash연산으로 구할 수 있습니다. 

컨트랙트hash값이 **bcb08a0977ed986612c29cc9a7cbf92c6bd66d86**일 경우.

````
restClient.getContract('bcb08a0977ed986612c29cc9a7cbf92c6bd66d86').then(res => {
    console.log(res)
})
````

만약 해당 요청이 avm내용을 리턴했다면 컨트랙트이 성공적으로 체인에 배치되었음을 의미합니다. 

## SmartX를 통한 컨트랙트 배치 

 [SmartX](http://smartx.ont.io)는 스마트 컨트랙트 프로그래밍, 배치 및 호출에 대한 개발자용 원스톱 도구입니다. 구체적인 사용방법은 [smart문서](https://github.com/ontio/ontology-smartcontract/blob/master/smart-contract-tutorial/SmartX_Tutorial_cn.md)을 참고하십시오.  

![](https://upload-images.jianshu.io/upload_images/150344-1186fa3b18f9752f.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

먼저 smartx에서 작성한 컨트랙트을 컴파일해야 합니다. 컨트랙트 컴파일 완료 후에는 배치할 컨트랙트을 선택합니다.  

![](https://upload-images.jianshu.io/upload_images/150344-5f94d283e690512d.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

테스트넷 환경 선택 시 SmartX는 디폴트 계정을 제공하여 컨트랙트배치 비용지불 및 트랜젝션 서명 시에 사용할 수 있습니다. 

‘배치’버튼을 클릭하면 SmartX가 손쉽게 컨트랙트을 배치합니다.

