using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameResultScreen : MonoBehaviour
{
    [SerializeField] private bool _isLoseScreen;
    [SerializeField] private TextMeshProUGUI _description;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Show()
    {
        Time.timeScale = 0;

        if (_isLoseScreen)
        {
            GlobalData info = GlobalData.instance;
            _description.text =
                $"But you killed {info.skulls} enemies in {info.currentWave} waves. Good job!";
        }

        _animator.SetTrigger("Unhide");
    }
}
