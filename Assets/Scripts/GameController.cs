using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool paused;
    private GameObject pauseCanvas;
    // Start is called before the first frame update
    void Start()
    {
        pauseCanvas = GameObject.FindGameObjectWithTag("Pause");
        paused = pauseCanvas.activeSelf;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        pauseCanvas.SetActive(!pauseCanvas.activeSelf);
        paused = pauseCanvas.activeSelf;
    }

    public bool IsPaused()
    {
        return paused;
    }
}
