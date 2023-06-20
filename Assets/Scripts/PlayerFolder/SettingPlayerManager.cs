using UnityEngine;

public class SettingPlayerManager : MonoBehaviour
{
    void Awake()
    {
        StaticPlayerManager.PlayerManager = this.gameObject.GetComponent<PlayerManager>();
    }
}
