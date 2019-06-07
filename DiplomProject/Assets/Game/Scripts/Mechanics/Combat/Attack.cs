using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    GameObject xyz_range;
    public int damage ;
    public int wpn_damage;
    public int armor;
    // Use this for initialization
    public GameObject hpbarTriger;
    public GameObject plr;

    private void OnTriggerStay(Collider col)
    {

        switch (col.tag)
        {
            case "Enemy_xyz":
                break;
        }


    }
    // Update is called once per frame
    void Update ()
    {

		
	}
}
