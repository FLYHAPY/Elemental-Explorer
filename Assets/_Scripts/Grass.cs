using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Grass : MonoBehaviour
{
    public Grass BurningVines;
    public bool isburning;
    public float timeToBurn;
    private float spreadTime = 1;
    private float timeToDestroy = 2;
    public float checkRadius;
    public bool nextGrassBurning;
    public GameObject fire;
    public Light2D firelight;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isburning == true)
        {
            timeToBurn += Time.deltaTime;
        }

        if(timeToBurn > 0 && timeToBurn < 0.01  ) 
        {
            Instantiate(fire, transform.position, transform.rotation);
        }

        if(timeToBurn >= spreadTime)
        {
            gameObject.tag = "isburning";
        }

        if (timeToBurn >= timeToDestroy)
        {
            Destroy(gameObject);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, checkRadius);

        foreach (Collider2D col in colliders)
        {
            if(col.gameObject.tag == "isburning")
            {
                Burn();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Bullet")
        {
            Burn();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "WaterBullet")
        {
            Destroy(collision.gameObject);
        }
    }

    private void Burn()
    {
        GetComponent<SpriteRenderer>().color = new Color(0xCC / 255f, 0x6F / 255f, 0x5C / 255f, 1.0f);
        isburning = true;
        firelight.intensity = 1.0f;
    }
}
