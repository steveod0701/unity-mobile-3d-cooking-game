using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticManagement : MonoBehaviour
{
    StaticManagement staticManagement;
    public static bool isUsingKnife;
    public static List<GameObject> currentDish = new List<GameObject>();
    public static bool isServingTime;
    public static bool ableToInteractWithThings = false;

    private void Start()
    {
        isUsingKnife = false;
        isServingTime = false;
    }
}
