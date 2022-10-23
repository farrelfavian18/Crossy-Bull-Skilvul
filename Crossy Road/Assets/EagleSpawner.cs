using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EagleSpawner : MonoBehaviour
{
    [SerializeField] GameObject eaglePrefab;
    [SerializeField] int spawnZPos = 7;
    [SerializeField] Player player;
    [SerializeField] float timeOut = 5f;
    [SerializeField] float timer = 0f;
    float playerLastMaxTravel = 0;

    private void SpawnEagle()
    {
        player.enabled = false;
        var position = new Vector3
        (
            player.transform.position.x,
            1,
            player.CurrentTravel + spawnZPos
        );
        var rotation = Quaternion.Euler(0, 180, 0);
        var eagleObject = Instantiate(eaglePrefab, position, rotation);
        var eagle = eagleObject.GetComponent<Eagle>();
        eagle.SetUpTarget(player);
    }

    private void Update()
    {
        //jika player maju
        if (player.MaxTravel != playerLastMaxTravel)
        {

            //maka reset timer
            timer = 0;
            playerLastMaxTravel = player.MaxTravel;
            return;
        }

        //kalo ga maju" jalankan timer
        if (timer < timeOut)
        {
            timer += Time.deltaTime;
            return;
        }
        if (player.IsJumping() == false && player.IsDie == false)
            SpawnEagle();
    }
}
