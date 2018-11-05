---
title:
keywords: sample homepage
sidebar: Dapp_ko
permalink: Introduction_of_Ontology_Smart_Contract_ko.html
folder: doc_ko/Dapp
giturl: https://github.com/ontio/ontology-smartcontract/blob/master/smart-contract-tutorial/Introduction_of_Ontology_Smart_Contract_cn.md
---

<h1 align="center">온톨로지 스마트 컨트랙트 소개</h1>
<p align="center" class="version">Version 1.0.0 </p>

[English](./Introduction_of_Ontology_Smart_Contract_en.html) / 한국어

## 스마트 컨트랙트이란?

스마트 컨트랙트는 1994년 Nick Szabo가 최조 제안한 개념입니다. 정보 제공방식으로 계약을 보급, 검증 또는 시행하도록 고안한 컴퓨터 프로토콜입니다. 스마트 컨트랙트는 제 3자 없이 트랜션가 가능하고, 이러한 트랜잭션는 추적이 가능하며 트랜잭션내역을 수정할 수 없습니다.

블록체인 기술의 탈중앙화, 조작불가, 신뢰성이라는 환경이 스마트 컨트랙트가 시장에서 유용하게 사용될 수 있고, 자리잡도록 도왔습니다.

## 온톨로지 스마트 컨트랙트의 특징은 무엇인가요?

온톨로지 스마트 컨트랙트는 다기능, 확장성, 고성능, 다국어, 교차계약, 교차가상머신 등 모든 기능을 하나의 시스템에 통합하였습니다. 또한, 온톨로지 스마트 컨트랙트는 여러 프로그래밍 언어를 지원하여 개발자가 새로운 프로그래밍 언어를 익힐 시간을 절약할 수 있도록 합니다. 현재까지 지원되는 언어는 C#, Python등으로 Java, C++, Rust, Go, and JavaScript를 포함한 다른 언어까지 지원할 것 입니다.


온톨로지 스마트 컨트랙트는 확실한 고성능의 확장기능 제공에 대화형 서비스와 버추얼 머신 두가지 모듈을 포함합니다. 대화형 서비스는 버추얼 머신와 블록체인 원장간의 상호작용을 제공합니다. 버추얼 머신는 스마트 컨트랙트의 실행환경을 제공합니다. 대화형 서비스는 기본 서비스와 NEO VM서비스를 포함합니다. 기본 서비는 기본 체인에 특수 스마트 컨트랙트 구현을 제공하며 신속하고 편리하게 사용할 수 있게합니다. NeoVM 서비스는 NeoVM의 API를 제공하여 외부에서 통신하므로 스마트 컨트랙트의 호출 기능을 향상시킬 수 있습니다.

![ontology smart contract architecture.png](./lib/images/smartcontract_architecture.png)


## 계약 유형

온톨로지 스마트 컨트랙트는 네이티브 계약과 NeoVm 계약 두 종류로 나뉩니다. 네이티브 계약은 온톨로지 기반에서 직접 작성한 계약으로 일반적인 계약처럼 계약 코드를 작성할 필요가 없으며 실행 효율성이 높아 보통계약에 대해 최적화 되어있습니다. Oracle, DID, 관리 권한, 데이터 거래소 모두 네이트브 계약 서비스로 포함되어있습니다. NeoVm계약은 NeoVm가상 시스템에서 실행되는 계약으로 해당 계약 코드를 직접 작성해야합니다. 현재까지 지원되는 언어는 Java, C #, Python입니다. NeoVm은 자체적으로 가볍고 확장 가능한 고성능 기능을 가지고 있어  Interop 서비스계층과의 결합을 통해 버추얼 머신과 원장간의 상호 작용을 원활하게 연결할 수 있습니다. .


![ontology smart contract type.png](./lib/images/smartcontract_type.png)


## 계약관리

가상머신 유형

```
// Prefix of address
type VmType byte

const (
Native = VmType(0xFF)
NEOVM  = VmType(0x80)
WASMVM = VmType(0x90)
)

```

계약구조

```
type VmCode struct {
VmType VmType
Code   []byte
}

```

transaction payload사용

```
// InvokeCode is an implementation of transaction payload for invoke smartcontract
type InvokeCode struct {
GasLimit common.Fixed64
Code     stypes.VmCode
}

```

온톨로지 스마트 컨트랙트를 실행하려면 계약을 실행하는 스크립트와 버추얼 머신이 필요합니다. 스마트 컨트랙트 스케줄러는 버추얼 머신의 유형에 따라 다른 컨트랙트를 실행합니다. 진행 과정에서 컨트랙트는 AppCall명령(컨트랙트 운영의 필수 파라미터 포함)을 호출하여 스마트 컨트랙트 스케줄러를 움직이게 합니다. 스케줄러는 입력된 파라미터에 따라 해당 버추얼머신 스크립트를 컨트랙트가 완성될 때까지 실행하도록 합니다.


![process](http://upload-images.jianshu.io/upload_images/150344-ac402b1c8eb3aa9a.jpeg?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)




