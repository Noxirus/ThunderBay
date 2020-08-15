using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    public Projectile projectile;
    public List<string> hitTargetTag;//the list of target that should hit on
    public float speed;
    public float projectileDuration;
    public float muzzleEffectDuration;
    public float hitEffectDuration;

    private Rigidbody rig;
    // Start is called before the first frame update
    void Start()
    {
        GameObject muzzle = Instantiate(projectile.muzzleEffect, transform.position, Quaternion.Euler(transform.forward));
        Destroy(muzzle.gameObject, muzzleEffectDuration);
        Destroy(gameObject, projectileDuration);
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rig.velocity = transform.forward * speed;
    }

    //
    private void OnTriggerEnter(Collider other)
    {
        //if it hits the correct object
        if (isInTargetList(other.gameObject)) {
            rig.velocity = Vector3.zero;//stop the projectile
            //disable all child object which are the effects
            for (int i = 0; i < transform.childCount; i++) {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            //do damage here

            GameObject hit = Instantiate(projectile.hitEffect, transform.position, Quaternion.Euler(transform.forward));
            Destroy(hit.gameObject, hitEffectDuration);
            Destroy(gameObject);
        }
    }

    //helper method to check if the hit object is in the target list
    private bool isInTargetList(GameObject obj) {
        int matches = 0;
        for (int i = 0; i < hitTargetTag.Count; i++) {
            if (obj.tag == hitTargetTag[i]) {
                matches++;
            }
        }
        return matches == 0;
    }
}
