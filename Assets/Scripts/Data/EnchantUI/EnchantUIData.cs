// --------------------------------------------------------- 
// EnchantUIData.cs 
// 
// CreateDay: 2023/06/28
// Creator  : Nomura
// --------------------------------------------------------- 
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "EnchantUIData", menuName = "Scriptables/CreateEnchantUIData")]
public class EnchantUIData : ScriptableObject
{
    public List<EnchantUI> _enchantUIs = new List<EnchantUI>();
}

[System.Serializable]
public class EnchantUI
{
    public string UIName;
    public Image UIImage;
}