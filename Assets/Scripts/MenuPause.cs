using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPause : MonoBehaviour
{
    [SerializeField] private GameObject menuPause;

    void Start()
    {
        VariableGlobale.pause = false;
        menuPause.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (VariableGlobale.pause)
            {
                menuPause.SetActive(false);
                VariableGlobale.pause = false;
            }
            else
            {
                menuPause.SetActive(true);
                VariableGlobale.pause = true;
            }
            
        }
    }
}
