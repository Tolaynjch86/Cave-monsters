using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Settings : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
     private Resolution[] _resolutions;
    private List<Resolution> _fileterdResolutions = new List<Resolution>();
    private int _currentResolutionIndex;
    private float _currentResolutionRefreshRate;
    private FullScreenMode _fullscreenMode;
   
    void Start()
    {
        _resolutions = Screen.resolutions;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            string resolution = _resolutions[i].width + "X" + _resolutions[i].height + " " + _resolutions[i].refreshRate + " Ãö";
            print(resolution);
        }

        _resolutionDropdown.ClearOptions();
        _currentResolutionRefreshRate = Screen.currentResolution.refreshRate;
        for (int i = 0; i < _resolutions.Length; i++)
        {
            if (_resolutions[i].refreshRate == _currentResolutionRefreshRate)
            {
                _fileterdResolutions.Add(_resolutions[i]);
            }
        }
        List<string> options = new List<string>();
        for (int i = 0; i < _fileterdResolutions.Count; i++)
        {
            string resolution = _fileterdResolutions[i].width + "X" + _fileterdResolutions[i].height + " " + _fileterdResolutions[i].refreshRate + " Ãö";
            options.Add(resolution);
            if (_fileterdResolutions[i].width == Screen.width && _fileterdResolutions[i].height == Screen.height)
            {
                _currentResolutionIndex = i;
            }
        }
        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = _currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _fileterdResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height,_fullscreenMode);
    }

    public void SetFullScreenMode(bool isFullScreen)
    {
        if (isFullScreen == true)
        {
            _fullscreenMode = FullScreenMode.FullScreenWindow;
        }
        else
        {
            _fullscreenMode=FullScreenMode.Windowed;
        }

        Screen.fullScreenMode = _fullscreenMode;
    }
}
