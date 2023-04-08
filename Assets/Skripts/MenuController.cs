using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject _panelSetting;
    // Start is called before the first frame update
    void Start()
    {
        _panelSetting.SetActive(false);
    }

    public void OnShowPanelSetting(bool inShow)
    {
        _panelSetting.SetActive(inShow);
    }
}