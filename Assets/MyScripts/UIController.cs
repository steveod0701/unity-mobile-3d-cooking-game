using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject foodButtonContainer;
    public GameObject foodButtonPrefab;
    public Transform foodSpawnPoint;
    public GameObject dishSpawn;

    private GameObject[] foodPrefabs;
    private GameObject[] dishPrefabs;
    private Sprite[] foodThumbnails;
    private int foodSpawnControllInt=0;
    private Vector3 startFoodSpawnPoint;
    private Vector3 translateAmountZ = new Vector3(0, 0, 5);
    private Vector3 translateAmountX = new Vector3(3, 0, 0);

    public AudioSource hmmSound;

    private void Awake()
    {
        foodThumbnails = Resources.LoadAll<Sprite>("FoodImages");
        foodPrefabs = Resources.LoadAll<GameObject>("FoodPrefabs");
        dishPrefabs = Resources.LoadAll<GameObject>("DishPrefabs");
        startFoodSpawnPoint = foodSpawnPoint.position;

        foreach (Sprite foodThumbnail in foodThumbnails)
        {
            GameObject instantiatedFB = Instantiate(foodButtonPrefab) as GameObject;
            instantiatedFB.GetComponent<Image>().sprite = foodThumbnail;
            instantiatedFB.transform.SetParent(foodButtonContainer.transform, false);

            string foodName = foodThumbnail.name;

            instantiatedFB.GetComponent<Button>().onClick.AddListener(() => InstantiateFoodPrefab(foodName));
        }
    }

    private void InstantiateFoodPrefab(string foodName)
    {
        foreach(GameObject foodPrefab in foodPrefabs)
        {
            if(foodName == foodPrefab.name)
            {
                Instantiate(foodPrefab,foodSpawnPoint.position,foodPrefab.transform.rotation);
            }
        }

        if(foodSpawnControllInt == 8)
        {
            foodSpawnControllInt = 0;
            foodSpawnPoint.transform.position = startFoodSpawnPoint;
        }

        else if (foodSpawnControllInt == 2 || foodSpawnControllInt == 5)
        {
            foodSpawnPoint.transform.Translate(new Vector3(0, 0, -10));
            foodSpawnPoint.transform.Translate(translateAmountX);
            foodSpawnControllInt++;
        }
        else if (foodSpawnControllInt != 2 || foodSpawnControllInt !=5)
        {
            foodSpawnPoint.transform.Translate(translateAmountZ);
            foodSpawnControllInt++;
        }
    }

    public void ObjectControll(GameObject go)
    {
        if (go.active)
        {
            go.SetActive(false);
        }
        else
        {
            go.SetActive(true);
        }
    }

    public void hmmSoundPlay()
    {
        hmmSound.Play();


    }
    public void SetParentNull(GameObject go)
    {
        if (go.transform.childCount > 2)
        {
            for(int i=2; i<go.transform.childCount; i++)
            {
                go.transform.GetChild(i).SetParent(null);
            }
        }
    }

    public void KnifeControll(GameObject go)
    {
        if (go.active)
        {
            go.SetActive(false);
            StaticManagement.isUsingKnife = false;
        }
        else
        {
            go.SetActive(true);
            StaticManagement.isUsingKnife = true;
        }
    }

    public void InstantiateDish()
    {
        int randomInt = Random.RandomRange(0, dishPrefabs.Length);
        Vector3 dishSpawnPoint = new Vector3(dishSpawn.transform.position.x, dishSpawn.transform.position.y+(dishSpawn.transform.childCount*0.6f), dishSpawn.transform.position.z);
        var instdish = Instantiate(dishPrefabs[randomInt],dishSpawnPoint,dishPrefabs[randomInt].transform.rotation);
        instdish.transform.SetParent(dishSpawn.transform);
    }

}
