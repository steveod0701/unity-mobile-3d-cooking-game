using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsFoodOnTable : MonoBehaviour
{
    private Camera cam; 
    public GameManager gameManager;
    public List<GameObject> foods = new List<GameObject>();
    private Transform holder;

    public int avocado;
    public int banana;
    public int bread;
    public int broccoli;
    public int carrot;
    public int cheese;
    public int leek;
    public int lemon;
    public int mushroom;
    public int onion;
    public int paprika;
    public int pear;
    public int pepper;
    public int pork;
    public int rice;
    public int salad;
    public int sandwich;
    public int sardine;
    public int tomato;

    private bool isBurnFoodOnDish=false;
    private bool isCuttedFoodOnDish = false;
    private bool isTooSalty = false;
    private bool isTooSpicy = false;

    public List<Food> avocados = new List<Food>();
    public List<Food> bananas = new List<Food>();
    public List<Food> breads = new List<Food>();
    public List<Food> broccolis = new List<Food>();
    public List<Food> carrots = new List<Food>();
    public List<Food> cheeses = new List<Food>();
    public List<Food> leeks = new List<Food>();
    public List<Food> lemons = new List<Food>();
    public List<Food> mushrooms = new List<Food>();
    public List<Food> onions = new List<Food>();
    public List<Food> paprikas = new List<Food>();
    public List<Food> pears = new List<Food>();
    public List<Food> peppers = new List<Food>();
    public List<Food> porks = new List<Food>();
    public List<Food> rices = new List<Food>();
    public List<Food> salads = new List<Food>();
    public List<Food> sandwichs = new List<Food>();
    public List<Food> sardines = new List<Food>();
    public List<Food> tomatos = new List<Food>();
    public List<Food> allFoods = new List<Food>();

    public Text successText;
    public Text failText;
    public GameObject successButton;
    public GameObject failButton;

    public string currentFood;

    private void Awake()
    {
        cam = Camera.main;
        gameManager = gameManager.GetComponent<GameManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Dish" )
        {
            /*if (other.name == "Rice(Clone)")
                return;*/

            other.transform.rotation = Quaternion.Euler(new Vector3(-90,180,0));
            gameManager.moveControllInt = 0;
            gameManager.previousView = cam.transform;
            StaticManagement.ableToInteractWithThings = false;
            cam.transform.position = gameManager.defaultView.position;
            cam.transform.rotation = gameManager.defaultView.rotation;
            gameManager.isMoving = false;
            gameManager.cookingPhaseCanvas.SetActive(false);
            ClearCurretnFoods();
            currentFood = null;

            holder = other.transform.GetChild(0);
            for(int i = 1; i < holder.childCount; i++)
            {
                foods.Add(holder.GetChild(i).gameObject);
                allFoods.Add(holder.GetChild(i).gameObject.GetComponent<Food>());
            }

            for(int i = 0; i < foods.Count; i++)
            {
                CheckFood(foods[i]);
                if (allFoods[i].isBurned == true)
                {
                    isBurnFoodOnDish = true;
                }
                if (allFoods[i].isCutted == true)
                {
                    isCuttedFoodOnDish = true;
                }
                if (allFoods[i].salt > 80)
                {
                    isTooSalty = true;
                }
                if (allFoods[i].pepper > 80)
                {
                    isTooSpicy = true;
                }
            }

            //many many if statements!!!
            {
                //nothing
                if (foods.Count == 0)
                {
                    failText.text = "공기를 먹으면 되나요...?\nㅜㅜ";
                    failButton.SetActive(true);
                    return;
                }

                else if (isBurnFoodOnDish)
                {
                    failText.text = "탄 음식을 먹을 수는 없어요...\n;ㅅ;";
                    failButton.SetActive(true);
                }

                else if(isTooSalty && isTooSpicy)
                {
                    failText.text = "너무 짜고...\n너무 맵네요...\n앗. 이것이 한국의 맛...?";
                    failButton.SetActive(true);
                }

                else if (isTooSalty)
                {
                    failText.text = "너무 짜요...\n이렇게 짜게 드시면 건강에 안좋답니다...";
                    failButton.SetActive(true);
                }

                else if (isTooSpicy)
                {
                    failText.text = "너무 맵네요...\n음식 맛이 전혀 느껴지지 않아요...";
                    failButton.SetActive(true);
                }

                else if (rice > 0)
                {
                    for (int i = 0; i < rices.Count; i++)
                    {
                        if (rices[i].isBoiled == false)
                        {
                            failText.text = "요즘 밀가루 음식이 많다보니...\n쌀을 어떻게 해야 하는지 까먹으신건가요...?";
                            failButton.SetActive(true);
                            return;
                        }
                        else
                        {
                            successText.text = "\n우리가 만든 것 우리가 먹자...\n우리 손으로 맨든...\n흰 쌀밥...\nyou made:\n그냥 쌀밥";
                            successButton.SetActive(true);
                        }
                    }
                }


                //not sliced
                else if (allFoods.Count < 2 && isCuttedFoodOnDish)
                {
                    failText.text = "한 조각...뿐인가요...?\n공복인데...";
                    failButton.SetActive(true);
                }

                else if (allFoods.Count < 2)
                {
                    failText.text = "먹기 좋도록 써는게 좋겠어요...\n우리는 문명인이니까요...";
                    failButton.SetActive(true);
                }

                // hardcore for pork!!!
                else if (pork > 0)
                {
                    for (int i = 0; i < porks.Count; i++)
                    {
                        if (porks[i].isRaw == true)
                        {
                            failText.text = "돼지고기...육회...?\n별로 먹고싶지 않네요...8ㅅ8";
                            failButton.SetActive(true);
                            return;
                        }
                    }

                    for (int i = 0; i < rices.Count; i++)
                    {
                        if (rices[i].isRaw == true)
                        {
                            failText.text = "생쌀이라니...\n지금은 21세기랍니다...";
                            failButton.SetActive(true);
                            return;
                        }
                    }

                    for (int i = 0; i < rices.Count; i++)
                    {
                        if (rices[i].isRoasted == true)
                        {
                            failText.text = "네 맞습니다...\n쌀은 구울수도 있죠...\n볶음밥...?";
                            failButton.SetActive(true);
                            return;
                        }
                    }

                    for (int i = 0; i < peppers.Count; i++)
                    {
                        if (peppers[i].isRaw == false)
                        {
                            failText.text = "당신은 고추를 조리해선 안됬어요...\n그리고 이게 그 결과입니다...";
                            failButton.SetActive(true);
                            return;
                        }
                    }


                    for (int i = 0; i < onions.Count; i++)
                    {
                        if (onions[i].isBoiled == false)
                        {
                            failText.text = "양파를 삶아주세요...\n그런 게임입니다...\n#주입식 요리";
                            failButton.SetActive(true);
                            return;
                        }
                    }

                    var porkSpicy = 0;
                    for (int i = 0; i < porks.Count; i++)
                    {

                        porkSpicy += porks[i].pepper;
                        if (porkSpicy == 0)
                        {
                            failText.text = "후추...후추...\n고기에 후추를 뿌리지 않으면...\n실패버튼을 다시 보게될거에요...";
                            failButton.SetActive(true);
                            return;
                        }
                    }

                    if (isCuttedFoodOnDish == false)
                    {
                        failText.text = "재료를...잘라주세요...\n먹기힘드니까요...";
                        failButton.SetActive(true);
                        return;
                    }

                    else if (pork > 0 && rice > 0 && onion > 0 && pepper > 0)
                    {
                        successText.text = "훌륭한 제육 정식...!\n보통 많이 먹으려면 '정식'이\n들어간 메뉴를 고르죠...\nyou made:\n제육 정식!";
                        successButton.SetActive(true);
                    }

                    else
                    {
                        failText.text = "나쁘진 않은데...\n레시피대로 다시 해보시는게 어떨까요...?";
                        failButton.SetActive(true);
                        return;
                    }
                }

                else if(bread>0 && cheese > 0)
                {
                    for (int i = 0; i < breads.Count; i++)
                    {
                        if (breads[i].isRaw == true)
                        {
                            failText.text = "빵을 구웁시다...\n룰에 맞게 살아야죠...8ㅅ8";
                            failButton.SetActive(true);
                            return;
                        }
                    }

                    var breadSalty = 0;
                    for (int i = 0; i < breads.Count; i++)
                    {

                        breadSalty += breads[i].salt;
                        if (breadSalty == 0)
                        {
                            failText.text = "빵...에...소금...\n이...없어요..";
                            failButton.SetActive(true);
                            return;
                        }
                    }

                    for (int i = 0; i < broccolis.Count; i++)
                    {
                        if (broccolis[i].isBoiled == false)
                        {
                            failText.text = "와...\n브로콜리가 안삶아졌네요...\n실패버튼 +1 드립니다...";
                            failButton.SetActive(true);
                            return;
                        }
                    }

                    if (isCuttedFoodOnDish == false)
                    {
                        failText.text = "와...\n재료가 안잘라젔네요...\n실패버튼 하나 더 드립니다...";
                        failButton.SetActive(true);
                        return;
                    }
                    
                    else if(bread>0 && cheese>0 && broccoli > 0)
                    {
                        successText.text = "사실 이 메뉴는...\n메뉴판에 5개를 채우려고...\n고안된 메뉴입니다...\nyou made:\n토스트!";
                        successButton.SetActive(true);
                        return;
                    }

                    else
                    {
                        failText.text = "나쁘진 않은데...\n레시피대로 다시 해보시는게 어떨까요...?";
                        failButton.SetActive(true);
                        return;
                    }
                }

                //sardine dish
                else if (sardine > 0)
                {
                    for (int i = 0; i < sardines.Count; i++)
                    {
                        if (sardines[i].isRaw == true)
                        {
                            failText.text = "날생선을 먹을 수는 없어요...\n8ㅅ8";
                            failButton.SetActive(true);
                            return;
                        }
                    }
                    for(int i=0; i<mushrooms.Count; i++)
                    {
                        if (mushrooms[i].isRaw == true)
                        {
                            failText.text = "버섯이 다 익지 않았네요...\n아쉽습니다...";
                            failButton.SetActive(true);
                            return;
                        }
                    }

                    for (int i = 0; i < onions.Count; i++)
                    {
                        if (onions[i].isRoasted == true)
                        {
                            failText.text = "왜 양파를 익힌 것이지...?\n자기과시...?";
                            failButton.SetActive(true);
                            return;
                        }
                    }

                    var sardineSalty = 0;
                    for (int i = 0; i < sardines.Count; i++)
                    {
                        
                        sardineSalty += sardines[i].salt;
                        if (sardineSalty==0)
                        {
                            failText.text = "왜 소금간을 하지 않았죠...?\n21세기에 소금이 그리도 귀했던가요...?";
                            failButton.SetActive(true);
                            return;
                        }
                    }

                    if (onion <2  || sardine <2)
                    {
                        failText.text = "재료들을 조금 써는게 좋겠어요...\n먹기가 힘드네요...";
                        failButton.SetActive(true);
                        return;
                    }

                    else if (onion > 1 && mushroom>0 && sardine > 1)
                    {
                        successText.text = "멋진 생선요리와 함께 즐거운 식사를...\nyou made:\n정어리구이.";
                        successButton.SetActive(true);
                        return;
                    }

                    else
                    {
                        failText.text = "나쁘진 않은데...\n레시피대로 다시 해보시는게 어떨까요...?";
                        failButton.SetActive(true);
                        return;
                    }
                }
                //bread dish
                else if(bread > 0)
                {
                    successText.text = "정말 배고프다면 빵만 먹어도 맛있답니다...\nyou made:\n그냥 빵.";
                    successButton.SetActive(true);
                }

                else if(avocado>0 && tomato>0 && pear>0&& lemon > 0 && banana>0)
                {
                    if(avocado==1 || tomato ==2 || pear==1 || lemon==1 || banana == 1)
                    {
                        if (isCuttedFoodOnDish == false)
                        {
                            failText.text = "재료중 잘리지 않은게 있네요...\n정성껏 잘라 보는게 어떨까요...?";
                            failButton.SetActive(true);
                            return;
                        }
                    }
                    else if (isCuttedFoodOnDish == true)
                    {
                        successText.text = "아보카도와 레몬까지 들어간...\n고급 샐러드네요...\nyou made:\n과일 샐러드!";
                        successButton.SetActive(true);
                    }
                    else
                    {
                        successText.text = "제작자의 if문 실수...\n죄송합니다...8ㅅ8\nyou made:\n과일 샐러드!";
                        successButton.SetActive(true);
                    }
                }
                else if (avocado > 0)
                {
                    successText.text = "아보카도는 밤과 비슷한 맛이라고 하네요...\nyou made:\n그냥 아보카도.";
                    successButton.SetActive(true);
                }

                else if (banana > 0)
                {
                    successText.text = "im a banana...\nim a banana...\nyou made:\n그냥 banana.";
                    successButton.SetActive(true);
                }

                else if (broccoli > 0)
                {
                    successText.text = "제작자는 브로콜리를 아주 좋아한다고 하네요...\nyou made:\n그냥 브로콜리.";
                    successButton.SetActive(true);
                }

                //boild broccoli add please!

                else if (carrot > 0)
                {
                    successText.text = "눈에 좋다는 당근...\n저도 참 좋아하는데요...\nyou made:\n그냥 당근.";
                    successButton.SetActive(true);
                }

                else if (cheese > 0)
                {
                    successText.text = "치즈 하면 삼각형이 떠오르시나요...?\n아니면 사각형이 떠오르시나요...?\nyou made:\n그냥 치즈.";
                    successButton.SetActive(true);
                }

                else if (leek > 0)
                {
                    successText.text = "흥겹게 파를 돌리던\n한 소녀가 생각나네요...\nyou made:\n그냥 파.";
                    successButton.SetActive(true);
                }

                else if (lemon > 0)
                {
                    successText.text = "all that can i eat is\njust a yellow lemon...♩♪\nyou made:\n그냥 레몬.";
                    successButton.SetActive(true);
                }

                else if (mushroom > 0)
                {
                    successText.text = "1up! 1up! 1up!\n혹은 몸이 커질수도 있겠죠...\nyou made:\n그냥 버섯.";
                    successButton.SetActive(true);
                }

                else if (onion > 0)
                {
                    successText.text = "자르면서 눈물이 나오시진 않으셨죠...?\n만일 그랬다면 사과드립니다...\nyou made:\n그냥 양파.";
                    successButton.SetActive(true);
                }

                else if (paprika > 0)
                {
                    successText.text = "피망? 파프리카? 피망? 파프리카?\n파프리카? 피망? 파프리카? 피망?\nyou made:\n그냥 파프리카.";
                    successButton.SetActive(true);
                }

                else if (pear > 0)
                {
                    successText.text = "더 좋은 모델, 더 완벽한 시스템을 원했지만...\n개발은 힘드네요...\nyou made:\n그냥 배.";
                    successButton.SetActive(true);
                }

                else if (pepper > 0)
                {
                    successText.text = "고추가 맵게 생기지 않았나요...?\nyou made:\n그냥 고추";
                    successButton.SetActive(true);
                }


                else if (salad > 0)
                {
                    successText.text = "채식을 시도해본 적이 있는데...\n저는 못하겠습니다...\nyou made:\n그냥 이파리";
                    successButton.SetActive(true);
                }

                else if (sandwich > 0)
                {
                    successText.text = "모델이 있길래 넣어본\n완제품 샌드위치...\n반짝반짝 빛나는 샌드위치...\nyou made:\n그냥 샌드위치";
                    successButton.SetActive(true);
                }

                else if (tomato > 0)
                {
                    successText.text = "나는야~ 주스될거야~\n나는야~ 케첩될거야~\nyou made:\n그냥 토마토";
                    successButton.SetActive(true);
                }

                //my mistake
                else
                {
                    successText.text = "저의 if 문에 허점이 있었네요...\n미안해요...";
                    successButton.SetActive(true);
                }
            }
        }
    }

    
    
    public void ClearCurretnFoods()
    {
        foods.Clear();
        avocado=0;
        banana=0;
        bread=0;
        broccoli=0;
        carrot=0;
        cheese=0;
        leek=0;
        lemon=0;
        mushroom=0;
        onion=0;
        paprika=0;
        pear=0;
        pepper=0;
        pork=0;
        rice=0;
        salad=0;
        sandwich=0;
        sardine=0;
        tomato=0;
        avocados.Clear();
        bananas.Clear();
        breads.Clear();
        broccolis.Clear();
        carrots.Clear();
        cheeses.Clear();
        leeks.Clear();
        lemons.Clear();
        mushrooms.Clear();
        onions.Clear();
        paprikas.Clear();
        pears.Clear();
        peppers.Clear();
        porks.Clear();
        rices.Clear();
        salads.Clear();
        sandwichs.Clear();
        sardines.Clear();
        tomatos.Clear();
        allFoods.Clear();

        isBurnFoodOnDish = false;
        isCuttedFoodOnDish = false;
        isTooSalty = false;
        isTooSpicy = false;
    }

    public void CheckFood(GameObject food)
    {
        if (food.name == "Avocado(Clone)")
        {
            avocado++;
            avocados.Add(food.GetComponent<Food>());
        }
        else if (food.name == "Banana(Clone)")
        {
            banana++;
            bananas.Add(food.GetComponent<Food>());
        }

        else if (food.name == "Bread(Clone)")
        {
            bread++;
            breads.Add(food.GetComponent<Food>());
        }
        else if (food.name == "Broccoli(Clone)")
        {
            broccoli++;
            broccolis.Add(food.GetComponent<Food>());
        }
        else if (food.name == "Carrot(Clone)")
        {
            carrot++;
            carrots.Add(food.GetComponent<Food>());
        }
        else if (food.name == "Cheese(Clone)")
        {
            cheese++;
            cheeses.Add(food.GetComponent<Food>());
        }
        else if (food.name == "Leek(Clone)")
        {
            leek++;
            leeks.Add(food.GetComponent<Food>());
        }
        else if (food.name == "Lemon(Clone)")
        {
            lemon++;
            lemons.Add(food.GetComponent<Food>());
        }
        else if (food.name == "Mushroom(Clone)")
        {
            mushroom++;
            mushrooms.Add(food.GetComponent<Food>());
        }
        else if (food.name == "Onion(Clone)")
        {
            onion++;
            onions.Add(food.GetComponent<Food>());
        }
        else if (food.name == "Paprika(Clone)")
        {
            paprika++;
            paprikas.Add(food.GetComponent<Food>());
        }
        else if (food.name == "Pear(Clone)")
        {
            pear++;
            pears.Add(food.GetComponent<Food>());
        }
        else if (food.name == "Pepper(Clone)")
        {
            pepper++;
            peppers.Add(food.GetComponent<Food>());
        }
        else if (food.name == "Pork(Clone)")
        {
            pork++;
            porks.Add(food.GetComponent<Food>());
        }
        else if (food.name == "Rice(Clone)")
        {
            rice++;
            rices.Add(food.GetComponent<Food>());
        }
        else if (food.name == "Salad(Clone)")
        {
            salad++;
            salads.Add(food.GetComponent<Food>());
        }
        else if (food.name == "Sandwich(Clone)")
        {
            sandwich++;
            sandwichs.Add(food.GetComponent<Food>());
        }
        else if (food.name == "Sardine(Clone)")
        {
            sardine++;
            sardines.Add(food.GetComponent<Food>());
        }
        else if (food.name == "Tomato(Clone)")
        {
            tomato++;
            tomatos.Add(food.GetComponent<Food>());
        }
    }
}
