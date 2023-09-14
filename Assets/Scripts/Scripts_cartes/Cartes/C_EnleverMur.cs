using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(menuName = "Cartes/EnleverMur", fileName = "Cartes")]
public class C_EnleverMur : Carte
{
    public override void activer(int id)
    {
        GameObject instantiation = Instantiate(activation, Vector3.zero, Quaternion.identity);
        activationEnleverMur truc = instantiation.GetComponent<activationEnleverMur>();
        truc.SetId(id);
        truc.SetCout(GetCoup);
    }
}