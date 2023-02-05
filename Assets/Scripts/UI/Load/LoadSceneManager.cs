using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine;

public class LoadSceneManager : MonoBehaviour
{
    public static LoadSceneManager Instance;

    [Header("Screens")]
    [SerializeField] private GameObject _loadScreenCanvas;

    [Header("Events")]
    [SerializeField] private UnityEvent _onLoadingStart;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            _loadScreenCanvas?.GetComponent<Animator>().SetTrigger("Hide");
        }
        else
        {
            Destroy(Instance);
        }
    }

    public async void LoadScene(string sceneName)
    {
        Time.timeScale = 1;
        _onLoadingStart?.Invoke();

        Animator loadAnimator = _loadScreenCanvas.GetComponent<Animator>();

        var scene = SceneManager.LoadSceneAsync(sceneName);

        scene.allowSceneActivation = false;

        loadAnimator.SetTrigger("Unhide");

        await Task.Delay(500);

        while (loadAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            await Task.Delay(100);
        }

        while (scene.progress < 0.9f)
        {
            await Task.Delay(100);
        }

        //loadAnimator.SetTrigger("Finish");

        //await Task.Delay(500);

        //while (loadAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        //{
        //    Debug.Log("Aga 3");
        //    await Task.Delay(100);
        //}

        scene.allowSceneActivation = true;
    }
}
