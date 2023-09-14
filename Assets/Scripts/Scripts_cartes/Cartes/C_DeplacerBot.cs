using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(menuName = "Cartes/DeplacerBot", fileName = "Cartes")]
public class C_DeplacerBot : Carte
{
    public override void activer(int id)
    {
        GameObject instantiation = Instantiate(activation, Vector3.zero, Quaternion.identity);
        activationDeplacerBot truc = instantiation.GetComponent<activationDeplacerBot>();
        truc.SetId(id);
        truc.SetCout(GetCoup);
    }
}
