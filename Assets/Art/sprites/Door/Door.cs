using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public List<GameObject> gameObjectsL;
    private Animator animator;
    
    private Collider2D coll;
    //public int sceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        gameObjectsL.RemoveAll(item => item == null);
        if (gameObjectsL.Count == 0)
        {
            //Debug.Log("Opened");
            animator.SetTrigger("Open");
            coll.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        
        if(other.tag == "Player")
        {
            Debug.Log("entered");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
        }
        
    }
}
