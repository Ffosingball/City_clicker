using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BuildingsManager : MonoBehaviour
{
    private List<GameObject> allStructures;
    [HideInInspector]
    public List<Structure> allBuildings;
    [HideInInspector]
    public Dictionary<string, List<RoadStructure>> allRoadsList;
    [HideInInspector]
    public Dictionary<string, GameObject> allFarms;
    [HideInInspector]
    public List<PassiveIncomeStructure> allPassiveIncomesBuilds;
    [HideInInspector]
    public int numOfHouses=0, numOfBigHouses=0, numOfCraft=0, numOfFarms;
    public float costMultiplier=1.8f, adderIncrease=1, multiplier=1.15f, probabilityOfCreatingARoad=0.95f, probabilityOfCreatingAddRoad=0.1f;
    public int leastAmountOfBuildingsToCreateAddRoad=10, maxWidth=950, maxHeight=550, middleWidth=670, middleheight=370, smallWidth=300, smallHeight=150, farmWidth=130, farmHeight=100, farmWdisplacement=15, farmHdisplacement=10;
    public float houseCost=20, bigHouseCost=40, craftCost=80, roadLength=5, farmCost;
    public Text houseText, bigHouseText, craftText, farmText, numOfHousesText, numOfBigHousesText, numOfCraftHousesText, numOfFarmsText;
    public Button houseButton, bigHouseButton, craftButton, farmButton;
    public GameObject[] houses;
    public GameObject[] bigHouses;
    public GameObject[] roadTypes;
    public GameObject[] craftBuildings;
    public GameObject[] farmingBuildings;
    public PassiveIncomeManager passiveIncomeManager;
    public SoundManager soundManager;
    public MapGenerator mapGenerator;

    private void Start()
    {
        allBuildings = new List<Structure>();
        allRoadsList = new Dictionary<string, List<RoadStructure>>();
        allFarms = new Dictionary<string, GameObject>();
        allPassiveIncomesBuilds = new List<PassiveIncomeStructure>();
        allStructures = new List<GameObject>();

        resetUI();

        Balance.setMultiplier(multiplier);
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
        soundManager.PlayPurchaseSound();
        Balance.updateBalance(houseCost);
        Balance.updateMultiplier();
        houseCost*=costMultiplier;
        houseText.text="\n"+Balance.outputCostCorrectly(houseCost);
        numOfHouses++;
        numOfHousesText.text="X"+numOfHouses;
        createStructure(houses, "House", true);
        passiveIncomeManager.increaseIncomePerHouse(1);
    }


    public void buyBigHouse()
    {
        soundManager.PlayPurchaseSound();
        Balance.updateBalance(bigHouseCost);
        Balance.updateAdder(adderIncrease);
        bigHouseCost*=costMultiplier;
        bigHouseText.text="\n"+Balance.outputCostCorrectly(bigHouseCost);
        numOfBigHouses++;
        numOfBigHousesText.text="X"+numOfBigHouses;
        createStructure(bigHouses, "BigHouse", true);
        passiveIncomeManager.increaseIncomePerBigHouse(1);
    }


    public void buyCraftHouse()
    {
        soundManager.PlayPurchaseSound();
        Balance.updateBalance(craftCost);
        craftCost*=costMultiplier;
        craftText.text="\n"+Balance.outputCostCorrectly(craftCost);
        numOfCraft++;
        numOfCraftHousesText.text="X"+numOfCraft;

        GameObject newCraft = createStructure(craftBuildings, "CraftHouse", true);
        CraftHouseBehaviour newBehaviour = newCraft.GetComponent<CraftHouseBehaviour>();
        newBehaviour.passiveIncomeManager = passiveIncomeManager;
        newBehaviour.soundManager = soundManager;
    }


    public void buyFarm()
    {
        soundManager.PlayPurchaseSound();
        Balance.updateBalance(farmCost);
        farmCost*=costMultiplier;
        farmText.text="\n"+Balance.outputCostCorrectly(farmCost);
        numOfFarms++;
        numOfFarmsText.text="X"+numOfFarms;

        GameObject newFarm = createStructure(farmingBuildings, "Farm", false);
        FarmBehaviour newBehaviour = newFarm.GetComponent<FarmBehaviour>();
        newBehaviour.passiveIncomeManager = passiveIncomeManager;
        newBehaviour.soundManager = soundManager;
    }


    public void decreseCostBigHouse(float decreaseBy)
    {
        bigHouseCost/=decreaseBy;
        bigHouseText.text="\n"+Balance.outputCostCorrectly(bigHouseCost);
    }


    public void decreseCostHouse(float decreaseBy)
    {
        houseCost/=decreaseBy;
        houseText.text="\n"+Balance.outputCostCorrectly(houseCost);
    }


    private GameObject createStructure(GameObject[] structures, string type, bool createRoad)
    {
        string key="";
        int y=-1, x=-1;
        int houseNum = UnityEngine.Random.Range(0,structures.Length);
        Structure newStruct;
        PassiveIncomeStructure newStructPass;

        switch(type)
        {
            case "CraftHouse":
                y = UnityEngine.Random.Range(-(smallHeight),(smallHeight)+1);
                x = UnityEngine.Random.Range(-(smallWidth),(smallWidth)+1);
                newStructPass = new PassiveIncomeStructure(x,y,type,houseNum,"");
                allPassiveIncomesBuilds.Add(newStructPass);
                newStruct = newStructPass;
                break;
            case "House":
                generateRandomBetween(-middleWidth,middleWidth,-middleheight,middleheight,-smallWidth,smallWidth,-smallHeight,smallHeight,out x, out y);
                newStruct = new Structure(x,y,type,houseNum);
                break;
            case "BigHouse":
                y = UnityEngine.Random.Range(-(middleheight),(middleheight)+1);
                x = UnityEngine.Random.Range(-(middleWidth),(middleWidth)+1);
                newStruct = new Structure(x,y,type,houseNum);
                break;
            case "Farm":
                generateRandomBetween(-maxWidth/farmWidth,maxWidth/farmWidth,-maxHeight/farmHeight,maxHeight/farmHeight,-(middleWidth+farmWidth)/farmWidth,(middleWidth+farmWidth)/farmWidth,-(middleheight+farmHeight)/farmHeight,(middleheight+farmHeight)/farmHeight,out x, out y);
                
                int repeat=0;
                while(allFarms.ContainsKey(x+","+y+"") || repeat<10)
                {
                    generateRandomBetween(-maxWidth/farmWidth,maxWidth/farmWidth,-maxHeight/farmHeight,maxHeight/farmHeight,-(middleWidth+farmWidth)/farmWidth,(middleWidth+farmWidth)/farmWidth,-(middleheight+farmHeight)/farmHeight,(middleheight+farmHeight)/farmHeight,out x, out y);
                    repeat++;
                }

                key=x+","+y+"";
                x=(x*farmWidth)+UnityEngine.Random.Range(-farmWdisplacement,farmWdisplacement);
                y=(y*farmHeight)+UnityEngine.Random.Range(-farmHdisplacement,farmHdisplacement);
                newStructPass = new PassiveIncomeStructure(x,y,type,houseNum,key);
                allPassiveIncomesBuilds.Add(newStructPass);
                newStruct = newStructPass;
                break;
            default:
                Debug.Log("Unknown building type!");
                newStruct = new Structure(0,0,"",0);
                break;
        }

        GameObject newHouse = Instantiate(structures[houseNum], new Vector3(x,y,y+maxHeight), Quaternion.Euler(0f,0f,0f));
        allBuildings.Add(newStruct);
        allStructures.Add(newHouse);

        if(type=="Farm")
            allFarms.Add(key,newHouse);

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

        DestroyNatureAt(new Vector2(x,y));

        return newHouse;
    }


    private void generateRandomBetween(int x_min, int x_max, int y_min, int y_max, int x_inner_min, int x_inner_max, int y_inner_min, int y_inner_max, out int x, out int y)
    {
        //Debug.Log(y_max+"; "+y_inner_max);
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

            if(!allRoadsList.ContainsKey(index1+","+index2+"") && !allRoadsList.ContainsKey(index2+","+index1+""))
                createRoad(allBuildings[index1].getX(), allBuildings[index1].getY(), allBuildings[index2].getX(), allBuildings[index2].getY(),index1,index2);
        }
    }


    private void createRoadStructure(int index1, int index2)
    {
        if(!allRoadsList.ContainsKey(index1+","+index2+"") && !allRoadsList.ContainsKey(index2+","+index1+""))
        {
            //Debug.Log("Additional Road!");
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
        List<RoadStructure> tempPartsOfRoad = new List<RoadStructure>();
        //Debug.Log("Coord1: "+x1+", "+y1+"; Coord2: "+x2+", "+y2+"; ids: "+i1+", "+i2);
        string key = i1+","+i2+"";

        if(UnityEngine.Random.Range(0f,1f)<probabilityOfCreatingARoad)
        {
            if(UnityEngine.Random.Range(0,2)==0)
            {
                if(y1>y2)
                    tempPartsOfRoad.Add(createVerticalRoad(y1,y1,y2,x1,-1,key));
                else
                    tempPartsOfRoad.Add(createVerticalRoad(y2,y1,y2,x1,1,key));

                if(x1>x2)
                    tempPartsOfRoad.Add(createHorizontalRoad(y2,x1,x2,-1,key));
                else
                    tempPartsOfRoad.Add(createHorizontalRoad(y2,x1,x2,1,key));
            }
            else
            {
                if(x1>x2)
                    tempPartsOfRoad.Add(createHorizontalRoad(y1,x1,x2,-1,key));
                else
                    tempPartsOfRoad.Add(createHorizontalRoad(y1,x1,x2,1,key));

                if(y1>y2)
                    tempPartsOfRoad.Add(createVerticalRoad(y1,y1,y2,x2,-1,key));
                else
                    tempPartsOfRoad.Add(createVerticalRoad(y2,y1,y2,x2,1,key));
            }
        }

        allRoadsList.Add(key,tempPartsOfRoad);
        //Debug.Log(i1+""+i2+"");
    }


    public RoadStructure createVerticalRoad(int h, int y1, int y2, int x, int operand,string key)
    {   
        int y;
        if(operand==-1)
            y=y1-(Math.Abs(y1-y2)/2);
        else
            y=y1+(Math.Abs(y1-y2)/2);

        GameObject newPart = Instantiate(roadTypes[0], new Vector3(x,y,h+20+maxHeight), Quaternion.Euler(0f,0f,0f));
        newPart.transform.localScale=new Vector3(roadLength,Math.Abs(y1-y2),1);
        RoadStructure road = new RoadStructure(x,y,"Road",0,roadLength,Math.Abs(y1-y2),key,h+20+maxHeight);
        allStructures.Add(newPart);
        return road;
    }


    public RoadStructure createHorizontalRoad(int height, int x1, int x2, int operand, string key)
    {
        int x;
        if(operand==-1)
            x=x1-(Math.Abs(x1-x2)/2);
        else
            x=x1+(Math.Abs(x1-x2)/2);

        GameObject newPart = Instantiate(roadTypes[0], new Vector3(x,height,height+20+maxHeight), Quaternion.Euler(0f,0f,0f));
        newPart.transform.localScale=new Vector3(Math.Abs(x1-x2),roadLength,1);
        RoadStructure road = new RoadStructure(x,height,"Road",0,Math.Abs(x1-x2),roadLength,key,height+20+maxHeight);
        allStructures.Add(newPart);
        return road;
    }


    public void resetUI()
    {
        houseText.text="\n"+Balance.outputCostCorrectly(houseCost);
        bigHouseText.text="\n"+Balance.outputCostCorrectly(bigHouseCost);
        craftText.text="\n"+Balance.outputCostCorrectly(craftCost);
        farmText.text="\n"+Balance.outputCostCorrectly(farmCost);

        if(numOfHouses==0)
            numOfHousesText.text="";
        else
            numOfHousesText.text="X"+numOfHouses;

        if(numOfBigHouses==0)
            numOfBigHousesText.text="";
        else
            numOfBigHousesText.text="X"+numOfBigHouses;

        if(numOfCraft==0)
            numOfCraftHousesText.text="";
        else
            numOfCraftHousesText.text="X"+numOfCraft;

        if(numOfFarms==0)
            numOfFarmsText.text="";
        else
            numOfFarmsText.text="X"+numOfFarms;

        houseButton.interactable = false;
        bigHouseButton.interactable = false;
        craftButton.interactable = false;
        farmButton.interactable = false;
    }


    public void resetBuildings(GameData data)
    {
        bigHouseCost = data.bigHouseCost;
        houseCost = data.houseCost;
        farmCost = data.farmCost;
        craftCost = data.craftCost;
        numOfHouses = data.numOfHouses;
        numOfBigHouses = data.numOfBigHouses;
        numOfFarms = data.numOfFarms;
        numOfCraft = data.numOfCrafts;

        foreach(GameObject building in allStructures)
        {
            Destroy(building);
        }

        allBuildings = data.usualBuildingsList;
        allPassiveIncomesBuilds = data.specialBildingsList;
        
        List<RoadStructure> tempRoadList = data.roadList;
        allRoadsList.Clear();
        string key = "";
        List<RoadStructure> roadsWithKey = new List<RoadStructure>();
        foreach(RoadStructure road in tempRoadList)
        {
            if(key==road.getKey())
                roadsWithKey.Add(road);
            else
            {
                if(key!="")
                    allRoadsList.Add(key,roadsWithKey);
                
                key=road.getKey();
                roadsWithKey.Clear();
                roadsWithKey.Add(road);
            }
        }
        
        allFarms.Clear();
        allStructures.Clear();

        resetUI();
        recreateBuildings(tempRoadList);

        //Debug.Log("Done!");
    }


    public void recreateBuildings(List<RoadStructure> roadList)
    {
        //Recreate all structures
        int specialBuildsIndex=-1;
        foreach(Structure someStruct in allBuildings)
        {
            //Debug.Log("Here");
            GameObject newHouse;
            switch(someStruct.getType())
            {
                case "CraftHouse":
                    newHouse = Instantiate(craftBuildings[someStruct.getStructNum()], new Vector3(someStruct.getX(),someStruct.getY(),someStruct.getY()+maxHeight), Quaternion.Euler(0f,0f,0f));
                    CraftHouseBehaviour behaviour1 = newHouse.GetComponent<CraftHouseBehaviour>();
                    behaviour1.timeLeft = UnityEngine.Random.Range(0,passiveIncomeManager.periodInSecondsCraft);
                    behaviour1.passiveIncomeManager = passiveIncomeManager;
                    behaviour1.soundManager = soundManager;
                    specialBuildsIndex++;
                    //Debug.Log("craft");
                    break;
                case "House":
                    newHouse = Instantiate(houses[someStruct.getStructNum()], new Vector3(someStruct.getX(),someStruct.getY(),someStruct.getY()+maxHeight), Quaternion.Euler(0f,0f,0f));
                    //Debug.Log("house");
                    break;
                case "BigHouse":
                    newHouse = Instantiate(bigHouses[someStruct.getStructNum()], new Vector3(someStruct.getX(),someStruct.getY(),someStruct.getY()+maxHeight), Quaternion.Euler(0f,0f,0f));
                    //Debug.Log("bighouse");
                    break;
                case "Farm":
                    newHouse = Instantiate(farmingBuildings[someStruct.getStructNum()], new Vector3(someStruct.getX(),someStruct.getY(),someStruct.getY()+maxHeight), Quaternion.Euler(0f,0f,0f));
                    FarmBehaviour behaviour2 = newHouse.GetComponent<FarmBehaviour>();
                    behaviour2.timeLeft = UnityEngine.Random.Range(0,passiveIncomeManager.periodInSecondsCraft);
                    behaviour2.passiveIncomeManager = passiveIncomeManager;
                    behaviour2.soundManager = soundManager;
                    specialBuildsIndex++;
                    //Debug.Log("farm");
                    break;
                default:
                    Debug.Log("Unknown building type!");
                    newHouse=null;
                    break;
            }

            allStructures.Add(newHouse);

            if(someStruct.getType()=="Farm")
                allFarms.Add(allPassiveIncomesBuilds[specialBuildsIndex].getKey(),newHouse);
        }

        //Recreate all roads
        foreach(RoadStructure road in roadList)
        {
            GameObject newPart = Instantiate(roadTypes[0], new Vector3(road.getX(),road.getY(),road.getZHeight()), Quaternion.Euler(0f,0f,0f));
            newPart.transform.localScale=new Vector3(road.getWidth(),road.getHeight(),1);
            allStructures.Add(newPart);
        }

    }


    private void DestroyNatureAt(Vector2 position)
    {
        /*
        RaycastHit2D[] hits = Physics2D.BoxCastAll(
        (Vector2)position + new Vector2(0, 0.5f),
        new Vector2(20f, 20f), // размеры коробки
        0f,                    // угол поворота коробки
        Vector2.up,            // направление кастинга (вверх)
        1f,                    // дистанция кастинга
        LayerMask.GetMask("Nature") // слой, с которым проверяем столкновения
        );*/
        RaycastHit2D[] hits = Physics2D.BoxCastAll(position, new Vector2(40f, 40f), 0f, Vector2.up, 1f, LayerMask.GetMask("Nature"));
        
        //Destroy all founded nature
        foreach(var item in hits)
        {
            mapGenerator.allNatureStructures.Remove(item.collider.gameObject);
            Destroy(item.collider.gameObject);
            //Debug.Log("Nature removed");
        }
    }
}
