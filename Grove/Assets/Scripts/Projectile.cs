using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Projectile")]
public class Projectile : ScriptableObject
{
    public GameObject muzzleEffect;
    public GameObject projectileObject;
    public GameObject hitEffect;
}
