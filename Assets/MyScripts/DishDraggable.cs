using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishDraggable : MonoBehaviour
{
    //Used For Drag n Drop
    float offsetX;
    float offsetY;
    Vector3 dragStartPositionInScreen;
    Vector3 dragStartPosition;
    public bool isServingTime = false;
    private float safetyTime = .5f;
    private float currentTime = 0;
    private float desiredYpos = 19.7f;

    private GameObject dishWarpPoint;
    private GameObject gameManager;
    private EventManager eventManager;

    private void Awake()
    {
        dishWarpPoint = GameObject.Find("DishWarpPoint");
        gameManager = GameObject.Find("GameManager");
        eventManager = gameManager.GetComponent<EventManager>();
        if (gameObject.tag == "Beverage")
            desiredYpos = 19.4f;
    }

    private void OnMouseDown()
    {
        transform.SetParent(null);
        if (isServingTime)
        {
            currentTime = Time.time;
            ServeDish();
            isServingTime = false;
            gameObject.GetComponent<MeshRenderer>().material = GetComponent<ObjectReset>().previousMat;
            StaticManagement.isServingTime = !StaticManagement.isServingTime;
            eventManager.TriggerPrepareDish();
        }

        else if (StaticManagement.isUsingKnife == false)
        {
            dragStartPositionInScreen = Camera.main.WorldToScreenPoint(transform.position);
            dragStartPosition = transform.position;
            offsetX = Input.mousePosition.x - dragStartPositionInScreen.x;
            offsetY = Input.mousePosition.y - dragStartPositionInScreen.y;
        }
    }

    private void OnMouseDrag()
    {
        if (StaticManagement.isUsingKnife == false && Time.time > currentTime+safetyTime)
        {
            float screenPosX = Input.mousePosition.x - offsetX;
            float screenPosY = Input.mousePosition.y - offsetY;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPosX, screenPosY, dragStartPositionInScreen.z));
            worldPos.y = desiredYpos;
            //worldPos.y = dragStartPosition.y;
            transform.position = worldPos;
        }
    }

    public void ServeDish()
    {
        transform.position = dishWarpPoint.transform.position;
    }
}
