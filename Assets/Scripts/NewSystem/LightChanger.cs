using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChanger : MonoBehaviour
{
    [SerializeField]
    private GameObject _tutorialLight = default;

    [SerializeField]
    private GameObject _mainLight = default;

    private GameProgress _gameProgrss = default;

    private void Awake()
    {
        _gameProgrss = FindObjectOfType<GameProgress>();
        _gameProgrss.readOnlyGameProgressProperty.Subject.Subscribe(
            progressType =>
            {
                if (progressType == GameProgressType.tutorial)
                {
                    _mainLight.SetActive(false);
                    _tutorialLight.SetActive(true);
                }

                if (progressType == GameProgressType.gamePreparation)
                {
                    _tutorialLight.SetActive(false);
                    _mainLight.SetActive(true);
                }

                if (progressType == GameProgressType.result)
                {
                    _mainLight.SetActive(false);
                    _tutorialLight.SetActive(true);
                }
            });
    }
}
