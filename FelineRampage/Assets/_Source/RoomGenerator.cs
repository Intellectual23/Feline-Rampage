using System.Collections.Generic;
using Room;
using UnityEngine;

public class RoomGenerator: MonoBehaviour
{
    public static RoomGenerator Instance;
    public List<GameObject> _map = new();
    public List<GameObject> _allPrefabs = new();
    
    public List<GameObject> _rightPathRooms = new();
    public List<GameObject> _leftPathRooms = new();
    public List<GameObject> _frontPathRooms = new();
    public List<GameObject> _backPathRooms = new();
    
    public enum SpawnDirection
    {
        Front, 
        Back, 
        Left, 
        Right
    }

    public void Start()
    {
        
    }
    
    public void SpawnRoom()
    {
        
    }
    
    public void SpawnRoomAfterLoad(MapWrapper wrapper)
    {
        int prefabID = wrapper._prefabID;
        Vector3 position = new Vector3(wrapper._x, wrapper._y, wrapper._z);
        GameObject room = Instantiate(Instance._allPrefabs[prefabID], position, Quaternion.identity);
        room.transform.GetComponent<RoomView>()._isActive = wrapper._isActive;
        _map.Add(room);
    }
}