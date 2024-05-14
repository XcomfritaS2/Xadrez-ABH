using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "carro")
        {
            target.gameObject.transform.parent = transform;
            target.gameObject.SetActive(false);
        }
    }


}
