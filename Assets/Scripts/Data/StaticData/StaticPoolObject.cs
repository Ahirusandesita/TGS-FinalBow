// --------------------------------------------------------- 
// StaticPoolObject.cs 
// 
// CreateDay: 2023/06/08
// Creator  : TakayanagiSora
// --------------------------------------------------------- 
using UnityEngine;
public class StaticPoolObject
{
    public static GameObject poolController = default;
}

public class SettingPoolController :MonoBehaviour
{
    private void Start()
    {
        StaticPoolObject.poolController = this.gameObject;
    }
}