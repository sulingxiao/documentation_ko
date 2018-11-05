---
title:
keywords: sample homepage
sidebar: Dapp_ko
permalink: ontology_dapp_dapi_ko.html
folder: doc_ko/Dapp
giturl: https://github.com/ontio/documentation/blob/master/walletDevDocs/ontology_dapp_dapi_ko.md
---

[English](./ontology_dapp_dapi_en.html) / 한국어

<h1 align="center">Ontology dAPI</h1>
<p align="center" class="version">Version 1.0.0 </p>


## 초록

이 제안에는 두가지 중요 부분이 있습ㄴ다.

* dApps 개발을 위해 Javascript API가 제안되었습니다. 이 dAPI를 사용하면 dApp이 Ontology 블록 체인과 통신하고 사용자가 dApp 자체를 신뢰하지 않고도 전송, ONT ID 등록 및 기타 요청을 할 수 있습니다. 신뢰 문제는 dAPI 제공 업체로 이동합니다.

* 통신 프로토콜은 dAPI 제공자 개발을 위해 제안됩니다. 이를 통해 여러 월렛 구현자가 dApp 사용자에게 동일한 통합 서비스를 제공하고 dApp 개발의 단편화를 방지 할 수 있습니다.

## 모티베이션

현재 dApp은 SDK (Typescript, Java, Python 등) 중 하나를 사용하여 온톨로지 네트워크와 통신합니다. 이 설정에는 세 가지 주요 단점이 있습니다.

1. dApp의 사용자는 프라이빗 키를 사용하여 dApp 개발자를 신뢰해야하며 dApp를 통해 중개 된 정보는 합법적입니다.

2. SDK는 매우 강력하여 사용하기가 어렵습니다. 보다 간소화 된 API를 사용하면 개발자가 애플리케이션 자체에 집중할 수 있습니다.

3. 외부 서명 메커니즘과의 통합을 구현하기가 어렵습니다 (예 : Ledger, Trezor).

## 사양

이 제안은 다음 기능과 정의를 사용합니다.

* **SDK**, 네트워크와의 낮은 수준의 통신을 구현하고 dApp용 고급 인터페이스를 제공하는 소프트웨어 개발 키트입니다..

* **dApp**, 웹 환경에서 실행되는 분산 특성을 가진 애플리케이션입니다. 이 애플리케이션은 가치 전달, 계약 이행 및 참여자 간의 신분식별을 위해 온톨로지 네트워크를 사용합니다.

* **dAPI**, OEP 가 제안하는 dApp용 API입니다.

* **dAPI 제공업체**, 웹 브라우져 플러그인 또는 기타 수단의 형태로 dAPI을 구현하여 공급자와 사용자의 상호작용을 API호출 워크 플로에 삽입할 수 있습니다.(예:전송확인)

* **알림 이벤트**, 스마트 컨트랙트에서 실행에서 브로드케스팅된 이벤트

* **NEOVM**, Neo/Ontology 스마트 컨트랙트 실행을 위한 가벼운 가상 머신

## OEP-6

https://github.com/backslash47/OEPs/blob/oep-dapp-api/OEP-6/OEP-6.mediawiki

## Implementation

https://apidoc.ont.io/dapi/