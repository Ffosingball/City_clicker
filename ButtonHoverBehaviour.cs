using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool isMouseOver = false;
    public Camera mainCamera;
    public GameObject infoPanelToShow;
    private GameObject gameObject;
    public float width, height, waitFor=2f;
    public bool showAbove=true;
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
        }
        else
        {
            Destroy(gameObject);
            gameObject=null;
        }
    }
}
