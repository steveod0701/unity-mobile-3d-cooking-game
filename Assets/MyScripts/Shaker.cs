using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{ 
    //i should use inheritance
    //Used For Drag n Drop
    float offsetX;
    float offsetY;
    Vector3 dragStartPositionInScreen;
    Vector3 dragStartPosition;

    Vector3 previousPosition;
    Vector3 newPosition;
    float velocity;
    float minSpieceVelocity = 0.005f;
    float spiceHeight = 25f;
    float minDisableTime = 0.2f;
    float spiceStartTime;

    private Quaternion dragStartRotation = Quaternion.Euler(-190f, 90f, 0);
    private Quaternion previousRotation = Quaternion.Euler(-90f, 0, 90);

    public GameObject spice;

    private void OnMouseDown()
    {
        if (StaticManagement.isUsingKnife == false)
        {
            dragStartPositionInScreen = Camera.main.WorldToScreenPoint(transform.position);
            dragStartPosition = transform.position;
            offsetX = Input.mousePosition.x - dragStartPositionInScreen.x;
            offsetY = Input.mousePosition.y - dragStartPositionInScreen.y;

            transform.rotation = dragStartRotation;
            transform.position = (new Vector3(transform.position.x, spiceHeight, transform.position.z));
        }
    }

    private void OnMouseDrag()
    {
        if (StaticManagement.isUsingKnife == false)
        {
            newPosition = transform.position;
            velocity = (newPosition - previousPosition).magnitude * Time.deltaTime;
            previousPosition = newPosition;

            float screenPosX = Input.mousePosition.x - offsetX;
            float screenPosY = Input.mousePosition.y - offsetY;
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPosX, screenPosY, dragStartPositionInScreen.z));
            //worldPos.y = dragStartPosition.y;
            worldPos.y = spiceHeight;
            transform.position = worldPos;

            transform.rotation = dragStartRotation;
            if (velocity > minSpieceVelocity)
            {
                spice.SetActive(true);
                spiceStartTime = Time.time;
            }
            else if (velocity <= minSpieceVelocity && Time.time > spiceStartTime + minDisableTime)
            {
                spice.SetActive(false);
            }
        }
    }

    private void OnMouseUp()
    {
        if (StaticManagement.isUsingKnife == false)
        {
            transform.rotation = previousRotation;
            transform.position = new Vector3(newPosition.x, dragStartPosition.y, newPosition.z);
            spice.SetActive(false);
        }
    }
}
