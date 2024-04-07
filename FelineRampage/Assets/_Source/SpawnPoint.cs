using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpawnPoint : MonoBehaviour
{
    public RoomGenerator.SpawnDirection Direction;
    Random rnd = new Random();

    private bool wasSpawned = false;
    private float waitTime = 3f;

    public void Start()
    {
        Destroy(gameObject, waitTime);
        Invoke(nameof(Spawn), 0.2f);
    }

    private void Spawn()
    {
        if (!wasSpawned)
        {
            switch (Direction)
            {
                // тут др квантерион айдентити
                case RoomGenerator.SpawnDirection.Front:
                    GameObject room1 = Instantiate(RoomGenerator.Instance._frontPathRooms[rnd.Next(0, RoomGenerator.Instance._frontPathRooms.Count)], 
                        transform.position, Quaternion.identity);
                    RoomGenerator.Instance._map.Add(room1);
                    break;
                case RoomGenerator.SpawnDirection.Back:
                    GameObject room2 = Instantiate(RoomGenerator.Instance._backPathRooms[rnd.Next(0, RoomGenerator.Instance._backPathRooms.Count)], 
                        transform.position, Quaternion.identity);
                    RoomGenerator.Instance._map.Add(room2);
                    break;
                case RoomGenerator.SpawnDirection.Left:
                    GameObject room3 = Instantiate(RoomGenerator.Instance._leftPathRooms[rnd.Next(0, RoomGenerator.Instance._leftPathRooms.Count)], 
                        transform.position, Quaternion.identity);
                    RoomGenerator.Instance._map.Add(room3);
                    break;
                case RoomGenerator.SpawnDirection.Right:
                    GameObject room4 = Instantiate(RoomGenerator.Instance._rightPathRooms[rnd.Next(0, RoomGenerator.Instance._rightPathRooms.Count)], 
                        transform.position, Quaternion.identity);
                    RoomGenerator.Instance._map.Add(room4);
                    break;
                default:
                    Debug.Log("spawn direction is null or wrong");
                    break;
            }

            wasSpawned = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("RoomPoint") && other.GetComponent<SpawnPoint>().wasSpawned)
        {
            Destroy(gameObject);
        }
    }
}
