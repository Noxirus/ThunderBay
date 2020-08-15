using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantController : MonoBehaviour
{
    public bool burnt = true;
    Material material;
    Color burnedColor;
    Color growingColor = Color.green;

    void Start()
    {
        material = GetComponent<Renderer>().material;
        
        burnedColor = material.color;
    }
    public void Regrow()
    {
        if (burnt)
        {
            burnt = false;
            material.color = growingColor;
        }
        else
        {
            return;
        }
    }
    public void Burn()
    {
        if (!burnt)
        {
            burnt = true;
            material.color = burnedColor;
        }
    }
}
