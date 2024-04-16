using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Room;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

public class SpawnPoint : MonoBehaviour
{
    public RoomGenerator.SpawnDirection Direction;
    Random rnd = new Random();

    public bool _wasSpawned = false;
    private float waitTime = 4f;

    public void Start()
    {
        Destroy(gameObject, waitTime);
        Invoke(nameof(Spawn), 0.2f);
    }
    
    private void Spawn()
    {
        RoomGenerator.Instance._hasTreasures = RoomGenerator.Instance._map.Any(room => room.CompareTag("TreasuresRoom"));
        RoomGenerator.Instance._hasShop = RoomGenerator.Instance._map.Any(room => room.CompareTag("ShopRoom"));
        RoomGenerator.Instance._hasBoss = RoomGenerator.Instance._map.Any(room => room.CompareTag("BossRoom"));
        if (!_wasSpawned)
        {
            switch (Direction)
            {
                case RoomGenerator.SpawnDirection.Front:
                    // only corridors - 1-4
                    if (RoomGenerator.Instance._roomCounter <= RoomGenerator.Instance._corridorsInMapCounter)
                    {
                        GameObject  room = Instantiate(RoomGenerator.Instance._frontPathRooms[rnd.Next(1, 5)], 
                            transform.position, Quaternion.identity);
                        RoomGenerator.Instance._map.Add(room);
                    } else // тупики
                    {
                        if (!RoomGenerator.Instance._hasShop)
                        {
                            GameObject room = Instantiate(RoomGenerator.Instance._frontPathRooms[5], 
                                transform.position, Quaternion.identity);
                            RoomGenerator.Instance._map.Add(room);
                        } else if (!RoomGenerator.Instance._hasBoss)
                        {
                            GameObject room = Instantiate(RoomGenerator.Instance._frontPathRooms[7], 
                                transform.position, Quaternion.identity);
                            RoomGenerator.Instance._map.Add(room);
                        } else if (!RoomGenerator.Instance._hasTreasures)
                        {
                            GameObject room = Instantiate(RoomGenerator.Instance._frontPathRooms[6], 
                                transform.position, Quaternion.identity);
                            RoomGenerator.Instance._map.Add(room);
                        }
                        else
                        {
                            GameObject room = Instantiate(RoomGenerator.Instance._frontPathRooms[0], 
                                transform.position, Quaternion.identity);
                            RoomGenerator.Instance._map.Add(room);
                        }
                    }

                    RoomGenerator.Instance._roomCounter++;
                    break;
                case RoomGenerator.SpawnDirection.Back:
                    // only corridors - 1-7
                    if (RoomGenerator.Instance._roomCounter <= RoomGenerator.Instance._corridorsInMapCounter)
                    {
                        GameObject room = Instantiate(RoomGenerator.Instance._backPathRooms[rnd.Next(1, 8)], 
                            transform.position, Quaternion.identity);
                        RoomGenerator.Instance._map.Add(room);
                    } else // тупики
                    {
                        if (!RoomGenerator.Instance._hasShop)
                        {
                            GameObject room = Instantiate(RoomGenerator.Instance._backPathRooms[8], 
                                transform.position, Quaternion.identity);
                            RoomGenerator.Instance._map.Add(room);
                        } else if (!RoomGenerator.Instance._hasBoss)
                        {
                            GameObject room = Instantiate(RoomGenerator.Instance._backPathRooms[10], 
                                transform.position, Quaternion.identity);
                            RoomGenerator.Instance._map.Add(room);
                        } else if (!RoomGenerator.Instance._hasTreasures)
                        {
                            GameObject room = Instantiate(RoomGenerator.Instance._backPathRooms[9], 
                                transform.position, Quaternion.identity);
                            RoomGenerator.Instance._map.Add(room);
                        }
                        else
                        {
                            GameObject room = Instantiate(RoomGenerator.Instance._backPathRooms[0], 
                                transform.position, Quaternion.identity);
                            RoomGenerator.Instance._map.Add(room);
                        }
                    }
                    
                    RoomGenerator.Instance._roomCounter++;
                    break;
                case RoomGenerator.SpawnDirection.Left:
                    // only corridors - 1-4
                    if (RoomGenerator.Instance._roomCounter <= RoomGenerator.Instance._corridorsInMapCounter)
                    {
                        GameObject room = Instantiate(RoomGenerator.Instance._leftPathRooms[rnd.Next(1, 5)], 
                            transform.position, Quaternion.identity);
                        RoomGenerator.Instance._map.Add(room);
                    } else // тупики
                    {
                        if (!RoomGenerator.Instance._hasShop)
                        {
                            GameObject room = Instantiate(RoomGenerator.Instance._leftPathRooms[5], 
                                transform.position, Quaternion.identity);
                            RoomGenerator.Instance._map.Add(room);
                        } else if (!RoomGenerator.Instance._hasBoss)
                        {
                            GameObject room = Instantiate(RoomGenerator.Instance._leftPathRooms[7], 
                                transform.position, Quaternion.identity);
                            RoomGenerator.Instance._map.Add(room);
                        } else if (!RoomGenerator.Instance._hasTreasures)
                        {
                            GameObject room = Instantiate(RoomGenerator.Instance._leftPathRooms[6], 
                                transform.position, Quaternion.identity);
                            RoomGenerator.Instance._map.Add(room);
                        }
                        else
                        {
                            GameObject room = Instantiate(RoomGenerator.Instance._leftPathRooms[0], 
                                transform.position, Quaternion.identity);
                            RoomGenerator.Instance._map.Add(room);
                        }
                    }
                    
                    RoomGenerator.Instance._roomCounter++;
                    break;
                case RoomGenerator.SpawnDirection.Right:
                    // only corridors - 1-4
                    if (RoomGenerator.Instance._roomCounter <= RoomGenerator.Instance._corridorsInMapCounter)
                    {
                        GameObject room = Instantiate(RoomGenerator.Instance._rightPathRooms[rnd.Next(1, 5)], 
                            transform.position, Quaternion.identity);
                        RoomGenerator.Instance._map.Add(room);
                    } else // тупики
                    {
                        if (!RoomGenerator.Instance._hasShop)
                        {
                            GameObject room = Instantiate(RoomGenerator.Instance._rightPathRooms[5], 
                                transform.position, Quaternion.identity);
                            RoomGenerator.Instance._map.Add(room);
                        } else if (!RoomGenerator.Instance._hasBoss)
                        {
                            GameObject room = Instantiate(RoomGenerator.Instance._rightPathRooms[7], 
                                transform.position, Quaternion.identity);
                            RoomGenerator.Instance._map.Add(room);
                        } else if (!RoomGenerator.Instance._hasTreasures)
                        {
                            GameObject room = Instantiate(RoomGenerator.Instance._rightPathRooms[6], 
                                transform.position, Quaternion.identity);
                            RoomGenerator.Instance._map.Add(room);
                        }
                        else
                        {
                            GameObject room = Instantiate(RoomGenerator.Instance._rightPathRooms[0], 
                                transform.position, Quaternion.identity);
                            RoomGenerator.Instance._map.Add(room);
                        }
                    }
                    
                    RoomGenerator.Instance._roomCounter++;
                    break;
            }

            _wasSpawned = true;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"{other.name} = other. on trigger");
        if (other.CompareTag("RoomPoint") && other.GetComponent<SpawnPoint>()._wasSpawned)
        {
            Debug.Log($"spawn point. {gameObject} was deleted");
            Destroy(gameObject);
        }

        if (other.CompareTag("Room"))
        {
            Debug.Log("room was deleted");
            RoomGenerator.Instance._map.Remove(gameObject);
            Destroy(gameObject);
        }
        
        if (other.CompareTag("ShopRoom"))
        {
            Debug.Log("shoproom was deleted");
            RoomGenerator.Instance._map.Remove(gameObject);
            Destroy(gameObject);
        }
        
        if (other.CompareTag("BossRoom"))
        {
            Debug.Log("bossroom was deleted");
            RoomGenerator.Instance._map.Remove(gameObject);
            Destroy(gameObject);
        }
        
        if (other.CompareTag("TreasuresRoom"))
        {
            Debug.Log("treasuresroom was deleted");
            RoomGenerator.Instance._map.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log($"{other.name} = other. on trigger");
        if (other.CompareTag("RoomPoint") && other.GetComponent<SpawnPoint>()._wasSpawned)
        {
            Debug.Log($"spawn point. {gameObject} was deleted");
            Destroy(gameObject);
        }

        if (other.CompareTag("Room"))
        {
            Debug.Log("room was deleted");
            RoomGenerator.Instance._map.Remove(gameObject);
            Destroy(gameObject);
        }
        
        if (other.CompareTag("ShopRoom"))
        {
            Debug.Log("shoproom was deleted");
            RoomGenerator.Instance._map.Remove(gameObject);
            Destroy(gameObject);
        }
        
        if (other.CompareTag("BossRoom"))
        {
            Debug.Log("bossroom was deleted");
            RoomGenerator.Instance._map.Remove(gameObject);
            Destroy(gameObject);
        }
        
        if (other.CompareTag("TreasuresRoom"))
        {
            Debug.Log("treasuresroom was deleted");
            RoomGenerator.Instance._map.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
