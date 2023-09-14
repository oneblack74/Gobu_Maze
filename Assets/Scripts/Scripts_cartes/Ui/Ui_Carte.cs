using UnityEngine;
using UnityEngine.UI;

public class Ui_Carte : MonoBehaviour
{
    [SerializeField] bool aCarte;
    [SerializeField] private Image image;
    private Carte carte;
    [SerializeField] private int id;
    
    private CarteManager carteManager;
    void Start()
    {   
        carteManager = GameObject.Find("CarteManager").GetComponent<CarteManager>();
        aCarte = true;
    }

    void Update()
    {
        if(carteManager.main.Count >= id)
        {
            carte = carteManager.main[id-1];
            aCarte = true;
        }
        else
        {
            aCarte = false;
        }

        if (aCarte)
        {
            Color couleurActuelle = image.color;
            couleurActuelle.a = 1f;
            image.color = couleurActuelle;

            image.sprite = carte.GetSprite;
        }
        else
        {
            Color couleurActuelle = image.color;
            couleurActuelle.a = 0f;
            image.color = couleurActuelle;
        }
    }

    public bool ACarte
    {
        get {return aCarte;}
    }

    public Carte GetCarte
    {
        get {return carte;}
    }

    public int ID
    {
        get {return id;}
    }
}
