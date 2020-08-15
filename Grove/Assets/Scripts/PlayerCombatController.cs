using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    public GameObject projectile;
    public Transform shootPoint;
    public float frequency;
    public float Damage;

    private float counter = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && counter <= 0) {
            GameObject proj = Instantiate(projectile, shootPoint.position,Quaternion.Euler(shootPoint.forward));
            proj.GetComponent<ProjectileController>().SetDamage(Damage);
            counter = 1 / frequency;
        }
        counter -= Time.deltaTime;
    }
}
