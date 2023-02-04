using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class FullScreenChanger : MonoBehaviour
{
    private bool _isFullScreen;

    private void Start()
    {
        _isFullScreen = Screen.fullScreen;
        GetComponent<Toggle>().isOn = _isFullScreen;
    }

    public bool IsFullScreen
    {
        set
        {
            _isFullScreen = value;
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, _isFullScreen);
            Debug.Log(_isFullScreen);
            Debug.Log(Screen.fullScreen);
        }
    }
}
