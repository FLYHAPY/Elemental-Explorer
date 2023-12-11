using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public Grass BurningVines;
    public bool isburning;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (BurningVines != null && BurningVines.isburning == true )
        {
            StartCoroutine(Burn());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            StartCoroutine(Burn());
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.tag == "WaterBullet" )
        {
            Destroy(collision.gameObject);
        }
    }

    private IEnumerator Burn()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(5f);

        gameObject.SetActive(false);
        isburning = true;
    }
}
