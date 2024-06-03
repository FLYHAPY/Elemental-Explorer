using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;


public enum MageType
{
    Normal,
    Fire,
    Ice,
    Earth,
    Air
}

public class ElementsCharacter : MonoBehaviour
{
    [Header("Player movement && Shoot")]
    public SpriteRenderer spriterendere;
    public Rigidbody2D rb;
    public float speed, bulletSpeed;
    public float xAxis, yAxis;
    public Vector2 moveDir;
    public Transform shootRot, FirePos;
    public GameObject bulletPrefab;
    public float timer;
    public bool isShooting;
    public Quaternion angle;
    public Light2D playerlight;

    [Header("Crystals & Type")]
    public Transform firecrystal;
    public Transform watercrystal;
    public Transform earthcrystal;
    public Transform airCrystal;
    public MageType currentMageType = MageType.Normal;
    public float changeTypeDistance = 2f;

    [Header("Golem")]
    public Transform golomSpawnPosition;
    public GameObject golomPrefab;
    private GameObject Golem;
    public bool transformedToGolem;

    [Header("Flying")]
    public GameObject Air;
    public bool isFlying;
    public bool cantSwitch;
    public float checkRadius;

    [Header("Audio")]
    public AudioSource fireBall;
    public AudioSource iceBall;
    public AudioSource Wind;
    public AudioSource golemSpawn;
    public AudioSource golemDeath;

    float distanceToFireCrystal;
    float distanceToWaterCrystal;
    float distanceToEarthCrystal;
    float distanceToAirCrystal;

    [Header("Animations")]
    public Animator animator;
    public bool ismoving;
    public RuntimeAnimatorController Mage_Nothing;
    public RuntimeAnimatorController Mage_Fire;
    public RuntimeAnimatorController Mage_Ice;
    public RuntimeAnimatorController Mage_Earth;
    public RuntimeAnimatorController Mage_Wind;
    public float lastXaxis;
    public float lastYaxis;
    public RuntimeAnimatorController Fire_Ball;
    public RuntimeAnimatorController Water_Ball;

