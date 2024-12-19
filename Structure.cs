using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Structure
{
    private GameObject structure;
    private int x, y, id;
    private static int lastID=0;
    private string type;

    public Structure(GameObject structure, int x, int y, string type)
    {
        this.structure = structure;
        this.x = x;
        this.y = y;
        id=lastID;
        lastID++;
    }

    public int getX(){return x;}

    public int getY(){return y;}

    public int getID(){return id;}

    public GameObject getStructure(){return structure;}

    public string getType(){return type;}
}
