---
title:
keywords: sample homepage
sidebar: SDKs_ko
permalink: ontology_java_sdk_asset_ko.html
folder: doc_ko/SDKs
giturl: https://github.com/ontio/ontology-java-sdk/blob/master/docs/cn/asset.md
---

<h1 align="center"> 디지털자산 </h1>

<p align="center" class="version">Version 1.0.0 </p>

[English](./ontology_java_sdk_asset_en.html) / 중국어

##    지갑문서 및 규범

지갑문서는 Json서식의 데이터 저장 문서이며 다수의 디지털ID 및 디지털자산계정을 동시에 저장할 수 있습니다. 상세한 내용은 <지갑문서규정>을 참고하세요.  
(../en/Wallet_File_Specification.md)。

디지털자산을 관리하기 위해 먼저 지갑문서를 설치 후 실행하세요. 

```java
//지갑문서가 없으면 자동으로 설치됩니다.
OntSdk ontSdk = OntSdk.getInstance();
ontSdk.openWalletFile("Demo.json");
```
> 비고: 현재는 문서형식의 지갑문서만 지원하고 데이터베이스 또는 기타 저장방식으로도 확장 가능합니다.  

## 자산계정의 데이터구조 설명
`address` 는 Base58코드의 계정주소입니다.
`label` 은 계정이름입니다.
`isDefault` 는 계정 디폴트여부를 의미하며, 디폴트값은 false입니다.
`lock` 은 계정이 유저에 의한 홀딩여부를 의미합니다. 클라이언트단에서는 홀딩된 계정에 있는 자금을 소비할 수 없습니다. 
`algorithm` 은 프라이빗키 알고리즘 명칭입니다. 
`parameters` 는 암호알고리즘에 필요한 파라미터입니다.
`curve` 는 타원곡선의 명칭입니다. 
`key` 는 NEP-2서식의 프라이빗키입니다. 해당 필드는 null(주소 또는 비표준 주소만 해독)로 표기될 수 있습니다.
`encAlg` 프라이빗키 암호알고리즘 명칭으로 고정값은 aes-256-ctr입니다.
`salt` 프라이빗 암호해독 파라미터입니다.
`extra` 는 클라이언트단에서 별도의 정보를 저장하는 필드입니다. 해당 필드는 null로 표기될 수 있습니다. 
`signatureScheme` 는 트랜젝션 서명 시 사용되는 서명방식입니다. 
`hash` hash알고리즘은 프라이빗키 생성에 사용됩니다. 


```java
public class Account {
    public String label = "";
    public String address = "";
    public boolean isDefault = false;
    public boolean lock = false;
    public String algorithm = "";
    public Map parameters = new HashMap() ;
    public String key = "";
    @JSONField(name = "enc-alg")
    public String encAlg = "aes-256-gcm";
    public String salt = "";
    public String hash = "sha256";
    public String signatureScheme = "SHA256withECDSA";
    public Object extra = null;
}
```

## 디지털자산 계정의 관리

지갑의 자산계정 관리방법은 하기 예시를 참고하세요.

* 디지털자산 계정을 생성합니다.

```java
OntSdk ontSdk = OntSdk.getInstance();
Account acct = ontSdk.getWalletMgr().createAccount("password");
//성 된 계정이나 ID는 메모리에만 저장됩니다. wallet 파일에 쓸 때 쓰기 API가 호출되어야합니다.
ontSdk.getWalletMgr().writeWallet();
```


* 등록한 계정 또는 ID는 메모리에만 존재하므로, 지갑문서에 기입하려면 라이팅 인터페이스를 호출해야 합니다.  

```java
ontSdk.getWalletMgr().getWallet().removeAccount(address);
//지갑 기입
ontSdk.getWalletMgr().writeWallet();
```

* 디폴트 디지털자산계정 설정

```java
ontSdk.getWalletMgr().getWallet().setDefaultAccount(index);
ontSdk.getWalletMgr().getWallet().setDefaultAccount("address");
```
> Note: Index는 index번째 설치된account가 디폴트계정임을 표시하고, address는 해당 address와 대응하는 account가 디폴트계정임을 표시합니다. 

## 오리지널 디지털자산 인터페이스


오리지널 디지털자산은 ONT와 ONG를 포함합니다. 트랜젝션 구성, 트랜젝션 서명, 트랜젝션송출을 팩킹합니다. 

#### 1. 이체
```java
String sendTransfer(Account sendAcct, String recvAddr, long amount, Account payerAcct, long gaslimit, long gasprice)
```
기능설명: 발신자가 지정한 수량의 자산을 수신자 계정으로 이체합니다. 

파라미터 설명：

