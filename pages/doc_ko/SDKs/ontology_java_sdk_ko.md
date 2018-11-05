---
title:
keywords: sample homepage
sidebar: SDKs_ko
permalink: ontology_java_sdk_ko.html
folder: doc_ko/SDKs
giturl: https://github.com/ontio/ontology-java-sdk/blob/master/docs/cn/README.md
---

<h1 align="center"> Ontology Java SDK 소개 </h1>

<p align="center" class="version">Version 1.0.0 </p>

[English](./ontology_java_sdk_en.html) / 한국어

<h1 align="center">전체소개</h1>





해당 프로젝트의 주체는 Java SDK이며 이는 종합적인 SDK입니다. 현재는 현지 지갑관리, 디지털신분 관리, 디지털자산 관리, 스마트 컨트랙트 설치 및 호출, 노드와의 통신 등을 지원합니다. 향후 더 많은 기능과 어플리케이션을 지원할 예정입니다.  

## 주요기능

- [소개](sdk_get_start.md)
- [인터페이스 기본 정보](./ontology_java_sdk_interface_ko.html)
- [블록체인 노드 기본작동](./ontology_java_sdk_basic_ko.html)
- [지갑문서 및 규정](Wallet_File_Specification_cn.md)
- [디지털신분 및 신뢰성명 관리](./ontology_java_sdk_identity_claim_ko.html)
- [디지털자산](./ontology_java_sdk_asset_ko.html)
- [디지털 증명저장](./ontology_java_sdk_attest_ko.html)
- [권한관리](./ontology_java_sdk_auth_ko.html)
- [스마트 설치 및 호출](./ontology_java_sdk_smartcontract_ko.html)
- [오류코드](./ontology_java_sdk_errorcode_ko.html)
- [API 문서](https://apidoc.ont.io/javasdk/)

## 코드구조 설명

* acount：퍼블릭 및 프라이빗키 생성 등 계정과 관련된 동작
* common： 제네럴 기초 인터페이스
* core：코어층은 계약, 트랜젝션, 서명 등을 포함 
* crypto： ECC/SM 등 암화화 관련
* io：io 동작
* network：restful\rpc\websocket과 체인통신 인터페이스
* sdk：SDK하부에 대한 팩킹, Info정보, 통신관리, Claim관리, 지갑관리, 익셉션
* ontsdk클래스: 관리자와 트랜젝션 사례를 제공합니다. 관리자는 walletMgr、connManager를 포함합니다.
* walletMgr지갑관리자는 주로 디지털신분 및 디지털자산 계정을 관리하고 유저가 체인에 트랜젝션을 전송할 때 프라이빗키로 서명해야 합니다. 
* connManager와 체인상 통신관리. 트랜젝션 전송 또는 조회 시 반드시 관리자를 연결해야 합니다.
 
## 설치방법

### JDK 8의 개발환경을 조성하십시오. 

>**주의** 
SDK에서 사용하는 key의 길이는 128자리를 초과합니다. Java의 안전전략문서는 key의 길이에 제한이 있기 때문에 local_policy.jar와 US_export_policy.jar를 모두 다운받아 JRE베이스 ${java_home}/jre/lib/security목록에서 대응하는 jar팩으로 변환해야 합니다. 

jar팩 다운로드 주소：

>http://www.oracle.com/technetwork/java/javase/downloads/jce8-download-2133166.html


### Build

```
$ mvn clean install
```

### 사전준비

* [Ontology노드](https://github.com/ontio/ontology/releases)실행 시 메인넷 또는 테스트넷, 프라이빗넷에서 모두 가능합니다. rpc포트를 확보하면 방문가능하며 SDK를 확보하면 RPC서버에 연결할 수 있습니다. 
