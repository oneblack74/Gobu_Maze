using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LabyrintheGenerateur : MonoBehaviour
{
    public Cellule cellulePrefab;
    public int largeur;
    public int hauteur;
    public float tailleCellulePx;

    public IA ia;

    private List<Cellule> cellules;

    public GameObject fantome;
    public GameObject trappe;
    public GameObject cle;

    public CameraController cam;

    public Tilemap tilemap;

    void Start()
    {
        tailleCellulePx /= 100;
        cellules = new List<Cellule>();

        CreerLabyrinthe();
        cam.PlacerCamera(fantome);
    }

    public List<Cellule> GetCellules() {
        return cellules;
    }

    public int GetLargeur() {
        return largeur;
    }

    public int GetHauteur() {
        return hauteur;
    }

    public float GetTailleCellulePx() {
        return tailleCellulePx;
    }

    public Cellule GetCellule(int lig, int col) {
        return cellules[lig * largeur + col];
    }

    public Cellule GetCellulePrefab()
    {
        return cellulePrefab;
    }

    public void CreerLabyrinthe() {
        DetruireLabyrinthe();
        GenererLabyrinthe();
        GenererTrous();
        Carreler(cellules);
        Vector2Int spawnFantome = coordSpawnFantome();
        Vector2Int spawnTrappe = coordSpawnTrappe();
        Vector2Int spawnCle = coordSpawnCle();
        fantome.transform.position = new Vector3(spawnFantome.x+0.5f, spawnFantome.y+0.5f, fantome.transform.position.z);
        trappe.transform.position = new Vector3(spawnTrappe.x+0.5f, spawnTrappe.y+0.5f, trappe.transform.position.z);
        cle.transform.position = new Vector3(spawnCle.x+0.5f, spawnCle.y+0.5f, cle.transform.position.z);

        VariableGlobale.coordFantome = spawnFantome;
        VariableGlobale.coordTrappe = spawnTrappe;
        VariableGlobale.coordCle = spawnCle;

        VariableGlobale.tourRestant = ia.TourMax();
    }

    public void GenererLabyrinthe() {
        Cellule cell;
        for (float lig = 0; lig < largeur; lig++) {
            for (float col = 0; col < hauteur; col++) {
                Vector3 coord = new Vector3(lig * tailleCellulePx, col * tailleCellulePx, 0);
                cell = Instantiate(cellulePrefab, coord, Quaternion.identity, transform);
                cell.Typer(E_Cellule.Mur);
                cellules.Add(cell);
            }
        }

        Stack<Cellule> chemin = new Stack<Cellule>();
        List<Cellule> visitees = new List<Cellule>();
        int cellIndex;
        int cellLig;
        int cellCol;
        List<char> directionsPossibles;
        int choixDirection;
        Cellule cellChoisie;
        Cellule cellEntreDeux;
        
        List<int> departs = Departs();
        chemin.Push(cellules[departs[Random.Range(0, departs.Count)]]);
        chemin.Peek().Typer(E_Cellule.Sol);
        while (chemin.Count > 0) {
            cell = chemin.Peek();
            cellIndex = cellules.IndexOf(cell);
            cellLig = cellIndex / largeur;
            cellCol = cellIndex % largeur;
            directionsPossibles = DirectionsPossibles(cellLig, cellCol, chemin, visitees);
            if (directionsPossibles.Count > 0) {
                choixDirection = Random.Range(0, directionsPossibles.Count);
                switch (directionsPossibles[choixDirection]) {
                    case 'g':
                        cellChoisie = cellules[cellLig * largeur + (cellCol - 2)];
                        cellEntreDeux = cellules[cellLig * largeur + (cellCol - 1)];
                        break;
                    case 'd':
                        cellChoisie = cellules[cellLig * largeur + (cellCol + 2)];
                        cellEntreDeux = cellules[cellLig * largeur + (cellCol + 1)];
                        break;
                    case 'h':
                        cellChoisie = cellules[(cellLig - 2) * largeur + cellCol];
                        cellEntreDeux = cellules[(cellLig - 1) * largeur + cellCol];
                        break;
                    case 'b':
                        cellChoisie = cellules[(cellLig + 2) * largeur + cellCol];
                        cellEntreDeux = cellules[(cellLig + 1) * largeur + cellCol];
                        break;
                    default:
                        cellChoisie = new Cellule();
                        cellEntreDeux = new Cellule();
                        break;
                }
                chemin.Push(cellChoisie);
                cellChoisie.Typer(E_Cellule.Sol);
                cellEntreDeux.Typer(E_Cellule.Sol);
            }
            else {
                visitees.Add(cell);
                chemin.Pop();
            }
        }
    }

    private void DetruireLabyrinthe() {
        if (cellules.Count == 0)
            return;
        Cellule cell;
        while (cellules.Count > 0) {
            cell = cellules[0];
            cellules.RemoveAt(0);
            Destroy(cell.gameObject);
        }
    }

    private List<int> Departs() {
        List<int> departs = new List<int>();
        for (int i = largeur + 1; i < largeur * 2 - 1; i += 2) {
            departs.Add(i);
            departs.Add(i + largeur * (hauteur - 3));
        }
        for (int i = 3; i < hauteur - 3; i += 2) {
            departs.Add(i * largeur + 1);
            departs.Add((i + 1) * largeur - 2);
        }
        return departs;
    }

    private List<char> DirectionsPossibles(int cellLig, int cellCol, Stack<Cellule> chemin, List<Cellule> visitees) {
        List<char> directionsPossibles = new List<char>();
        if (DirectionPossible(cellLig, cellCol - 2, chemin, visitees)) {
            directionsPossibles.Add('g');
        }
        if (DirectionPossible(cellLig, cellCol + 2, chemin, visitees)) {
            directionsPossibles.Add('d');
        }
        if (DirectionPossible(cellLig - 2, cellCol, chemin, visitees)) {
            directionsPossibles.Add('h');
        }
        if (DirectionPossible(cellLig + 2, cellCol, chemin, visitees)) {
            directionsPossibles.Add('b');
        }
        return directionsPossibles;
    }

    private bool DirectionPossible(int cellLig, int cellCol, Stack<Cellule> chemin, List<Cellule> visitees) {
        if (!CoordValide(cellLig, cellCol)) {
            return false;
        }
        Cellule cell = cellules[cellLig * largeur + cellCol];
        return !chemin.Contains(cell) && !visitees.Contains(cell);
    }

    private bool CoordValide(int lig, int col) {
        return lig >= 0 && lig < hauteur && col >= 0 && col < largeur;
    }

    private List<int> CellulesTrouables() {
        List<int> cellulesTrouables = new List<int>();
        for (int lig = 1; lig < hauteur - 1; lig += 2) {
            for (int col = 1; col < largeur - 1; col += 2) {
                cellulesTrouables.Add(lig * largeur + col);
            }
        }
        return cellulesTrouables;
    }

    private void GenererTrous() {
        int nombreTrous = Mathf.FloorToInt((float)cellules.Count * 0.025f);
        List<int> cellulesTrouables = CellulesTrouables();
        int indexCell;
        int cellLig;
        int cellCol;
        List<char> directions = new List<char> {'g', 'd', 'h', 'b'};
        char dir;
        int indexCellChoisie;
        Cellule cellChoisie;
        List<int> bordures = Bordures();
        int cpt = 0;
        while (cpt < nombreTrous) {
            indexCell = cellulesTrouables[Random.Range(0, cellulesTrouables.Count)];
            cellLig = indexCell / largeur;
            cellCol = indexCell % largeur;
            dir = directions[Random.Range(0, directions.Count)];
            switch (dir) {
                case 'g':
                    indexCellChoisie = cellLig * largeur + cellCol - 1;
                    break;
                case 'd':
                    indexCellChoisie = cellLig * largeur + cellCol + 1;
                    break;
                case 'h':
                    indexCellChoisie = (cellLig - 1) * largeur + cellCol;
                    break;
                case 'b':
                    indexCellChoisie = (cellLig + 1) * largeur + cellCol;
                    break;
                default:
                    indexCellChoisie = 0;
                    break;
            }
            cellChoisie = cellules[indexCellChoisie];
            if (!bordures.Contains(indexCellChoisie) && cellChoisie.GetCelluleType() == E_Cellule.Mur) {
                cellChoisie.Typer(E_Cellule.Sol);
                cellulesTrouables.Remove(indexCell);
                cpt++;
            }
        }
    }

    private List<int> Bordures() {
        List<int> bordures = new List<int>();
        for (int i = 0; i < largeur; i++) {
            bordures.Add(i);
            bordures.Add(i + largeur * (hauteur - 1));
        }
        for (int i = 1; i < hauteur - 1; i++) {
            bordures.Add(i * largeur);
            bordures.Add((i + 1) * largeur - 1);
        }
        return bordures;
    }

    public void Carreler(List<Cellule> cellulesACarreler) {
        List<int> pourtour;
        int indexCell;
        int cellLig;
        int cellCol;
        int cellTmpLig;
        int cellTmpCol;
        List<int[]> directions = new List<int[]>() {
            new int[] {-1, 0},
            new int[] {0, -1},
            new int[] {0, 1},
            new int[] {1, 0}
        };
        Tile cellTIle;
        foreach(Cellule cell in cellulesACarreler) {
            pourtour = new List<int>();
            indexCell = cellules.IndexOf(cell);
            cellLig = indexCell / largeur;
            cellCol = indexCell % largeur;
            foreach(int[] dir in directions) {
                cellTmpLig = cellLig + dir[0];
                cellTmpCol = cellCol + dir[1];
                if (cellTmpLig < 0 || cellTmpLig >= hauteur || cellTmpCol < 0 || cellTmpCol >= largeur) {
                    pourtour.Add(-1);
                } else {
                    switch(cellules[cellTmpLig * largeur + cellTmpCol].GetCelluleType()) {
                        case E_Cellule.Mur:
                            pourtour.Add(0);
                            break;
                        case E_Cellule.Sol:
                            pourtour.Add(1);
                            break;
                    }
                }
            }
            cellTIle = cell.Carreler(pourtour.ToArray());
            tilemap.SetTile(new Vector3Int(cellLig, cellCol, 0), cellTIle);
        }
    }

    public Vector2Int coordSpawnFantome() {
        List<Vector2Int> coordsPossibles = new List<Vector2Int>();
        for (int col = 1; col < largeur - 1; col++) {
            if (cellules[largeur + col].GetCelluleType() == E_Cellule.Sol) {
                coordsPossibles.Add(new Vector2Int(1, col));
            }
        }
        return coordsPossibles[Random.Range(0, coordsPossibles.Count)];
    }

    public Vector2Int coordSpawnTrappe() {
        List<Vector2Int> coordsPossibles = new List<Vector2Int>();
        for (int col = 1; col < largeur - 1; col++) {
            if (cellules[largeur * (hauteur - 2) + col].GetCelluleType() == E_Cellule.Sol) {
                coordsPossibles.Add(new Vector2Int(hauteur - 2, col));
            }
        }
        return coordsPossibles[Random.Range(0, coordsPossibles.Count)];
    }

    public Vector2Int coordSpawnCle() {
        List<Vector2Int> coordsPossibles = new List<Vector2Int>();
        for (int lig = 2; lig < largeur - 2; lig++) {
            for (int col = 1; col < hauteur - 1; col++){
                if (cellules[lig * largeur + col].GetCelluleType() == E_Cellule.Sol) {
                    coordsPossibles.Add(new Vector2Int(lig, col));
                }
            }
        }
        return coordsPossibles[Random.Range(0, coordsPossibles.Count)];
    }
}