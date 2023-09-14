using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject coutRestant;
    // Start is called before the first frame update
    void Start()
    {
        VariableGlobale.tourCount = 0;
    
        VariableGlobale.end = false;
    }

    // Update is called once per frame
    void Update()
    {
        coutRestant.GetComponent<TextMeshProUGUI>().text = VariableGlobale.tourRestant.ToString();
        if (VariableGlobale.tourRestant <= 0)
        {
            VariableGlobale.end = true;
        }
    }
}
