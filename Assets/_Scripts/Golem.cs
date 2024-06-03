using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{

    private GameObject player;
    
    [Header("Golem movement")]
    public Rigidbody2D rb;
    public float speed;
    public float xAxis, yAxis;
    public Vector2 moveDir;

    [Header("Golem animations")]
    public Animator animator;
    public bool ismoving;
    public float lastXaxis;
    public float lastYaxis;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis = Input.GetAxisRaw("Vertical");

        moveDir = Vector3.Normalize(new Vector2(xAxis, yAxis));

        animator.SetFloat("Xaxis", xAxis);
        animator.SetFloat("Yaxis", yAxis);
        animator.SetBool("ismoving", ismoving);

        if (moveDir != Vector2.zero)
        {
            ismoving = true;
        }
        else
        {
            ismoving = false;
        }

        if (xAxis != 0)
        {
            lastXaxis = xAxis;
            lastYaxis = 0;
        }
        if (yAxis != 0)
        {
            lastYaxis = yAxis;
            lastXaxis = 0;
        }

        animator.SetFloat("XaxisLast", lastXaxis);
        animator.SetFloat("YaxisLast", lastYaxis);

        rb.velocity = (((Vector3.up * moveDir.y) + (Vector3.right * moveDir.x)) * speed);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.GetComponent<ElementsCharacter>().transformedToGolem = false;
            Destroy(gameObject);
        }
    }
}
