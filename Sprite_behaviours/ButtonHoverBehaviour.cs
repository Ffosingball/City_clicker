using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isMouseOver = false;
    public Camera mainCamera;
    public GameObject infoPanelToShow;
    private GameObject gameObject;
    public float width, height, waitFor=2f;
    public bool showAbove=true;
    public int numOfTextToShow=-1;
    public UIManager uIManager;
    public MovementManager movementManager;
    private Text importantText=null, otherText=null;
    private float timePassed=0;



    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
        timePassed=0;
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
        timePassed=0;
    }


    private void Update()
    {
        timePassed+=Time.deltaTime;

        if (isMouseOver && timePassed>=waitFor)
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 0.09f;
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
            if(showAbove)
            {
                worldPosition.x=worldPosition.x+((width/2)*(movementManager.currentOrthographicSize/600)+10);
                worldPosition.y=worldPosition.y+((height/2)*(movementManager.currentOrthographicSize/600)+10);
                Debug.Log(20*(movementManager.currentOrthographicSize/600));
            }
            else
            {
                worldPosition.x=worldPosition.x+((width/2)*(movementManager.currentOrthographicSize/600)+10);
                worldPosition.y=worldPosition.y-((height/2)*(movementManager.currentOrthographicSize/600)-10);
                Debug.Log(20*(movementManager.currentOrthographicSize/600));
            }

            if(gameObject==null)
            {
                gameObject = Instantiate(infoPanelToShow, worldPosition, Quaternion.Euler(0,0,0));
                //Debug.Log("Created");
                InfoPanelResizer l = gameObject.GetComponent<InfoPanelResizer>();
                l.movementManager=movementManager;
                l.resize();
                //Debug.Log("Assigned");
            }
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
        }
        else
        {
            Destroy(gameObject);
            gameObject=null;
        }
    }
}
