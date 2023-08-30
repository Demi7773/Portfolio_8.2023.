using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Resource Text Plug Ins")]
    [SerializeField] Text daysTxt;

    [SerializeField] Text populationTxt;
    [SerializeField] Text workersTxt;
    [SerializeField] Text unemployedTxt;
    [SerializeField] Text soldiersTxt;

    [SerializeField] Text foodTxt;
    [SerializeField] Text woodTxt;
    [SerializeField] Text ironTxt;
    [SerializeField] Text goldTxt;

    [SerializeField] Text farmTxt;
    [SerializeField] Text ironMineTxt;

    [Header("Text Plug Ins")]
    [SerializeField] Text notificationTxt;

    [Header("Buttons")]
    // gather resources buttons
    [SerializeField] Button gatherWoodButton;
    [SerializeField] Button gatherIronButton;

    // sell resources buttons
    [SerializeField] Button sellFoodButton;
    [SerializeField] Button sellWoodButton;
    [SerializeField] Button sellIronButton;

    // buy resources buttons
    [SerializeField] Button buyFoodButton;
    [SerializeField] Button buyWoodButton;
    [SerializeField] Button buyIronButton;

    // explore buttons
    [SerializeField] Button exploreButton;
    [SerializeField] Button huntButton;
    [SerializeField] Button raidButton;

    [Header("Resources")]
    [SerializeField] int days = 1;

    [SerializeField] int population;
    [SerializeField] int workers;
    [SerializeField] int unemployed;
    [SerializeField] int soldiers;

    [SerializeField] int food;
    [SerializeField] int wood;
    [SerializeField] int iron;
    [SerializeField] int gold;

    [SerializeField] int farm;
    [SerializeField] int ironMine;

    [Header("Notification Panel")]
    // List for Notifications
    //List<string> lists = new List<string>();
    Queue<GameObject> queueS = new Queue<GameObject>();
    [SerializeField] GameObject textPrefab;
    [SerializeField] Transform textPosition;

    [Header("Stats")]
    [SerializeField] float dayLength = 24f;
    // modifies food loss
    [SerializeField] float foodLossMin = 0.5f;
    [SerializeField] float foodLossMax = 1f;

    // modifiers for min and max value of population loss
    [SerializeField] float populationLossMin = 0f;
    [SerializeField] float populationLossMax = 0.5f;
    // range of days for population gain
    [SerializeField] float populationGainDaysMin = 1f;
    [SerializeField] float populationGainDaysMax = 3f;
    // threshold for popBoost
    [SerializeField] int populationBoostThreshold = 10;
    [SerializeField] float popBoostModifier = 0.5f;
    // hunt modifiers
    [SerializeField] float huntMin = 0f;
    [SerializeField] float huntMax = 1f;
    // farm and mine modifiers
    [SerializeField] float farmMaxFoodGain = 25f;
    [SerializeField] float mineMaxIronGain = 50f;
    // taxes modifiers
    [SerializeField] float taxesPopThreshold = 200;
    // prices for sell
    [SerializeField] float foodSellPrice = 0.1f;
    [SerializeField] float woodSellPrice = 0.2f;
    [SerializeField] float ironSellPrice = 0.6f;
    // raid mods
    [SerializeField] int raidThreshold = 100;

    [Header("Bools")]
    [SerializeField] bool gameOver = false;
    [SerializeField] bool isPlodan = true;

    [Header("Animators")]
    [SerializeField] Animator huntAnimator;
    [SerializeField] Animator raidAnimator;



    private void Start()
    {
        SetResourceUI();
        StartCoroutine(DayGain());
        StartCoroutine(FoodLoss());
        StartCoroutine(PopGainOverTime());
        StartCoroutine(Taxes());
        StartCoroutine(FarmFood());
        StartCoroutine(MineIron());

        //InvokeRepeating(nameof(Fabjan), 0, 1);
    }

    // UPDATES DAYS
    IEnumerator DayGain()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(dayLength);
            days++;
            Debug.Log($"Day {days}");
            daysTxt.text = $"Day {days}";
        }
    }



    // UPDATES FOOD LOSS BY DAY, IF FOOD < 0 CALLS POPULATIONLOSS
    IEnumerator FoodLoss()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(dayLength);
            int foodLoss = -(int)Random.Range(foodLossMin * population, foodLossMax * population);
            FoodChange(foodLoss);
            Debug.Log($"FoodLoss {-foodLoss}, {food} remaining");
            if (food < 0)
            {
                int popLoss = -(int)Random.Range(population * populationLossMin, population * populationLossMax);
                StarvingPopLoss(popLoss);
            }
        }
    }



    // UPDATES POPULATION GAIN
    IEnumerator PopGainOverTime()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds (Random.Range(populationGainDaysMin * dayLength, populationGainDaysMax * dayLength));

            if (population > populationBoostThreshold && isPlodan)
            {
                int popBoost = (int) Random.Range(1, popBoostModifier * population);
                Debug.Log($"popBoost {popBoost}");
                PopulationChange(popBoost);
            }
            populationTxt.text = $"{population} ppl";
        }
    }
    void StarvingPopLoss(int loss)
    {
        PopulationChange(loss);

        //notificationTxt.text += $"\nWe do not have enough food! {loss} people died!";

        Notifications($"\nWe do not have enough food! {loss} people died!");

        if (population <= 0)
        {
            Debug.LogError("Game Over");
            gameOver = true;
        }
    }



    // TAXES
    IEnumerator Taxes()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(dayLength * 7);

            if (population >= taxesPopThreshold)
            {
                TaxThePopulation();
            }
        }
    }
    void TaxThePopulation()

    {
        int gdChange = (int)(population * 0.4f);
        GoldChange(gdChange);
        Debug.Log($"TAXES: {gdChange} gold gained, gold: {gold}");
    }



    // GATHER RESOURCES
    public void GatherWoodButton()
    {
        Invoke(nameof(GatheredWood), dayLength * 2);
        gatherWoodButton.interactable = false;
    }
    void GatheredWood()
    {
        int wdChange = Random.Range(5, population);
        WoodChange(wdChange);
        Debug.Log($"{wdChange} wood gained, wood: {wood}");
        gatherWoodButton.interactable= true;
    }

    public void GatherIronButton()
    {
        Invoke(nameof(GatheredIron), dayLength * 6);
        gatherIronButton.interactable = false;
    }
    void GatheredIron()
    {
        int irChange = (int) Random.Range(0.5f * population, population);
        IronChange(irChange);
        Debug.Log($"{irChange} iron gained, iron: {iron}");
        gatherIronButton.interactable = true;
    }



    // FARM AND MINE
    public void BuildFarm()
    {
        if (wood >= 20 && iron >= 10 && population >= 4)
        {
            WoodChange(-20);
            IronChange(-10);
            PopulationChange(-2);
            farm++;

            farmTxt.text = $"{farm}";
            Notifications("We have built a farm.");
        }
        else
        {
            Notifications("U broke.");
        }
    }
    IEnumerator FarmFood()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(dayLength);
            int gdChange = 1;
            int fdChange = (int) (farm * Random.Range(0, farmMaxFoodGain));
            FoodChange(fdChange);
            GoldChange(-gdChange);
        }
    }

    public void BuildIronMine()
    {
        if (wood >= 50 && iron >= 10 && gold >= 100 && population >= 7)
        {
            WoodChange(-50);
            IronChange(-10);
            GoldChange(-100);
            PopulationChange(-5);
            ironMine++;

            ironMineTxt.text = $"{ironMine}";
            Notifications("We have built an Iron Mine.");
        }
        else
        {
            Notifications("U broke.");
        }
    }
    IEnumerator MineIron()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(dayLength);
            int fdChange = 15 * ironMine;
            int wdChange = 5 * ironMine;
            int irChange = (int)(ironMine * Random.Range(mineMaxIronGain * 0.5f, mineMaxIronGain));
            FoodChange(-fdChange);
            WoodChange(-wdChange);
            IronChange(irChange);
        }
    }

    public void DismantleIronMine()
    {
        if(ironMine >= 1)
        {
            ironMine--;

        }
    }


    // SELL
    public void SellFoodButton()
    {
        if (food >= 10)
        {
            int fdChange = 10;
            FoodChange(-fdChange);
            int gdChange = (int)(fdChange * foodSellPrice);
            GoldChange(gdChange);
            Debug.Log($"Sold {fdChange} food for {gdChange} gold");
        }
        else
        {
            //notificationTxt.text += "\nGather more iron.";

            Notifications("\nNot enough food to sell.");
        }
    }
    public void SellWoodButton()
    {
        if (wood >= 10)
        {
            int wdChange = 10;
            WoodChange(-wdChange);
            int gdChange = (int)(wdChange * woodSellPrice);
            GoldChange(gdChange);
            Debug.Log($"Sold {-wdChange} wood for {gdChange} gold");
        }
        else
        {
            //notificationTxt.text += "\nGather more wood, dumbass";

            Notifications("\nGather more wood, dumbass");
        }
    }
    public void SellIronButton()
    {
        if (iron >= 10)
        {
            int irChange = 10;
            IronChange(-irChange);
            int gdChange = (int)(irChange * ironSellPrice);
            GoldChange(gdChange);
            Debug.Log($"Sold {-irChange} iron for {gdChange} gold");
        }
        else
        {
            //notificationTxt.text += "\nGather more iron.";

            Notifications("\nGather more iron.");
        }
    }



    // BUY
    public void BuyFood()
    {
        if (gold >= 10)
        {
            int gdChange = 10;
            GoldChange(-gdChange);
            int fdChange = 50;
            FoodChange(fdChange);
        }
        else
        {
            Notifications("Pare nisu problem, para nema");
        }
    }
    public void BuyWood()
    {
        if (gold >= 20)
        {
            int gdChange = 20;
            GoldChange(-gdChange);
            int wdChange = 50;
            WoodChange(wdChange);
        }
        else
        {
            Notifications("Pare nisu problem, para nema");
        }
    }
    public void BuyIron()
    {
        if (gold >= 60)
        {
            int gdChange = 60;
            GoldChange(-gdChange);
            int irChange = 50;
            IronChange(irChange);
        }
        else
        {
            Notifications("Pare nisu problem, para nema");
        }
    }



    // EXPLORE
    public void ExplorationButton()
    {
        StartCoroutine(Explore());
        exploreButton.interactable = false;
    }
    IEnumerator Explore()
    {
        yield return new WaitForSeconds(dayLength);
        //int rollExplore = Random.Range(0, 3);

        // testing
        int rollExplore = Random.Range(1, 3);

        if (rollExplore == 2)
        {
            huntButton.gameObject.SetActive(true);
            huntAnimator.SetBool("isMoving", true);
            //notificationTxt.text += "\nYou have discovered hunting grounds!";

            Notifications("\nYou have discovered hunting grounds!");
        }
        else if (rollExplore == 1)
        {
            raidButton.gameObject.SetActive(true);
            //notificationTxt.text += "\nYou have discovered an enemy town";
            raidAnimator.SetBool("isMoving", true);

            Notifications("\nYou have discovered an enemy town");
        }
        else
        {
            exploreButton.interactable = true;
            //notificationTxt.text += "\nYour scouts have finished exploring and discovered nothing";

            Notifications("\nYour scouts have finished exploring and discovered nothing");
        }
    }

    // HUNT
    public void HuntButton()
    {
        Invoke(nameof(HuntedFood), dayLength);
        huntAnimator.SetBool("isMoving", false);
        huntButton.gameObject.SetActive(false);
    }
    void HuntedFood()
    {
        int fdChange = (int)Random.Range(huntMin * population, huntMax * population);
        FoodChange(fdChange);
        exploreButton.interactable = true;
        Debug.Log($"{fdChange} food gained, food: {food}");
    }

    // RAID
    public void RaidButton()
    {
        if (population > raidThreshold)
        {
            Invoke(nameof(Raid), dayLength * 1f);
            raidAnimator.SetBool("isMoving", false);
            raidButton.gameObject.SetActive(false);
        }
        else
        {
            //notificationTxt.text += "\nMo'š ga jebat";

            Notifications("\nMo'š ga jebat");
        }
    }
    private void Raid()
    {
        exploreButton.interactable = true;
        int raidRoll = Random.Range(0, 3);

        if (raidRoll == 2)
        {
            int popChange = -(int)Random.Range(0.5f * population, population);
            int fdChange = -(int)Random.Range(0.5f * food, food);
            int wdChange = -(int)Random.Range(0.5f * wood, wood);
            int irChange = -(int)Random.Range(0.5f * iron, iron);
            int gdChange = -(int)Random.Range(0.5f * gold, gold);

            ChangeAllResources(popChange, fdChange, wdChange, irChange, gdChange);
            //PopulationChange(popChange);
            //FoodChange(fdChange);
            //WoodChange(wdChange);
            //IronChange(irChange);
            //GoldChange(gdChange);
            //SetResourceUI();

            Debug.Log("Raid fail");

            //notificationTxt.text += "\nGit Gud lol";
            Notifications("\nGit Gud lol");
        }

        else if (raidRoll == 1)
        {
            int popChange = (int)Random.Range(-0.5f * population, 0.5f * population);
            int fdChange = (int)Random.Range(-0.5f * food, 0.5f * food);
            int wdChange = (int)Random.Range(-0.5f * wood, 0.5f * wood);
            int irChange = (int)Random.Range(-0.5f * iron, 0.5f * iron);
            int gdChange = (int)Random.Range(-0.5f * gold, 0.5f * gold);

            ChangeAllResources(popChange, fdChange, wdChange, irChange, gdChange);
            //population += populationChange;
            //food += foodChange;
            //wood += woodChange;
            //iron += ironChange;
            //gold += goldChange;
            //SetResourceUI();

            Debug.Log("Raid neutral");

            //notificationTxt.text += "\nSometimes mabey good sometimes mabey shit";
            Notifications("\nSometimes mabey good sometimes mabey shit");
        }

        else
        {
            int popChange = (int)Random.Range(0.5f * population, population);
            int fdChange = (int)Random.Range(0.5f * food, food);
            int wdChange = (int)Random.Range(0.5f * wood, wood);
            int irChange = (int)Random.Range(0.5f * iron, iron);
            int gdChange = (int)Random.Range(0.5f * gold, gold);

            ChangeAllResources(popChange, fdChange, wdChange, irChange, gdChange);
            //population += populationChange;
            //food += foodChange;
            //wood += woodChange;
            //iron += ironChange;
            //gold += goldChange;
            //SetResourceUI();

            Debug.Log("Raid success");
            //notificationTxt.text += "\nMe go raid, me get stuff";
            Notifications("\nMe go raid, me get stuff");
        }
    }


    // RANDOM EVENTS
    IEnumerator RandomEventGenerator()
    {
        yield return new WaitForSeconds(Random.Range(dayLength, dayLength * 7f));
        int rollEvent = Random.Range(0, 101);

        if(rollEvent <= 5)
        {
            Flood();
        }
        else if(rollEvent <= 15)
        {
            Sifilis();
        }
        else if (rollEvent <= 25)
        {
            Festival();
        }
    }

    void Flood()
    {
        int popChange = (int) Random.Range(population * 0.2f, population * 0.8f);
        int fdChange = (int) Random.Range(food * 0.2f, food * 0.5f);
        int gdChange = (int)Random.Range(gold * 0.2f, gold * 0.4f);

        PopulationChange(popChange);
        FoodChange(fdChange);
        GoldChange(gdChange);
        SetResourceUI();

        //notificationTxt.text += "\nKak se zove ovo jezero";

        Notifications("\nKak se zove ovo jezero");
    }

    void Sifilis()
    {
        isPlodan = false;
        Invoke(nameof(SifilisEffect), dayLength * 7);

        //notificationTxt.text += "\nBurn baby, burn!";

        Notifications("\nBurn baby, burn!");
    }
    void SifilisEffect()
    {
        int popChange = -(int)Random.Range(population * 0.1f, population * 0.2f);
        PopulationChange(popChange);
        isPlodan = true;
    }

    void Festival()
    {
        int popChange = (int) Random.Range(0.2f, 0.4f * population);
        int gdChange = (int)Random.Range(0.2f * population, 0.5f * population);
        int fdChange = -(int)Random.Range(0.1f * population, 0.25f * population);

        PopulationChange(popChange);
        GoldChange(gdChange);
        FoodChange(fdChange);
        
        //notificationTxt.text += "\nDodji na Ultru";

        Notifications("\nDodi na Ultru");

        int coinFlip = Random.Range(0, 10);
        if (coinFlip == 0)
        {
            Sifilis();
        }

        SetResourceUI();
    }

    
    // NOTIFICATIONS
    void Fabjan()
    {
        string pitanje = "pitanje";
        Notifications(pitanje);
    }
    void Notifications(string notification)
    {
        ////test
        //queueS.Enqueue(notification);

        //for (int i = queueS.Count; i > 0; i--)
        //{
        //    if (i >= 5)
        //    {
        //        notificationTxt.text = null;
        //        queueS.Dequeue();
        //    }
        //    else
        //    {
        //        notificationTxt.text += queueS.Dequeue();
        //        notificationTxt.text += "\n";
        //    }
        //}

        if (textPrefab == null || textPosition == null)
        {
            return;
        }

        Text tempText = textPrefab.GetComponent<Text>();
        tempText.text = notification;

        GameObject tempObject = Instantiate(textPrefab, textPosition);

        queueS.Enqueue(tempObject);

        if (queueS.Count > 5)
        {
            GameObject oldQueue = queueS.Dequeue();
            Destroy(oldQueue);
        }
    }


    // RESOURCE CHANGES
    void ChangeAllResources(int popChange, int fdChange, int wdChange, int irChange, int gdChange)
    {
        PopulationChange(popChange);
        FoodChange(fdChange);
        WoodChange(wdChange);
        IronChange(irChange);
        GoldChange(gdChange);
        SetResourceUI();
    }
    void PopulationChange(int popChange)
    {
        population += popChange;
        populationTxt.text = $"{population} ppl";
    }
    void FoodChange(int fdChange)
    {
        food += fdChange;
        foodTxt.text = $"{food} kg";
    }
    void IronChange(int irChange)
    {
        iron += irChange;
        ironTxt.text = $"{iron} kg";
    }
    void WoodChange(int wdChange)
    {
        wood += wdChange;
        woodTxt.text = $"{wood} m";
    }
    void GoldChange(int gdChange)
    {
        gold += gdChange;
        goldTxt.text = $"{gold} gold";
    }

    // UPDATE RESOURCE UI
    void SetResourceUI()
    {
        daysTxt.text = $"Day {days}";

        populationTxt.text = $"{population} ppl";
        workersTxt.text = $"{workers} ppl";
        unemployedTxt.text = $"{unemployed} ppl";
        soldiersTxt.text = $"{soldiers} ppl";

        foodTxt.text = $"{food} kg";
        ironTxt.text = $"{iron} kg";
        woodTxt.text = $"{wood} m";
        goldTxt.text = $"{gold} gold";

        farmTxt.text = $"{farm}";
        ironMineTxt.text = $"{ironMine}";
    }
}
