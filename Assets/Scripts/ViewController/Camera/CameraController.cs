using UnityEngine;
using ViewController.Player;

public class CameraController : MonoBehaviour
{
    public Player Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var cameraPosition = Player.transform.position + Vector3.up * 0.5f;
        cameraPosition.z = -10;
        transform.position = cameraPosition;
    }
}
