using UnityEngine;
using UnityEngine.UI;

public class SkipTour : MonoBehaviour
{
    [SerializeField] private IA ia;
    [SerializeField] private GameObject ecranWin;
    [SerializeField] private GameObject ecranLose;
    public Button boutonSkip;

    void Start()
    {
        boutonSkip.onClick.AddListener(skip);
        VariableGlobale.tourFantome = false;
        ecranLose.SetActive(false);
        ecranWin.SetActive(false);
    }

    public void skip()
    {
        //Debug.Log(VariableGlobale.end);
        if (!VariableGlobale.end && !VariableGlobale.tourFantome)
        {
            if (VariableGlobale.nbTourMine <= 0)
            {
                bool test = ia.move();
                Debug.Log(test);
                if (test)
                {
                    VariableGlobale.energie++;
                    VariableGlobale.tourCount++;
                    VariableGlobale.tourRestant--;
                    Debug.Log(VariableGlobale.tourRestant);
                    if (VariableGlobale.tourRestant <= 0)
                    {
                        VariableGlobale.end = true;
                        ecranWin.SetActive(true);
                    }
                }
                else
                {
                    ecranLose.SetActive(true);
                }
            }
            else
            {
                VariableGlobale.energie++;
                VariableGlobale.tourCount++;
                VariableGlobale.tourRestant--;
                VariableGlobale.nbTourMine--;
            }
            
        }
        else if (VariableGlobale.end)
        {
            ecranLose.SetActive(true);
        }
        
    }
}
