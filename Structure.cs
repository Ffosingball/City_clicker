using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure : MonoBehaviour 
{
    private GameObject structure;
    private int x, y;
    private string type;

    public Structure(GameObject structure, int x, int y, string type)
    {
        this.structure = structure;
        this.x = x;
        this.y = y;
    }

    public int getX(){return x;}

    public int getY(){return y;}

    public GameObject getStructure(){return structure;}

    public string getType(){return type;}
}
