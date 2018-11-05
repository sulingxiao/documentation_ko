---
값itle:
keywords: sample homepage
sidebar: SDKs_ko
permalink: ontology_ts_sdk_smartcontract_ko.html
folder: doc_ko/SDKs
giturl: https://github.com/ontio/ontology-ts-sdk/blob/master/docs/cn/smart_contract.md
---

[English](./ontology_ts_sdk_smartcontract_en.html) / 한국어

<h1 align="center">스마트 컨트랙트의 설치 및 호출</h1>
<p align="center" class="version">Version 0.7.0 </p>

## 1. 계약설치

계약을 설치하려면 상응하는 트랜젝션을 생성하고 체인에 전송하여 실행해야 합니다. 

계약을 구성하려면 계약내용의 16진수 문자열 및 설정 파라미터를 제공해야 합니다. 

계약내용은 일반적으로 .avm문서(NEO버추얼머신과 호응) 및 .wasm문서(WASM버추얼머신과 호응)로 제공합니다. 

설정 파라미터는 다음과 같습니다.

```code```계약내용은 16진수 문자열입니다. 

```vmType```은 버추얼머신이며 현재 함수옵션은 다음과 같습니다.


```
export enum VmType {
    NativeVM = 0xFF,
    NEOVM    = 0x80,
    WASMVM   = 0x90
}
```

```name```은 일반 문자열로 된 계약 명칭이며 옵션값 입니다.
 
```codeVersion```은 일반 문자열로 된 계약 버전이며 옵션값 입니다. 

```author```은 일반 문자열로 된 계약작성자이며 옵션값 입니다.

```email```은 일반 문자열로 된 계약작성자의 메일주소이며 옵션값 입니다.

```desp```는 일반 문자열로 된 계약서술이며 옵션값 입니다.

```needStorage```는 저장필요의 여부이며 불리언 및 옵션값 입니다. 

````
import * as core from '../src/core'

//계약 내용 획득
var fs = require('fs')
var path = require('path')
let idContractAvm = fs.readFileSync(path.join(__dirname, '../src/smartcontract/data/IdContract.avm'))
var idContractAvmCode = ab2hexstring(idContractAvm)
var name = 'test',
	codeVersion = '1.0',
	author = 'alice',
	email = '',
	desp = '',
	needStorage = true;
//트랜젝션 구성	
var tx = core.makeDeployCodeTransaction(idContractAvmCode, VmType.NEOVm, name, codeVersion, author, email, desp, needStorage)
//트랜젝션 보내기
var param = buildRestfulParam(tx)
var url = TEST_ONT_URL.sendRawTxByRestful
axios.post(url, param).then((res:any)=> {
	console.log('deploy res: '+ JSON.stringify(res.data))
	//6초 후 설치 결과 조회.
    setTimeout(function () {
    	getContract(code, vmType)
    }, 6000)
    }).catch(err => {
   	   console.log('err: '+ err)
})

//체인에서의 계약 조회
const getContract = (avmCode, vmType=VmType.NEOVM) => {
    const codeHash = Address.fromVmCode(avmCode,vmType).serialize()
    let url = `${TEST_ONT_URL.REST_URL}/api/v1/contract/${codeHash}`
    console.log('url : '+ url)
    axios.get(url).then((res)=>{
        console.log(res.data)
    }).catch(err => {
        console.log(err)
    })
}
````

## 2. 계약호출

계약은 반드시 설치완료 후에 호출할 수 있습니다. 계약을 호출하려면 상응하는 트랜젝션을 생성하고 체인에 전송하여 실행해야 합니다. 

### 2.1 abi문서를 통한 트랜젝션 구성

일반적인 스마트 컨트랙트은 상응하는 .avm문서와 .abi문서(NEO버추얼머신과 호응)로 컴파일될 수 있습니다. .abi 문서를 읽음으로써 스마트컨트랙트의 트랜잭션을 편리하게 구성 호출할 수 있습니다. .abi문서를 통해 편리하게 계약 트랜젝션을 호출할 수 있습니다. 구성된 트랜젝션은 유저 프라이빗서명이 필요할 수도 있습니다.      

TS SDK에서 Abi와 관련된 클래스는 AbiInfo, AbiFunction, Parameter등이 있습니다. 

```
class AbiInfo {
    hash : string
    entrypoint : string
    functions : Array<AbiFunction>
}

```

```hash```는 스마트 컨트랙트의 hash함수입니다. 계약주소라고도 불리며 각각의 계약을 구분하는데 사용합니다. 

```entrypoint```은 계약의 엔터함수명입니다.

```functions```은 계약이 제공하는 함수집합입니다. 

````
class AbiFunction {
    name : string
    returntype : string
    parameters : Array<Parameter>
}
````

```name```은 함수명칭입니다. 

```returntype```은 함수리턴값값 유형입니다. 

```parameters```는 함수 파라미터 리스트입니다.

````
class Parameter {
    name : string
    type : ParameterType
    value : any
}
````

```name```은 파라미터 명칭입니다. 

```type```는 파라미터 유형입니다. 

