using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class DetectionZone : MonoBehaviour
{
    Collider2D col;
    public List<Collider2D> detectiveColliders = new List<Collider2D>();
    void Awake()
    {
        col = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        detectiveColliders.Add(collider);
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        detectiveColliders.Remove(collider);
    }
}
