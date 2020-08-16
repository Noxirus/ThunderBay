using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    UserInterface userInterface;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    Camera cam;
    Rigidbody rb;

    [Header("Growth")]
    [SerializeField][Range(0f, 100f)]float energy = 20f;
    [SerializeField]int lifeGems = 0;
    [SerializeField] float energyRecharge = 1f;
    bool restoring = false;
    GameObject nearbyTree;

    [Header("Combat")]
    [SerializeField] GameObject magicPrefab = null;
    [SerializeField] Transform magicPoint = null;
    List<GameObject> magic = new List<GameObject>();
    [SerializeField] int numOfMagic = 10;
    [SerializeField] int health = 3;
    [SerializeField] Stat Damage;
    bool immune = false;
    float immuneTimer = 2f;
    bool playerAlive = true;
    int deathTimer = 3;

    public Action<int> OnDamageChangeCallBack;//delegate for all changing all the magic object

    void Start()
    {
        Setup();
    }
    void Update()
    {
        if (playerAlive)
        {
            MovePlayer();
            LookAtMouse();
            EnergyChange();
            if (Input.GetMouseButtonDown(0))
            {
                ShootMagic();
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                GrowTree();
            }
        }
    }

    void EnergyChange()
    {
        if (restoring)
        {
            energy += energyRecharge * Time.deltaTime * 3;
        }
        else
        {
            energy -= energyRecharge * Time.deltaTime;
            if(energy <= 0)
            {
                PlayerDeath();
            }
        }
        userInterface.UpdateEnergy(energy);
    }

    public void EnterOrLeaveGrowth(bool entering)
    {
        if (entering)
        {
            restoring = true;
        }
        else
        {
            restoring = false;
        }
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(new Vector3(horizontal, 0f, vertical), Space.World);

    }

    void LookAtMouse()
    {
        RaycastHit hit;
        Ray mouseRay = cam.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(mouseRay, out hit)){
            transform.LookAt(new Vector3(hit.point.x, 0f, hit.point.z));
        }    
    }

    void ShootMagic()
    {
        for(int i = 0; i < magic.Count; i++)
        {
            if (!magic[i].activeInHierarchy)
            {
                magic[i].transform.position = magicPoint.transform.position;
                magic[i].transform.rotation = magicPoint.transform.rotation;
                magic[i].SetActive(true);
                return;
            }
        }
    }

    void GrowTree()
    {
        if (energy >= 10 && nearbyTree)
        {
            if (nearbyTree.GetComponent<PlantController>().Regrow())
            {
                if(health < 3)
                {
                    health += 1;
                    userInterface.UpdateHealth(health);
                } 
                energy -= 10f;
                userInterface.UpdateEnergy(energy);
                GainLifeGems(5);
                
            }
        }
        else
        {
            Debug.Log("Not enough energy or no nearby tree");
        }
    }

    void Setup()
    {
        rb = GetComponent<Rigidbody>();
        userInterface = GameObject.FindGameObjectWithTag("UserInterface").GetComponent<UserInterface>();
        userInterface.UpdateEnergy(energy);
        userInterface.UpdateGems(lifeGems);
        cam = Camera.main;
        Transform projectileParent = GameObject.FindGameObjectWithTag("Projectiles").transform;

        for (int i = 0; i < numOfMagic; i++)
        {
            GameObject tempMagic = Instantiate(magicPrefab, projectileParent);
            OnDamageChangeCallBack += tempMagic.GetComponent<ProjectileController>().SetDamage;
            magic.Add(tempMagic);
            tempMagic.SetActive(false);
        }
        OnDamageChangeCallBack(Damage.getValue());//initialize the damage for the fire ball
        userInterface.SetMaxEnergy(energy);
    }

    void PlayerDeath()
    {
        userInterface.MiddleTextMessage("Game Over");
        playerAlive = false;
        rb.constraints = RigidbodyConstraints.None;
        StartCoroutine(DeathTimer());
    }

    IEnumerator DeathTimer()
    {
        yield return new WaitForSeconds(deathTimer);
        SceneManager.LoadScene(0);
    }

    public void TakeHit(int damage)
    {
        if (!immune)
        {
            Debug.Log("Hit Taken");
            health -= damage;
            userInterface.UpdateHealth(health);
            if (health <= 0)
            {
                PlayerDeath();
            }
            StartCoroutine(ImmuneTimer());
        }
    }
    IEnumerator ImmuneTimer()
    {
        immune = true;
        yield return new WaitForSeconds(immuneTimer);
        immune = false;
    }

    public void GainLifeGems(int amount)
    {
        lifeGems += amount;
        userInterface.UpdateGems(lifeGems);
        //Level 1
        if (lifeGems >= 30)
        {
            moveSpeed = 7f;
        }
        //Level 2
        if(lifeGems >= 60)
        {
            moveSpeed = 8f;
        }
        //Level 3
        if(lifeGems >= 90)
        {
            moveSpeed = 10f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tree"))
        {
            nearbyTree = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == nearbyTree)
        {
            nearbyTree = null;
        }
    }

    //call when the player receive damage power up
    public void PowerUp(int additionDamage) {
        Damage.addModifier(additionDamage);
        OnDamageChangeCallBack(Damage.getValue());
    }
}
