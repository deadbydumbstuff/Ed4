using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "TileMapData", menuName = "Scriptable Objects/TileMapData")]
public class TileMapData : ScriptableObject
{
    public TileBase[] tiles;
    public TerrainType type;
    public enum TerrainType
    {
        Path = 0,
        Grass = 1,
        DifficultTerrain = 5,
        Water = 10,
        Impassible = -1
    }
}
