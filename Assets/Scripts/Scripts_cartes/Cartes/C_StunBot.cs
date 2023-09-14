using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[CreateAssetMenu(menuName = "Cartes/StunBot", fileName = "Cartes")]
public class C_StunBot : Carte
{
    public override void activer(int id)
    {
        GameObject instantiation = Instantiate(activation, Vector3.zero, Quaternion.identity);
        activationStunBot truc = instantiation.GetComponent<activationStunBot>();
        truc.SetId(id);
        truc.SetCout(GetCoup);
    }
}