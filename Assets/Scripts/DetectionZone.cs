using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{
    public List<Collider2D> detectedObjs = new List<Collider2D>();
    public Collider2D col;
    // Start is called before the first frame update
   
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            detectedObjs.Add(other);
        }
        
    }

    void OnTriggerExit2D(Collider2D other) {
        detectedObjs.Remove(other);
    }

 
}
