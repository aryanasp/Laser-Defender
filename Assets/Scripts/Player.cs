using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : SpaceShip
{
    //config params
    float padding = 0.3f;
    private float immortalTime = 0.55f;
    
    float xMin;
    float xMax;
    float yMin;
    float yMax;


    //cached
    Coroutine firingCoroutine;


    //state
    float horizontal;
    float vertical;
    
    
    private bool immortal = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        direction = Vector2.up;
        base.Start();
        
        SetUpBoundaries();
    }

    private void SetUpBoundaries()
    {
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - MySpriteRenderer.size.x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - MySpriteRenderer.size.y - padding;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleMovement();
    }

    protected override void Update()
    {
        HandleInput();
        base.Update();
    }

    private IEnumerator IndicateImmortal()
    {
        while (immortal)
        {
            MySpriteRenderer.enabled = false;
            yield return new WaitForSeconds(.03f);
            MySpriteRenderer.enabled = true;
            yield return new WaitForSeconds(.03f);
        }
    }

    private void HandleMovement()
    {
        MyRigidbody2D.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed / 2);
        Vector2 param = MyRigidbody2D.velocity * Time.fixedDeltaTime;
        transform.position = new Vector2(
            Mathf.Clamp(transform.position.x, xMin, xMax), 
            Mathf.Clamp(transform.position.y, yMin, yMax)) ;
    }

    private void HandleInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy Laser")
        {
            StartCoroutine(HandleDamage(100));
        }
        if (other.gameObject.tag == "Enemy")
        {
            StartCoroutine(HandleDamage(CrashDamage));
        }
    }

    protected override void HandleDeath()
    {
        base.HandleDeath();
        
        FindObjectOfType<Level>().LoadGameOver();
    }

    private IEnumerator HandleDamage(float damageRecieved)
    {
        if (!immortal)
        {
            if (IsDead)
            {
                HandleDeath();
            }
            else
            {
                Health -= (int) damageRecieved;
                if(Health < 0)
                {
                    Health = 0;
                }
                immortal = true;
                StartCoroutine(IndicateImmortal());
                yield return new WaitForSeconds(immortalTime);
                immortal = false;
            }
        }
    }

}
