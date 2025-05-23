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
        Grass = 200,
        DifficultTerrain = 500,
        Water = 1000,
        Impassible = -1
    }
}
