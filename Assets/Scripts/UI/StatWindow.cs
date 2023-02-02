using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Globalization;

public class StatWindow : MonoBehaviour
{
    public TextMeshProUGUI headerText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI dmgText;
    public TextMeshProUGUI spdText;
    public TextMeshProUGUI rngText;
    public TextMeshProUGUI descriptionText;

    public TowerSelector[] towers;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        bool anyHighlighted=false;
        foreach(var tower in towers)
        {
            if (tower.isHighlighted)
            {
                SetData(tower.header, tower.cost, tower.DMG, tower.SPD, tower.RNG, tower.description);
                anyHighlighted = true;
                break;
            }
        }
        Show(anyHighlighted);
    }

    public void SetData(string header, int cost, float dmg, float spd, float rng, string desc)
    {
        headerText.text = header;
        costText.text = cost.ToString();
        dmgText.text = dmg % 1 == 0 ? dmg.ToString() : dmg.ToString("F1", CultureInfo.InvariantCulture);
        spdText.text = spd % 1 == 0 ? spd.ToString() : spd.ToString("F1", CultureInfo.InvariantCulture);
        rngText.text = rng % 1 == 0 ? rng.ToString() : rng.ToString("F1", CultureInfo.InvariantCulture);
        descriptionText.text = desc;
    }

    public void Show(bool show)
    {
        animator.SetBool("Show", show);
    }
}
