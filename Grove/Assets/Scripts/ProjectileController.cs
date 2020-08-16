using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField] Projectile projectile;
    [SerializeField] List<string> hitTargetTag;//the list of target that should hit on
    [SerializeField] float speed;
    [SerializeField] float projectileDuration;
    [SerializeField] float muzzleEffectDuration;
    [SerializeField] float hitEffectDuration;
    int Damage;

    private Rigidbody rig;
    private string TagName;
    private bool isEnabled = false;
    // Start is called before the first frame update
    void OnEnable()
    {
        rig = GetComponent<Rigidbody>();
        rig.velocity = transform.up * speed;
        if (isEnabled)
        {
            GameObject muzzle = Instantiate(projectile.muzzleEffect, transform.position, Quaternion.Euler(transform.forward));
            Destroy(muzzle.gameObject, muzzleEffectDuration);
            StopAllCoroutines();
            StartCoroutine(magicCounter(projectileDuration));
        }
        else
        {
            isEnabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    //
    private void OnTriggerEnter(Collider other)
    {
        //if it hits the correct object
        if (DoDamage(other.gameObject)) {
            rig.velocity = Vector3.zero;//stop the projectile
            //do damage here

            GameObject hit = Instantiate(projectile.hitEffect, transform.position, Quaternion.Euler(transform.forward));
            Destroy(hit.gameObject, hitEffectDuration);
            gameObject.SetActive(false);
        }
    }

    //helper method to check if the hit object is in the target list
    private bool DoDamage(GameObject obj) {
        int matches = 0;

        if (obj.CompareTag("Enemy"))
        {
            obj.GetComponent<EnemyController>().TakeHit(Damage);
            matches++;
        }
        else if (obj.CompareTag("Player"))
        {
            obj.GetComponentInParent<PlayerController>().TakeHit(Damage);
            matches++;
        }
        else if (obj.CompareTag("Tree"))
        {
            matches++;
        }
        return matches > 0;
    }

    IEnumerator magicCounter(float Duration) {
        yield return new WaitForSeconds(Duration);
        gameObject.SetActive(false);
    }

    public void SetDamage(int dmg) {
        Damage = dmg;
    }
}
