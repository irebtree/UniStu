using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;
using System.Security.Cryptography;
using System.Text;
/// <summary>
/// 文件加密
/// </summary>
public class EncryptScript : EditorWindow {

    static string cryKey = "yinweijuyinweijuyinweiju12345678";
    static string outputPath;
    [MenuItem("EncryptData/DoEncrypt")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        outputPath = "";
        EncryptScript window = (EncryptScript)EditorWindow.GetWindow(typeof(EncryptScript));
        window.title = "文件加密";
        window.Show();
    }
 
    void OnGUI()
    {
        GUILayout.Label("设置：", EditorStyles.boldLabel);

        cryKey = EditorGUILayout.PasswordField("设置加密的密码（32位）:", cryKey);
        //EditorGUILayout.LabelField("已输入 :" + cryKey);
        EditorGUILayout.SelectableLabel("已输入 :" + cryKey);
        if(cryKey.Length>0)
            GUILayout.Label("已输入： " + cryKey.Length + " 位", EditorStyles.boldLabel);

        GUILayout.BeginHorizontal("box");
        GUILayout.Label("选择一个文件夹进行加密: ", EditorStyles.boldLabel, GUILayout.Width(150));
        if (GUILayout.Button("选择", GUILayout.Width(80)))
        {
            // Debug.Log("jiami");
            EncryptHandle();
        }
        GUILayout.EndHorizontal();

        EditorGUILayout.HelpBox("密码必须是32位，文件夹里的文件都将被进行加密，不包含下一层文件夹的文件：", MessageType.Info);
        GUILayout.Label("Save in : " + outputPath, EditorStyles.boldLabel);
        
    }

     //[MenuItem("EncryptData/DoEncrypt")]
    static void EncryptHandle()
    {
        string path = EditorUtility.OpenFolderPanel("Select a folder", "", "");
        if (string.IsNullOrEmpty(path))
            return;
          
          string[] files = Directory.GetFiles(path);
         // string rootDire = Directory.GetDirectoryRoot(path);

          //Debug.Log("path :" + path);
         // Debug.Log("rootDir : " + rootDire);
          outputPath = path + "/" + "Encrypted";
          if(!Directory.Exists(outputPath))
          {
              Directory.CreateDirectory(outputPath);
           }

          foreach (string readPath in files)
          {
              Debug.Log("file : " + readPath);
              string savePath = readPath.Replace(path, path + "/" + "Encrypted");
              Debug.Log("fileToSave : " + savePath);

             byte[] readByte = ReadFile(readPath);
             byte[] encryptedByte = ToEncrypt(readByte);

             WriteFile(encryptedByte, savePath);
          }
          Debug.Log("加密完成，保存在： " + outputPath);
          
    }

     static byte[] ReadFile(string path)
     {
        FileStream fs = new FileStream(path, FileMode.Open);
        byte[] data = new byte[fs.Length];
        fs.Read(data, 0, data.Length);
        return data;
    }

   static void WriteFile(byte[] data, string path)
    {
        Debug.Log("Save in : " + path);
        // File.WriteAllBytes(path, fileArray);

        FileInfo fi = new FileInfo(path);
        Stream sw = fi.Create();
        sw.Write(data, 0, data.Length);
        sw.Close();
        sw.Dispose();
    }

     //加密
   static byte[] ToEncrypt(byte[] toE)
    {
        //加密解密使用相同的key，key必须是32位
        RijndaelManaged rDel = new RijndaelManaged();
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes(cryKey);//UnicodeEncoding.Default.GetBytes(cryKet);
        rDel.Key = keyArray;
        rDel.Mode = CipherMode.ECB;
        rDel.Padding = PaddingMode.PKCS7;

        ICryptoTransform eTransform = rDel.CreateEncryptor();
        byte[] encryptData = eTransform.TransformFinalBlock(toE, 0, toE.Length);
        return encryptData;
    }

    //解密
    byte[] ToDecrypt(byte[] toD)
    {
        byte[] keyArray = UTF8Encoding.UTF8.GetBytes(cryKey);//UnicodeEncoding.Default.GetBytes(cryKet);
        RijndaelManaged rDel = new RijndaelManaged();
        rDel.Key = keyArray;
        rDel.Mode = CipherMode.ECB;
        rDel.Padding = PaddingMode.PKCS7;

        ICryptoTransform dTransform = rDel.CreateDecryptor();
        byte[] decryptData = dTransform.TransformFinalBlock(toD, 0, toD.Length);
        return decryptData;
    }
}
