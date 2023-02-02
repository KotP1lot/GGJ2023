using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildTowers : MonoBehaviour
{
    public Camera mainCamera;
    //[SerializeField] private GameObject tower; //змінти з часом
    [SerializeField] private Tile[] roots;

    private GameObject destroedTower;

    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tilemap tilemapOverlay;
    [SerializeField] private Tile overlayGreen;
    [SerializeField] private Tile overlayRed;
    [SerializeField]private List<GameObject> towers = new List<GameObject>();

    private PlayerMovement player;
    private void Start()
    {
        mainCamera = Camera.main;
        Tower.onDestroyTower += InstantiateRoot;

        player = GetComponent<PlayerMovement>();
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
        tilemapOverlay.SetTile(tilePos, overlayRed);
    }

    private void InstantiateRoot() 
    {
        Destroy(destroedTower);
        Vector3Int tile = tilemap.WorldToCell(destroedTower.transform.position);
        tilemap.SetTile(tile, roots[Random.Range(0, roots.Length)]);
        tilemapOverlay.SetTile(tile, overlayGreen);
    }
    public bool BuildNewTower(GameObject tower, Vector3 placeForBuid) 
    {
        //Debug.Log(mainCamera.ScreenToWorldPoint(Input.mousePosition).x + ":" + mainCamera.ScreenToWorldPoint(Input.mousePosition).y);
        if (IsTileRoot(placeForBuid, out Vector3Int tilePos) && Distance(transform.position, mainCamera.ScreenToWorldPoint(Input.mousePosition)) < player.buildRadius)
        {
            towers.Add(Instantiate(tower, new Vector2(tilePos.x + 0.5f, tilePos.y + 0.5f), Quaternion.identity));
            HideRoot(tilePos);
            return true;
        }
        return false;
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

    private float Distance(Vector2 v1, Vector2 v2)
    {
        Vector3 difference = new Vector3(v1.x - v2.x,v1.y - v2.y);
        return Mathf.Sqrt(Mathf.Pow(difference.x, 2f) +Mathf.Pow(difference.y, 2f));
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    BuildNewTower(tower, mainCamera.ScreenToWorldPoint(Input.mousePosition));

        //}
        //if (Input.GetMouseButtonDown(1)) 
        //{
        //    DestroyTower(towers[0].IsUnityNull()? null : towers[0]);
        //}
    }
}
