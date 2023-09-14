using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum E_Cellule {
    Mur,
    Sol,
    Marqueur
}

public class Cellule : MonoBehaviour
{
    private E_Cellule type;
    private SpriteRenderer spriteRenderer;

    // mur : 0 | sol : 1 | vide : -1
    // {gauche, bas, haut, droite}
    private List<int[]> pourtoursMurs = new List<int[]>() {
            new int[] {1, 1, 1, 1}, // 0
            new int[] {0, 1, 1, 1}, // 1
            new int[] {1, 0, 1, 1}, // 2
            new int[] {1, 1, 0, 1}, // 3
            new int[] {1, 1, 1, 0}, // 4
            new int[] {0, 1, 1, 0}, // 5
            new int[] {1, 0, 0, 1}, // 6
            new int[] {0, 0, 1, 1}, // 7
            new int[] {0, 1, 0, 1}, // 8
            new int[] {1, 0, 1, 0}, // 9
            new int[] {1, 1, 0, 0}, // 10
            new int[] {0, 0, 0, 1}, // 11
            new int[] {0, 0, 1, 0}, // 12
            new int[] {0, 1, 0, 0}, // 13
            new int[] {1, 0, 0, 0}, // 14
            new int[] {0, 0, 0, 0}, // 15
            new int[] {-1, 0, 0, 1}, // 16
            new int[] {-1, 0, 0, 0}, // 17
            new int[] {0, -1, 1, 0}, // 18
            new int[] {0, -1, 0, 0}, // 19
            new int[] {0, 1, -1, 0}, // 20
            new int[] {0, 0, -1, 0}, // 21
            new int[] {1, 0, 0, -1}, // 22
            new int[] {0, 0, 0, -1}, // 23
            new int[] {0, 0, -1, -1}, // 24
            new int[] {0, -1, 0, -1}, // 25
            new int[] {-1, 0, -1, 0}, // 26
            new int[] {-1, -1, 0, 0} // 27
        };
    public Tile[] spritesMurs;
    private List<int[]> pourtoursSols = new List<int[]>() {
            new int[] {0, 0, 0, 0}, // 0
            new int[] {1, 0, 0, 0}, // 1
            new int[] {0, 1, 0, 0}, // 2
            new int[] {0, 0, 1, 0}, // 3
            new int[] {0, 0, 0, 1}, // 4
            new int[] {1, 0, 0, 1}, // 5
            new int[] {0, 1, 1, 0}, // 6
            new int[] {1, 1, 0, 0}, // 7
            new int[] {1, 0, 1, 0}, // 8
            new int[] {0, 1, 0, 1}, // 9
            new int[] {0, 0, 1, 1}, // 10
            new int[] {1, 1, 1, 0}, // 11
            new int[] {1, 1, 0, 1}, // 12
            new int[] {1, 0, 1, 1}, // 13
            new int[] {0, 1, 1, 1}, // 14
            new int[] {1, 1, 1, 1} // 15
        };
    public Tile[] spritesSols;

    public Sprite[] sprites;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public E_Cellule GetCelluleType() {
        return type;
    }

    public void Typer(E_Cellule type) {
        this.type = type;
        switch(type) {
            case E_Cellule.Mur:
                spriteRenderer.sprite = sprites[0];
                break;
            case E_Cellule.Sol:
                spriteRenderer.sprite = sprites[1];
                break;
            default:
                spriteRenderer.sprite = sprites[2];
                break;
        }
    }

    public Tile Carreler(int[] pourtour) {
        bool verif;
        switch (type) {
            case E_Cellule.Mur:
                for (int i = 0; i < pourtoursMurs.Count; i++) {
                    verif = true;
                    for (int j = 0; j < pourtour.Length; j++) {
                        if (pourtoursMurs[i][j] != pourtour[j]) {
                            verif = false;
                            break;
                        }
                    }
                    if (verif) {
                        // spriteRenderer.sprite = spritesMurs[i];
                        return spritesMurs[i];
                    }
                }
                break;
            case E_Cellule.Sol:
                for (int i = 0; i < pourtoursSols.Count; i++) {
                    verif = true;
                    for (int j = 0; j < pourtour.Length; j++) {
                        if (pourtoursSols[i][j] != pourtour[j]) {
                            verif = false;
                            break;
                        }
                    }
                    if (verif) {
                        // spriteRenderer.sprite = spritesSols[i];
                        return spritesSols[i];
                    }
                }
                break;
        }
        return null;
    }
}
