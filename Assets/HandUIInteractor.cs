using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandUIInteractor : MonoBehaviour
{
    private Button button;
    private bool allowedTap = true;
    void Start()
    {
        button = GetComponent<Button>();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "UI Interactor" && allowedTap)
        {
            allowedTap = false;
            Invoke("cooldown", 1f);
            button.onClick.Invoke();
            
        }
    }
    void cooldown()
    {
        allowedTap = true;
    }
    
}
