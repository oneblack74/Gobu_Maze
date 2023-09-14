using UnityEngine;
using TMPro;

public class Ui_Energie : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI energie;

    void Update()
    {
        energie.text = VariableGlobale.energie.ToString();
    }
}
