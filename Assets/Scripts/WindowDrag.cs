using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class WindowDrag : MonoBehaviour, IDragHandler, IEndDragHandler  
{
    public float limitXMin;
    public float limitXMax;
    public float limitYMin;
    public float limitYMax;
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

      
    

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector2(Mathf.Clamp(transform.localPosition.x,limitXMin,limitXMax),Mathf.Clamp(transform.localPosition.y,limitYMin,limitYMax));
    }
}