| 파라미터      | 필드   | 유형  | 서술 |             설명 |
| ----- | ------- | ------ | ------------- | ----------- |
| 파라미터 입력 | sendAcct| Account | 발신자 계정 | 필수 |
|        | recvAddr    | Account | 수신자 주소   | 필수 |
|        | amount        | long | 이체 자산 수량|필수|
|        | payerAcct| Account  |트랜젝션 비용 지불 계정 | 필수|
|        | gaslimit   | long | 발행자 및 신청자 ontid 성명 | 필수 |
|        | gasprice   | long | gas가격 | 필수 |
| 파라미터 송촐 | 트랜젝션hash   | String  | 트랜젝션hash  |  |

#### 2. 자산이체 권한
```java
String sendApprove(Account sendAcct, String recvAddr, long amount, Account payerAcct, long gaslimit, long gasprice)
```
기능설명: sendAddr계정은 recxAddr이 amount수량의 자산을 이체하는 것을 수락합니다. 

파라미터 설명 : 
       
| 파라미터      | 필드   | 유형  | 서술 |             설명 |
| ----- | ------- | ------ | ------------- | ----------- |
| 파라미터 입력 | sendAcct| Account | 발신자 계정 | 필수 |
|        | recvAddr    | Account | 수신자 주소   | 필수 |
|        | amount        | long | 수락된 자산 수량|필수|
|        | payerAcct| Account  |트랜젝션 비용 지불 계정 | 필수|
|        | gaslimit   | long | 발행자 및 신청자 ontid 성명 | 필수 |
|        | gasprice   | long | gas가격 | 필수 |
| 파라미터 송촐 | 트랜젝션hash   | String  | 트랜젝션hash  |  |

#### 3. TransferFrom

```java
String sendTransferFrom(Account sendAcct, String fromAddr, String toAddr, long amount, Account payerAcct, long gaslimit, long gasprice)
```
기능설명 : sendAcct계정이 fromAddr계정에서 amount수량의 자산을 toAdd계정으로 이체합니다. 

파라미터 설명：     
        
| 파라미터      | 필드   | 유형  | 서술 |             설명 |
| ----- | ------- | ------ | ------------- | ----------- |
| 파라미터 입력 | sendAcct| Account | 발신자 계정 | 필수 |
|        | fromAddr    | Account | 자산 입금 계좌 주소  | 필수 |
|        | toAddr    | Account | 자산입금계좌 주소      | 필수 |
|        | amount        | long | 이체하는 자산수량 |필수|
|        | payerAcct| Account  |트랜젝션비용 지불계정 | 필수|
|        | gaslimit   | long | 발행자 및 신청자 ontid 성명   | 필수 |
|        | gasprice   | long | gas가격 | 필수 |
| 파라미터 입력 | 트랜젝션hash   | String  | 트랜젝션hash  |  |

#### 4. 잔액조회

```java
long queryBalanceOf(String address)
```
기능설명: 계정의address자산잔액을 조회합니다. 

파라미터 설명:

```address```： 계정주소

리턴값: 계정 잔액

5. Allowance 조회
```java
long queryAllowance(String fromAddr,String toAddr)
```
기능설명: fromAdder이 수락하는 toAdder의 이체수량을 조회합니다.

파라미터 설명:

```fromAddr```: 출금계좌를 지정하는 계좌주소

```toAddr```: 입금계좌를 수락하는 계좌주소

리턴값: 수락된 이체수량

#### 6. 자산명 조회

```java
String queryName()
```
기능설명: 자산명 정보 조회

파라미터 설명:

치: 자산명칭


#### 7. 자산symbol조회

```java
String querySymbol()
```
기능설명: 자산symbol정보 조회

파라미터 설명:


리턴값: symbol정보


#### 8. 자산정확도 조회

```java
long queryDecimals()
```
기능설명: 자산정확도 조회

파라미터 설명:

리턴값: 도

     
#### 9. 자산의 총 공급량 조회:
```java
long queryTotalSupply()
```
기능설명: 자산의 정확도 조회

파라미터 설명:

리턴값: 정확도

자산의 총 공급량 조회:

```java
//step1:ontSdk예시 파악 
OntSdk sdk = OntSdk.getInstance();
sdk.setRpc(url);
sdk.openWalletFile("OntAssetDemo.json");
//step2:ontSdk예시 파악 
ont = sdk.nativevm().ont()
//step3:이체방식 호출
com.github.ontio.account.Account account1 = new com.github.ontio.account.Account(privateKey,SignatureScheme.SHA256WITHECDSA);
ontSdk.nativevm().ont().sendTransfer(account1,"TA4pCAb4zUifHyxSx32dZRjTrnXtxEWKZr",10000,account1,ontSdk.DEFAULT_GAS_LIMIT,0);
```

