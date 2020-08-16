using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float timeout = 2f;
    [SerializeField] int damage = 1;

    private void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(KillTimer());
    }

    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        //check if the object is an enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().TakeHit(damage);
            gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<PlayerController>().TakeHit(damage);
            gameObject.SetActive(false);
        }
        else if (other.gameObject.CompareTag("Tree"))
        {
            gameObject.SetActive(false);
        }
    }

    IEnumerator KillTimer()
    {
        yield return new WaitForSeconds(timeout);
        gameObject.SetActive(false);
    }
}
