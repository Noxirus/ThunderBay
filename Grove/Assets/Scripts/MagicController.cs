using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicController : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float timeout = 2f;

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
            other.gameObject.GetComponent<EnemyController>().TakeHit(1);
        }
    }

    IEnumerator KillTimer()
    {
        yield return new WaitForSeconds(timeout);
        gameObject.SetActive(false);
    }
}