## Nep-5 스마트 컨트랙트 디지털자산 예시

nep-5문서：
>https://github.com/neo-project/proposals/blob/master/nep-5.mediawiki

디지털자산 템플릿:
>https://github.com/neo-project/examples/tree/master/ICO_Template


|방법|파라미터입력|리턴값|서술|
|:--|:--|:--|:--|
|sendInit    |boolean preExec|String|ture일 경우 이미 초기화 여부를 테스트하기 위한 사전작동을 표시한 것입니다. False일 경우는 초기화 계약 파라미터입니다.|
|sendTransfer|String sendAddr, String password, String recvAddr, int amount|String|자산이체|
|sendBalanceOf|String addr|String| 계좌잔액파악|
|sendTotalSupply||String|정확도 파악|
|sendName||String|명칭 획득|
|sendDecimals||String|정확도 조회|
|sendSymbol||String|Token이니셜 조회|


nep-5스마트 컨트랙트 양식：

```
using Neo.SmartContract.Framework;
using Neo.SmartContract.Framework.Services.Neo;
using Neo.SmartContract.Framework.Services.System;
using System;
using System.ComponentModel;
using System.Numerics;

namespace Nep5Template
{
    public class Nep5Template : SmartContract
    {
        //Token Settings
        public static string Name() => "Nep5Template Token";
        public static string Symbol() => "TMP";
        public static readonly byte[] community = "AXK2KtCfcJnSMyRzSwTuwTKgNrtx5aXfFX".ToScriptHash();
        public static byte Decimals() => 8;
        private const ulong factor = 100000000; //decided by Decimals()

        //ICO Settings
        private const ulong totalAmount = 1000000000 * factor;
        private const ulong communityCap = 1000000000 * factor;

        [DisplayName("transfer")]
        public static event Action<byte[], byte[], BigInteger> Transferred;

        public static Object Main(string operation, params object[] args)
        {
            if (Runtime.Trigger == TriggerType.Application)
            {
                if (operation == "init") return Init();
                if (operation == "totalSupply") return TotalSupply();
                if (operation == "name") return Name();
                if (operation == "symbol") return Symbol();
                if (operation == "transfer")
                {
                    if (args.Length != 3) return false;
                    byte[] from = (byte[])args[0];
                    byte[] to = (byte[])args[1];
                    BigInteger value = (BigInteger)args[2];
                    return Transfer(from, to, value);
                }
                if (operation == "balanceOf")
                {
                    if (args.Length != 1) return 0;
                    byte[] account = (byte[])args[0];
                    return BalanceOf(account);
                }
                if (operation == "decimals") return Decimals();
            }
            return false;
        }

        // 초기화 파라미터
        public static bool Init()
        {
            byte[] total_supply = Storage.Get(Storage.CurrentContext, "totalSupply");
            if (total_supply.Length != 0) return false;

            Storage.Put(Storage.CurrentContext, community, communityCap);
            Transferred(null, community, communityCap);

            Storage.Put(Storage.CurrentContext, "totalSupply", totalAmount);
            return true;
        }

        // get the total token supply
        // 이미 발행된 token총량 획득
        public static BigInteger TotalSupply()
        {
            Runtime.CheckSig(new byte[1]{ 1 },  new byte[]{2},new byte[]{ 3});
            return Storage.Get(Storage.CurrentContext, "totalSupply").AsBigInteger();
        }

        // function that is always called when someone wants to transfer tokens.
        // 트랜잭션Token 호출
        public static bool Transfer(byte[] from, byte[] to, BigInteger value)
        {
            if (value <= 0) return false;
            if (!Runtime.CheckWitness(from)) return false;
            if (from == to) return true;
            BigInteger from_value = Storage.Get(Storage.CurrentContext, from).AsBigInteger();
            if (from_value < value) return false;
            if (from_value == value)
                Storage.Delete(Storage.CurrentContext, from);
            else
                Storage.Put(Storage.CurrentContext, from, from_value - value);
            BigInteger to_value = Storage.Get(Storage.CurrentContext, to).AsBigInteger();
            Storage.Put(Storage.CurrentContext, to, to_value + value);
            Transferred(from, to, value);
            return true;
        }

        // get the account balance of another account with address
        // 주소에 따라 token잔액 획득
        public static BigInteger BalanceOf(byte[] address)
        {
            return Storage.Get(Storage.CurrentContext, address).AsBigInteger();
        }
    }
}
```


스마트 컨트랙트의 설치 및 호출방법은 다음을 참고하세요.[smartcontract](./ontology_java_sdk_smartcontract_ko.html)。
