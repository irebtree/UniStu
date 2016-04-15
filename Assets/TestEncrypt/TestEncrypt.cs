using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
public class TestEncrypt : MonoBehaviour {
    public Texture2D tex;

    public Texture2D ttt;

    string cryKet = "yinweijuyinweijuyinweiju12345678";
	// Use this for initialization
	void Start () {
       // Test();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Test()
    {
        byte[] bytes = tex.EncodeToJPG();
        string pathPreFix = Application.dataPath;
        string format = "fi.jpg";
        // Debug.Log(pathPreFix);
        // ttt = new Texture2D(400, 533);
        // ttt.LoadImage(bytes);
        // byte[] enCrypt = Encrypt(bytes);
        // byte[] decrypt = Decrypt(enCrypt);
         SaveFile(bytes, pathPreFix, format);
    }

    public void Save()
    {
        StartCoroutine(DoSave());
    }

    IEnumerator DoSave()
    {
        string pathPreFix = Application.dataPath;
        string format = "fff.txt";
        string path = pathPreFix + "/" + format;
        Debug.Log("file in : " + path);

        //WWW www = new WWW(path);
        //yield return www;

       // byte[] readData = www.bytes;

        FileStream fs = new FileStream(path, FileMode.Open);
        byte[] readData = new byte[fs.Length];
        fs.Read(readData, 0, readData.Length);

        // ttt = new Texture2D(400, 533);
        // ttt.LoadImage(readData);
        byte[] encryptData = Encrypt(readData);

        SaveFile(encryptData, pathPreFix, "efff.txt");
        yield return 0;
    }


    byte[] Encrypt(byte[] toEncryptArray)
    {
        RijndaelManaged rDel = new RijndaelManaged();
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes(cryKet);//UnicodeEncoding.Default.GetBytes(cryKet);
        rDel.Key = keyArray;
        rDel.Mode = CipherMode.ECB;
        rDel.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = rDel.CreateEncryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
        return resultArray;
    }


    byte[] Decrypt(byte[] toDecrypt)
    {
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes(cryKet);//UnicodeEncoding.Default.GetBytes(cryKet);
        RijndaelManaged rDel = new RijndaelManaged();
        rDel.Key = keyArray;
        rDel.Mode = CipherMode.ECB;
        rDel.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = rDel.CreateDecryptor();
        byte[] result = cTransform.TransformFinalBlock(toDecrypt, 0, toDecrypt.Length);
        return result;
    }

    void SaveFile(byte[] fileArray, string pathPrefix, string format)
    {
        string path = pathPrefix + "/" + format;
        Debug.Log("Save in : " + path);
        // File.WriteAllBytes(path, fileArray);

        FileInfo fi = new FileInfo(path);
        Stream sw = fi.Create();
        sw.Write(fileArray, 0, fileArray.Length);
        sw.Close();
        sw.Dispose();
        
     }

     public void ReadFile()
     {
        //string pathPreFix = Application.dataPath;
        // string format = "pgtest.jpg";
        // string path = pathPreFix + "/" + format;
        //MemoryStream ms = new MemoryStream(); 
        //  StreamReader sr = new StreamReader(path);

        // FileStream fs = new FileStream(path, FileMode.Open);
        // byte[] data = new byte[fs.Length];
        // fs.Read(data, 0, data.Length);

        //  byte[] cryptData = Decrypt(data);
        //ttt = new Texture2D(100, 100);
        // ttt.LoadImage(cryptData);
        StartCoroutine(ToLoadFile());
     }

    IEnumerator ToLoadFile()
    {
        string pathPreFix = Application.dataPath;
        string format = "fff.txt";
        string path = pathPreFix + "/" + format;
        Debug.Log("file in : " + path);
       // WWW www = new WWW(path);
        //yield return www;

         FileStream fs = new FileStream(path, FileMode.Open);
         byte[] data = new byte[fs.Length];
         fs.Read(data, 0, data.Length);

        //byte[] dataByte = www.bytes;
        byte[] cryptData = Decrypt(data);
        yield return 0;
         //ttt = new Texture2D(400, 533);
        // ttt.LoadImage(dataByte);

  
    }

    public void DoLoadWWW()
    {
        StartCoroutine(LoadWWW()); 
    }

    IEnumerator LoadWWW()
    {
        string url ="file://" + Application.dataPath + "/" + "efff.txt";
        Debug.Log("www == " + url);
        WWW www = new WWW(url);
        yield return www;

        byte[] encData = www.bytes;
        byte[] deData = Decrypt(encData);

        AssetBundle ab = AssetBundle.LoadFromMemory(deData);
        yield return ab;
       // AssetBundle ab = www.assetBundle;
        GameObject go = (GameObject) ab.LoadAsset("template00");
        Instantiate(go);
        
     }
}