```value```는 파라미터값 입니다. 

계약abi파일을 읽고 해당 객체를 생성하면 트랜잭션을 빌드하는데 필요한 함수이름, 파라미터 및 계약 해시를 제공하는 것이 편리합니다.

트랜잭션을 빌드하는 함수에 필요한 파라미터는 아래와 같습니다.

```
function makeInvokeTransaction(funcName : string, parameters : Array<Parameter>, contractHash : string, vmType : VmType = VmType.NEOVM, fees : Array<Fee> = [])
```

```funcName```은 호출한 계약의 함수명칭입니다. 

```parameters```는 함수의 파라미터 대상 리스트입니다. 

```contractHash```는 계약 hash입니다. 

```vmType```는 버추얼머신 유형입니다. 

```fees```는 트랜젝션 전송 비용입니다. 

다음은 ONT ID스마트 컨트랙트에서 ONT ID를 등록하는 방식을 예시로 전체 과정을 설명한 내용입니다. 

````
//abi문서 읽기. 이 문서는 JSON을 불러옵니다.
import abiJson from '../smartcontract/data/idContract.abi'
//abi내용 해석
const abiInfo = AbiInfo.parseJson(JSON.stringify(abiJson))

//AbiFunction읽기

const abiInfo = AbiInfo.parseJson(JSON.stringify(abiJson))

//AbiFunction읽기
const abiFunction = abiInfo.getFunction('RegIdWithPublicKey')

const privateKey = '7c47df9664e7db85c1308c080f398400cb24283f5d922e76b478b5429e821b95'
const publicKey = '1202037fa2bbad721197da4f2882e4f8e4ef6a77dbc7cfbd72267cdd72dd60ac30b41e'
const ontid = '6469643a6f6e743a544d7876617353794747486e7574674d67657158443556713265706b6a476f737951'

//파라미터 구성. 이 파라미터 유형은 16진수 문자열인 ByteArray입니다.
let p1 = new Parameter(f.parameters[0].getName(), ParameterType.ByteArray, ontid)
let p2 = new Parameter(f.parameters[1].getName(), ParameterType.ByteArray, publicKey)

//파라미터 설정
abiFunction.setParasValue(p1, p2)

//트랜젝션 대상 구성
let fees = []
let vmType = VmType.NEOVM
let tx = makeInvokeTransaction(abiFunction.name, abiFunction.parameters, abiInfo.hash, vmType, fees)

//트랜젝션 서명. 현재 최종적으로 구성된 트랜젝션을 얻을 수 있습니다. 
signTransaction(tx, privateKey)
````

### 2.2 WASM기반 계약의 트랜잭션 구성

WASM기반 버추얼머신의 스마트컨트랙트는 계약 컴파일러에 abi문서가 생성되지 않습니다. 하지만 정확한 함수명과 파라미터 대상에 대해 동일한 방법으로 트랜젝션을 구성할 수 있습니다. 

````
const codeHash = '9007be541a1aef3d566aa219a74ef16e71644715'
const params = [
		new Parameter('p1', ParameterType.Int, 20), 
		new Parameter('p2', ParameterType.Int, 30)
	]
const funcName = 'add'
let tx = makeInvokeTransaction(funcName, params, codeHash, VmType.WASMVM)
````

### 2.3 트랜젝션 전송

다양한 방식으로 트랜젝션을 체인에 전송하여 실행할 수 있습니다. 

#### 2.3.1 Restful

```
//팩킹대상을 이용하여 요청 전송
let restClient = new RestClient()
//사용 이전에 트랜젝션 대상을 이용하여 요청 파라미터 생성 
restClient.sendRawTransaction(tx.serialize()).then(res => {
    console.log(res)
})
```

요청은 해당 트랜젝션hash를 리턴하고 트랜젝션hash를 통해 계약의 실행결과를 조회해야 합니다. 비교적 간단한 방법은 온톨지의 블록체인 브라우저에서 조회하는 것입니다. 

#### 2.3.2 Rpc

````
let rpcClient = new RpcClient()
rpcClient.sendRawTransaction(tx.serialize()).then(res => {
    console.log(res)
})
````

Rpc요청과 Restful은 유사하며 리턴결과는 모두 트랜젝션 hash입니다. 

#### 2.3.3 Websocket

Websocket을 통해 요청을 전송하면 백그라운드에서 푸시한 정보를 모니터링 할 수 있습니다. 만약 계약에 시나리오 푸시가 명시되어 있으면 계약방식 호출 후 상응하는 푸시 정보가 나타납니다. 

````
//요청 파라미터 구성
let param = buildTxParam(tx)
let txSender = new TxSender(TEST_ONT_URL.SOCKET_URL)
//콜백 파라미터 정의
//@param err 결과오류 
//@param res 모니터링한 정보
//@param socket websocket대상
const callback = (err, res, socket) => {
    if(err) {
        console.log(err)
        socket.close()
        return;
    }
    if(res.Action === 'Notify') {
        console.log('Notify: '+ JSON.stringify(res))
        socket.close()
    }
}
txSender.sendTxWithSocket(param, callback)
````















