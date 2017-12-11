using UnityEngine;
using System.Collections;


public class ToolUser : MonoBehaviour {

	public Material capMaterial;

	// Use this for initialization
	void Start ()
    {

		
	}
	
	void Update()
    {
        
		if(Input.GetMouseButtonDown(0))
        {


            RaycastHit hit;


            if(Physics.Raycast(transform.position, transform.forward, out hit))
            //if(Physics.BoxCast(transform.position,new Vector3(0.2f,1,3.5f),new Vector3(transform.position.x,0,transform.position.z), out hit))                
            {

				GameObject victim = hit.collider.gameObject;
                if (victim.tag == "Food")
                {
                    GameObject[] pieces = BLINDED_AM_ME.MeshCut.Cut(victim, transform.position, transform.right, capMaterial);
                    Debug.Log(victim.name);
                    
                    BoxCollider victimsCollider = victim.GetComponent<BoxCollider>();
                    Destroy(victimsCollider);
                    victim.AddComponent<BoxCollider>();
                    

                    if (!pieces[1].GetComponent<Rigidbody>())
                    {
                        pieces[1].AddComponent<Rigidbody>();
                        pieces[1].AddComponent<BoxCollider>();
                        pieces[1].AddComponent<Draggable>();
                        pieces[1].gameObject.tag = "Food";




                    }
                }
				//Destroy(pieces[1], 1);
			}

		}
	}

	void OnDrawGizmosSelected() {

		Gizmos.color = Color.green;

		Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5.0f);
		Gizmos.DrawLine(transform.position + transform.up * 0.5f, transform.position + transform.up * 0.5f + transform.forward * 5.0f);
		Gizmos.DrawLine(transform.position + -transform.up * 0.5f, transform.position + -transform.up * 0.5f + transform.forward * 5.0f);

		Gizmos.DrawLine(transform.position, transform.position + transform.up * 0.5f);
		Gizmos.DrawLine(transform.position,  transform.position + -transform.up * 0.5f);

	}

}
