using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    public playercontroller controller;
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
            return;
        controller.OnCharacterColliderHit(collision.collider);
    }
}
