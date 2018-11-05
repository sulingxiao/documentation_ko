---
title:
keywords: sample homepage
sidebar: Dapp_ko
permalink: SmartX_Tutorial_ko.html
folder: doc_ko/Dapp
giturl: https://github.com/ontio/ontology-smartcontract/blob/master/smart-contract-tutorial/SmartX_Tutorial_cn.md
---

<h1 align="center">SmartX 강좌</h1>
<p align="center" class="version">Version 1.0.0 </p>

[English](./SmartX_Tutorial_en.html) / 한국어
                                
## SmartX란?

SmartX는 컴파일러 및 스마트 컨트랙트의 설치와 호출에 사용되는 원스톱IDE으로써 풍부한 스마트 컨트랙트 템플릿 및 강력한 온라인 에디터를 제공합니다. SmartX이라는 툴에 기반하여 스마트 컨트랙트 수요자는 템플릿컨트랙트을 참고 및 사용하거나 커뮤니티 개발자에게 개발에 필요한 컨트랙트의 제작을 위탁 할 수 있습니다. 향후 스마트 컨트랙트의 개발자는 자신이 코딩한 스마트 컨트랙트을 트랜잭션하거나 공동으로 스마트 컨트랙트의 개발에 참여하여 자신의 전공 및 지식으로 수익을 얻을 수 있습니다. 

다음으로, GitHub등과 같은 분산식 소프트웨어 코드 위탁보관 플랫폼과 마찬가지로 여러 사람의 공동참여하고 공유하는 컨트랙트을 지원할 것입니다. 이와 동시에 경제적인 보상체계와 공정한 거버넌스 정책을 결합하여 모든 참여자들의 공헌을 반영함으로써 스마트 컨트랙트 프로그래밍, 스마트 컨트랙트 트랜젝션, 협업, 커뮤니티 구축 등을 통합하여 우수한 스마트 컨트랙트 개발 환경을 조성하고자 합니다.   

## 등록

[SmartX사이트](http://smartx.ont.io/#/)

먼저, ONT ID를 생성하여 귀하의 스마트 컨트랙트 프로젝트를 관리해야 합니다. 메인 페이지에서 ‘등록’을 클릭하여 계정을 등록합니다. 다음을 참고하십시오.  

![등록](https://upload-images.jianshu.io/upload_images/150344-6beeb3324ef05ac9.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

암호를 2회 입력 후(암호 길이는 6자 이상) ‘ONT ID생성’을 클릭하여 계정을 등록합니다. 그리고 ‘다운로드’를 클릭하여 ONT ID문서를 다운로드하고 프라이빗키를 백업합니다. ONT ID문서는 귀하의 암호화된 ONT ID와 프라이빗키를 저장하며 프라이빗키는 ONT ID계정을 복구할 수 있는 유일한 인증서이므로 잘 보관하시기 바랍니다. 
 
![ONT ID문서](https://upload-images.jianshu.io/upload_images/150344-a312b6edd22caf32.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

![](https://upload-images.jianshu.io/upload_images/150344-5b2f2519b025cebe.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

만약 이미 ONT ID가 있다면 ‘ONT ID복구’를 클릭하여 ONT ID를 조회합니다. 프라이빗키와 2회의 암호를 입력 후 ‘ONT ID’를 클릭하면 ONT ID를 찾을 수 있습니다. 

![복구](https://upload-images.jianshu.io/upload_images/150344-4bf4133ccb19f075.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

## 로그인

등록 후 ONT ID를 갖게 되면 해당 문서를 선택하고 암호만 입력하면 귀하의 계정에 로그인할 수 있습니다. 다음을 참고하십시오.  

![로그인](https://upload-images.jianshu.io/upload_images/150344-e3848962a4dfe0d1.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

로그인 후, 프로그램 목차 페이지로 이동합니다. 이미 저장한 프로젝트 혹은 새로운 프로그램으로 선택이 가능합니다.

![프로젝트 생성](https://upload-images.jianshu.io/upload_images/150344-17ec830db0f4d948.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

## 다음절차 – 프로그래밍&스마트 컨트랙트 컴파일

* **NEO버추얼머신**에 대한 [SmartX](http://smartx.ont.io)를 제공하여 귀하의 스마트 컨트랙트 프로그래밍 및 컴파일, 호출을 도와드립니다.

먼저 프로젝트를 생성한 후 귀하가 선호하는 언어를 선택하고 스마트 컨트랙트의 프로그래밍을 시작합니다. 

![언어선택](https://upload-images.jianshu.io/upload_images/150344-de1bad190b1c6c66.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

그리고 프로젝트 에디트 화면에 들어갑니다. 중간부분은 주로 컨트랙트 에디터입니다. 우측은 작동보드이며 중간 하단은 작동의 아웃풋결과 출력입니다.   

![컴파일](https://upload-images.jianshu.io/upload_images/150344-d100aa119363ec2c.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

### 스마트 컨트랙트 프로그래밍

다음으로 귀하의 스마트 컨트랙트 프로그래밍을 시작할 수 있습니다. 이를 위해 다양한 실용템플릿을 제공하므로 참고하시기 바랍니다.  

[다양한 구체적 예시]
(https://github.com/ontio/documentation/tree/master/smart-contract-tutorial/examples) 

### 스마트 컨트랙트 컴파일

컨트랙트 프로그래밍 완료 후 작동보드의 컴파일 버튼을 클릭하면 컨트랙트을 컴파일 할 수 있습니다. 

만일 귀하의 컨트랙트언어가 정확하면 상응하는 ABI문서와 AVM문서가 컴파일 되고 작동보드에 표시될 것입니다. 

## 두 번째 – 스마트 컨트랙트 설치

다음으로 컨트랙트을 블록체인에 설치할 수 있습니다. 귀하가 선택한 네트워크가 테스트넷이라면 이 부분에서는 gas를 소비하지 않습니다. 설치버튼을 클릭하여 컨트랙트을 설치합니다. 설치결과는 아웃풋창에 출력되며 결과에 들어있는 트랜젝션 hash를 온톨로지의 (https://explorer.ont.io/)에 카피하여 설치 성공여부를 다시 한번 확인할 수 있습니다. 

SmartX외에 온톨로지의 SDK를 이용하여 컨트랙트을 설치할 수 있습니다. 더 많은 정보는 관련 문서를 참고하십시오. 

[>> Java SDK](https://ontio.github.io/documentation/ontology_java_sdk_smartcontract_en.html)

[>> TS SDK](https://ontio.github.io/documentation/ontology_ts_sdk_smartcontract_en.html)

![스마트 컨트랙트 설치]
(https://upload-images.jianshu.io/upload_images/150344-d0160bc4a38a804d.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)


## 세 번째 – 스마트 컨트랙트 호출

마지막으로 컨트랙트에 있는 방식을 실행할 수 있습니다. 실행할 방식을 선택하고 정확한 파라미터 함수 입력 후 실행버튼을 클릭하여 스마트 컨트랙트을 호출합니다. 호출한 결과는 아웃풋창에 출력됩니다. 

![ 스마트 컨트랙트 호출]
(https://upload-images.jianshu.io/upload_images/150344-5229fe6d34a67372.png?imageMogr2/auto-orient/strip%7CimageView2/2/w/1240)

SmartX외에 온톨로지의 SDK를 이용하여 컨트랙트을 호출할 수 있습니다. 더 많은 정보는 관련 문서를 참고하십시오. 

[>> Java SDK](https://ontio.github.io/documentation/ontology_java_sdk_smartcontract_en.html)

[>> TS SDK](https://ontio.github.io/documentation/ontology_ts_sdk_smartcontract_en.html)


