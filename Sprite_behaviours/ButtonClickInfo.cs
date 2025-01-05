using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickInfo : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject infoPanelToShow;
    private GameObject gameObject;
    public float width, height, waitFor=2f;
    public bool showAbove=true;
    public int numOfTextToShow=-1;
    public UIManager uIManager;
    public SoundManager soundManager;
    private Text importantText=null, otherText=null;
    private bool isShown=false;


    private void Update()
    {      
        if (Input.GetMouseButtonDown(0))
            isShown=false;
        
        if(!isShown)
        {
            Destroy(gameObject);
            gameObject=null;
        }
            
    }


    public void showInfo()
    {
        soundManager.PlayClickSound();

        if(isShown==false)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 0.09f;
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            if(showAbove)
            {
                worldPosition.x=worldPosition.x+width/2+20;
                worldPosition.y=worldPosition.y+height/2+20;
            }
            else
            {
                worldPosition.x=worldPosition.x+width/2+20;
                worldPosition.y=worldPosition.y-height/2-20;
            }

            if(gameObject==null)
                gameObject = Instantiate(infoPanelToShow, worldPosition, Quaternion.Euler(0,0,0));
            else
                gameObject.transform.position = worldPosition;

            if(importantText==null || otherText==null)
            {
                GameObject childCanva = gameObject.transform.Find("Canvas").gameObject;
                GameObject childText = childCanva.transform.Find("ImportantInfoText").gameObject;
                importantText = childText.GetComponent<Text>();
                childText = childCanva.transform.Find("AdditionalInfoText").gameObject;
                otherText = childText.GetComponent<Text>();
            }

            uIManager.textToShow[numOfTextToShow](importantText, otherText);

            isShown=true;
        }
    }
}
