using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerManager : MonoBehaviour
{
    public Transform tp;
    public Cinemachine.CinemachineVirtualCamera target;
    public List<Cinemachine.CinemachineVirtualCamera> cameras;
    public SeamusController character;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!character.posteleporting && (Vector2)character.transform.position == character.destination)
        {
            foreach (Cinemachine.CinemachineVirtualCamera i in cameras)
            {
                i.enabled = false;
            }
            target.enabled = true;

            character.transform.position = tp.position;
            character.teleporting = true;
            character.posteleporting = true;
        }
    }
}
