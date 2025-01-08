using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapGenerator : MonoBehaviour
{
    //Dictionary of all nature objects
    public Dictionary<GameObject, Structure> allNatureStructures;
    public StructureNatureWeighted[] natureStructures;

    public int width, height, minAmountOfNature, maxAmountOfNature;


    private void Start() 
    {
        allNatureStructures = new Dictionary<GameObject, Structure>();
        generateNature();
    }

    
    //Create specific type of nature objects
    public void generateNature()
    {
        int amountOfObjects = UnityEngine.Random.Range(minAmountOfNature, maxAmountOfNature);

        float totalWeight=0;
        foreach(StructureNatureWeighted weights in natureStructures)
        {
            totalWeight += weights.weight;
        }

        int index=0;
        foreach(StructureNatureWeighted objects in natureStructures)
        {
            for (int i = 0; i<(amountOfObjects/totalWeight)*objects.weight; i++)
            {
                Vector3 position;
                int x = UnityEngine.Random.Range(-width, width);
                int y = UnityEngine.Random.Range(-height, height);
                position.x=x;
                position.y=y;
                position.z=position.y+505;

                if(position.z<0)
                    position.z=1;

                GameObject natureStructure = Instantiate(objects.prefab, position, Quaternion.identity);
                natureStructure.transform.SetParent(transform);
                allNatureStructures.Add(natureStructure, new Structure(x,y,"Nature",index));
            }

            index++;
        }
    }


    public void resetNature(GameData data)
    {
        foreach(KeyValuePair<GameObject, Structure> nObject in allNatureStructures)
        {
            Destroy(nObject.Key);
        }
        allNatureStructures.Clear();

        List<Structure> tempList = data.natureObjects;

        foreach(Structure newNature in tempList)
        {
            int z = newNature.getY()+505;
            if(z<0)
                    z=1;

            GameObject natureStructure = Instantiate(natureStructures[newNature.getStructNum()].prefab, new Vector3(newNature.getX(),newNature.getY(),z), Quaternion.identity);
            natureStructure.transform.SetParent(transform);
            allNatureStructures.Add(natureStructure, newNature);
        }
    }
}



[Serializable]//Значит это будет отображаться в инспекторе в юнити
public struct StructureNatureWeighted
{
    //Структура - это пользовательский тип данных, который позволяет объединять 
    //несколько связанных данных в одну логическую единицу.
    public GameObject prefab;
    //Атрибут Range, который задает диапазон допустимых значений для следующего 
    //поля. В инспекторе редактора Unity это будет отображаться в виде ползунка 
    //(слайдера), где можно выбрать значение в пределах от 0 до 1.
    [Range(0,1)]
    public float weight;
}
