using UnityEngine;
using TMPro;

public class Ui_Pioche : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private CarteManager carteManager;

    void Start()
    {
        carteManager = GameObject.Find("CarteManager").GetComponent<CarteManager>();
    }

    void Update()
    {
        text.text = carteManager.pioche.Count.ToString();
    }
}
