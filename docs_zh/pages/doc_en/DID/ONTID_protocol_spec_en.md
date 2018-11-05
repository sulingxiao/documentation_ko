---
title:
keywords: sample homepage
sidebar: DID_en
permalink: ONTID_protocol_spec_en.html
folder: doc_en/DID
giturl: https://github.com/ontio/ontology-DID/blob/master/docs/en/ONTID_protocol_spec.md
---

English / [中文](./ONTID_protocol_spec_zh.html)

<h1 align="center">Ontology Distributed Identification Protocol</h1>
<p align="center" class="version">Version 0.7.0 </p>


“Entity” refers to individuals, legal entities (organizations, enterprises, institutions, etc.), objects (mobile phones, automobiles, IoT devices, etc.), and contents (articles, copyrights, etc.) in the real world, and “identity” refers to the entity's identity within the network. Ontology uses Ontology Identifier (ONT ID) to identify and manage the entities' identities. On Ontology Blockchain, one entity can correspond to multiple individual identities, and there is no relation between multiple identities.

The ONT ID is a decentralized identification protocol and it has the features of decentralization, self-management, privacy protection, security and ease of use. Each ONT ID corresponds to an ONT ID Description Object (DDO).

> The ONT ID protocol has been completely implemented by the smart contract of Ontology Blockchain. As a protocol layer, it follows a decoupled design, so it is not limited to Ontology Blockchain, but can also be implemented on other blockchains.

## Identification Protocol Specification

### 1.1 ONT ID generation

The ONT ID is a URI that is generated by each entity itself. The generation algorithm needs to guarantee that the collision probability is extremely low. Beside, when someone register an ONT ID on Ontology, the consensus node can check whether the ID is already registered.

ONT ID generation algorithm:

To prevent the user from entering the ONT ID by mistake, we define a valid ONT ID that must contain 4 bytes of verification data. We are going to describe in detail how to generate a valid ONT ID.
```json
 1. Generate a 32-byte temporary random nonce, and calculate h = Hash160 (nonce), data = <VER> || h;
 2. Calculate a 4-byte verification data, that is, checksum = SHA256(SHA256(data))[0:3];
 3. Make idString = data || checksum;
 4. Cascade "did:ont:" with data, that is, ontId = "did:ont:" || idString;
 5. Output ONT ID.
```

Above you see, `<ont>` is a network identifier, and `<VER>` is a 1 byte version label. In ONT, `<VER> = 41, <ont> = "ont"`. That is to say , the first 8 bytes of identity in Ontology are "did:ont:", plus a 25 byte long idString, which constitutes a complete ONT ID.

### 1.2 Self-management
Ontology applies digital signature technology to guarantee entities have rights to manage their own identities. The ONT ID is bound to the entity's public key when it registers, thereby indicating its ownership. The use of the ONT ID and the modification of its attributes require the owner's digital signature. The entity can independently determine the scope of use of its ONT ID and set the public key bounded by ONT ID and manage the attributes of the ONT ID.

### 1.3 Multiple key binding
Ontology supports a variety of domestic and international standardized digital signature algorithms such as RSA, ECDSA, and SM2. The algorithm applied to the key that is bounded by ONT ID should be specified. At the same time, an ONT ID can bound multiple different keys to meet the usage requirements of entities in different application scenarios.

