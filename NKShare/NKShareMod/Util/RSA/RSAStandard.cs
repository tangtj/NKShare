using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;

namespace NKShare.NKShareMod.Uitly.RSA {
    class RSAStandard {
        /// <summary>   
        /// 生成xml格式的公钥、私钥，长度为2048
        /// </summary>   
        /// <returns>公钥键"PUBLIC",私钥键"PRIVATE"</returns>   
        private Dictionary<string, string> createXmlKeys() {
            Dictionary<string, string> keyPair = new Dictionary<string, string>();
            RSACryptoServiceProvider provider = new RSACryptoServiceProvider(2048);
            keyPair.Add("PUBLIC", provider.ToXmlString(false));
            keyPair.Add("PRIVATE", provider.ToXmlString(true));
            return keyPair;
        }
        /// <summary>   
        /// 生成标准格式（pub/X509、pvt/PKCS8）的公钥、私钥，长度为2048
        /// </summary>   
        /// <returns>公钥键"PUBLIC",私钥键"PRIVATE"</returns>
        public Dictionary<string, string> createRSAKeys() {
            Dictionary<string, string> xmlKeys = createXmlKeys();
            string xmlPub = xmlKeys["PUBLIC"];
            string xmlPvt = xmlKeys["PRIVATE"];
            Dictionary<string, string> RSAKeys = new Dictionary<string, string>();
            RSAKeys.Add("PUBLIC", xmlPub);
            RSAKeys.Add("PRIVATE", xmlPvt);
            return RSAKeys;
        }
        /// <summary>  
        /// RSA私钥格式转换，.net->java  
        /// </summary>  
        /// <param name="privateKey">.net生成的私钥</param>  
        /// <returns></returns>  
        private string RSAPrivateKeyDotNet2Java(string privateKey) {
            try {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(privateKey);
                BigInteger m = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Modulus")[0].InnerText));
                BigInteger exp = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Exponent")[0].InnerText));
                BigInteger d = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("D")[0].InnerText));
                BigInteger p = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("P")[0].InnerText));
                BigInteger q = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Q")[0].InnerText));
                BigInteger dp = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("DP")[0].InnerText));
                BigInteger dq = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("DQ")[0].InnerText));
                BigInteger qinv = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("InverseQ")[0].InnerText));
                RsaPrivateCrtKeyParameters privateKeyParam = new RsaPrivateCrtKeyParameters(m, exp, d, p, q, dp, dq, qinv);
                PrivateKeyInfo privateKeyInfo = PrivateKeyInfoFactory.CreatePrivateKeyInfo(privateKeyParam);
                byte[] serializedPrivateBytes = privateKeyInfo.ToAsn1Object().GetEncoded();
                return Convert.ToBase64String(serializedPrivateBytes);
            } catch {
                return null;
            }
        }
        /// <summary>  
        /// RSA公钥格式转换，.net->java  
        /// </summary>  
        /// <param name="publicKey">.net生成的公钥</param>  
        /// <returns></returns>  
        private string RSAPublicKeyDotNet2Java(string publicKey) {
            try {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(publicKey);
                BigInteger m = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Modulus")[0].InnerText));
                BigInteger p = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Exponent")[0].InnerText));
                RsaKeyParameters pub = new RsaKeyParameters(false, m, p);
                SubjectPublicKeyInfo publicKeyInfo = SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(pub);
                byte[] serializedPublicBytes = publicKeyInfo.ToAsn1Object().GetDerEncoded();
                return Convert.ToBase64String(serializedPublicBytes);
            } catch {
                return null;
            }
        }
        /// <summary>
        /// RSA私钥格式转换，java->.net
        /// </summary>
        /// <param name="privateKey">java生成的RSA私钥</param>
        /// <returns></returns>
        private string RSAPrivateKeyJava2DotNet(string privateKey) {
            try {
                RsaPrivateCrtKeyParameters privateKeyParam = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(Convert.FromBase64String(privateKey));
                return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent><P>{2}</P><Q>{3}</Q><DP>{4}</DP><DQ>{5}</DQ><InverseQ>{6}</InverseQ><D>{7}</D></RSAKeyValue>",
                    Convert.ToBase64String(privateKeyParam.Modulus.ToByteArrayUnsigned()),
                    Convert.ToBase64String(privateKeyParam.PublicExponent.ToByteArrayUnsigned()),
                    Convert.ToBase64String(privateKeyParam.P.ToByteArrayUnsigned()),
                    Convert.ToBase64String(privateKeyParam.Q.ToByteArrayUnsigned()),
                    Convert.ToBase64String(privateKeyParam.DP.ToByteArrayUnsigned()),
                    Convert.ToBase64String(privateKeyParam.DQ.ToByteArrayUnsigned()),
                    Convert.ToBase64String(privateKeyParam.QInv.ToByteArrayUnsigned()),
                    Convert.ToBase64String(privateKeyParam.Exponent.ToByteArrayUnsigned()));
            } catch {
                return null;
            }
        }
        /// <summary>  
        /// RSA公钥格式转换，java->.net  
        /// </summary>  
        /// <param name="publicKey">java生成的公钥</param>  
        /// <returns></returns>  
        private string RSAPublicKeyJava2DotNet(string publicKey) {
            try {
                RsaKeyParameters publicKeyParam = (RsaKeyParameters)PublicKeyFactory.CreateKey(Convert.FromBase64String(publicKey));
                return string.Format("<RSAKeyValue><Modulus>{0}</Modulus><Exponent>{1}</Exponent></RSAKeyValue>",
                    Convert.ToBase64String(publicKeyParam.Modulus.ToByteArrayUnsigned()),
                    Convert.ToBase64String(publicKeyParam.Exponent.ToByteArrayUnsigned()));
            } catch {
                return null;
            }
        }
        /// <summary>  
        /// RSA加密,使用xml公钥
        /// </summary>  
        /// <param name="encryptString">待加密的字符串(建议utf-8)</param>  
        /// <param name="xmlPublicKey">xml公钥</param>  
        /// <returns>Base64形式密文</returns>  
        private string RSAEncryptByXmlPub(string srcStr, string xmlPublicKey) {
            try {
                byte[] PlainTextBArray;
                byte[] CypherTextBArray;
                string Result;
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPublicKey);
                PlainTextBArray = (new UTF8Encoding()).GetBytes(srcStr);
                CypherTextBArray = rsa.Encrypt(PlainTextBArray, false);
                Result = Convert.ToBase64String(CypherTextBArray);
                return Result;
            } catch {
                return null;
            }
        }
        /// <summary>
        /// 标准RSA加密，使用标准公钥
        /// </summary>
        /// <param name="src">待加密的字符串(建议utf-8)</param>
        /// <param name="pubKey">标准公钥</param>
        /// <returns>Base64形式密文</returns>
        public string RSAEncryptByPub(string src, string pubKey) {
            try {
                return RSAEncryptByXmlPub(src, RSAPublicKeyJava2DotNet(pubKey));
            } catch {
                return null;
            }

        }
        /// <summary>  
        /// RSA解密,使用xml私钥
        /// </summary>  
        /// <param name="xmlPrivateKey">私钥</param>  
        /// <param name="decryptString">待解密的字符串</param>  
        /// <returns></returns>  
        private string RSADecryptByXmlPvt(string decryptString, string xmlPrivateKey) {
            try {
                byte[] PlainTextBArray;
                byte[] DypherTextBArray;
                string Result;
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPrivateKey);
                PlainTextBArray = Convert.FromBase64String(decryptString);
                DypherTextBArray = rsa.Decrypt(PlainTextBArray, false);
                Result = (new UTF8Encoding()).GetString(DypherTextBArray);
                return Result;
            } catch {
                return null;
            }
        }
        /// <summary>
        /// 标准RSA解密，使用标准私钥
        /// </summary>
        /// <param name="src"></param>
        /// <param name="pvtKey"></param>
        /// <returns></returns>
        public string RSADecryptByPvt(string src, string pvtKey) {
            try {
                return RSADecryptByXmlPvt(src, RSAPrivateKeyJava2DotNet(pvtKey));
            } catch {
                return null;
            }
        }
        /// <summary>  
        /// RSA加密,使用xml私钥
        /// </summary>  
        /// <param name="xmlPrivateKey">xml私钥</param>  
        /// <param name="m_strEncryptString">待加密数据</param>  
        /// <returns>加密后的数据（Base64）</returns>  
        public string RSAEncryptByXmlPvt(string strEncryptString, string xmlPrivateKey) {
            try {
                //加载私钥  
                RSACryptoServiceProvider privateRsa = new RSACryptoServiceProvider();
                privateRsa.FromXmlString(xmlPrivateKey);
                //转换密钥  
                AsymmetricCipherKeyPair keyPair = DotNetUtilities.GetKeyPair(privateRsa);
                IBufferedCipher c = CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");// 参数与Java中加密解密的参数一致       
                                                                                      //第一个参数为true表示加密，为false表示解密；第二个参数表示密钥  
                c.Init(true, keyPair.Private);
                byte[] DataToEncrypt = Encoding.UTF8.GetBytes(strEncryptString);
                byte[] outBytes = c.DoFinal(DataToEncrypt);//加密  
                string strBase64 = Convert.ToBase64String(outBytes);
                return strBase64;
            } catch {
                return null;
            }
        }
        /// <summary>
        /// 标准RSA加密，使用标准私钥
        /// </summary>
        /// <param name="src">源内容，建议uft-8</param>
        /// <param name="pvtKey">标准私钥</param>
        /// <returns>密文，base64形式</returns>
        public string RSAEncryptByPvt(string src, string pvtKey) {
            try {
                return RSAEncryptByXmlPvt(src, RSAPrivateKeyJava2DotNet(pvtKey));
            } catch {
                return null;
            }
        }
        /// <summary>
        /// RSA解密,使用xml公钥
        /// </summary>
        /// <param name="xmlPublicKey">xml公钥</param>
        /// <param name="strEncryptString">Base64密文</param>
        /// <returns>明文，utf-8编码</returns>
        public string RSADecryptByXmlPub(string strEncryptString, string xmlPublicKey) {
            try {
                //得到公钥
                RsaKeyParameters keyParams = RSAPublicKeyDotNet2DoNetObj(xmlPublicKey);
                //参数与Java中加密解密的参数一致
                IBufferedCipher c = CipherUtilities.GetCipher("RSA/ECB/PKCS1Padding");
                //第一个参数 true-加密，false-解密；第二个参数表示密钥
                c.Init(false, keyParams);
                //对密文进行base64解码
                byte[] dataFromEncrypt = Convert.FromBase64String(strEncryptString);
                //解密
                byte[] outBytes = c.DoFinal(dataFromEncrypt);
                //明文
                string clearText = Encoding.UTF8.GetString(outBytes);
                return clearText;
            } catch {
                return null;
            }
        }
        /// <summary>
        /// 标准RSA解密，使用标准公钥
        /// </summary>
        /// <param name="src">Base64密文</param>
        /// <param name="pubKey">标准公钥</param>
        /// <returns>明文，utf-8编码</returns>
        public string RSADecryptByPub(string src, string pubKey) {
            try {
                return RSADecryptByXmlPub(src, RSAPublicKeyJava2DotNet(pubKey));
            } catch {
                return null;
            }
        }
        /// <summary>
        /// 将xml公钥转成c#对象
        /// </summary>
        /// <param name="publicKey">xml公钥</param>
        /// <returns>RsaKeyParameters对象</returns>
        private RsaKeyParameters RSAPublicKeyDotNet2DoNetObj(string publicKey) {
            try {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(publicKey);
                BigInteger m = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Modulus")[0].InnerText));
                BigInteger p = new BigInteger(1, Convert.FromBase64String(doc.DocumentElement.GetElementsByTagName("Exponent")[0].InnerText));
                RsaKeyParameters pub = new RsaKeyParameters(false, m, p);
                return pub;
            } catch {
                Console.WriteLine("RSAPublicKeyDotNet2DoNetObj() 异常");
            }
            return null;
        }
        /// <summary>   
        /// 截取字节数组部分字节   
        /// </summary>   
        /// <param name="input"></param>   
        /// <param name="offset">起始偏移位</param>   
        /// <param name="length">截取长度</param>   
        /// <returns></returns>   
        private static byte[] getSplit(byte[] input, int offset, int length) {
            byte[] output = new byte[length];
            for (int i = offset; i < offset + length; i++) {
                output[i - offset] = input[i];
            }
            return output;
        }
    }
}