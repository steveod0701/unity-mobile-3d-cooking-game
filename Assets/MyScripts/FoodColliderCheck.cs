using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodColliderCheck : MonoBehaviour
{
    private GameObject holder;
    private GameObject parent;

    public List<GameObject> foodsOnDish = new List<GameObject>();
    public string currentFoodName;

    private void Awake()
    {
        holder = transform.parent.gameObject;
        parent = holder.transform.parent.gameObject;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            other.transform.SetParent(holder.transform);
            foodsOnDish.Add(other.gameObject);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            other.transform.SetParent(null);
            foodsOnDish.Remove(other.gameObject);
        }


    }
}

