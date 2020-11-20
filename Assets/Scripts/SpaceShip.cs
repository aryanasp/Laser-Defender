using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceShip : MonoBehaviour
{
    //config params
    protected Camera gameCamera;
    [Header("Movement")]
    [SerializeField]
    protected int movementSpeed;
    [Header("Projectile")]
    [SerializeField]
    protected GameObject laserPrefab;
    [SerializeField]
    [Header("Stats")]
    protected float health = 500;
    [SerializeField]
    protected float powerFire = 40;
    [SerializeField]
    protected float crashDamage = 200f;
    [SerializeField]
    protected float fireCooldown = 0.3f;
    [SerializeField]
    GameObject explosionVFX;
    [SerializeField]
    AudioClip laserSFX;
    [SerializeField]
    [Range(0, 1)] private float laserSoundVolume = 0.5f;
    [SerializeField]
    AudioClip deathSFX;
    [SerializeField]
    [Range(0, 1)] private float deathSoundVolume = 0.05f;

    //state
    bool checkDeath = false;
    protected float maxHealth;

    public int MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public float Health { get => health; set => health = value; }
    public float PowerFire { get => powerFire; set => powerFire = value; }
    public float CrashDamage { get => crashDamage; set => crashDamage = value; }
    public float FireCooldown { get => fireCooldown; set => fireCooldown = value; }
    


    //cached
    Rigidbody2D myRigidbody2D;
    SpriteRenderer mySpriteRenderer;

    public Rigidbody2D MyRigidbody2D { get => myRigidbody2D; set => myRigidbody2D = value; }
    public SpriteRenderer MySpriteRenderer { get => mySpriteRenderer; set => mySpriteRenderer = value; }


    //state
    protected Vector2 direction;
    private bool  isDead;

    public bool IsDead
    {
        get
        {
            isDead = health <= 0;
            return isDead;
        }
    }



    // Start is called before the first frame update
    protected virtual void Start()
    {
        
        MyRigidbody2D = GetComponent<Rigidbody2D>();
        MySpriteRenderer = GetComponent<SpriteRenderer>();
        gameCamera = Camera.main;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (IsDead && !checkDeath)
        {
            HandleDeath();
        }
    }

    protected virtual void HandleDeath()
    {
        checkDeath = true;
        GameObject explosion = (GameObject) Instantiate(explosionVFX, transform.position, transform.rotation);
        explosion.transform.SetParent(transform);
        AudioSource.PlayClipAtPoint(deathSFX, gameCamera.transform.position, deathSoundVolume);
        Destroy(explosion, 0.3f);
        Destroy(gameObject, 0.2f);
    }

    protected IEnumerator FireContinously()
    {
        while (true)
        {
            HandleFire();
            yield return new WaitForSeconds(FireCooldown);
        }
    }

    protected void HandleFire()
    {
        int directionDegree = direction == Vector2.up ? 0 : 180;
        GameObject laser = (GameObject) Instantiate(laserPrefab, transform.Find("Laser Position").position, Quaternion.Euler(new Vector3(0, 0, directionDegree)));
        laser.GetComponent<Laser>().LaserProjectileSpeed = PowerFire;
        laser.GetComponent<Laser>().Direction = direction;
        AudioSource.PlayClipAtPoint(laserSFX, gameCamera.transform.position, laserSoundVolume);
    }
}

