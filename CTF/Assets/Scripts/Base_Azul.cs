using System; // TODO: check if it compile without this
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Base_Azul : MonoBehaviour
{
    
    
     //mutex
    private static  Mutex mut = new Mutex();
    public BoxCollider2D baseCollider;
    private int isOpenTo;
    
    void Start() {
         isOpenTo = -1;
         baseCollider = GetComponent<BoxCollider2D>();
    }
 
 
    void OnTriggerEnter2D(Collider2D collider) {
        
        //Debug.Log("is requesting the mutex "+Thread.CurrentThread.ManagedThreadId);
        mut.WaitOne();
        if(collider.gameObject.layer == 8) {
          if(collider.gameObject.tag == "Player_1" && isOpenTo == -1) {
                isOpenTo = 1;
                 collider.gameObject.layer = 4;
          }
          if(collider.gameObject.tag == "Player_2" && isOpenTo == -1) {
                isOpenTo = 2;
                 collider.gameObject.layer = 4;
          } 
        } else {
          if(collider.gameObject.tag == "Player_1" && isOpenTo == 1) {
                isOpenTo = -1;
                 collider.gameObject.layer = 8;
          }
          if(collider.gameObject.tag == "Player_2" && isOpenTo == 2) {
                isOpenTo = -1;
                 collider.gameObject.layer = 8;
          } 
        }
                
        //Debug.Log("has realease the current mutex "+Thread.CurrentThread.ManagedThreadId);
        //collider.gameObject.layer = 8;
        mut.ReleaseMutex();
    }
 
    void OnTriggerExit2D(Collider2D collider) {
        
        //Debug.Log("is requesting the mutex "+Thread.CurrentThread.ManagedThreadId);
        mut.WaitOne();
        if(collider.gameObject.layer == 8) {
          if(collider.gameObject.tag == "Player_1" && isOpenTo == -1) {
                isOpenTo = 1;
                 collider.gameObject.layer = 4;
          }
          if(collider.gameObject.tag == "Player_2" && isOpenTo == -1) {
                isOpenTo = 2;
                 collider.gameObject.layer = 4;
          } 
        } else {
          if(collider.gameObject.tag == "Player_1" && isOpenTo == 1) {
                isOpenTo = -1;
                 collider.gameObject.layer = 8;
          }
          if(collider.gameObject.tag == "Player_2" && isOpenTo == 2) {
                isOpenTo = -1;
                 collider.gameObject.layer = 8;
          } 
        }
                
        //Debug.Log("has realease the current mutex "+Thread.CurrentThread.ManagedThreadId);
        //collider.gameObject.layer = 8;
        mut.ReleaseMutex();
    }    
    /*
    void OnTriggerEnter2D(Collider2D collider) {
            Debug.Log("is requesting the mutex "+Thread.CurrentThread.ManagedThreadId);
            mut.WaitOne();
            Debug.Log("is in the mutex player: " + Thread.CurrentThread.ManagedThreadId);
            while(collider.gameObject.tag == "Player" && isOpen) {
                Debug.Log("is in the mutex player: " + Thread.CurrentThread.ManagedThreadId);
                //collider.gameObject.layer = 4;
                //Debug.Log("is requesting the mutex player: " + Thread.CurrentThread.ManagedThreadId);
                //if(baseCollider.bounds.Contains(collider.bounds.min) 
                //&& baseCollider.bounds.Contains(collider.bounds.max)) {
                    collider.gameObject.layer = 4;
                    isOpen = !exit(collider);
            }  
            
            isOpen = true;
            mut.ReleaseMutex();
            
           
        //}
    }
    
    bool exit(Collider2D collider) {
            //collider.gameObject.layer = 4;
            //Debug.Log("is requesting the mutex player: " + Thread.CurrentThread.ManagedThreadId);
            if(!baseCollider.bounds.Contains(collider.bounds.min) 
            && !baseCollider.bounds.Contains(collider.bounds.max)) {
                collider.gameObject.layer = 8;
                Debug.Log("has realease the current mutex "+Thread.CurrentThread.ManagedThreadId);
                return true; // is pen
            }
            return false;
    
}
/*
    void OnTriggerExit2D(Collider2D collider) {
        
        // check mutex
        //the play is still inside critical area?
        if(collider.gameObject.tag == "Player") {
            if(!baseCollider.bounds.Contains(collider.bounds.min) 
              && !baseCollider.bounds.Contains(collider.bounds.max)) {
                Debug.Log("is leaving the protected area" + Thread.CurrentThread.ManagedThreadId);
                collider.gameObject.layer = 8;
            }
        }

    }
    
    private static void UseResource(Collider2D collider) {
        Debug.Log("is requesting the mutex "+Thread.CurrentThread.ManagedThreadId);
        
        mut.WaitOne();
        Debug.Log("is in the mutex player: " + Thread.CurrentThread.ManagedThreadId);
        /*
        if(baseCollider.bounds.Contains(collider.bounds.min) 
              && baseCollider.bounds.Contains(collider.bounds.max)) {
                collider.gameObject.layer = 4;
        } else {
            collider.gameObject.layer = 8;
            Debug.Log("has realease the current mutex "+Thread.CurrentThread.ManagedThreadId);
           
        }
         collider.gameObject.layer = 4;
         
         
         mut.ReleaseMutex();
    }*/
}
