using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //config params
    [SerializeField]
    int movementSpeed;
    [SerializeField]
    GameObject laserPrefab;
    [SerializeField]
    float padding = 1f;
    Camera gameCamera;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    Coroutine firingCoroutine;


    //cachecd
    Rigidbody2D myRigidbody2D;
    SpriteRenderer mySpriteRenderer;

    //state
    float horizontal;
    float vertical;
    private bool fire;
    [SerializeField]
    private float fireCooldown = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        gameCamera = Camera.main;
        SetUpBounaries();
    }

    private void SetUpBounaries()
    {
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - mySpriteRenderer.size.x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - mySpriteRenderer.size.y - padding;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleMovement();
    }

    private void Update()
    {
        HandleInput();
    }
    
    IEnumerator FireContinously()
    {
        while (true)
        {
            HandleFire();
            yield return new WaitForSeconds(fireCooldown);
        }
    }

    private void HandleFire()
    {
        GameObject laserGameObject = Instantiate(laserPrefab, transform.Find("Laser Position").position, Quaternion.identity) as GameObject; 
    }

    private void HandleMovement()
    {
        myRigidbody2D.velocity = new Vector2(horizontal * movementSpeed, vertical * movementSpeed / 2);
        Vector2 param = myRigidbody2D.velocity * Time.fixedDeltaTime;
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
}
