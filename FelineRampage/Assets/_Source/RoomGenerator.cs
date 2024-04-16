using System.Collections.Generic;
using System.Linq;
using Room;
using UnityEngine;

// генерю карту из путей и тупиков
// если тупиков меньше 3 то стираю всё к чертям и переделываю карту
// с конца тупики заменяю соответственно на босса шоп и треассуре

public class RoomGenerator: MonoBehaviour
{
    public static RoomGenerator Instance;
    public List<GameObject> _map = new();
    public List<GameObject> _allPrefabs = new();
    
    public List<GameObject> _rightPathRooms = new();
    public List<GameObject> _leftPathRooms = new();
    public List<GameObject> _frontPathRooms = new();
    public List<GameObject> _backPathRooms = new();

    public GameObject _startRoom;
    public int _roomCounter;
    public int _corridorsInMapCounter;
    public bool _hasShop;
    public bool _hasBoss;
    public bool _hasTreasures;
    
    public enum SpawnDirection
    {
        Front, 
        Back, 
        Left, 
        Right
    }

    public void Start()
    {
        _roomCounter = 0;
        _hasShop = false;
        _hasBoss = false;
        _hasTreasures = false;
        InstantiateStartRoom();
        Invoke(nameof(CheckMap), 3f);
        MusicSounds.Instance._musicManager.clip = MusicSounds.Instance._sounds[0];
        MusicSounds.Instance._musicManager.Play();
    }

    public void CheckMap()
    {
        Debug.Log(_map.Count);
        Debug.Log("check map started");
        List<GameObject> roomsToAdd = new();
        foreach (GameObject room in _map)
        {
            bool left = room.GetComponent<RoomView>()._hasLeftPath;
            bool right = room.GetComponent<RoomView>()._hasRightPath;
            bool back = room.GetComponent<RoomView>()._hasBackPath;
            bool front = room.GetComponent<RoomView>()._hasFrontPathInitially;
            Vector3 position1 = room.transform.position;
            float x = position1.x;
            float y = position1.y;
            float z = position1.z;

            Debug.Log(room.name);
            if (left)
            {
                Vector3 position = new Vector3(x - (float)19.2, y, z);
                bool flag = false;
                foreach (GameObject room2 in _map.Where(room2 => room2.transform.position == position))
                {
                    flag = true;
                }
                
                foreach (GameObject room2 in roomsToAdd.Where(room2 => room2.transform.position == position))
                {
                    flag = true;
                }

                if (position == new Vector3(0, 0, 0))
                {
                    flag = true;
                }
                if (!flag) {
                    GameObject newRoom = Instantiate(_rightPathRooms[0], position, Quaternion.identity);
                    roomsToAdd.Add(newRoom);
                    Debug.Log("слева комнату пристроили!!!!!!!!!");
                }
                
            }
            if (right)
            {
                Vector3 position = new Vector3(x + (float)19.2, y, z);
                bool flag = false;
                foreach (GameObject room2 in _map.Where(room2 => room2.transform.position == position))
                {
                    flag = true;
                }
                foreach (GameObject room2 in roomsToAdd.Where(room2 => room2.transform.position == position))
                {
                    flag = true;
                }
                if (position == new Vector3(0, 0, 0))
                {
                    flag = true;
                }
                if (!flag) {
                    GameObject newRoom = Instantiate(_leftPathRooms[0], position, Quaternion.identity);
                    roomsToAdd.Add(newRoom);
                    Debug.Log("справа комнату пристроили!!!!!!!!!");
                }
            }
            if (back)
            {
                Vector3 position = new Vector3(x, y - (float)10.81, z);
                bool flag = false;
                foreach (GameObject room2 in _map.Where(room2 => room2.transform.position == position))
                {
                    flag = true;
                }
                foreach (GameObject room2 in roomsToAdd.Where(room2 => room2.transform.position == position))
                {
                    flag = true;
                }
                if (position == new Vector3(0, 0, 0))
                {
                    flag = true;
                }
                if (!flag) {
                    GameObject newRoom = Instantiate(_frontPathRooms[0], position, Quaternion.identity);
                    roomsToAdd.Add(newRoom);
                    Debug.Log("снизу комнату пристроили!!!!!!!!!");
                }
            }
            if (front)
            {
                Vector3 position = new Vector3(x, y + (float)10.81, z);
                bool flag = false;
                foreach (GameObject room2 in _map.Where(room2 => room2.transform.position == position))
                {
                    flag = true;
                }
                foreach (GameObject room2 in roomsToAdd.Where(room2 => room2.transform.position == position))
                {
                    flag = true;
                }
                if (position == new Vector3(0, 0, 0))
                {
                    flag = true;
                }
                if (!flag) {
                    GameObject newRoom = Instantiate(_backPathRooms[0], position, Quaternion.identity);
                    roomsToAdd.Add(newRoom);
                    Debug.Log("сверху комнату пристроили!!!!!!!!!");
                }
            }
        }

        foreach (GameObject roomNew in roomsToAdd)
        {
            _map.Add(roomNew);
        }
        Invoke(nameof(CheckBoss), 0.1f);
    }

