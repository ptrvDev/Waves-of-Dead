using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
public class playerController : MonoBehaviour
{
    private GameObject soldier;
    private Rigidbody rb;
    private Animator animController;
    public uiHealthBar healthbar;
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Recoil recoil;
    public Text ammoDisplay;
    public Image deathScreen;
    public Texture2D cursor;

    public float bulletSpeed = 30;
    int ammo = 75;
    public float lifeTime = 3;
    public GameObject muzzleFlash;

    private float speed = 350;

    public int maxHealth = 100;
    public int currentHealth;

    private bool canSprint = true;
    private bool isSprinting = false;
    private bool isWalking = false;

    private bool canShoot = true;
    public bool isShooting = false;
    public float shootDelay = 0;

    private bool isDead = false;

    public float deathScreenDelay = 2;
    public bool deathScreenEnabled = false;



    private void Start()
    {
        Cursor.SetCursor(cursor, new Vector2(0,0), CursorMode.Auto);
        rb = GetComponent<Rigidbody>();
        soldier = GameObject.Find("Soldier Graphics");
        animController = soldier.GetComponent<Animator>();

        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);

        animController.SetBool("isIdle", true);
        muzzleFlash.SetActive(false);
        muzzleFlash.GetComponent<ParticleSystem>().Pause();

        ammoDisplay.text = ammo.ToString();
    }


    private void FixedUpdate()
    {
        //Fixing soldiers position
        soldier.transform.position = transform.position;

        //Figures out which animation to play
        animatorUpdate();

        //Update the ammo UI element
        ammoDisplay.text = ammo.ToString();

        //Update the healthbar UI element
        healthbar.SetHealth(currentHealth);


        //Makes the player sprint
        if (!isDead)
        {
            //Makes the player sprint
            Sprint();

            //Simple health manager
            healthManager();

            //Makes the player shoot 

            Shoot();

            if (isShooting)
            {
                muzzleFlash.SetActive(true);
                muzzleFlash.GetComponent<ParticleSystem>().Play();
            }
            else
            {
                muzzleFlash.SetActive(false);
                muzzleFlash.GetComponent<ParticleSystem>().Pause();
            }

        } else
        {
            canShoot = false;
            canSprint = false;
        }

        if (isDead && !deathScreenEnabled)
        {
            deathScreenDelay -= Time.deltaTime;
            if (deathScreenDelay <= 0)
            {
                deathScreen.GetComponent<WinLoseMenu>().enableAll();
                deathScreenEnabled = true;
            }
        }




    }

    private void animatorUpdate()
    {
        if(isSprinting || isShooting || isWalking || isDead)
        {
            animController.SetBool("isIdle", false);
        }

        if (isWalking)
        {
            animController.SetBool("isSprinting", false);
            animController.SetBool("isWalking", true);
            animController.SetBool("isShooting", false);
            animController.SetBool("isDead", false);  
        }
        else if (isSprinting)
        {
            animController.SetBool("isSprinting", true);
            animController.SetBool("isWalking", false);
            animController.SetBool("isShooting", false);
            animController.SetBool("isDead", false);
        }
        else if (isShooting)
        {
            animController.SetBool("isShooting", true);
            animController.SetBool("isSprinting", false);
            animController.SetBool("isWalking", false);
            animController.SetBool("isDead", false);

        } else if (isDead)
        {
            animController.SetBool("isShooting", false);
            animController.SetBool("isSprinting", false);
            animController.SetBool("isWalking", false);
            animController.SetBool("isDead", true);
        }


        if (!isSprinting && !isShooting && !isWalking && !isDead)
        {
            animController.SetBool("isIdle", true);
            animController.SetBool("isSprinting", false);
            animController.SetBool("isShooting", false);
            animController.SetBool("isWalking", false);
            animController.SetBool("isDead", false);


        }

    }

    private void Sprint()
    {
        if (canSprint)
        {
            Vector3 moveVec = new Vector3(CrossPlatformInputManager.GetAxisRaw("SprintHorizontal") , 0, CrossPlatformInputManager.GetAxisRaw("SprintVertical"));
            transform.LookAt(transform.position + moveVec * Time.deltaTime);
            if (moveVec.magnitude >= 0.1f)
            {
                if(moveVec.magnitude < 0.7f)
                {
                    isWalking = true;
                    isSprinting = false;
                } else if(moveVec.magnitude >= 0.7f)
                {
                    isSprinting = true;
                    isWalking = false;
                }
                
                rb.velocity = moveVec * speed * Time.deltaTime;
            } else if(moveVec.magnitude < 0.1f)
            {
                isSprinting = false;
                isWalking = false;
            }
        }        
    }


    private void Shoot()
    {

        if (!isDead)
        {
           
            Vector3 lookVec = new Vector3(CrossPlatformInputManager.GetAxisRaw("ShootHorizontal"), 0, CrossPlatformInputManager.GetAxisRaw("ShootVertical"));
            if (lookVec.magnitude >= 0.1)
            {
                canSprint = false;
                isSprinting = false;
                isWalking = false;
                if (ammo > 0)
                {
                    isShooting = true;
                } else
                {
                    isShooting = false;
                }
                
                transform.LookAt(transform.position + lookVec * Time.deltaTime);
            }
            if (lookVec.magnitude < 0.1)
            {
                canSprint = true;
                isShooting = false;
            }

            if (isShooting)
            {
                recoil.Fire();

                shootDelay -= Time.deltaTime;
                if (shootDelay <= 0)
                {
                    GameObject bullet = Instantiate(bulletPrefab);

                    bullet.transform.position = bulletSpawn.position;

                    Vector3 rotation = bullet.transform.rotation.eulerAngles;

                    bullet.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);

                    bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);

                    StartCoroutine(DestroyBullet(bullet, lifeTime));

                    ammo--;
                    
                    shootDelay = 0.2f;
                }
            }
        }

    }

    private IEnumerator DestroyBullet(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(bullet);
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    private void healthManager()
    {
        if(currentHealth >= 100)
        {
            currentHealth = maxHealth;
        }
        if(currentHealth <= 0)
        {
            isSprinting = false;
            isWalking = false;
            isShooting = false;
            isDead = true;
        }
    }

    public void LevelFinished()
    {
        Debug.Log("Level Finished");
        canSprint = false;
        isSprinting = false;
        isWalking = false;
        isDead = false;
    }

    public void CollectHealth()
    {
        currentHealth += 30;
        Debug.Log("success");
    }

    public void CollectAmmo()
    {
        ammo += 45;
        Debug.Log("success");
    }

}
