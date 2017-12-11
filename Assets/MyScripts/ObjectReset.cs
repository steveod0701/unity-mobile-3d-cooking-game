using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectReset : MonoBehaviour
{
    Vector3 startPosition;
    Quaternion startRotation;
    private UnityAction resetListener;
    private UnityAction destroyListener;
    private UnityAction serveDishListener;
    Rigidbody objectRigidBody;

    public bool destroyThis = true;
    public bool isDish = false;
    public Material prepareDishMat;
    public Material previousMat;

    

    void Awake ()
    {
        startPosition = gameObject.transform.position;
        startRotation = gameObject.transform.rotation;
        resetListener = new UnityAction(ResetPosition);
        destroyListener = new UnityAction(DestroyAll);
        serveDishListener = new UnityAction(PrepareDish);
        objectRigidBody = gameObject.GetComponent<Rigidbody>();
        if (isDish)
        {
            StaticManagement.currentDish.Add(gameObject);
            previousMat = gameObject.GetComponent<MeshRenderer>().material;
        }
	}
    
    private void OnEnable()
    {
        EventManager.StartListening("Reset", resetListener);
        if (destroyThis)
        {
            EventManager.StartListening("DestroyAll", destroyListener);
        }
        if (isDish)
        {
            EventManager.StartListening("PrepareDish", serveDishListener);
        }
    }

    private void OnDisable()
    {
        EventManager.StopListening("Reset", resetListener);
        if (destroyThis)
        {
           EventManager.StopListening("DestroyAll", destroyListener);
        }
        if (isDish)
        {
            EventManager.StopListening("PrepareDish", serveDishListener);
        }
    }

    public void ResetPosition()
    {
        transform.SetPositionAndRotation(startPosition, startRotation);
        objectRigidBody.velocity = Vector3.zero;
        objectRigidBody.angularVelocity = new Vector3(0, 0, 0);
    }

    public void DestroyAll()
    {
        Destroy(gameObject);
    }

    public void PrepareDish()
    {
        for (int i = 0; i < StaticManagement.currentDish.Count; i++)
        {
            if (gameObject == StaticManagement.currentDish[i])
            {
                DishDraggable dishDraggable = StaticManagement.currentDish[i].GetComponent<DishDraggable>();
                MeshRenderer dishMeshRenderer = dishDraggable.gameObject.GetComponent<MeshRenderer>();

                if (StaticManagement.isServingTime == true)
                {
                    dishMeshRenderer.material = prepareDishMat;
                    dishDraggable.isServingTime = true;
                }

                if (StaticManagement.isServingTime == false)
                {
                    dishMeshRenderer.material = previousMat;
                    dishDraggable.isServingTime = false;
                }
            }
        }
    }
}
