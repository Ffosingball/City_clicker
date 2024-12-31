using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class RoadStructure : Structure
{
    private float width, height;
    private string key;

    public RoadStructure(int x, int y, string type, int structNum, float width, float height, string key) : base(x,y,type,structNum)
    {
        this.width = width;
        this.height = height;
        this.key = key;
    }

    public float getWidth(){return width;}

    public float getheight(){return height;}

    public string getKey(){return key;}
}
