using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using System;

public class PauseController : MonoBehaviour
{
    [SerializeField] private UnityEvent _onPauseOpen;
    [SerializeField] private UnityEvent _onPauseClose;

    private bool _isActive = false;

    public bool IsActive 
    { 
        set 
        {
            _isActive = value;

            if (_isActive)
            {
                _onPauseOpen.Invoke();
            }
            else
            {
                _onPauseClose.Invoke();
            }

            Time.timeScale = Convert.ToInt32(!_isActive);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            IsActive = !_isActive;
        }
    }
}
