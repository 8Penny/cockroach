using System;

using UnityEngine;

public class BorderChecker : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("ENTER");
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("EXIT");
    }
}
