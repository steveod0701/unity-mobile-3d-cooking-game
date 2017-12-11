using UnityEngine;
using System.Collections;


public class FoodCut : MonoBehaviour
{
    public Material capMaterial;
    private GameObject victim;
    private BoxCollider cut;
    private RigidbodyConstraints pieceConstraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    private Rigidbody pieceRigidBody;

    private BoxCollider victimBoxCol;
    private Draggable draggable;

    private void Start()
    {
        cut = GetComponent<BoxCollider>();
        draggable = GameObject.Find("KnifeHandle").GetComponent<Draggable>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            victim = other.gameObject;
            cut.enabled = false;
        }

        CutFood();
    }

    public void CutFood()
    {                                               
        if (victim.tag == "Food")
        {
            GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position,/*new Vector3(0,0,1)*/draggable.knifeDirection, capMaterial);
            //victim.transform.right

            //if (!pieces[1].GetComponent<Rigidbody>())
            victimBoxCol = victim.GetComponent<BoxCollider>();
            Destroy(victimBoxCol);
            victim.AddComponent<BoxCollider>();
            pieces[1].AddComponent<Rigidbody>();
            pieces[1].AddComponent<BoxCollider>();
            pieces[1].AddComponent<Draggable>();
            pieces[1].gameObject.tag = "Food";
            pieces[1].AddComponent<ObjectReset>();

            pieceRigidBody = pieces[1].GetComponent<Rigidbody>();
            pieceRigidBody.constraints = pieceConstraints;
            //pieceRigidBody.velocity = new Vector3(0, 0, 10);
            //pieces[0].name = victim.name;
            //pieces[1].name = victim.name;
        }            
                
    }
    
}
