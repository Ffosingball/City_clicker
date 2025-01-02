using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Structure
{
    private int x, y, id, structNum;
    private static int lastID=0;
    private string type;

    public Structure(int x, int y, string type, int structNum)
    {
        this.x = x;
        this.y = y;
        id=lastID;
        lastID++;
        this.type = type;
        this.structNum = structNum;
        //Debug.Log("id: "+id+"; x: "+x+"; y: "+y);
    }

    public int getX(){return x;}

    public int getY(){return y;}

    public int getID(){return id;}
    public int getStructNum(){return structNum;}

    public string getType(){return type;}
}
