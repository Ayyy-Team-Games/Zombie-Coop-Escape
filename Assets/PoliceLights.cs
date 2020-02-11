using System.Collections;
using UnityEngine;

public class PoliceLights : MonoBehaviour
{

    [SerializeField] Light redLight;
    [SerializeField] Light blueLight;

    private Vector3 redTemp;
    private Vector3 blueTemp;

    [SerializeField] int speed;


    void Update()
    {
        redTemp.y += speed * Time.deltaTime;
        blueTemp.y -= speed * Time.deltaTime;

        redLight.transform.Rotate(1,0,0);
        blueLight.transform.Rotate(-1,0,0);
    }
}