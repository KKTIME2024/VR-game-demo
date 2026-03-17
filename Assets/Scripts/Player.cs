using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] Transform head;

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(string.Format("Player health: {0}", health));
    }

    public Vector3 GetHeadPosiotion()
    {
        return head.position;
    }
}