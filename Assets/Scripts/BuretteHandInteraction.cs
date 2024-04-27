using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuretteHandInteraction : MonoBehaviour
{
    [SerializeField] private ParticleSystem pouringWater;
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.name == "Hand_Index3_CapsuleCollider" || other.gameObject.name == "Hand_Index2_CapsuleCollider" || other.gameObject.name == "Hand_Thumb3_CapsuleCollider")
        {
            Debug.Log("<color=green>Hand has touched the cube!</color>");
            pouringWater.Play();
            
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        pouringWater.Stop();
    }
}
