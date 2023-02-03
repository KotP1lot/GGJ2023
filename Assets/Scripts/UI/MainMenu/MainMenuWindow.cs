using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuWindow : MonoBehaviour
{
    private Animator _animator;

    private bool _isOpened = false;

    public bool IsOpened 
    { 
        set 
        { 
            _isOpened = value;
            _animator.SetBool("Open", _isOpened);
        } 
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void CheckWindowStatus()
    {
        _isOpened = !_isOpened;
        _animator.SetBool("Open", _isOpened);
    }
}
