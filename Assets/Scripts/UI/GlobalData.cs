using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
using Cinemachine;

public class GlobalData : MonoBehaviour
{
    public static GlobalData instance;
    [Header("Data")]
    public int treeMaxHP = 100;
    public int startingBones = 15;
    public int startingSkulls = 0;

    [Space(6)]
    [Header("UI")]
    public TextMeshProUGUI bonesText;
    public TextMeshProUGUI skullsText;
    public TextMeshProUGUI waveText;
    public Slider treeHP;

    [Space(6)]
    [Header("Defeat")]
    [SerializeField] private UnityEvent _onDefeat;
    public Transform treeTransform;
    public CinemachineVirtualCamera virCam;
    public float waitBeforeLost = 3;

    public int bones { get; private set; }
    public int skulls { get; private set; }
    public int currentWave { get; private set; }

    public enum TowerType { Squirrel, Bigroot, Shroom, Rose, Bear }
    public enum CursorType { Click, Hand, Grab, Cross, Point }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        bones = 0;
        skulls = 0;
        currentWave = 0;

        treeHP.maxValue = treeMaxHP;
        treeHP.value = treeMaxHP;

        GetBones(startingBones);
        GetSkulls(startingSkulls);
        NextWave();
    }

    public void GetBones(int amount)
    {
        bones += amount;
        if (bones > 999) bones = 999;

        bonesText.text = bones.ToString();
    }
    public void SpendBones(int amount)
    {
        bones -= amount;
        if (bones < 0) bones = 0;

        bonesText.text = bones.ToString();
    }

    public void GetSkulls(int amount)
    {
        skulls += amount;
        if (skulls > 999) skulls = 999;

        skullsText.text = skulls.ToString();
    }

    public void DamageTree(float amount)
    {
        treeHP.value -= amount;

        if(treeHP.value == 0)
        {
            virCam.Follow = treeTransform;
            Time.timeScale = 0;

            StartCoroutine(endGame());
        }
    }

    private IEnumerator endGame()
    {
        yield return new WaitForSecondsRealtime(waitBeforeLost);
        _onDefeat?.Invoke();
    }

    public void HealTree(float amount)
    {
        treeHP.value += amount;
    }

    public void NextWave()
    {
        currentWave++;

        waveText.text = $"WAVE {currentWave}";
    }

    public float Distance(Vector2 v1, Vector2 v2)
    {
        Vector3 difference = new Vector3(v1.x - v2.x, v1.y - v2.y);
        return Mathf.Sqrt(Mathf.Pow(difference.x, 2f) + Mathf.Pow(difference.y, 2f));
    }

    private void Update()
    {

    }
}
