using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BuildingsManager : MonoBehaviour
{
    private List<Structure> allStructuresList, allBuildings;
    private Dictionary<string, List<GameObject>> allRoadsList;
    private int numOfHouses=0, numOfBigHouses=0, numOfCraft=0, numOfFarms;
    public float costMultiplier=1.8f, adderIncrease=1, multiplierIncrease=0.2f, probabilityOfTheCornerRoad=0.4f, probabilityOfCreatingAddRoad=0.1f;
    public int leastAmountOfBuildingsToCreateAddRoad=10, maxWidth=950, maxHeight=550, middleWidth=670, middleheight=370, smallWidth=300, smallHeight=150;
    public float houseCost=20, bigHouseCost=40, craftCost=80, roadLength=5, farmCost;
    public Text houseText, bigHouseText, craftText, farmText, numOfHousesText, numOfBigHousesText, numOfCraftHousesText, numOfFarmsText;
    public Button houseButton, bigHouseButton, craftButton, farmButton;
    public GameObject[] houses;
    public GameObject[] bigHouses;
    public GameObject[] roadTypes;
    public GameObject[] craftBuildings;
    public GameObject[] farmingBuildings;
    public PassiveIncomeManager passiveIncomeManager;

    private void Start()
    {
        allStructuresList = new List<Structure>();
        allBuildings = new List<Structure>();
        allRoadsList = new Dictionary<string, List<GameObject>>();

        houseText.text="\n"+houseCost;
        bigHouseText.text="\n"+bigHouseCost;
        craftText.text="\n"+craftCost;
        farmText.text="\n"+farmCost;
        numOfHousesText.text="";
        numOfBigHousesText.text="";
        numOfCraftHousesText.text="";
        numOfFarmsText.text="";

        houseButton.interactable = false;
        bigHouseButton.interactable = false;
        craftButton.interactable = false;
        farmButton.interactable = false;
    }

    private void FixedUpdate()
    {
        if(Balance.getBalance()>=houseCost)
            houseButton.interactable = true;
        else
            houseButton.interactable = false;

        if(Balance.getBalance()>=bigHouseCost)
            bigHouseButton.interactable = true;
        else
            bigHouseButton.interactable = false;

        if(Balance.getBalance()>=craftCost)
            craftButton.interactable = true;
        else
            craftButton.interactable = false;

        if(Balance.getBalance()>=farmCost)
            farmButton.interactable = true;
        else
            farmButton.interactable = false;

        //Debug.Log(Screen.width+"; "+Screen.height);
    }


    public void buyHouse()
    {
        Balance.updateBalance(houseCost);
        Balance.updateAdder(adderIncrease);
        houseCost*=costMultiplier;
        houseText.text="\n"+Balance.outputCostCorrectly(houseCost);
        numOfHouses++;
        numOfHousesText.text="X"+numOfHouses;
        createStructure(houses, "House", true);
    }


    public void buyBigHouse()
    {
        Balance.updateBalance(bigHouseCost);
        Balance.updateMultiplier(multiplierIncrease);
        bigHouseCost*=costMultiplier;
        bigHouseText.text="\n"+Balance.outputCostCorrectly(bigHouseCost);
        numOfBigHouses++;
        numOfBigHousesText.text="X"+numOfBigHouses;
        createStructure(bigHouses, "BigHouse", true);
    }


    public void buyCraftHouse()
    {
        Balance.updateBalance(craftCost);
        craftCost*=costMultiplier;
        craftText.text="\n"+Balance.outputCostCorrectly(craftCost);
        numOfCraft++;
        numOfCraftHousesText.text="X"+numOfCraft;

        GameObject newCraft = createStructure(craftBuildings, "CraftHouse", true);
        CraftHouseBehaviour newBehaviour = newCraft.GetComponent<CraftHouseBehaviour>();
        newBehaviour.passiveIncomeManager = passiveIncomeManager;
    }


    public void buyFarm()
    {
        Balance.updateBalance(farmCost);
        farmCost*=costMultiplier;
        farmText.text="\n"+Balance.outputCostCorrectly(farmCost);
        numOfFarms++;
        numOfFarmsText.text="X"+numOfFarms;

        GameObject newFarm = createStructure(farmingBuildings, "Farm", false);
        FarmBehaviour newBehaviour = newFarm.GetComponent<FarmBehaviour>();
        newBehaviour.passiveIncomeManager = passiveIncomeManager;
    }


    private GameObject createStructure(GameObject[] structures, string type, bool createRoad)
    {
        int y=-1, x=-1;
        switch(type)
        {
            case "CraftHouse":
                y = UnityEngine.Random.Range(-(smallHeight),(smallHeight)+1);
                x = UnityEngine.Random.Range(-(smallWidth),(smallWidth)+1);
                break;
            case "House":
            case "BigHouse":
                generateRandomBetween(-middleWidth,middleWidth,-middleheight,middleheight,-smallWidth,smallWidth,-smallHeight,smallHeight,out x, out y);
                break;
            case "Farm":
                generateRandomBetween(-maxWidth,maxWidth,-maxHeight,maxHeight,-middleWidth,middleWidth,-middleheight,middleheight,out x, out y);
                break;
            default:
                Debug.Log("Unknown building type!");
                break;
        }

        int houseNum = UnityEngine.Random.Range(0,structures.Length);

        GameObject newHouse = Instantiate(structures[houseNum], new Vector3(x,y,y+maxHeight), Quaternion.Euler(0f,0f,0f));
        Structure newStruct = new Structure(newHouse,x,y,type);
        allStructuresList.Add(newStruct);
        allBuildings.Add(newStruct);

        if(createRoad)
        {
            createRoadStructure(allBuildings.Count-1);

            if(allBuildings.Count>=leastAmountOfBuildingsToCreateAddRoad)
            {
                if(UnityEngine.Random.Range(0f,1f)<=probabilityOfCreatingAddRoad)
                {
                    createRoadStructure(UnityEngine.Random.Range(0,allBuildings.Count),UnityEngine.Random.Range(0,allBuildings.Count));
                }
            }
        }

        return newHouse;
    }


    private void generateRandomBetween(int x_min, int x_max, int y_min, int y_max, int x_inner_min, int x_inner_max, int y_inner_min, int y_inner_max, out int x, out int y)
    {
        int A1 = (x_inner_max - x_inner_min) * (y_max - y_inner_max);
        int A2 = (x_inner_max - x_inner_min) * (y_inner_min - y_min); 
        int A3 = (x_inner_min - x_min) * (y_max - y_min);
        int A4 = (x_max - x_inner_max) * (y_max - y_min); 

        int A_total = A1 + A2 + A3 + A4;

        int r = UnityEngine.Random.Range(0, A_total);

        if (r < A1)
        {
            x = UnityEngine.Random.Range(x_inner_min, x_inner_max);
            y = UnityEngine.Random.Range(y_inner_max, y_max);
        }
        else if(r < A1 + A2)
        {
            x = UnityEngine.Random.Range(x_inner_min, x_inner_max);
            y = UnityEngine.Random.Range(y_min, y_inner_min);
        }
        else if(r < A1 + A2 + A3)
        {
            x = UnityEngine.Random.Range(x_min, x_inner_min);
            y = UnityEngine.Random.Range(y_min, y_max);
        }
        else
        {
            x = UnityEngine.Random.Range(x_inner_max, x_max);
            y = UnityEngine.Random.Range(y_min, y_max);
        }
    }



    private void createRoadStructure(int index1)
    {
        if(allBuildings.Count>1)
        {
            int index2=findTheClosest(index1);

            if(!allRoadsList.ContainsKey(index1+""+index2+"") && !allRoadsList.ContainsKey(index2+""+index1+""))
                createRoad(allBuildings[index1].getX(), allBuildings[index1].getY(), allBuildings[index2].getX(), allBuildings[index2].getY(),index1,index2);
        }
    }


    private void createRoadStructure(int index1, int index2)
    {
        if(!allRoadsList.ContainsKey(index1+""+index2+"") && !allRoadsList.ContainsKey(index2+""+index1+""))
        {
            Debug.Log("Additional Road!");
            createRoad(allBuildings[index1].getX(), allBuildings[index1].getY(), allBuildings[index2].getX(), allBuildings[index2].getY(),index1,index2);
        }
    }


    private int findTheClosest(int indexTarget)
    {
        double minDistance=100000;
        int closestIndex=-1;
        for(int i=0; i<allBuildings.Count-1; i++)
        {
            if(Math.Sqrt(Math.Pow(allBuildings[i].getX()-allBuildings[indexTarget].getX(),2)+Math.Pow(allBuildings[i].getY()-allBuildings[indexTarget].getY(),2))<minDistance)
            {
                closestIndex = i;
                minDistance=Math.Sqrt(Math.Pow(allBuildings[i].getX()-allBuildings[indexTarget].getX(),2)+Math.Pow(allBuildings[i].getY()-allBuildings[indexTarget].getY(),2));
            }
        }

        return closestIndex;
    }


    private void createRoad(int x1, int y1, int x2, int y2, int i1, int i2)
    {
        List<GameObject> tempPartsOfRoad = new List<GameObject>();
        //Debug.Log("Coord1: "+x1+", "+y1+"; Coord2: "+x2+", "+y2+"; ids: "+i1+", "+i2);

        if(UnityEngine.Random.Range(0f,1f)<probabilityOfTheCornerRoad)
        {
            if(UnityEngine.Random.Range(0,2)==0)
            {
                if(y1>y2)
                    tempPartsOfRoad.Add(createVerticalRoad(y1,y1,y2,x1,-1));
                else
                    tempPartsOfRoad.Add(createVerticalRoad(y2,y1,y2,x1,1));

                if(x1>x2)
                    tempPartsOfRoad.Add(createHorizontalRoad(y2,x1,x2,-1));
                else
                    tempPartsOfRoad.Add(createHorizontalRoad(y2,x1,x2,1));
            }
            else
            {
                if(x1>x2)
                    tempPartsOfRoad.Add(createHorizontalRoad(y1,x1,x2,-1));
                else
                    tempPartsOfRoad.Add(createHorizontalRoad(y1,x1,x2,1));

                if(y1>y2)
                    tempPartsOfRoad.Add(createVerticalRoad(y1,y1,y2,x2,-1));
                else
                    tempPartsOfRoad.Add(createVerticalRoad(y2,y1,y2,x2,1));
            }
        }
        else
        {
            Debug.Log("Unusual road!");
        }

        allRoadsList.Add(i1+""+i2+"",tempPartsOfRoad);
        //Debug.Log(i1+""+i2+"");
    }


    public GameObject createVerticalRoad(int h, int y1, int y2, int x, int operand)
    {   
        int y;
        if(operand==-1)
            y=y1-(Math.Abs(y1-y2)/2);
        else
            y=y1+(Math.Abs(y1-y2)/2);

        GameObject newPart = Instantiate(roadTypes[0], new Vector3(x,y,h+20+maxHeight), Quaternion.Euler(0f,0f,0f));
        newPart.transform.localScale=new Vector3(roadLength,Math.Abs(y1-y2),1);
        allStructuresList.Add(new Structure(newPart,x,y,"RoadPart"));
        return newPart;
    }


    public GameObject createHorizontalRoad(int height, int x1, int x2, int operand)
    {
        int x;
        if(operand==-1)
            x=x1-(Math.Abs(x1-x2)/2);
        else
            x=x1+(Math.Abs(x1-x2)/2);

        GameObject newPart = Instantiate(roadTypes[0], new Vector3(x,height,height+20+maxHeight), Quaternion.Euler(0f,0f,0f));
        newPart.transform.localScale=new Vector3(Math.Abs(x1-x2),roadLength,1);
        allStructuresList.Add(new Structure(newPart,x,height,"RoadPart"));
        return newPart;
    }
}
