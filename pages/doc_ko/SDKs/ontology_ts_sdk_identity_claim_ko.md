---
title:
keywords: sample homepage
sidebar: SDKs_ko
permalink: ontology_ts_sdk_identity_claim_ko.html
folder: doc_ko/SDKs
giturl: https://github.com/ontio/ontology-ts-sdk/blob/master/docs/cn/identity_claim.md
---

[English](./ontology_ts_sdk_identity_claim_en.html) / 한국어

<h1 align="center">디지털신분과 신뢰성명</h1>
<p align="center" class="version">Version 0.7.0 </p>

## 1.1 신분 생성하기

ONT ID는 탈중앙화식 신원 인증으로, 유저의 각종 디지털신분 인증을 관리할 수 있습니다. 디지털신분(Identity)는 ONT SDK가 도출해낸 코어 클래스이며 해당 클래스는 신분을 대표하는 ONT ID속성을 포함합니다. 

> 디지털신분에 관한 자세한 정보는 [ONT TS SDk]의 관련정보를 확인하십시오. 

SDK를 통해 신분을 생성할 수 있습니다. 신분 생성 과정에서 유저 프라이빗키를 기반으로 ONT ID를 생성합니다.  

> ONT ID 규정은 [ONT ID생성규정]을 참고하십시오. (./ontology_ts_sdk_identity_claim_ko.html#1.1_ONT_ID生成)

신분생성에 필요한 파라미터는 다음과 같습니다:

**privateKey**유저 프라이빗키는 SDK가 제공하는 방식으로 안전하게 프라이빗키를 생성할 수 있습니다.

**password**는 프라이빗키의 암호화 및 암호해독에 사용됩니다. 

**label**는 신분의 명칭입니다. 

**params**는 프라이빗키 암호화 알고리즘 파라미터 입니다. 옵션파라미터이며 디폴트 값은 다음과 같습니다. 

```
{
    cost: 4096,
    blockSize: 8,
    parallel: 8,
    size: 64
}
```

```
import {Identity, Crypto} from 'ontology-ts-sdk';
//generate a random private key
const privateKey = Crypto.PrivateKey.random();

var identity = Identity.create(privateKey, password, label)
console.log(identity.ontid)
```

## 1.2 ONT ID를  체인에 등록하기

신분생성 후에 신분의 ONT ID를 체인에 등록해야 완전한 신분생성이 완료됩니다. 

ONT ID 체인 전송은 트랜젝션을 전송하는 과정이 필요합니다. SDK를 호출하여 제공하는 방식을 통해 트랜젝션 대상을 구성할 수 있습니다.  

전형적인 시나리오는 갓 생성한 ONT ID와 유저 프라이빗키로 트랜젝션 대상을 구성하는 것입니다. 

### 트랜젝션 구성
````
import {OntidContract} from 'ontology-ts-sdk';
import {TransactionBuilder} from 'ontology-ts-sdk'

//suppose we already got a identity
const did = identity.ontid;
//we need the public key, which can be generate from private key
const pk = privateKey.getPublicKey();
const gasPrice = '0';
const gasLimit = '20000;
const tx = OntidContract.buildRegisterOntidTx(did, pk, gasPrice, gasLimit);
Transaction.signTransaction(tx, privateKey);
````

해당 방법으로 리턴되는 것은 트랜젝션 대상의 직렬화된 파라미터이고 그 후엔 해당 파라미터를 전송하며 Websocket 또는 http로 요청하는 방식으로 전송됩니다. websocket을 예로 들어보면 이렇게 해야만 체인상에서 푸시하여 돌아오는 정보를 모니터링하여 ONT ID가 체인에 성공적으로 등록되었는지 확인할 수 있습니다.   

### 트랜젝션 Payer서명 추가하기

ONT ID를 체인에 등록하는 트랜잭션은 수수료가 부과되므로 트랜젝션에 하나의 Payer를 지정하고 Payer서명을 추가해야 합니다. 

````
import {TransactionBuilder} from 'ontology-ts-sdk'
//we also need an account to pay for the gas
//supporse we have an account and the privateKey
tx.payer = account.address
//Then sign the transaction with payer's account
//we already got transaction created before,add the signature.
TransactionBuilder.addSign(tx, privateKeyOfAccount)
````

### 트랜젝션 전송
Websocket, Restful및RPC등 다양한 트랜젝션 전송방식이 있습니다. 여기에서는 Restful를 예로 들어보겠습니다. 먼저 트랜젝션을 전송할 노드를 지정합니다. 만약 지정하지 않으면 디폴트가 테스트 넷에 전송됩니다. 

````
import {RestClient, CONST} from 'ontology-ts-sdk'

const rest = new RestClient(CONST.TEST_ONT_URL.REST_URL);
rest.sendRawTransaction(tx.serialize()).then(res => {
    console.log(res)
})
````

리턴 결과는 다음과 같습니다. 

````
{ Action: 'sendrawtransaction',
  Desc: 'SUCCESS',
  Error: 0,
  Result: 'dfc598649e0f3d9ff94486a80020a2775e1d474b843255f8680a3ac862c58741',
  Version: '1.0.0' }
````

만일 성공적으로 결과가 리턴 되었으면 ONT ID가 체인에 잘 등록되었음을 의미합니다. 따라서 인터페이스 조회체인에서 ONT ID와 관련된 정보를 조회할 수 있습니다.   

### 1.3 DDO조회

###트랜젝션 생성

````
import {OntidContract} from 'ontology-ts-sdk';
//we use identity's ONT ID to create the transaction
const tx = OntidContract.buildGetDDOTx(identity.ontid)
````
###트랜젝션 전송
트랜젝션 조회에는 gas가 부과되지 않고 또한 payer 및 payer서명도 지정할 필요가 없습니다. 트랜젝션 전송방식의 두 번째 파라미터는 전송된 것이 행의 파라미터인지 여부를 나타냅니다. 사전집행 트랜젝션은 수신된 노드에서만 처리되므로 컨센서스를 기다릴 필요가 없습니다. 일반적으로 데이터를 조회하는데 사용됩니다.  
````
import {RestClient} from 'ontology-ts-sdk';
const rest = new RestClient();
rest.sendRawTransaction(tx, true).then(res => {
    console.log(res);
}
````
리턴 결과는 다음과 같습니다. 

````
{ Action: 'sendrawtransaction',
      Desc: 'SUCCESS',
      Error: 0,
      Result:
       { State: 1,
         Gas: 20000,
         Result: '26010000002103547c5abdbe66677ba7001cefd773f01a19c6360b15ee51c6db43911f046564fc0000' },
      Version: '1.0.0' }
      
````
**Result**는 직렬화된 DDO입니다. (ONT ID object)
반직렬화로 상세 데이터를 얻을 수 있습니다.

````
const ddo = DDO.deserialize(response.Result.Result);
console.log(ddo);
````

## 2. 서명 신뢰성명 

유저는 각기 다른 여러 신분을 가지고 있을 수 있습니다. 예를 들어 정부기관에서 발급한 신분증을 가진 유저는 모두 국가시민이라는 신분을 가지고 있고, 또 일상생활 속 다양한 상황에서 자신의 신분증을 제시하여 신분을 증명할 수 있습니다. 신분증은 바로 국민신분에 대한 정부기관의 보증서 입니다.   

또 다른 예시로, 어느 대학 졸업생은 모교대학의 졸업생 신분을 가지게 됩니다. 이 신분은 학교에서 발급한 졸업증명서로 증명할 수 있습니다. 현재는 새로운 방식으로 졸업생의 신분을 증명하는데, 바로 블록체인 기술입니다. 신뢰성명과 유저의 ONT ID를 연동하는 것입니다. 마찬가지로 유저는 여러 기관 서 각기 다른 신뢰성명을 얻을 수 있습니다.  

> 모든 ONT ID 소유자(Owner)는 모두 자신 또는 타인에게 신뢰성명을 발급할 수 있습니다. 

> 정부기관, 대학교, 은행, 에스크로(CA등), 생체인식기술 등 모두 특정한 파트너쉽으로 온톨로지 생태계에 들어와 현실적인 신뢰기관이 될 수 있습니다. 만약 당신이 인증서비스 협력파트너가 될 수 있다면 [인증서비스 파트너쉽 접근기준]을 참고하십시오. (./verification_provider_specification_ko.html)。

중국 복단대학에서 발급한 디지털 졸업증명서를 예로 들어 제3자가 유저에게 부여하는 신분성명을 어떻게 획득하는지 알아보겠습니다.  

Alice는 복단대학의 학생이고 학교에 디지털 졸업증명서를 요청했다고 가정했을 때, 학교에서 Alice의 신분을 검증한 후 SDK의 api를 호출하여 신뢰성명을 생성합니다. 해당 성명에는 Alice의 졸업정보가 들어있고 학교의 프라이빗키로 성명에 서명 합니다.  

### 신뢰성명 생성

````
import {Claim} from 'ontology-ts-sdk';

const signature = null;
const useProof = false;
const claim = new Claim({
	messageId: '1',
	issuer: 'did:ont:AJTMXN8LQEFv3yg8cYKWGWPbkz9KEB36EM',
	subject: 'did:ont:AUEKhXNsoAT27HJwwqFGbpRy8QLHUMBMPz',
	issueAt: 1525800823
}, signature, useProof);

claim.version = '0.7.0';
claim.context = 'https://example.com/template/v1';
claim.content = {
	Name: 'Alice',
	Age: '22',
	......
};

````

신뢰성명의 속성은 다음과 같습니다. 

**signature**는 신뢰성명의 서명입니다. 신뢰성명을 검증할 때 사용되며 초기화 시에는 공백상태 입니다. 

**useProof**은 성명 직렬화 규정을 JWT또는JWT-X로 지정합니다. True는 JWT-X를 사용했음을 의미합니다.  

**messageId**는 성명 ID입니다. 

**issuer**는 성명 발급자의 ONT입니다. 

**subject**는 성명 수신자의 ONT ID입니다. 

**issueAt**는 성명이 생성된 시간입니다. 타임스탬프로 표기합니다. 

**version**는 성명 버전입니다. 

**context**는 성명템플릿이 온라인으로 저장한 url입니다. 

**content**은 JSON서식의 성명내용입니다. 

## 체인상 증명저장 신뢰성명
성명 발급자는 성명을 체인에 저장할 수 있습니다.
방법 파라미터는 다음과 같습니다. 

**url**은 트랜젝션이 전송된 노드의 websocket url입니다. 

**privateKey**는 발급자의 프라이빗키이며 트랜젝션 서명에 사용됩니다. 

**gasPrice**는 트랜젝션의 gas price입니다. 

**gasLimit**는 트랜젝션의 gas limit입니다. 

**payer**는 트랜젝션 gas의 payer입니다. 

````
const url = 'http://polaris1.ont.io:20335';
const gasPrice = '500';
const gasLimit = '20000';
const payer = new Address('AMLn5W7rz1sYd1hGpuQUfsnmUuUco22pM8');
const privateKey = new PrivateKey('44fd06de5a6529f3563aad874fb6c8240....')
const result = await claim.attest(url, gasPrice, gasLimit, payer, privateKey);
````
리턴결과가 ture면 성명저장 완료를 의미합니다. 

### 2.3 성명 철회
성명 발급자는 신뢰성명을 철회할 수 있습니다. 
방법 파라미터는 다음과 같습니다. 
**url**은 트랜젝션이 전송된 노드의 websocket url입니다. 

**privateKey**는 발급자의 프라이빗키이며 트랜젝션 서명에 사용됩니다. 

**gasPrice**는 트랜젝션의 gas price입니다. 

**gasLimit**는 트랜젝션의 gas limit입니다. 

**payer**는 트랜젝션 gas의 payer입니다. 

````
const url = 'http://polaris1.ont.io:20335';
const gasPrice = '500';
const gasLimit = '20000';
const payer = new Address('AMLn5W7rz1sYd1hGpuQUfsnmUuUco22pM8');
const privateKey = new PrivateKey('44fd06de5a6529f3563aad874fb6c8240....')
const result = await claim.revoke(url, gasPrice, gasLimit, payer, privateKey);
````
리턴된 결과가 true이면 철회 섬영이 성공한 것입니다.

### 2.4 신뢰성명 검증

상기 내용에서 제3자가 부여한 신분성명을 획득하는 법을 알아보았습니다. 유저가 필요 시 이 성명들을 제시할 수 있습니다. 또한 성명은 SDK가 제공하는 방식을 통해 진실성과 조작여부를 검증할 수 있습니다.  

Alice가 구직활동을 하는 상황을 예로 들어 신뢰성명을 검증하는 과정을 알아보겠습니다. 

Alice가 B회사에 지원할 때 복단대학에서 발급한 디지털 졸업증명서를 제출하였습니다. 해당 증명서는 claim성명서식에 부합하는 JSON문서입니다. B회사는 ONT SDK를 호출하여 성명을 검증할 수 있습니다. 성명검증이 필요한 모든 유저는 이 방식을 호출하여 체인상의 성명상태를 조회하고 검증할 수 있습니다. 

방법 파라미터는 다음과 같습니다. 
**url** 는 노드의 Restful인터페이스 url입니다. 

````
const url = 'http://polaris1.ont.io:20335';
const result = await claim.getStatus(url);

````
만일 성명상태가 ‘이미 존재함’이고 성명 발급자도 정확하다면 성명검증에 통과하였음을 의미합니다.  
