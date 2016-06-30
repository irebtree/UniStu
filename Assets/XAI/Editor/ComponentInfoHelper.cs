using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
#if UNITY_EDITOR
using System.Reflection;
using System.Linq;
using UnityEditor;
public class ComponentInfoHelper  {
	[MenuItem("Test/oooo")]
	static void  MTest()
	{
		List<Type> lis= FindAllDerivedTypes<XBaseState>();
		Debug.Log(lis.Count);
	}

	public  static List<Type> FindAllDerivedTypes<T>()
	{
		return FindAllDerivedTypes<T>(Assembly.GetAssembly(typeof(T)));
	}


	private static List<Type> FindAllDerivedTypes<T>(Assembly assembly)
	{
		Type derivedType = typeof(T);
		return assembly.GetTypes().Where(t => t != derivedType && derivedType.IsAssignableFrom(t) && !t.IsAbstract).ToList();
	}


}
#endif
