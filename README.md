# GeekPaymentSample

A .NET payment sample that use GeekPayment API for the .NET platform.

[GeekPayment API Document](https://apidoc.geekpayment.com)

## 应用标识
标记使用GeekPayment API的应用。  
由GeekPayment平台分配，没有该标识的应用不能与GeekPayment API交互。

## 数字签名
GeekPayment API对每个请求数据验签。应用生成RSA公钥/私钥，将公钥上传给GeekPayment平台，GeekPayment API使用该公钥验签。  
GeekPayment API对每个返回数据添加数字签名，应用可对返回数据验签。GeekPayment平台分配应用一个PEM格式的RSA公钥，应用使用该公钥验签。

RSA公钥/私钥，由OpenSSL生成的公钥/私钥字符串，建议密钥长度2048位。  
签名算法是SHA256WithRSA。  
示例中的私钥格式是PKCS8。

## 运行

#### 环境
.NET Core 2.2  
.NET Framework 4.5以上

#### 配置
配置应用，Geek/AppProperties.cs文件:
- AppID，应用标识。
- GeekPublicKey，GeekPayment平台分配的公钥，Base64格式。
- PrivateKey，应用的私钥，base64格式。

配置GeekPayment API，Geek/GeekPaymentProperties.cs文件：
- Scheme，传输协议
- Host，GeekPayment API的地址
