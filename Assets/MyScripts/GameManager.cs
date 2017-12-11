using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //Controlls Cooking Phase With Button
    public GameObject mainCam;
    public GameObject normalPhaseCanvas;
    public GameObject cookingPhaseCanvas;
    public GameObject blade;

    public float moveSpeed = 0.5f;
    public Transform fridgeView;
    public Transform cookingView;
    public Transform beverageView;
    public Transform defaultView;

    public int moveControllInt;
    public Transform previousView;
    private Transform disiredView;
    public Book book;

    public bool isMoving;

    private void Start()
    {
        moveControllInt = 0;
        previousView = mainCam.transform;
        isMoving = false;
    }

    IEnumerator SmoothMoveRoatate(GameObject go, Vector3 previousPostion, Vector3 disiredPosition,
        Vector3 previousRotation, Vector3 disiredRotation, float operatingTime)
    {
        isMoving = true;
        float step = 0.0f;
        float rate = 1.0f / operatingTime;
        float smoothStep = 0.0f;
        float lastStep = 0.0f;

        while (step < 1f)
        {
            step += Time.deltaTime * rate;
            smoothStep = Mathf.SmoothStep(0.0f, 1.0f, step);
            go.transform.Translate((disiredPosition.x - previousPostion.x) * (smoothStep - lastStep),
                (disiredPosition.y - previousPostion.y) * (smoothStep - lastStep),
                (disiredPosition.z - previousPostion.z) * (smoothStep - lastStep), Space.World);

            go.transform.Rotate((disiredRotation.x - previousRotation.x) * (smoothStep - lastStep), 0, 0);
            go.transform.Rotate(Vector3.up, (disiredRotation.y - previousRotation.y) * (smoothStep - lastStep),Space.World);

            lastStep = smoothStep;
            yield return null;
        }

        if (step > 1.0f)
        {
            go.transform.Translate((disiredPosition.x - previousPostion.x) * (1.0f - lastStep),
                (disiredPosition.y - previousPostion.y) * (1.0f - lastStep),
                (disiredPosition.z - previousPostion.z) * (1.0f - lastStep), Space.World);

            go.transform.Rotate((disiredRotation.x - previousRotation.x) * (1 - lastStep), 0, 0);
            go.transform.Rotate(Vector3.up, (disiredRotation.y - previousRotation.y) * (1 - lastStep),Space.World);
        }
        isMoving = false;
    }

    //<=    =>
    //-1    1
    public void MoveStart(int dir)
    {
        if (!isMoving)
        {
            SetMove(dir);
            StartCoroutine(SmoothMoveRoatate(mainCam,
                previousView.position, disiredView.position,
                previousView.rotation.eulerAngles, disiredView.rotation.eulerAngles,
                moveSpeed));
            previousView = disiredView;
        }
    }

    private void SetMove(int dir)
    {
        moveControllInt += dir;
        switch (moveControllInt)
        {

            case 0:
                disiredView = cookingView;
                cookingPhaseCanvas.SetActive(true);
                break;
            case 1:
                disiredView = beverageView;
                cookingPhaseCanvas.SetActive(false);
                blade.SetActive(false);
                StaticManagement.isUsingKnife = false;
                StaticManagement.ableToInteractWithThings = true;
                break;
            case 2:
                disiredView = defaultView;
                StaticManagement.ableToInteractWithThings = false;
                StaticManagement.isUsingKnife = true;
                moveControllInt = 0;
                break;
            case -1:
                disiredView = fridgeView;
                cookingPhaseCanvas.SetActive(false);
                blade.SetActive(false);
                StaticManagement.isUsingKnife = false;
                StaticManagement.ableToInteractWithThings = true;
                break;
            case -2:
                disiredView = defaultView;
                StaticManagement.ableToInteractWithThings = false;
                StaticManagement.isUsingKnife = true;
                moveControllInt = 0;
                break;
        }
    }

    public void BaldeControll()
    {
        if (blade.activeSelf == true)
        {
            blade.SetActive(false);
            StaticManagement.isUsingKnife = false;
        }
    }
}
