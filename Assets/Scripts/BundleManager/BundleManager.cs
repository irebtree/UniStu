using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using System.Threading;
using UnityEngine.UI;
public class LoadedAssetBundle
{
	public AssetBundle m_AssetBundle;
	public int m_ReferencedCount;
	
	public LoadedAssetBundle(AssetBundle assetBundle)
	{
		m_AssetBundle = assetBundle;
		m_ReferencedCount = 0;
	}
}

public class BundleManager : MonoBehaviour 
{
	public static BundleManager instance;
	public static Dictionary<string, LoadedAssetBundle> loadedAssetBundles{get;private set;}
	public delegate void OnCacheBundleLoaded();
	public static event OnCacheBundleLoaded onCacheBundleLoaded;

	static string ip = "http://192.168.0.200";
	static string basePath;
	static string prefix = "file://";
	static string bundlePathLocal;
	public static string bundlePathRemote;
	static string manifestPath;

	static Dictionary<string, string[]> m_Dependencies = new Dictionary<string, string[]> ();
    static Dictionary<string, string> bundleVariants;
	static List<string> loadingBundle = new List<string>();
	static AssetBundleManifest manifest = null;
	public string manifestExtension = ".txt";
	public GameObject warmingWindow;
	public GameObject progress;

	bool updateProg = true;
	bool isShowing = false;
	int counter=0;

	static object lockObj = new object();
	
	public bool isReady 
	{
		get { return !object.ReferenceEquals(manifest, null);}
	}

	
	public static string GetPlatform()
	{
		#if UNITY_IOS
		return "iOS";
		#elif UNITY_ANDROID
		return "Android";
		#elif UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
		return "PC";
		#elif UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
		return "OSX";
		#elif UNITY_WEBPLAYER
		return "Web";
		#elif UNITY_WP8
		return"WP8";
		#else
		return "error";
		Debug.Log("unsupported platform");
		#endif
	}
	
	
}




