using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPlayerMovement : MonoBehaviour
{
    public float speed = 3.0f;
    public bool isBonusActive = false;
    private Rigidbody2D rb;
    private Vector3 velo;
    private Vector3 moveDirection;
    

    [Header("Dash Settings") ]
    [SerializeField] private float dashingCooldown = 1f;
    [SerializeField] private float dashingDuration = 1f;
    [SerializeField] private float dashingSpeed = 10f;
    private bool isDashing;
    private bool canDash = true;

    // Use this for initialization
    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
        {
            return;
        }
        // Could replace the above with the following code
        float veloX = Input.GetAxisRaw("Horizontal");
        float veloY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(veloX, veloY, 0).normalized;

        // Move the player ship to where the mouse is clicked on-screen
        if (Input.GetButton("Fire1"))
        {
            Vector3 newpos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(newpos.x, newpos.y, 0.0f);
        }
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            StartCoroutine(Dash());

        }
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }


        rb.velocity = new Vector3(moveDirection.x * speed, moveDirection.y * speed, 0f);
    }

    void OnBecameInvisible()
    {
        Camera cam = Camera.main;

        if (cam != null)
        {
            Vector3 viewportPosition = cam.WorldToViewportPoint(transform.position);

            Vector3 newPosition = transform.position;

            if (viewportPosition.x > 1 || viewportPosition.x < 0)
            {
                newPosition.x = -newPosition.x;
            }

            if (viewportPosition.y > 1 || viewportPosition.y < 0)
            {
                newPosition.y = -newPosition.y;
            }

            transform.position = newPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("upgrade"))
        {
            collision.gameObject.SetActive(false);
            StartCoroutine(playerBonus());
        }
    }

    private IEnumerator playerBonus()
    {
        isBonusActive = true;
        yield return new WaitForSeconds(5);
        isBonusActive = false;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        rb.velocity = new Vector3(moveDirection.x * dashingSpeed, moveDirection.y * dashingSpeed, 0f);
        yield return new WaitForSeconds(dashingDuration);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

}
