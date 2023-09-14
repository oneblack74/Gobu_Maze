using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(menuName = "Cartes/PoserMur", fileName = "Cartes")]
public class C_PoserMur : Carte
{
    public override void activer(int id)
    {
        GameObject instantiation = Instantiate(activation, Vector3.zero, Quaternion.identity);
        activationPoserMur truc = instantiation.GetComponent<activationPoserMur>();
        truc.SetId(id);
        truc.SetCout(GetCoup);
    }
}
