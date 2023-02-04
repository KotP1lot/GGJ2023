using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TutorialImageGallery : MonoBehaviour
{
    [SerializeField] private List<GameObject> _images;

    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;

    private int _currentImage = 0;

    private void Start()
    {
        foreach (GameObject image in _images)
        {
            image.SetActive(false);
        }

        _images[_currentImage].SetActive(true);

        _leftButton.interactable = false;
    }

    public void SwitchToLeft()
    {
        if (_currentImage - 1 <= 0)
        {
            _leftButton.interactable = false;
        }

        if (_currentImage - 1 < _images.Count - 1)
        {
            _rightButton.interactable = true;
        }

        _images[_currentImage].SetActive(false);
        _currentImage--;
        _images[_currentImage].SetActive(true);
    }

    public void SwitchToRight()
    {
        if (_currentImage + 1 > 0)
        {
            _leftButton.interactable = true;
        }

        if (_currentImage + 1 >= _images.Count - 1)
        {
            _rightButton.interactable = false;
        }

        _images[_currentImage].SetActive(false);
        _currentImage++;
        _images[_currentImage].SetActive(true);
    }
}
