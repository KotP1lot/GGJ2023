using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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


    public int bones { get; private set; }
    public int skulls { get; private set; }
    public int currentWave { get; private set; }

    public enum TowerType { Squirrel, Bigroot, Shroom, Rose, Bear }
    public enum CursorType { Click, Hand, Grab, Cross }

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

    public void Yo(string type)
    {
        Debug.Log("yo " + type);
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
        if (skulls > 99) skulls = 99;

        skullsText.text = skulls.ToString();
    }
    public void SpendSkullss(int amount)
    {
        skulls -= amount;
        if (skulls < 0) skulls = 0;

        skullsText.text = skulls.ToString();
    }

    public void DamageTree(int amount)
    {
        treeHP.value -= amount;
    }

    public void HealTree(int amount)
    {
        treeHP.value += amount;
    }

    public void NextWave()
    {
        currentWave++;

        waveText.text = $"WAVE {currentWave}";
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            HealTree(10);
        }
    }
}