    [Header("UI")]
    public GameObject text;
    public GameObject text2;
    public GameObject image;
    public GameObject button;
    public GameObject button2;
    public bool ispaused;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cantSwitch = true;

    }

    void ChangeController(RuntimeAnimatorController controller)
    {
        // Set the new Animator Controller
        animator.runtimeAnimatorController = controller;

        // Optionally, you can reset the current state if needed
        // animator.Play("YourDefaultAnimationStateName");
    }

    // Update is called once per frame
    void Update()
    {
        Golem = GameObject.FindGameObjectWithTag("Golem");
        if(ispaused == false)
        {
            xAxis = Input.GetAxisRaw("Horizontal");
            yAxis = Input.GetAxisRaw("Vertical");
        }
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");
        Air = GameObject.FindGameObjectWithTag("Air");

        moveDir = Vector3.Normalize(new Vector2(xAxis, yAxis));

        animator.SetFloat("Xaxis", xAxis);
        animator.SetFloat("Yaxis", yAxis);
        animator.SetBool("ismoving", ismoving);
        animator.SetBool("isShooting", isShooting);
        animator.SetBool("isFlying", isFlying);

        if (xAxis != 0) 
        {
            lastXaxis = xAxis;
            lastYaxis = 0;
        }
        if(yAxis != 0)
        {
            lastYaxis = yAxis;
            lastXaxis = 0;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            image.gameObject.SetActive(true);
            text.gameObject.SetActive(true);
            text2.gameObject.SetActive(true);
            button.gameObject.SetActive(true);
            button2.gameObject.SetActive(true);
            ispaused = true;
        }

        animator.SetFloat("XaxisLast", lastXaxis);
        animator.SetFloat("YaxisLast", lastYaxis);

        if (ismoving == false)
        {
            if (lastXaxis > 0)
            {
                angle = Quaternion.Euler(0, 0, 0);
            }
            else if (lastXaxis < 0)
            {
                angle = Quaternion.Euler(0, 0, 180);
            }
            if (lastYaxis > 0)
            {
                angle = Quaternion.Euler(0, 0, 90);
            }
            else if (lastYaxis < 0)
            {
                angle = Quaternion.Euler(0, 0, 270);
            }
        }else if (ismoving == true)
        {
            if (lastYaxis > 0 && xAxis > 0)
            {
                angle = Quaternion.Euler(0, 0, 45);
            }
            else if (lastYaxis < 0 && xAxis < 0)
            {
                angle = Quaternion.Euler(0, 0, 225);
            }
            else if (lastYaxis > 0 && xAxis < 0)
            {
                angle = Quaternion.Euler(0, 0, 135);
            }
            else if (lastYaxis < 0 && xAxis > 0)
            {
                angle = Quaternion.Euler(0, 0, 315);
            }
        }

        if (moveDir != Vector2.zero)
        {
            shootRot.right = moveDir;
            ismoving = true;
        }
        else if(moveDir.x < 0.5 && moveDir.y < 0.5)
        {
            ismoving = false;
        }

        if ( transformedToGolem == false && ispaused == false) 
        {
            rb.velocity = (((Vector3.up * moveDir.y) + (Vector3.right * moveDir.x)) * speed);
        }

        if (isShooting == true)
        {
            timer += Time.deltaTime;
  
        }
        if(timer > 1.1)
        {
            isShooting = false;
            timer = 0;
        }


        if (firecrystal != null) 
        { 
            distanceToFireCrystal = Vector2.Distance(transform.position, firecrystal.position);
            if (distanceToFireCrystal < changeTypeDistance)
        {
            ChangeMageType(MageType.Fire);
            isFlying = false;
        }
        }
        if (watercrystal != null)
        {
            distanceToWaterCrystal = Vector2.Distance(transform.position, watercrystal.position);
            if (distanceToWaterCrystal < changeTypeDistance)
            {
                ChangeMageType(MageType.Ice);
                isFlying = false;
            }
        }
        if (earthcrystal != null)
        {
            distanceToEarthCrystal = Vector2.Distance(transform.position, earthcrystal.position);
            if (distanceToEarthCrystal < changeTypeDistance)
            {
                ChangeMageType(MageType.Earth);
                isFlying = false;
            }
        }
        if (airCrystal != null)
        {
            distanceToAirCrystal = Vector2.Distance(transform.position, airCrystal.position);
            if (distanceToAirCrystal < changeTypeDistance)
            {
                ChangeMageType(MageType.Air);
            }
        }


        if (Input.GetKeyDown(KeyCode.Space) && ispaused == false && currentMageType != MageType.Normal && currentMageType != MageType.Earth && currentMageType != MageType.Air && isShooting == false)
        {
            isShooting = true;
            Invoke("ShootBullet", 0.5f);
        }
        else if(Input.GetKeyDown(KeyCode.Space) && currentMageType == MageType.Earth)
        {
            if(transformedToGolem == false && Golem == null && ispaused == false) 
            { 
                GameObject golom = Instantiate(golomPrefab, golomSpawnPosition.position, Quaternion.identity);
                transformedToGolem = true;
                rb.velocity = Vector2.zero;
                golemSpawn.Play();
            }
            else
            {
                transformedToGolem = false;
                golemDeath.Play();
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.Space) && currentMageType == MageType.Air && cantSwitch == true && ispaused == false)
        {
            if(isFlying == false)
            {
                Air.GetComponent<TilemapCollider2D>().isTrigger = (true);
                isFlying = true;
                Debug.Log("switched to flying");
                Wind.Play();
            }
            else
            {
                isFlying = false;
                Debug.Log("not flying");
                Air.GetComponent<TilemapCollider2D>().isTrigger = (false);
                Wind.Stop();
            }
        }
        if (isFlying == false)
        {
            Air.GetComponent<TilemapCollider2D>().isTrigger = (false);
        }
    }

    void ShootBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab, FirePos.position, angle);
        Destroy(newBullet, 5f);
        newBullet.GetComponent<Rigidbody2D>().velocity = shootRot.right * bulletSpeed;

        if (currentMageType == MageType.Fire)
        {
            newBullet.tag = "Bullet";
            newBullet.layer = 7;
            fireBall.Play();
            newBullet.GetComponent<Animator>().Play("Fire Ball");
        }
        else if (currentMageType == MageType.Ice)
        {
            newBullet.tag = "WaterBullet";
            iceBall.Play();
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
                    ChangeController(Mage_Fire);
                    timer = 0;
                    isFlying = false;
                    Wind.Stop();
                    playerlight.intensity = 1;
                    break;
                case MageType.Ice:
                    ChangeController(Mage_Ice);
                    timer = 0;
                    isFlying = false;
                    Wind.Stop();
                    playerlight.intensity = 1;
                    break;
                case MageType.Earth:
                    ChangeController(Mage_Earth);
                    Wind.Stop();
                    timer = 0;
                    isFlying = false;
                    playerlight.intensity = 1;
                    break;
                case MageType.Air:
                    ChangeController(Mage_Wind);
                    timer = 0;
                    playerlight.intensity = 1;
                    break;
            }
        }
    }
        
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Air")
        {
            cantSwitch = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Air" && isFlying == true)
        {
            cantSwitch = false;
        }
    }
}
