using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class RoadStructure : Structure
{
    private float width, height, zHeight;
    private string key;

    public RoadStructure(int x, int y, string type, int structNum, float width, float height, string key, float zHeight) : base(x,y,type,structNum)
    {
        this.width = width;
        this.height = height;
        this.key = key;
        this.zHeight = zHeight;
    }

    public float getWidth(){return width;}

    public float getHeight(){return height;}

    public float getZHeight(){return zHeight;}

    public string getKey(){return key;}
}
