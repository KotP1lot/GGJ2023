using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;
using UnityEngine.UI;

public class TowerMenu : MonoBehaviour
{
    public Tower tower;
    [Space(7)]
    public GameObject enabler;
    [Space(7)]
    public GameObject destroyButton;
    public GameObject upgradeButton;
    public GameObject statPanel;
    public GameObject cost;
    [Space(7)]
    public TextMeshProUGUI costText;
    public TextMeshProUGUI DMGText;
    public TextMeshProUGUI SPDText;
    public TextMeshProUGUI RNGText;
    public Color textColor;
    public Color upgradeColor;
    [Space(7)]
    public CircleRenderer towerRange;
    [Space(7)]
    public Image lvlStar;
    public Sprite[] levels;
    public ParticleSystem upgradeParticle;

    private Animator destroyAnimator;
    private Animator upgradeAnimator;
    private Animator statPanelAnimator;

    private CursorChanger upgradeChanger;

    private PlayerMovement player;
    private BuildTowers towerBuilder;

    private Button upgrade;
    private Button destroyer;

    private bool Enabled = false;
    
    void Start()
    {
        destroyAnimator = destroyButton.GetComponent<Animator>();
        upgradeAnimator = upgradeButton.GetComponent<Animator>();
        statPanelAnimator = statPanel.GetComponent<Animator>();

        upgradeChanger = upgradeButton.GetComponent<CursorChanger>();

        upgrade = upgradeButton.GetComponent<Button>();
        destroyer = destroyButton.GetComponent<Button>();

        var playerObj = GameObject.Find("Player");
        player = playerObj.GetComponent<PlayerMovement>();
        towerBuilder = playerObj.GetComponent<BuildTowers>();

        destroyer.interactable = false;
        upgrade.interactable = false;
    }

    void Update()
    {
        cost.SetActive(upgradeChanger.isHovered);

        var curLvl = tower.GetLvLInfo(tower.GetCurrentLvL());
        var nextLvl = tower.GetLvLInfo(tower.GetCurrentLvL() + 1);

        towerRange.radius = curLvl.Range;
 
        if (upgradeChanger.isHovered && nextLvl.Damage != curLvl.Damage)
        {
            DMGText.color = upgradeColor;
            DMGText.text = nextLvl.Damage.ToString("F1", CultureInfo.InvariantCulture);
        }
        else
        {
            DMGText.color = textColor;
            DMGText.text = curLvl.Damage.ToString("F1", CultureInfo.InvariantCulture);
        }

        if (upgradeChanger.isHovered && nextLvl.AttackSpeed != curLvl.AttackSpeed)
        {
            SPDText.color = upgradeColor;
            SPDText.text = nextLvl.AttackSpeed.ToString("F1", CultureInfo.InvariantCulture);
        }
        else
        {
            SPDText.color = textColor;
            SPDText.text = curLvl.AttackSpeed.ToString("F1", CultureInfo.InvariantCulture);
        }

        if (upgradeChanger.isHovered && nextLvl.Range != curLvl.Range)
        {
            RNGText.color = upgradeColor;
            RNGText.text = nextLvl.Range.ToString("F1", CultureInfo.InvariantCulture);
        }
        else
        {
            RNGText.color = textColor;
            RNGText.text = curLvl.Range.ToString("F1", CultureInfo.InvariantCulture);
        }
        costText.text = nextLvl.LvLCost.ToString();
        
        if (GlobalData.instance.Distance(tower.transform.position,player.transform.position) <= player.buildRadius)
        {
            enabler.SetActive(true);
        }
        else
        {
            enabler.SetActive(false);
            if (Enabled) MenuEnabled(false);
        }

        if (tower.isMaxLvl())
        {
            upgrade.interactable = false;

            cost.SetActive(false);

            upgradeChanger.selectedType = GlobalData.CursorType.Cross;
        }
        else if(GlobalData.instance.bones < nextLvl.LvLCost && Enabled)
        {
            upgrade.interactable = false;
            upgradeChanger.selectedType = GlobalData.CursorType.Cross;
        }
        else
        {
            upgrade.interactable = Enabled;
            upgradeChanger.selectedType = GlobalData.CursorType.Click;
        }
    }

    public void MenuEnabled(bool enable)
    {
        if (Enabled && enable) Enabled = false;
        else Enabled = enable;

        var curLvl = tower.GetLvLInfo(tower.GetCurrentLvL());

        destroyAnimator.SetBool("menuIsOpen", Enabled);
        upgradeAnimator.SetBool("menuIsOpen", Enabled);
        statPanelAnimator.SetBool("menuIsOpen", Enabled);

        towerRange.radius = curLvl.Range;
        towerRange.gameObject.SetActive(Enabled);

        destroyer.interactable = Enabled;
        cost.SetActive(false);
    }

    public void Upgrade()
    {
        if (tower.Upgrade())
        {
            lvlStar.sprite = levels[tower.GetCurrentLvL()];
            upgradeParticle.Play();
        }
    }

    public void Destroy()
    {
        int returnMoney = Mathf.CeilToInt(tower.GetLvLInfo(tower.GetCurrentLvL()).LvLCost * 0.35f);
        if (towerBuilder.DestroyTower(tower.gameObject))
        {
            MenuEnabled(false);
            GlobalData.instance.GetBones(returnMoney);
        }
    }
}
