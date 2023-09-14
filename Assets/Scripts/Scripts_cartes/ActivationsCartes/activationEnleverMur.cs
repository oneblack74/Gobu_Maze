using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activationEnleverMur : MonoBehaviour
{
    private LabyrintheGenerateur labyrintheGenerateur;
    private List<Cellule> cellules;
    private int largeur;
    private int hauteur;
    public GameObject sprite;
    private Camera cameraJeu;
    private int id;
    private int cout;

    public void Start() {
        labyrintheGenerateur = GameObject.Find("MazeGenerator").GetComponent<LabyrintheGenerateur>();
        cellules = labyrintheGenerateur.GetCellules();
        largeur = labyrintheGenerateur.GetLargeur();
        hauteur = labyrintheGenerateur.GetHauteur();
        sprite.SetActive(false);
        cameraJeu = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public void SetId(int id) {
        this.id = id;
    }

    public void SetCout(int cout) {
        this.cout = cout;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) {
            Annulation();
        }
        Vector3 mousePosition = Input.mousePosition;
        Vector3 coordSouris = cameraJeu.ScreenToWorldPoint(mousePosition);
        int cellHoverLig = Mathf.FloorToInt(coordSouris.x);
        int cellHoverCol = Mathf.FloorToInt(coordSouris.y);
        int cellHoverIndex = cellHoverLig * largeur + cellHoverCol;
        if (cellHoverIndex < 0 || cellHoverIndex >= largeur * hauteur) {
            if (Input.GetMouseButtonDown(0)) {
                Annulation();
            }
            return;
        }
        Cellule cellHover = cellules[cellHoverIndex];
        switch(cellHover.GetCelluleType()) {
            case E_Cellule.Mur:
                if (cellHoverLig > 0 && cellHoverLig < hauteur - 1 && cellHoverCol > 0 && cellHoverCol < largeur - 1) {
                    transform.position = new Vector3(cellHoverLig + 0.5f, cellHoverCol + 0.5f, 0);
                    sprite.SetActive(true);
                    if (Input.GetMouseButtonDown(0)) {
                        if (VariableGlobale.energie < cout) {
                            Annulation();
                            return;
                        }
                        VariableGlobale.energie -= cout;
                        cellHover.Typer(E_Cellule.Sol);
                        List<Cellule> cellulesACarreler = new List<Cellule>();
                        for (int lig = -1; lig <= 1; lig++) {
                            for (int col = -1; col <= 1; col++) {
                                cellulesACarreler.Add(cellules[(cellHoverLig + lig) * largeur + (cellHoverCol + col)]);
                            }
                        }
                        labyrintheGenerateur.Carreler(cellulesACarreler);
                        Utilisation();
                    }
                }
                break;
            default:
                sprite.SetActive(false);
                break;
        }
    }

    private void Utilisation() {
        VariableGlobale.cartActive = false;
        Transform carte = GameObject.Find("Carte" + id.ToString()).GetComponent<Transform>();
        carte.position = new Vector3(carte.position.x, carte.position.y - 50, carte.position.z);
        GameObject.Find("CarteManager").GetComponent<CarteManager>().Jouer(id - 1);
        Destroy(this.gameObject);
    }

    private void Annulation() {
        VariableGlobale.cartActive = false;
        Transform carte = GameObject.Find("Carte" + id.ToString()).GetComponent<Transform>();
        carte.position = new Vector3(carte.position.x, carte.position.y - 50, carte.position.z);
        Destroy(this.gameObject);
    }
}
