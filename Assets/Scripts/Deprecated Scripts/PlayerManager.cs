using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  public static PlayerManager Instance { get; private set; }
    [Header("Player")]
    public GameObject player;

       
    void Start()
    {
     if (Instance != null && Instance != this) { Destroy(this); }
     else { Instance = this; }
    }
}
