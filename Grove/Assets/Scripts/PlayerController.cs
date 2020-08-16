using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    Camera cam;

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
    bool immune = false;
    float immuneTimer = 2f;

    void Start()
    {
        Setup();
    }
    void Update()
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
                energy -= 10f;
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
        cam = Camera.main;
        Transform projectileParent = GameObject.FindGameObjectWithTag("Projectiles").transform;

        for (int i = 0; i < numOfMagic; i++)
        {
            GameObject tempMagic = Instantiate(magicPrefab, projectileParent);
            magic.Add(tempMagic);
            tempMagic.SetActive(false);
        }
    }

    void PlayerDeath()
    {

    }

    public void TakeHit(int damage)
    {
        if (!immune)
        {
            Debug.Log("Hit Taken");
            health -= damage;
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
        //Level 1
        if(lifeGems >= 30)
        {
            moveSpeed = 10f;
        }
        //Level 2
        if(lifeGems >= 60)
        {

        }
        //Level 3
        if(lifeGems >= 90)
        {

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
}
