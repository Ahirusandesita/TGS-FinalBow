// 81-C# NomalScript-NewScript.cs
//
//CreateDay:2023/06/19
//Creator  :Nomura
//

using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NumberObjectData", menuName = "Scriptables/CreateNumberData")]
public class NumberObject:ScriptableObject
{
	public List<NumberObjectData> numberObject = new List<NumberObjectData>();
}

[System.Serializable]
public class NumberObjectData
{
	public string objectName;
	public GameObject numberObject;
}