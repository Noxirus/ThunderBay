using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    GameObject nearbyTree;

    void Start()
    {
        
    }
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        transform.Translate(new Vector3(horizontal, 0f, vertical));
    }

    void GrowTree()
    {
        if (nearbyTree)
        {
            //Decrement the amount of energy the player has, checks to ensure there is enough
            //clense the tree
            //update the percentage of map regrown
            //sends function calls to game manager and the tree to change its state
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with: " + other.gameObject.name);
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
