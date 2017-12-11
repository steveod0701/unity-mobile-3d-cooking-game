using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public bool hasMetFire = false;
    public float firstFireMetTime = 0f;
    public float lastFireMetTime = 0f;
    public bool isRaw = true;
    public bool isRoasted = false;
    public bool isBurned = false;
    public bool isCutted = false;
    public bool isBoiled = false;
    public int pepper = 0;
    public int salt = 0;

    private void OnParticleCollision(GameObject other)
    {
        if (other.name == "Salt")
        {
            salt++;
        }
        else
        {
            pepper++;
        }
    }
}
