using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //[SerializeField] GameObject animal;
    [SerializeField] Vector3 offset;
    [SerializeField] Player player;

    private void Start()
    {
        offset = this.transform.position - player.transform.position;
    }

    Vector3 lastAnimalPos;
    void Update()
    {
        if (player.IsDie || lastAnimalPos == player.transform.position)
            return;

        var targetAnimalPos = new Vector3(
            player.transform.position.x,
            0,
            player.transform.position.z
        );

        transform.position = targetAnimalPos + offset;
        //lastAnimalPos = animal.transform.position;
        lastAnimalPos = player.transform.position;
    }
}