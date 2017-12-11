using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour
{   
    //Used To Draw Trail at KnifeTip
    public Transform knifeTipPosition;
    public GameObject knifeTrailPrefab;
    public GameObject fastKnifeTrailPrefab;

    //Prefabs To Instantiate KinfeTrail   
    private GameObject currentKnifeTrail;
    private GameObject fastKnifeTrail;
    
    //Used To Calculate KnifeVelocity
    Vector3 previousKnifePosition;
    Vector3 newKnifePosition;

    //Used For Drag n Drop
    float offsetX;
    float offsetY;
    Vector3 dragStartPositionInScreen;
    Vector3 dragStartPosition;

    public Vector3 knifeDirection;

    float knifeVelocity;

    //Minimum KnifeTip Velocity To Cut Food
    float minEffectVelocity;
    
    //Prevents Too Much Food Cutting
    int cutControlInt;

    //BoxCollider Used For Cut
    public GameObject cut;
    private BoxCollider cutBoxCollider;
    

    private void Start()
    {
        if (gameObject.name == "KnifeHandle")
        {
            cutBoxCollider = cut.GetComponent<BoxCollider>();
        }        
    }

    private void OnMouseDown()
    {
        dragStartPositionInScreen = Camera.main.WorldToScreenPoint(transform.position);
        
        if (transform.gameObject.tag == "HandTools" || transform.gameObject.tag=="Food")
        {
            dragStartPosition = new Vector3(transform.position.x, 23.1f, transform.position.z);
        }
        else
        {
            dragStartPosition = transform.position;
        }


        offsetX = Input.mousePosition.x - dragStartPositionInScreen.x;
        offsetY = Input.mousePosition.y - dragStartPositionInScreen.y;

        //We can Also Check Touched Tools With Raycast
        /*Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.gameObject.tag == "HandTools")
            {
                transform.SetPositionAndRotation(new Vector3(transform.position.x, -3.08f, transform.position.y), Quaternion.Euler(-65, 90, -90));
            }
        }*/
        if (transform.gameObject.tag == "HandTools")
        {
            transform.SetPositionAndRotation(new Vector3(transform.position.x, 23.1f, transform.position.y), Quaternion.Euler(-65, 270, -90));
        }//25
        
        /*else if (transform.gameObject.tag == "Food")
        {
            transform.SetPositionAndRotation(new Vector3(transform.position.x, 23.1f, transform.position.y), Quaternion.identity);
        }*/

        else if(transform.name == "KnifeHandle")
        {
            previousKnifePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
        }
    }

    private void OnMouseDrag()
    {   
        float screenPosX = Input.mousePosition.x - offsetX;
        float screenPosY = Input.mousePosition.y - offsetY;

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(screenPosX,screenPosY,dragStartPositionInScreen.z));
        


        worldPos.y = dragStartPosition.y;
        transform.position = worldPos;
        if(transform.name == "KnifeHandle")
        {
            newKnifePosition = worldPos;
            knifeVelocity = (newKnifePosition - previousKnifePosition).magnitude * Time.deltaTime;
            knifeDirection = (newKnifePosition - previousKnifePosition).normalized;
            previousKnifePosition = newKnifePosition;

            //Prevents too much Cut
            if (cutControlInt < 60)
            {
                cutControlInt++;
            }
            
            //knifeTrail
            if (knifeVelocity > 0 && knifeVelocity <0.013)
            {
                currentKnifeTrail = Instantiate(knifeTrailPrefab, knifeTipPosition);
                Destroy(currentKnifeTrail, .3f);
            }
            if (knifeVelocity > 0.013)
            {
                fastKnifeTrail = Instantiate(fastKnifeTrailPrefab, knifeTipPosition);
                Destroy(fastKnifeTrail, .3f);
            }
            
            //Food cut
            if (knifeVelocity > 0.013 && cutControlInt>40)
            {                                
                cutControlInt = 0;
                cutBoxCollider.enabled = true;
                Invoke("CutColliderDisable", .1f);
            }
        }
    }

    private void OnMouseUp()
    {
        if (transform.gameObject.tag == "HandTools")
        {
            transform.SetPositionAndRotation(transform.position, Quaternion.Euler(0,180,0));            
        }
    }

    void CutColliderDisable()
    {
        cutBoxCollider.enabled = false;
    }

}
