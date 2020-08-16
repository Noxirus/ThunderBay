using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 10f;
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
    [SerializeField] int numOfMagic = 5;
    [SerializeField] int health = 3;
    [SerializeField] Stat Damage;
    bool immune = false;
    float immuneTimer = 2f;

    public Action<int> OnDamageChangeCallBack;//delegate for all changing all the magic object

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
        if (nearbyTree)
        {
            nearbyTree.GetComponent<PlantController>().Regrow();
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
            OnDamageChangeCallBack += tempMagic.GetComponent<ProjectileController>().SetDamage;
            magic.Add(tempMagic);
            tempMagic.SetActive(false);
        }
    }

    public void TakeHit(int damage)
    {
        if (!immune)
        {
            Debug.Log("Hit Taken");
            health -= damage;
            if (health <= 0)
            {
                Debug.Log("Character has died");
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

    public void PowerUp(int additionDamage) {
        Damage.addModifier(additionDamage);
        OnDamageChangeCallBack(Damage.getValue());
    }
}
