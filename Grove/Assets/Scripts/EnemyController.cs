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
    int currentHealth;
    CharacterAnimationController animateController;
    float originalSpeed;

    [Header("Combat")]
    bool rangedEnemy = false;
    [SerializeField]GameObject fireProjectilePrefab = null;
    List<GameObject> fireProjectiles = new List<GameObject>();
    int numberOfProjectiles = 2;
    [SerializeField] Transform firePoint = null;
    bool shotRecently = false;
    [SerializeField]float shootCooldown = 2f;
    [SerializeField] int maxHealth;
    [SerializeField] EnemyHealthBar healthBar;
    [SerializeField] float HitParalyzeTime;

    [Header("Animation Counter")]
    [SerializeField] float OnDieDelay;

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
        CheckDistanceToTarget();
        animateController.animateMove();
    }

    void CheckDistanceToTarget()
    {
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget <= 1.5f && treeTarget != null)
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
        else if (distanceToTarget <= 1.5f)
        {
            ChooseTarget();
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
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(currentHealth);
    }

    public void TakeHit(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            player.GetComponent<PlayerController>().GainLifeGems(2);
            StartCoroutine(OnDie());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(OnParalyze());
        }
        healthBar.SetHealth(currentHealth);
    }
    void Shoot(GameObject target)
    {
        if (!shotRecently)
        {
            transform.LookAt(target.transform.position);
            animateController.animateAttack();
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
        animateController = GetComponent<CharacterAnimationController>();
        originalSpeed = navigation.speed;
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
    
    IEnumerator OnDie()
    {
        animateController.animateDie();
        navigation.speed = 0;
        yield return new WaitForSeconds(OnDieDelay);//wait until the animation is done
        navigation.speed = originalSpeed;
        gameObject.SetActive(false);//disable the gameObject
    }

    IEnumerator OnParalyze()
    {
        float counter = HitParalyzeTime;
        navigation.speed = 0;
        animateController.animateDamage();//call the damage animation
        while (counter >= 0)
        {
            
            counter -= Time.deltaTime;
            yield return null;
        }

        navigation.speed = originalSpeed;
    }
}
