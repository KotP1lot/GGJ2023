using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ShrubsTower : Tower
{
    [SerializeField] private string pathLayer;
    [SerializeField] private string envLayer;
    [SerializeField] private GameObject shrubRoots;
    [SerializeField] private bool attacking = false;
    [SerializeField] private float attackTime;

    private Tilemap pathObj;
    private List<Vector3Int> FindPathCells(Vector3 shrubPos)
    {
        List<Vector3Int> cells = new List<Vector3Int>();
        Vector3Int shurbPosInt = pathObj.WorldToCell(shrubPos);
        GameObject[] existingShrubRoots = GameObject.FindGameObjectsWithTag("ShrubRoots");
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (!pathObj.GetTile(new Vector3Int(shurbPosInt.x + x, shurbPosInt.y + y)).IsUnityNull()) 
                {
            
                    bool isExist = false;
                    foreach (GameObject existingShrubRoot in existingShrubRoots) 
                    {
                        if (pathObj.WorldToCell(existingShrubRoot.transform.position).Equals(new Vector3Int(shurbPosInt.x + x, shurbPosInt.y + y)))
                        {
                            existingShrubRoot.GetComponent<ShrubRoots>().Reset(attackTime);
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist)cells.Add(new Vector3Int(shurbPosInt.x + x, shurbPosInt.y + y));
                }
            }
        }
        Debug.Log(cells.Count);
        return cells;
    }
    IEnumerator Attacking()
    {
        yield return new WaitForSeconds(attackTime);

        lastAttackTime = Time.time;
        attacking = false;
        isAttacking = false;
    }

    protected override void Attack()
    {
        if (!attacking)
        {
            base.Attack();
        }
    }
    public override void OnAnimationTrigger()
    {
        base.OnAnimationTrigger();
        pathObj = GameObject.Find(pathLayer).GetComponent<Tilemap>();

        List<Vector3Int> wereToSpawn = FindPathCells(transform.position);

        attacking = true;
        AudioManager.instance.Play("Rose");
        foreach (Vector3Int cell in wereToSpawn)
        {
            GameObject root = Instantiate(shrubRoots, new Vector2(cell.x + 0.5f, cell.y + 0.5f), Quaternion.identity);
            root.GetComponent<ShrubRoots>().SpawnRoot(attackTime, lvlList[currentLvL].Damage);
        }

        StartCoroutine(Attacking());
    }
}
