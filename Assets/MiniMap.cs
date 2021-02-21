using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public GameObject MapView;
    public GameObject zoomInBtn;
    public GameObject zoomOutBtn;
    
    private void Update()
    {
        Transform map = MapView.transform;
        if (map.transform.localScale == new Vector3(2,2,2))
        {
            zoomOutBtn.SetActive(false);
        }
        else
        {
            zoomOutBtn.SetActive(true);

        }
        if (map.localScale == new Vector3(6, 6, 6))
        {
            zoomInBtn.SetActive(false);
        }
        else
        {
            zoomInBtn.SetActive(true);

        }
        
    }
    public void zoomIn()
    {
        MapView.transform.localScale = new Vector3(MapView.transform.localScale.x + 1, MapView.transform.localScale.y + 1, MapView.transform.localScale.z + 1);
    }
    public void zoomOut()
    {
        MapView.transform.localScale = new Vector3(MapView.transform.localScale.x - 1, MapView.transform.localScale.y - 1, MapView.transform.localScale.z - 1);

    }
    public void updatePosition()
    {
        Vector2 updatedPos;
        if (MapView.transform.localScale == new Vector3(6, 6, 6))
        {
            updatedPos = new Vector2(Mathf.Clamp(MapView.transform.localPosition.x, -891, 1958), Mathf.Clamp(MapView.transform.localPosition.y, -452, 1320));
            MapView.transform.localPosition = updatedPos;
        }
        if (MapView.transform.localScale == new Vector3(5, 5, 5))
        {
            updatedPos = new Vector2(Mathf.Clamp(MapView.transform.localPosition.x, -602, 1440), Mathf.Clamp(MapView.transform.localPosition.y, -255, 1298));
            MapView.transform.localPosition = updatedPos;
        }
        if (MapView.transform.localScale == new Vector3(4, 4, 4))
        {
            updatedPos = new Vector2(Mathf.Clamp(MapView.transform.localPosition.x, -472, 1147), Mathf.Clamp(MapView.transform.localPosition.y, -170, 890));
            MapView.transform.localPosition = updatedPos;
        }
        if (MapView.transform.localScale == new Vector3(3, 3, 3))
        {
            updatedPos = new Vector2(Mathf.Clamp(MapView.transform.localPosition.x, -265, 782), Mathf.Clamp(MapView.transform.localPosition.y, -50, 617));
            MapView.transform.localPosition = updatedPos;
        }
        if (MapView.transform.localScale == new Vector3(2, 2, 2))
        {
            updatedPos = new Vector2(Mathf.Clamp(MapView.transform.localPosition.x, -110, 456), Mathf.Clamp(MapView.transform.localPosition.y, 19, 341));
            MapView.transform.localPosition = updatedPos;
        }
    }
}
