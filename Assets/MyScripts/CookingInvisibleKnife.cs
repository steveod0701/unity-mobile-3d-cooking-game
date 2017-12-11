using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingInvisibleKnife : MonoBehaviour
{

    RaycastHit hitInfo;
    public Material capMaterial;
    private BoxCollider victimBoxCol;
    private Rigidbody pieceRigidBody;
    private RigidbodyConstraints pieceConstraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    float rayDistance;
    Vector3 previousPosition;
    Vector3 newPosition;
    Vector3 knifeDirection;
    float knifeVelocity;
    float lastCutTime;
    float lastTouhcTime;

    public float minVelocity = .2f;
    public float cutCoolTime = .1f;
    public float touchCanceledTime = .2f;
    public AudioSource knifeSound;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray m_Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            

            if (Physics.Raycast(m_Ray, out hitInfo))
            {
                newPosition = hitInfo.point;
                knifeVelocity = (newPosition - previousPosition).magnitude * Time.deltaTime;
                knifeDirection = (newPosition - previousPosition);
                previousPosition = newPosition;

                this.transform.position = hitInfo.point+new Vector3(0,1,0);
                if ( Time.time > lastCutTime + cutCoolTime && hitInfo.collider.tag == "Food" && knifeVelocity >= minVelocity && lastTouhcTime+touchCanceledTime > Time.time && hitInfo.collider.gameObject.layer != 9 )
                {
                    knifeSound.Play();
                    lastCutTime = Time.time;
                    GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(hitInfo.collider.gameObject, transform.position, Vector3.forward, capMaterial);

                    victimBoxCol = hitInfo.collider.gameObject.GetComponent<BoxCollider>();
                    Destroy(victimBoxCol);
                    hitInfo.collider.gameObject.AddComponent<BoxCollider>();
                    pieces[1].AddComponent<Rigidbody>();
                    pieces[1].AddComponent<BoxCollider>();
                    pieces[1].AddComponent<DraggableRefact>();
                    pieces[1].gameObject.tag = "Food";
                    pieces[1].AddComponent<ObjectReset>();

                    pieces[1].AddComponent<Food>();
                    Food pieceOneFood = pieces[1].GetComponent<Food>();
                    Food pieceZeroFood = hitInfo.collider.gameObject.GetComponent<Food>();
                    pieceZeroFood.isCutted = true;
                    pieceOneFood.isCutted = true;
                    pieceOneFood.isBoiled = pieceZeroFood.isBoiled;
                    pieceOneFood.isRaw = pieceZeroFood.isRaw;
                    pieceOneFood.isBurned = pieceZeroFood.isBurned;
                    pieceOneFood.isRoasted = pieceZeroFood.isRoasted;
                    pieceOneFood.salt = pieceZeroFood.salt;
                    pieceOneFood.pepper = pieceZeroFood.pepper;

                    pieces[1].transform.Translate(new Vector3(0, 0, 0.1f),Space.World);
                    
                    var pieceBoxCollider = hitInfo.collider.gameObject.GetComponent<BoxCollider>();
                    pieceBoxCollider.isTrigger = true;

                    pieceRigidBody = pieces[1].GetComponent<Rigidbody>();
                    pieceRigidBody.constraints = pieceConstraints;
                    pieceRigidBody.isKinematic = true;
                    pieceRigidBody.useGravity = false;

                    /*var pieceOneBoxCol = pieces[1].GetComponent<BoxCollider>();
                    if(pieceOneBoxCol.size.x >0.9f && pieces[1].layer != 8)
                    {
                        //Destroy(pieces[1].gameObject);
                    }*/
                }
            }

            lastTouhcTime = Time.time;
        }
    }
}
