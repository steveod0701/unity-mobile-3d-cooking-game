using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleKnife : MonoBehaviour
{
    RaycastHit hitInfo;
    private RigidbodyConstraints pieceConstraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
    float rayDistance;
    float cutControllInt;
    float cutControllLimit =9;
    float knifeVelocity;
    Vector3 previousPosition;
    Vector3 newPosition;
    Vector3 trailOffset = new Vector3(0, 0.01f, 0);

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MoveNCut();
        }
    }

    private void MoveNCut()
    {
        Debug.Log(knifeVelocity);
        Ray m_Ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        newPosition = hitInfo.point;
        knifeVelocity = (newPosition - previousPosition).magnitude * Time.deltaTime;
        previousPosition = newPosition;

        if (cutControllInt < cutControllLimit)
            cutControllInt++;

        if (Physics.Raycast(m_Ray, out hitInfo))
        {
            var hitCol = hitInfo.collider;
            newPosition = hitInfo.point;
            knifeVelocity = (newPosition - previousPosition).magnitude * Time.deltaTime;
            previousPosition = newPosition;
            this.transform.position = hitInfo.point + trailOffset;

            if (hitCol.tag=="Food" && cutControllInt == cutControllLimit && knifeVelocity>0.01)
            {
                //Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float xPosDif = Mathf.Abs(hitInfo.point.x - hitCol.gameObject.transform.position.x);
                float scaleX = hitCol.gameObject.transform.localScale.x/2 - xPosDif;
                hitCol.gameObject.transform.localScale = new Vector3(hitCol.gameObject.transform.localScale.x - scaleX,
                    hitCol.gameObject.transform.localScale.y,
                    hitCol.gameObject.transform.localScale.z);
                
                    CreateCutObj(
                    new Vector3((hitInfo.point.x>hitCol.transform.position.x)
                    ? hitInfo.point.x+scaleX/2
                    : hitInfo.point.x-scaleX/2
                    , hitCol.transform.position.y
                    , hitCol. transform.position.z),
                    new Vector3(scaleX, hitCol.transform.localScale.y,hitCol.transform.localScale.z)
                    , hitCol.gameObject.GetComponent<MeshRenderer>().material
                    );
                cutControllInt = 0;
            }
        }
    }

    private void CreateCutObj(Vector3 pos, Vector3 scale, Material mat)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.transform.localPosition = pos;
        go.transform.localScale = scale;
        go.AddComponent<Rigidbody>();
        go.AddComponent<DraggableRefact>();
        go.GetComponent<MeshRenderer>().material = mat;
        go.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
    }
}
