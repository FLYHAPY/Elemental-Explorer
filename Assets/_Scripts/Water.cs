using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Water : MonoBehaviour
{

    public Tilemap waterTilemap;
    public Tilemap iceTilemap;
    public Tile iceTile;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "WaterBullet")
        {
            Debug.Log("Hit");
            foreach (ContactPoint2D hit in collision.contacts)
            {
                Debug.Log(hit.point);

                Vector2 hitPos = Vector2.zero;

                hitPos.x = hit.point.x - 0.01f * hit.normal.x * -1.01f;
                hitPos.y = hit.point.y - 0.01f * hit.normal.y * -1.01f;

                TileBase tile = waterTilemap.GetTile(waterTilemap.WorldToCell(hitPos));

                if(tile == null)
                { return; }

                
                    waterTilemap.SetTile(waterTilemap.WorldToCell(hitPos), null);
                    iceTilemap.SetTile(iceTilemap.WorldToCell(hitPos), iceTile);
                    Destroy(collision.gameObject);
                
            }
        }
    }
}