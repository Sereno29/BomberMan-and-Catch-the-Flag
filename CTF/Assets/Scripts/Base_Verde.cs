using System; // TODO: check if it compile without this
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Base_Verde : MonoBehaviour
{
     //mutex
     private static  Mutex mut = new Mutex();
     private bool isOpen;
     
    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Player") {
            Debug.Log("is requesting the mutex");
            //mut.WaitOne();
            
        }
    }
    
     void OnTriggerExit2D(Collider2D collider) {
        if(collider.gameObject.tag == "Player") {
          Debug.Log("is leaving the protected area");
        collider.gameObject.layer = 8;
           // mut.ReleaseMutex();
            
           // Debug.Log("{} has realease the current mutex", Thread.CurrentThread.Name);
        }
    }
   
    private static void UseResource() {
        //Debug.log("{0} is requesting the mutex", Thread.CurrentThread.Name);
        
        mut.WaitOne();
        
       // Debug.log("{0} has entered the protected area", Thread.CurrentThread.Name);
        
        //isOpen = false;
        
        //Debug.log("{0} is leaving the protected area", Thread.CurrentThread.Name);
        
        mut.ReleaseMutex();
        
        //isOpen = true; 
        
        //Debug.log("{} has realease the current mutex", Thread.CurrentThread.Name);
        
    }
}
