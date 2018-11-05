---
title:
keywords: sample homepage
sidebar: SDKs_ko
permalink: ontology_ts_sdk_asset_ko.html
folder: doc_ko/SDKs
giturl: https://github.com/ontio/ontology-ts-sdk/blob/master/docs/cn/asset.md
---

[English](./ontology_ts_sdk_asset_en.html) / 한국어

<h1 align="center">디지털자산관리 </h1>
<p align="center" class="version">Version 0.7.0 </p>

## 지갑 Wallet 

지갑 Wallet은 Json서식의 데이터 저장문서입니다. 온톨로지에서 Wallet은 디지털신분과 디지털 자산 모두를 저장할 수 있습니다. 

### Wallet 데이터규정

````
{
	name: string;
    defaultOntid: string;
    defaultAccountAddress : string;
    createTime: string;
    version: string;
    scrypt: {
        "n": number;
        "r": number;
        "p": number;
    };
    identities: Array<Identity>;
    accounts: Array<Account>;
    extra: null;
}
````

`name`은 유저가 지갑에서 얻은 명칭입니다. 

```defaultOntid```는 지갑 디폴트의 디지털신분 ONT ID입니다. 

```defaultAccountAddress```는 지갑 디폴트의 자산계정 주소입니다. 

```createTime```은 ISO서식으로 나타낸 지갑 생성시간입니다. 예시: "2018-02-06T03:05:12.360Z"

`version`의 현재 고정치는 1,0이며 향후 기능 업그레이드 사용을 남겨두고 있습니다.. 

`scrypt`는 암호알고리즘에 필요한 파라미터입니다. 해당 알고리즘은 지갑암호화 및 프라이빗키 해독에 사용됩니다. 

`identities`는 지갑의 모든 디지털신분 대상의 배열입니다. 

```accounts```는 지갑의 모든 디지털자산 계정대상의 배열입니다. 

```extra```는 클라이언트단 개발자가 별도 데이터필드를 저장할 때 사용하는 것이며 null로 표시될 수 있습니다. 

더 자세한 지갑데이터 규정은 다음을 참고하십시오.[Wallet_File_Specification](./Wallet_File_Specification_en.html).

### 지갑생성

유저는 자산이 없어도 지갑을 생성할 수 있습니다. 

#### 1) 빈 지갑 생성하기

유저는 지갑명칭만 입력하면 됩니다. 

````
import {Wallet} from 'Ont'
var wallet = Wallet.create( name )
````

#### 2) 계정을 생성하고 지갑에 추가하기

유저는 새 계정을 생성하기 위해 **프라이빗키, 암호, 계정명칭**으로 신규 계정을 생성할 수 있고 
하며 그 중 **프라이빗키**는 SDK가 제공하는 방식으로 생성 가능합니다. 또한 계정생성에 필요한 알고리즘 대상을 지정할 수도 있습니다. 알고리즘 대상의 구조는 다음과 같습니다.  

```
{
algorithm: string // 알고리즘 명칭
parameters: {}    // 알고리즘 파라미터
}
```

만약 전송하지 않으면 SDK는 디폴트 알고리즘으로 계정을 생성하게 됩니다. 
계정을 생성한 후 지갑에 추가합니다. 

````
import {Account, Core} from 'Ont'
var account = Account.create( privateKey, password, name )
var privateKey = Core.generatePrivateKeyStr()
wallet.addAccount(account)
````

### Account 데이터구조

````
{
  address : string,
  label : string,
  lock : boolean,
  algorithm : string,
  parameters : {},
  key : string,
  extra : null
}
````

```address```는base58코딩 계정주소입니다.
 
```label```은 계정 명칭입니다.

`lock`은 계정이 유저에 의해 홀딩되었는지 여부를 표시합니다. 클라이언트단에서는 홀딩 된 계정의 자산을 소비할 수 없습니다.  

`algorithm`는 암호알고리즘 명칭입니다. 

`parameters`는 암호알고리즘에 필요한 파라미터입니다. 

`key`는 NEP-2서식의 프라이빗키 입니다. 해당 필드는 null로 표기될 수 있습니다. (주소 또는 비표준 주소에만 해당)

`extra`는 클라이언트단의 별도정보 저장 필드입니다. 해당 필드는 null로 표기될 수 있습니다.

### 계정 생성

````
import {Account, Core} from 'Ont'
var privateKey = Core.generatePrivateKeyStr()
//@param {string} privateKey 사용자 프라이빗 키
//@param {string} password 비밀번호
//@param {string} label 사용자 성함
//@param {object} algorithmObj 파라미터 선택 가능, 암호화폐 알고리즘 대상
var account = Account.create(privateKey, password, label, algorithmObj)
````

### 계정 불러오기

백업데이터를 통해 계정을 불러올 수 있습니다. 

계정을 불러올 때 암호와 암호화된 프라이빗키를 검증할 수 있으며, 틀리면 그에 상응하는 오류코드가 발생합니다. 

````
import { Account } from 'Ont'
//@param {label} 계정 명칭
//@param {encryptedPrivateKey} 암호화된 프라이빗키
//@param {password} 프라이빗키 암호화에 사용하는 암호
var account;
try {
    account = Account.importAccount(label, encryptedPrivateKey, password)
} catch(error) {
    //암호 또는 프라이빗키가 정확하지 않습니다. 
}
````


## 디지털자산 이체 Transfer

#### 이체함수 설명
````
function makeTransferTransaction(tokenType:string, from : string, to : string, value : string,  privateKey : string)

tokenType: token 유형
from: 이체자의 퍼블릭 키 해시주소
to: 받는이의 퍼블릭 키 해시주소
value: 이체 수량, 곱하기 10^8로하여 소수점 아래의 수량에 대한 손해를 막는다.
privateKey: 이체자 퍼블릭키에 대응하는 프라이빗 키
````

####Token유형
````
TOKEN_TYPE = {
  ONT : 'ONT',  //Ontology Token
  ONG : 'ONG'   //Ontology Gas
}
````

#### 샘플
````
import { makeTransferTransaction, buildRestParam } from "../src/transaction/transactionBuilder";

var tx = makeTransferTransaction( 'ONT', '0144587c1094f6929ed7362d6328cffff4fb4da2', 'ffeeddccbbaa99887766554433221100ffeeddcc', '1000000000', '760bb46952845a4b91b1df447c2f2d15bb40ab1d9a368d9f0ee4bf0d67500160' )

var restData = buildRestParam(tx)

axios.post('127.0.0.1:20386', restData).then(res => {
       console.log('transfer response: ' + JSON.stringify(res.data))
   }).catch(err => {
       console.log(err)
   })
````

## 데이터자산 조회 getBalance

### 잔액조회 링크

````
//nodeURL 노드의 IP주소
//HTTP_REST_PORT노드가 Restful인터페이트에 노출한 포트
//address 잔액조회 주소
http://${nodeURL}:${HTTP_REST_PORT}/api/v1/balance/${address}
````

### 샘플:

````
let request = `http://127.0.0.1:20384/api/v1/balance/TA5uYzLU2vBvvfCMxyV2sdzc9kPqJzGZWq`
	axios.get(request).then((res : any) => {
		if(res.data.Error === 0) {
			let obj = {
				error : 0,
				result : res.data.Result
			}
		} else {
			let obj = {
				error: res.data.Error,
				result : ''
			}
		}
	}).catch( (err:any) => {
		let obj = {
			error: JSON.stringify(err),
			result: ''
		}
	})
````
