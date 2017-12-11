using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableRefact : MonoBehaviour
{
    //Used For Drag n Drop
    float offsetX;
    float offsetY;
    Vector3 dragStartPositionInScreen;
    Vector3 dragStartPosition;

    private void OnMouseDown()
    {
        if (StaticManagement.isUsingKnife == false)
        {
            dragStartPositionInScreen = Camera.main.WorldToScreenPoint(transform.position);
            dragStartPosition = transform.position;
            offsetX = Input.mousePosition.x - dragStartPositionInScreen.x;
            offsetY = Input.mousePosition.y - dragStartPositionInScreen.y;
        }
    }

    private void OnMouseDrag()
    {
        if (StaticManagement.isUsingKnife == false)
        {
            float screenPosX = Input.mousePosition.x - offsetX;
            float screenPosY = Input.mousePosition.y - offsetY;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPosX, screenPosY, dragStartPositionInScreen.z));
            worldPos.y = dragStartPosition.y;
            transform.position = worldPos;
        }
    }   
}
