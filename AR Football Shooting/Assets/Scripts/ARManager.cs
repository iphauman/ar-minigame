using UnityEngine;

public class ARManager : MonoBehaviour
{
    public bool tracked;

    // Update is called once per frame
    private void Update()
    {
        Time.timeScale = tracked ? 1 : 0;
    }

    public void Tracked()
    {
        tracked = true;
    }

    public void Lost()
    {
        tracked = false;
    }
}