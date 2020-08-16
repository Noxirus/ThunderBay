using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    public bool burnt = true;
    Material material;
    Color burnedColor;
    Color growingColor = Color.green;
    [SerializeField] GameObject growthArea = null;
    GameManager gameManager;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        burnedColor = material.color;
    }
    //TODO these will likely need to be coroutines so you have time to react to a tree growing/burning
    public bool Regrow()
    {
        if (burnt)
        {
            growthArea.SetActive(true);
            burnt = false;
            material.color = growingColor;
            gameManager.UpdateGameState();
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Burn()
    {
        Debug.Log("Triggered burning");
        if (!burnt)
        { 
            growthArea.SetActive(false);
            burnt = true;
            material.color = burnedColor;
            gameManager.UpdateGameState();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<PlayerController>().EnterOrLeaveGrowth(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInParent<PlayerController>().EnterOrLeaveGrowth(false);
        }
    }
}
