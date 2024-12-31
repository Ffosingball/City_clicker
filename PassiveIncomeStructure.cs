using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class PassiveIncomeStructure : Structure
{
    private float timeLeft;
    private string key;

    public PassiveIncomeStructure(int x, int y, string type, int structNum, float timeLeft, string key) : base(x,y,type,structNum)
    {
        this.timeLeft = timeLeft;
        this.key = key;
    }

    public float getTimeLeft(){return timeLeft;}

    public void setTimeLeft(float timeLeft){this.timeLeft=timeLeft;}

    public string getKey(){return key;}
}
