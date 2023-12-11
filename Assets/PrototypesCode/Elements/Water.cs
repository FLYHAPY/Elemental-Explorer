using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    public GameObject water;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            StartCoroutine(Burn());
        }
    }

    private IEnumerator Burn()
    {
        water.SetActive(false);

        yield return new WaitForSeconds(5f);

        water.SetActive(true);
    }
}
