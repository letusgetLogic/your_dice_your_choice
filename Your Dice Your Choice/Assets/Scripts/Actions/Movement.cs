using System.Collections.Generic;
using UnityEngine;

public class Movement
{
    public Dictionary<string, string> MoveDescription = new Dictionary<string, string>
    {
        {"MoveXTiles", "Move in any direction X Tiles"},
        {"MoveX+Tiles", "Move horizontally or vertically X Tiles"},
        {"MoveXxTiles", "Move diagonally X Tiles"},
        {"Move1-3Tiles", "Move in any direction X Tiles, if X is in the range 1-3"},
        {"Move1-3+Tiles", "Move horizontally or vertically X Tiles, if X is in the range 1-3"},
        {"Move1-3xTiles", "Move diagonally X Tiles, if X is in the range 1-3"},
        {"Move1Tile", "Move in any direction 1 Tile"},
        {"Move1+Tiles", "Move horizontally or vertically 1 Tile"},
        {"Move1xTiles", "Move diagonally 1 Tile"},
    };
    
    
}
