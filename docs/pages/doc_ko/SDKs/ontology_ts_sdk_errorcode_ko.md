---
title:
keywords: sample homepage
sidebar: SDKs_ko
permalink: ontology_ts_sdk_errorcode_ko.html
folder: doc_ko/SDKs
giturl: https://github.com/ontio/ontology-ts-sdk/blob/master/docs/cn/errorcode.md
---

[English](./ontology_ts_sdk_errorcode_en.html) / 한국어

<h1 align="center">오류코드</h1>
<p align="center" class="version">Version V0.7.0 </p>



| 리턴코드 | 서술정보                      | 설명                              |
| :------- | ----------------------------- | --------------------------------- |
| 0        | SUCCESS                       | 성공                              |
| 41001 | SESSION_EXPIRED | 세션 무효 또는 기한만료 (재 로그인 필요)  |
| 41002 | SERVICE_CEILING | 서비스 상한 초과 |
| 41003 | ILLEGAL_DATAFORMAT | 비합법적인 데이터 서식 |
| 41004 | INVALID_VERSION| 비합법적인 버전 |
| 42001 | INVALID_METHOD |무효화된 방식|
| 42002 | INVALID_PARAMS |무효화된 파라미터|
| 43001 | INVALID_TRANSACTION |무효화된 트랜젝션|
| 43002 | INVALID_ASSET |무효화된 자산|
| 43003 | INVALID_BLOCK |무효화된 블록|
| 44001 | UNKNOWN_TRANSACTION |트랜젝션을 찾을 수 없음|
| 44002 | UNKNOWN_ASSET |자산을 찾을 수 없음 |
| 44003 | UNKNOWN_BLOCK | 블록을 찾을 수 없음 |
| 44004 | UNKNWN_CONTRACT |계약을 찾을 수 없음|
| 45001 | INTERNAL_ERROR |내부 오류|
| 47001 | SMARTCODE_ERROR| 스마트 컨트랙트 오류 |
| 51000 | UNKNOWN_ONTID  | 존재하지 않는 ONT ID |
| 52000 | NETWORK_ERROR  | 네트워크 오류 |
| 53000 | Decrypto_ERROR | 디코딩 오류 |