    private void CheckBoss()
    {
        Debug.Log("check boss");
        RoomGenerator.Instance._hasTreasures = RoomGenerator.Instance._map.Any(room => room.CompareTag("TreasuresRoom"));
        RoomGenerator.Instance._hasShop = RoomGenerator.Instance._map.Any(room => room.CompareTag("ShopRoom"));
        RoomGenerator.Instance._hasBoss = RoomGenerator.Instance._map.Any(room => room.CompareTag("BossRoom"));
        if (!_hasBoss && _map.Any(x => x.GetComponent<RoomView>()._isDeadEnd))
        {
            int deadend = _map.FindLastIndex(x => x.GetComponent<RoomView>()._isDeadEnd);
            Vector3 position = _map[deadend].transform.position;
            bool hasFrontPath = _map[deadend].GetComponent<RoomView>()._hasFrontPath;
            bool hasBackPath = _map[deadend].GetComponent<RoomView>()._hasBackPath;
            bool hasRightPath = _map[deadend].GetComponent<RoomView>()._hasRightPath;
            bool hasLeftPath = _map[deadend].GetComponent<RoomView>()._hasLeftPath;
            Destroy(_map[deadend]);
            if (hasFrontPath)
            {
                _map[deadend] = Instantiate(_frontPathRooms[7], position, Quaternion.identity);
            } else if (hasBackPath)
            {
                _map[deadend] = Instantiate(_backPathRooms[10], position, Quaternion.identity);
            } else if (hasLeftPath)
            {
                _map[deadend] = Instantiate(_leftPathRooms[7], position, Quaternion.identity);
            }
            else
            {
                _map[deadend] = Instantiate(_rightPathRooms[7], position, Quaternion.identity);
            }
        }

        if (!_hasShop && _map.Any(x => x.GetComponent<RoomView>()._isDeadEnd))
        {
            int deadend = _map.FindLastIndex(x => x.GetComponent<RoomView>()._isDeadEnd);
            Vector3 position = _map[deadend].transform.position;
            bool hasFrontPath = _map[deadend].GetComponent<RoomView>()._hasFrontPath;
            bool hasBackPath = _map[deadend].GetComponent<RoomView>()._hasBackPath;
            bool hasRightPath = _map[deadend].GetComponent<RoomView>()._hasRightPath;
            bool hasLeftPath = _map[deadend].GetComponent<RoomView>()._hasLeftPath;
            Destroy(_map[deadend]);
            if (hasFrontPath)
            {
                _map[deadend] = Instantiate(_frontPathRooms[5], position, Quaternion.identity);
            } else if (hasBackPath)
            {
                _map[deadend] = Instantiate(_backPathRooms[8], position, Quaternion.identity);
            } else if (hasLeftPath)
            {
                _map[deadend] = Instantiate(_leftPathRooms[5], position, Quaternion.identity);
            }
            else
            {
                _map[deadend] = Instantiate(_rightPathRooms[5], position, Quaternion.identity);
            }
        }

        if (!_hasTreasures && _map.Any(x => x.GetComponent<RoomView>()._isDeadEnd))
        {
            int deadend = _map.FindLastIndex(x => x.GetComponent<RoomView>()._isDeadEnd);
            Vector3 position = _map[deadend].transform.position;
            bool hasFrontPath = _map[deadend].GetComponent<RoomView>()._hasFrontPath;
            bool hasBackPath = _map[deadend].GetComponent<RoomView>()._hasBackPath;
            bool hasRightPath = _map[deadend].GetComponent<RoomView>()._hasRightPath;
            bool hasLeftPath = _map[deadend].GetComponent<RoomView>()._hasLeftPath;
            Destroy(_map[deadend]);
            if (hasFrontPath)
            {
                _map[deadend] = Instantiate(_frontPathRooms[6], position, Quaternion.identity);
            } else if (hasBackPath)
            {
                _map[deadend] = Instantiate(_backPathRooms[9], position, Quaternion.identity);
            } else if (hasLeftPath)
            {
                _map[deadend] = Instantiate(_leftPathRooms[6], position, Quaternion.identity);
            }
            else
            {
                _map[deadend] = Instantiate(_rightPathRooms[6], position, Quaternion.identity);
            }
        }
    }

    public void InstantiateStartRoom()
    {
        Vector3 position = new Vector3(0, 0, 0);
        Debug.Log("start room was instantiated");
        GameObject room = Instantiate(_startRoom, position, Quaternion.identity);
        Game.Instance.CurrentRoom = room.GetComponent<RoomView>();
    }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    
    }
    
    public void SpawnRoomAfterLoad(MapWrapper wrapper)
    {
        int prefabID = wrapper._prefabID;
        Vector3 position = new Vector3(wrapper._x, wrapper._y, wrapper._z);
        GameObject room = Instantiate(Instance._allPrefabs[prefabID], position, Quaternion.identity);
        for (int i = 0; i < room.transform.GetComponent<RoomView>()._spawnPointAmount; ++i)
        {
            Destroy(room.transform.GetChild(3));
        }
        room.transform.GetComponent<RoomView>()._isActive = wrapper._isActive;
        _map.Add(room);
        Debug.Log(room.name);
    }
}