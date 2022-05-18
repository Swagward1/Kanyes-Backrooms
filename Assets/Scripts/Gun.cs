using System.Collections;
using UnityEngine;

/// <summary>
/// Made with via Brackeys on Youtube
/// https://www.youtube.com/watch?v=THnivyG0Mvo
/// </summary>

public class Gun : MonoBehaviour
{
    public float dreamKills;

    [Header("Gun Info")]
    [SerializeField] int damage = 2;
    [SerializeField] int range = 100;
    [SerializeField] float fireRate = .6f;
    [SerializeField] float nextTimeToFire;
    [SerializeField] float gunForce = 30f;
    [SerializeField] Transform gunBarrel;
    [SerializeField] GameObject bullet;
    public bool canReload;

    [Header("Ammo")]
    public int ammoMax = 10;
    public int currentAmmo;
    public bool currentlyReloading;
    //public Rigidbody rb;
    [SerializeField] float reloadTime = 2f;

    [SerializeField] Camera fpsCam;
    [SerializeField] ParticleSystem muzzleFlash;
    Animator anim;


    void Start()
    {
        currentAmmo = ammoMax;
        currentlyReloading = false;

        anim = GetComponent<Animator>();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= nextTimeToFire)
            Shoot();
        //if(Input.GetMouseButtonDown(0) && currentAmmo <= 0)
          //  StartCoroutine("ReloadGun");

        if(Input.GetKeyDown(KeyCode.R) && canReload)
        {
            if(currentAmmo < ammoMax)
            {
                StartCoroutine("ReloadGun");
            }
        }

        LimitAmmoCount();
    }

    void Shoot()
    {
        if(!currentlyReloading)
        {
            if (currentAmmo >= 1)
            {
                nextTimeToFire = Time.time + fireRate;
                muzzleFlash.Play();
                
                if(Time.timeScale == 1)
                    currentAmmo--;


                anim.SetTrigger("RecoilActive");

                //rb.AddForce(fpsCam.transform.forward * -gunForce, ForceMode.VelocityChange);
                RaycastHit hitInfo;
                if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hitInfo, range))
                {
                    EnemyHealth enemy = hitInfo.transform.GetComponent<EnemyHealth>();
                    
                    if (enemy != null)
                    {
                        enemy.TakeDamage(damage);
                        dreamKills++;
                    }
                    
                }
            }
        }
    }

    IEnumerator ReloadGun()
    {
        currentlyReloading = true;

        anim.SetTrigger("ReloadActive");
        yield return new WaitForSeconds(reloadTime);
        
        for (int i = 0; i < ammoMax; i++)
        {
            currentAmmo++;
        }

        currentlyReloading = false;
    }

    void LimitAmmoCount()
    {
        if(currentAmmo <= 0)
            currentAmmo = 0;
        if(currentAmmo >= ammoMax)
            currentAmmo = ammoMax;
    }
}