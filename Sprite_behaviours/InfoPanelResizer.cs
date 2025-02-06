using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanelResizer : MonoBehaviour
{

    public Transform panelSize;
    public MovementManager movementManager;

    public void Update()
    {
        resize();
    }

    public void resize(){
        if (movementManager != null)
            panelSize.localScale = new Vector3(1f,1f,1f)*(movementManager.currentOrthographicSize/600);
    }
}