### 1.4 Recovery of identity loss
The owner of the ONT ID can assign someone else to execute his management rights, such as modifying the attributes of the ONT ID and replacing the key when the key is lost. The assigned person can implement a variety of access control logic such as “AND”, “OR”, and “(m, n)-thresholds”. Refer to [Appendix A](#a-recovery-account-address) for more details.

### 1.5 Identity description object DDO specification

The identity description object DDO corresponding to the ONT ID is stored in the Ontology Blockchain. It is written to the blockchain by the controller of the DDO and is open to all users for reading.

The DDO specification contains the following information:
- `PublicKeys`：The information of the public key used for identity authentication, including public key id, public key type, and public key data;
- `Attributes`：All attributes make up a JSON object;
- `Recovery`：The assigned restorer can help reset the user's public key list.

For example,
```json
{
	"OntId": "did:ont:TVuF6FH1PskzWJAFhWAFg17NSitMDEBNoa",
	"Owners": [{
			"PubKeyId": "did:ont:TVuF6FH1PskzWJAFhWAFg17NSitMDEBNoa#keys-1",
			"Type": "ECDSA",
			"Curve": "nistp256",
			"Value":"022f71daef10803ece19f96b2cdb348d22bf7871c178b41f35a4f3772a8359b7d2"
		}, {
			"PublicKeyId": "did:ont:TVuF6FH1PskzWJAFhWAFg17NSitMDEBNoa#keys-2", 
			"Type": "RSA",
			"Length": 2048, 
			"Value": "3082010a...."
		}
	],
	"Attributes": {
		"OfficialCredential": {
			"Service": "PKI",
			"CN": "ont.io",
			"CertFingerprint": "1028e8f7043f12c0c2069bd7c7b3b26213964566"
		}
	},
	"Recovery": "TA63T1gxXPXWsBqHtBKcV4NhFBhw3rtkAF"
}
```

## Smart Contract Implementation Specification

“IdContract” is a smart contract implementation of the ONT ID protocol on the Ontology Blockchain platform. With the ONT IdContract, users can manage their own public key lists, modify their personal profiles, and add account restorers.

###  2.1 How to call
The external interface of the IdContract has only one main function. Its parameters include the name of the sub-function (called operation code - `op`) and the parameter lists – `params`, which is passed to the sub-function.。
```json
public static Object Main(string op, object[] params);
```
The return value of most of sub-functions is a boolean type, which represents the success or failure of the execution operation. After correct execution, an event message will be pushed to notify the caller. For the specific message type, please refer to the “**API description**” subsection.

#### The call of Ontology smart contract
By sending a transaction with a type of *InvocationTransaction*, transaction payload will include contract address and parameters. For more detailed information, please refer to [Contract Call](https://ontio.github.io/documentation/smart_contract_tutorial_overview_en.html).

### 2.2 IdContract Interface Definition
####  a. Identity registration
When the users registers their identities, they must submit a public key, and **this operation must be initiated by this public key**.

```json
bool RegIdWIthPublicKey(byte[] ontId, byte[] publicKey); 
```
 Parameters:
 - ontId: User ID, byte[] type；
 - publicKey: Public key, byte[] type.
   
 
####  b. Add a control key

The user adds a new public key to his public key list.
```json
bool AddKey(byte[] ontId, byte[] newPublicKey, byte[] sender); 
```
Parameters:
- ontId：User’s ont Id;
- newPublicKey：The new public key to be added;
- sender：The initiator of the transaction, the account's existing public key, or the restorer.
	
#### c. Delete a control key
Remove a public key from the user's public key list.
```json
bool RemoveKey(byte[] ontId, byte[] oldPublicKey, byte[] sender);
```
Parameters:
- ontId：User’s Ont ID;
- oldPublicKey：The old public key that needs to be deleted;
- sender：The initiator of the transaction, the account's existing public key, or the restorer.
	
#### d. Key recovery mechanism

Add and modify the account restorer.
```json
bool AddRecovery(byte[] ontId, byte[] recovery, byte[] publicKey);
```

In the function `AddRecovery`, `recovery` can be added if and only if the `publicKey` is the account's existing public key, and the restorer has not been set.

Parameters:
- ontId：User’s Ont ID;
- recovery：Recovery address;
- publicKey：User’s public key

```json
bool ChangeRecovery(byte[] ontId, byte[] newRecovery, byte[] oldRecovery);
```
This contract call must be initiated by oldRecovery.
Parameters:
- ontId：User’s Ont ID;
- newRecovery：New restorer
- oldRecovery：Existing restorer
	
#### e. Attribute management
The addition, deletion, and modification of the user’s attributes must be authorized by the user. An attribute consists of three parts, namely, the attribute's name, the type of the attribute's value, and the value itself. In current version of this spec, the type of attribute's value must either be described using [protocol buffers](https://developers.google.com/protocol-buffers/) or binary. For the first case, `type` must be a serialized `.proto` file. For the second case `type` must be `"binary"`. 

```json
bool AddAttribute(byte[] ontId, byte[] path, byte[] type, byte[] value, byte[] publicKey);

```
- ontId: User’s Ont ID;  path: The path of attribute name;
- type: Attribute type;  value: Attribute value;
- publicKey: The user's public key.

Must be called by a valid public key, and the `publicKey` is in the user's public key list. If the attribute does not exist, the attribute will be inserted. Otherwise the original attribute will be updated.

```json
bool AddAttributeArray(byte[] ontId, byte[] tuples, byte[] publicKey);
```

#### f. Query identity information
```json
byte[] GetDDO(byte[] ontId);
```
Return all the user's information, which is a serialization of a JSON object.

```json
byte[] GetPublicKeys(byte[] ontId);
```	
Return all the user's public keys.

#### g. Event push

IdContract contains three kinds of event messages:
- `Register`:  Push the messages related to identity registration.

	| Field | Type | Description |
	| :--- | :--- | :--- |
	|op| string | message type |
	| ontId | byte[] | registered Ont Id |

- `PublicKey`: Push the messages related to public key operations.

	| Field | Type | Description |
	| :--- | :--- | :--- |
	|op| string | message type："add" or "remove" |
	| ontId | byte[] | user's Ont Id |
	| publicKey | byte[] | public key data |

- `Attribute`: Push the messages related to attribute operations.

	| Field | Type | Description |
	| :--- | :--- | :--- |
	|op| string | message type："add"、"update"、"remove"  |
	| ontId | byte[] | user's Ont Id |
	| attrName | byte[] | attribute name |
	


## Appendix

### A. Recovery account address

The recovery account can implement a variety of access control logic, such as (m,n)-threshold control. A (m,n) threshold control account is managed by n public keys altogether. To use it, you have to gather at least m valid signatures.

- `(m, n) threshold` control account

	```
	0x02 || RIPEMD160(SHA256(n || m || publicKey_1 || ... || publicKey_n))
	```

- `AND` control account
   
   This is equivalent to (n, n) threshold control account.

- `OR` control account
  
   This is equivalent to (1, n) threshold control account.

