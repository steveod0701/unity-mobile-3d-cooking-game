using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : MonoBehaviour
{
    public GameObject fridgeCanvas;

    private GameManager gameManager;


    private void Start()
    {
        //gameManager = GameObject.Find("CookingStart").GetComponent<gameManager>();
    }

    private void OnMouseDown()
    {       
        //if (gameManager.isCooking == true)
        {
            if (StaticManagement.ableToInteractWithThings == true)
            {
                fridgeCanvas.SetActive(true);
                StaticManagement.isUsingKnife = true;
            }
        }
    }

    public void FridgeCanvasExit()
    {   
        fridgeCanvas.SetActive(false);
        StaticManagement.isUsingKnife = false;
    }
}
