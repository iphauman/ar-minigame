using UnityEngine;

public class Football : MonoBehaviour
{
    public float depthBoundary;
    public string dribbler;
    public FootballStatus status;

    public enum FootballStatus
    {
        Dribbling,
        Stopping
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.CompareTag("Goal"))
        {
            Debug.Log(dribbler + " shot into the goal.");
        }
    }
}