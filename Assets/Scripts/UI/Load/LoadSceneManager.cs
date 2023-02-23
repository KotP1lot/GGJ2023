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

    public void LoadScene(string sceneName)
    {
        StartCoroutine(SceneChanger(sceneName));
    }

    private IEnumerator SceneChanger(string sceneName)
    {
        Time.timeScale = 1;
        _onLoadingStart?.Invoke();

        Animator loadAnimator = _loadScreenCanvas.GetComponent<Animator>();

        var scene = SceneManager.LoadSceneAsync(sceneName);

        scene.allowSceneActivation = false;

        loadAnimator.SetTrigger("Unhide");

        yield return new WaitForSeconds(0.5f);

        while (loadAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return new WaitForSeconds(0.1f);
        }

        while (scene.progress < 0.9f)
        {
            yield return new WaitForSeconds(0.1f);
        }

        scene.allowSceneActivation = true;
    }
}
