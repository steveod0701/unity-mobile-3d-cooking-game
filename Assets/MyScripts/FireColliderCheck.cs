using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireColliderCheck : MonoBehaviour
{
    private float roastTime = 2.5f;
    private float boilTime = 3.5f;
    private float burnTime = 4.5f;
    private float fireCanceledTime = 0.8f;

    private List<Food> foodList = new List<Food>();
    public Color roastColor;
    public Color burnColor;
    public Color boilColor;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Food")
        {

            {
                Food food = other.GetComponent<Food>();
                food.firstFireMetTime = Time.time;
                foodList.Add(food);
            }

            
        }
    }

    private void OnTriggerStay(Collider other)
    {        
        for (int i = 0; i < foodList.Count; i++)
        {
            if (foodList[i] == other.gameObject.GetComponent<Food>())
            {
                foodList[i].lastFireMetTime = Time.time;

                Renderer m_renderer = foodList[i].gameObject.GetComponent<Renderer>();

                if (other.transform.parent != null)
                {
                    if (other.transform.parent.tag == "Pot")
                    {
                        if (foodList[i].lastFireMetTime - foodList[i].firstFireMetTime >= boilTime) // roast the food
                        {
                            m_renderer.material.color = boilColor;
                            foodList[i].isBoiled = true;
                            foodList[i].isRaw = false;
                        }
                    }
                }
                else if (foodList[i].lastFireMetTime - foodList[i].firstFireMetTime >= burnTime && foodList[i].isRoasted) // burn the food
                {
                    m_renderer.material.color = burnColor;
                    foodList[i].isBurned = true;
                    foodList[i].isRoasted = false;
                }

                else if (foodList[i].lastFireMetTime - foodList[i].firstFireMetTime >= roastTime && foodList[i].isRaw) // roast the food
                {
                    m_renderer.material.color = roastColor;
                    foodList[i].isRoasted = true;
                    foodList[i].isRaw = false;
                }


            }
        }     
    }

    private void OnTriggerExit(Collider other)
    {

        {
            if (other.tag == "Food")
            {
                Food food = other.GetComponent<Food>();
                food.firstFireMetTime = 0f;
                food.lastFireMetTime = 0f;
                foodList.Remove(food);
            }
        }
    }
}
