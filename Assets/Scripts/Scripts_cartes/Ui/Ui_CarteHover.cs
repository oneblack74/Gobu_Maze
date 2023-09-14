using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Ui_CarteHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image carteImage;
    [SerializeField] private GameObject descriptionCadre;
    [SerializeField] private GameObject nom;
    [SerializeField] private GameObject description;
    [SerializeField] private GameObject energie;
    [SerializeField] private Sprite[] sprite;
    [SerializeField] private Ui_Carte ui_carte;
    private bool hover;
    

    void Start()
    {
        descriptionCadre.SetActive(false);
        hover = false;
    }

    void Update()
    {
        if (!VariableGlobale.cartActive && hover && Input.GetMouseButtonDown(0))
        {
            ui_carte.GetCarte.activer(ui_carte.ID);
            transform.position = new Vector3(transform.position.x, transform.position.y + 50, transform.position.z);
            VariableGlobale.cartActive = true;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hover = true;
        
        string hexColor = "#388A41"; // Valeur hexadécimale de la couleur
        Color color;

        if (ColorUtility.TryParseHtmlString(hexColor, out color))
        {
            carteImage.color = color;
        }

        if(ui_carte.ACarte)
        {
            descriptionCadre.SetActive(true);
            nom.GetComponent<TextMeshProUGUI>().text = ui_carte.GetCarte.GetNom;
            description.GetComponent<TextMeshProUGUI>().text = ui_carte.GetCarte.GetDescription;

            energie.GetComponent<Image>().sprite = sprite[ui_carte.GetCarte.GetCoup - 1];
        }

        

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hover = false;
        
        carteImage.color = Color.white;
        descriptionCadre.SetActive(false);
    }
}
