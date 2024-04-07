namespace Room
{
  public class MapWrapper
  {
    public int _prefabID;
    public bool _isActive;
    public float _x;
    public float _y;
    public float _z;

    public MapWrapper(int prefabID, bool isActive, float x, float y, float z)
    {
      _prefabID = prefabID;
      _isActive = isActive;
      _x = x;
      _y = y;
      _z = z;
    }
  }
}