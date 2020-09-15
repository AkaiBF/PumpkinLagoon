using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dialogos : MonoBehaviour
{
    private bool activeSelf = false;
    private Queue<Conversation> conversation;
    private bool started = false;
    public Text nameText;
    public Text talkText;

    public void AddQueue(Queue<Conversation> conv)
    {
        Debug.Log("Me ha llegado la instrucción");
        Debug.Log("La cola tiene " + conv.Count + " parametros");
        conversation = conv;
        Debug.Log("Internamente tiene " + conversation.Count);
        this.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(conversation != null && conversation.Count > 0)
        {
            if (!started || Input.GetKeyDown(KeyCode.Return))
            {
                Conversation show = conversation.Dequeue();
                nameText.text = show.name;
                talkText.text = show.talk;
                started = true;
            } 
        } else
        {
            conversation = null;
            this.gameObject.SetActive(false);
            started = false;
        }
    }
}
