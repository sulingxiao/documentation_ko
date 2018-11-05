---
title:
keywords: sample homepage
sidebar: Wallet_ko
permalink: ontology_wallet_dev_overview_ko.html
folder: doc_ko/Wallet
giturl: https://github.com/ontio/documentation/blob/master/walletDevDocs/ontology_wallet_dev_overview_en.md
---

[English](./ontology_wallet_dev_overview_en.html) / 한국어



<h1 align="center">Wallet Integration</h1>
<p align="center" class="version">Version 1.0.0 </p>

The outline of this document is as follows:
* [Wallet Integration](#wallet-integration)
* [Native contract address](#native-contract-address)

 
## Wallet Integration

The wallet function includes giving an account for digit asset and digital identity. [wallet Specification](https://ontio.github.io/documentation/Wallet_File_Specification_en.html)

Wallet function lists:

| Module                |                    Sub Module                    |     Test case      |
| ------------------    | :----------------------------------------------: | :------------------: |
| Wallet management    |           create                                 |     Use the mnemonic words when creating wallet(Based on BIP39 and BIP44 specifications)   |
|                       |          export                                 |    Export keystore      |
|                      |           import                                 |   Import keystore to wallet  |
|                      |                                                  |   Import from the mnemonic words  |
|                      |                                                  |   Import from private key      |
|                      |                                                  |    Import from WIF            |
|                      |         delete                                   |   Delete wallet account     |
|                      |        Information service                      |    Query balance of ONT&ONG |
|                      |                                                  |   Query details of ONT&ONG  |
|                      |                                                  |  Query unclaim ONT&ONG   |
|    Transfer Assets   |          Transfer                                |    ONT/ONG transfer        |
|                      |                                                  |      ONG Claim              |
|   ONT ID management  |         Create                                   |   Create a new ONT ID and write it in blockchain   |
|                      |        Import                                    |     Import keystore of ONT ID |
|                      |        Export                                    |     Export keystore of ONT ID |
|                      |       Information service                        |    Query ONT ID events , create, delete,etc.    |
|                      |                                                  |    Query ONT ID             |



The specifications of sdk implementations wallet are Currently:

Java SDK ：[Java SDK](https://github.com/ontio/ontology-java-sdk/blob/master/docs) 

TypeScript SDK ：[Ts SDK](https://github.com/ontio/ontology-ts-sdk/tree/master/docs) 

Android SDK ：[Android SDK](https://github.com/ontio-community/ontology-andriod-sdk)

Golang SDK ：[Go SDK](https://github.com/ontio/ontology-go-sdk) 

Python SDK ：[Python SDK](https://github.com/ontio/ontology-python-sdk)


## Native contract address

contract | contract u160 address | Address
---|---|---
ONT Token | 0100000000000000000000000000000000000000| AFmseVrdL9f9oyCzZefL9tG6UbvhUMqNMV
ONG Token | 0200000000000000000000000000000000000000 | AFmseVrdL9f9oyCzZefL9tG6UbvhfRZMHJ
ONT ID | 0300000000000000000000000000000000000000 | AFmseVrdL9f9oyCzZefL9tG6Ubvho7BUwN
Global Params | 0400000000000000000000000000000000000000 | AFmseVrdL9f9oyCzZefL9tG6UbvhrUqmc2
Oracle | 0500000000000000000000000000000000000000 | AFmseVrdL9f9oyCzZefL9tG6UbvhzQYRMK
Authorization Manager(Auth) | 0600000000000000000000000000000000000000 | AFmseVrdL9f9oyCzZefL9tG6Ubvi9BuggV
Governance | 0700000000000000000000000000000000000000 | AFmseVrdL9f9oyCzZefL9tG6UbviEH9ugK
DDXF(Decentralized Exchange) | 0800000000000000000000000000000000000000 | AFmseVrdL9f9oyCzZefL9tG6UbviKTaSnK
