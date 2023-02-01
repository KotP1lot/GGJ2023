using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildTowers : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private string rootsLayer;
    [SerializeField] private GameObject tower; //змінти з часом
    [SerializeField] private Tile[] roots;

    private GameObject destroedTower;

    private Tilemap tilemap;
    [SerializeField]private List<GameObject> towers = new List<GameObject>();
    private void Start()
    {
        tilemap = GameObject.Find(rootsLayer).GetComponent<Tilemap>();
        mainCamera = Camera.main;
        Tower.onDestroyTower += InstantiateRoot;


    }

    private bool IsTileRoot(Vector3 mousePos, out Vector3Int tilePos) 
    {
        tilePos = tilemap.WorldToCell(mousePos);
        return !tilemap.GetTile(new Vector3Int(tilePos.x, tilePos.y)).IsUnityNull() 
            && tilemap.GetTile(new Vector3Int(tilePos.x, tilePos.y)).name.ToLower().Contains("root");
    }
    private void HideRoot(Vector3Int tilePos) 
    {
        tilemap.SetTile(tilePos, null);
    }

    private void InstantiateRoot() 
    {
        Destroy(destroedTower);
        Vector3Int tile = tilemap.WorldToCell(destroedTower.transform.position);
        tilemap.SetTile(tile, roots[Random.Range(0, roots.Length)]);
    }

    public void DestroyTower(GameObject tower)
    {
        if (!tower.IsUnityNull())
        {
            destroedTower = tower;
            towers.Remove(destroedTower);
            tower.GetComponent<Tower>().BeforeDestroy();
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsTileRoot(mainCamera.ScreenToWorldPoint(Input.mousePosition), out Vector3Int tilePos))
            {
                HideRoot(tilePos);
                towers.Add(Instantiate(tower, new Vector2(tilePos.x + 0.5f, tilePos.y + 0.5f), Quaternion.identity));
            }
        }
        if (Input.GetMouseButtonDown(1)) 
        {
            DestroyTower(towers[0].IsUnityNull()? null : towers[0]);
        }
    }
}
