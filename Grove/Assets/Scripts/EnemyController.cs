using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    GameObject target;
    PlantController treeTarget;
    NavMeshAgent navigation;
    List<GameObject> trees = new List<GameObject>();
    GameObject player;

    [Header("Combat")]
    bool rangedEnemy = false;
    [SerializeField]GameObject fireProjectilePrefab = null;
    List<GameObject> fireProjectiles = new List<GameObject>();
    int numberOfProjectiles = 2;
    [SerializeField] Transform firePoint = null;
    bool shotRecently = false;
    [SerializeField]float shootCooldown = 2f;

    void Awake()
    {
        Setup();
    }

    private void OnEnable()
    {
        SelectEnemyType();
        ChooseTarget();
    }

    private void Update()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        //Need to check if its a tree, then check distance, and check to see if the tree is growing or burning
        if(distanceToTarget <= 1.5f && treeTarget != null)
        {
            if (!treeTarget.burnt)
            {
                BurnTree(treeTarget);
            }
            else
            {
                ChooseTarget();
            }
        }
    }

    void BurnTree(PlantController tree)
    {
        tree.Burn();
    }

    void ChooseTarget()
    {
        int targetChoice = Random.Range(0, 4);
        if(targetChoice < 3)
        {
            int treeChoice = Random.Range(0, trees.Count);
            target = trees[treeChoice].gameObject;
            treeTarget = target.GetComponent<PlantController>();
        }
        else
        {
            target = player;
        }
        navigation.SetDestination(target.transform.position);
    }
    void SelectEnemyType()
    {
        int randomType = Random.Range(0, 2);
        if(randomType == 0)
        {
            rangedEnemy = true;
        }
        else
        {
            rangedEnemy = false;
        }
    }
    public void TakeHit(int damage)
    {
        gameObject.SetActive(false);
    }
    void Shoot(GameObject target)
    {
        if (!shotRecently)
        {
            transform.LookAt(target.transform.position);
            for (int i = 0; i < fireProjectiles.Count; i++)
            {
                if (!fireProjectiles[i].activeInHierarchy)
                {
                    fireProjectiles[i].transform.position = firePoint.transform.position;
                    fireProjectiles[i].transform.rotation = firePoint.transform.rotation;
                    fireProjectiles[i].SetActive(true);
                    StartCoroutine(ShootCooldown());
                    break;
                }
            }
        }
    }
    IEnumerator ShootCooldown()
    {
        shotRecently = true;
        yield return new WaitForSeconds(shootCooldown);
        shotRecently = false;
    }

    private void OnTriggerStay(Collider other)
    {
        //If the enemy is melee
        if (other.gameObject.CompareTag("Player"))
        {
            target = other.gameObject;
            treeTarget = null;
            navigation.SetDestination(target.transform.position);
            if (rangedEnemy)
            {
                Shoot(other.gameObject);
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeHit(1);
        }
    }
    void Setup()
    {
        navigation = GetComponent<NavMeshAgent>();
        GameObject[] tempTrees = GameObject.FindGameObjectsWithTag("Tree");
        for (int i = 0; i < tempTrees.Length; i++)
        {
            trees.Add(tempTrees[i]);
        }
        player = GameObject.FindGameObjectWithTag("Player");

        Transform projectileParent = GameObject.FindGameObjectWithTag("Projectiles").transform;
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            GameObject tempFireProjectile = Instantiate(fireProjectilePrefab, projectileParent);
            fireProjectiles.Add(tempFireProjectile);
            tempFireProjectile.SetActive(false);  
        }
    }
}
