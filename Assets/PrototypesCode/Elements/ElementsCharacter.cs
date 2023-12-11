using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MageType
{
    Normal,
    Fire,
    Ice
}

public class ElementsCharacter : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed, bulletSpeed;
    public float xAxis, yAxis;
    public Vector2 moveDir;
    public float changeTypeDistance = 2f;

    public GameObject bulletPrefab;

    public Transform shootRot, FirePos;

    public SpriteRenderer spriterendere;

    public MageType currentMageType = MageType.Normal;

    public Transform firecrystal;
    public Transform watercrystal;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        moveDir = Vector3.Normalize(new Vector2(xAxis, yAxis));

        if (moveDir != Vector2.zero)
            shootRot.right = moveDir;

        rb.velocity = (((Vector3.up * moveDir.y) + (Vector3.right * moveDir.x)) * speed);

        float distanceToFireCrystal = Vector2.Distance(transform.position, firecrystal.position);
        float distanceToWaterCrystal = Vector2.Distance(transform.position, watercrystal.position);


        if (Input.GetKeyDown(KeyCode.Space) && currentMageType != MageType.Normal)
        {
            GameObject newBullet = Instantiate(bulletPrefab, FirePos.position, Quaternion.identity);
            Destroy(newBullet, 5f);
            newBullet.GetComponent<Rigidbody2D>().velocity = shootRot.right * bulletSpeed;
            
            if(currentMageType == MageType.Fire) 
            {
                newBullet.tag = "Bullet";
            }
            else if (currentMageType == MageType.Ice)
            {
                newBullet.tag = "WaterBullet";
            }
        }

        if (distanceToFireCrystal < changeTypeDistance)
        {
            ChangeMageType(MageType.Fire);
        }
        else if (distanceToWaterCrystal < changeTypeDistance)
        {
            ChangeMageType(MageType.Ice);
        }

    }

    private void ChangeMageType(MageType newMageType)
    {
        // Change the mage type only if it's different from the current type
        if (newMageType != currentMageType)
        {
            currentMageType = newMageType;

            // Update mage properties based on type
            switch (newMageType)
            {
                case MageType.Fire:
                    spriterendere.color = Color.red;
                    break;
                case MageType.Ice:
                    spriterendere.color= Color.blue;                    
                    break;     
            }
        }
    }
}
