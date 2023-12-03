using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other)
   {
       if (other.GetComponent<InputReader>() != null)
       {
            SessionManager.Instance.SessionOver();    
       }
       else 
       {
        other.gameObject.SetActive(false);
       }
   }
}
