using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  public static PlayerManager Instance { get; private set; }
    [Header("Player")]
    public GameObject player;
   
    // Start is called before the first frame update

    
    void Start()
    {
     if (Instance != null && Instance != this) { Destroy(this); }
     else { Instance = this; }
     
        
    }
}
