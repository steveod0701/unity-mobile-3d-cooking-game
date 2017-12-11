using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beverage : MonoBehaviour
{
    public GameObject beverage;

    public Transform spawnPoint;

    private void OnMouseDown()
    {
        if(StaticManagement.ableToInteractWithThings==true)
            Instantiate(beverage, spawnPoint.position, beverage.transform.rotation);
    }
}
