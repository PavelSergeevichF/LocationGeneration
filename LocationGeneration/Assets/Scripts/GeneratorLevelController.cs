using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GeneratorLevelController
{
    private const int CountWall = 4;

    private Tilemap _tileMapGround;
    private Tile _tileGround;
    private int _widhtMap;
    private int _heightMap;
    private int _factorSmooth;
    private int _randomFillPercent;

    private int[,] _map;


    public GeneratorLevelController(GenerateLevelView generateLevelView)
    {
        _tileMapGround = generateLevelView.TileMapGround;
        _tileGround = generateLevelView.TileGround;
        _widhtMap = generateLevelView.WidhtMap;
        _heightMap = generateLevelView.HeightMap;
        _factorSmooth = generateLevelView.RandomFillPercent;
        _randomFillPercent = generateLevelView.RandomFillPercent;

         _map = new int[_widhtMap,_heightMap];
    }

    public void GenerateLevel()
    {
        RandomFillLevel();
        for(var i = 0; i < _factorSmooth-1; i++)
            SmoothMap();
        DrowTileOnMap();
    }

    private void DrowTileOnMap()
    {
        if (_map == null)
            return;
        for(var x = 0; x < _widhtMap; x++)
        {
            for (var y = 0; y < _heightMap; y++)
            {
                var positionTile = new Vector3Int(-_widhtMap / 2 + x, -_heightMap / 2 - y, 0);
                if(_map[x,y]==1)
                {
                    _tileMapGround.SetTile(positionTile, _tileGround);
                }
            }
        }

    }

    private void SmoothMap()
    {
        for(var x=0; x<_widhtMap; x++)
        {
            for(var y=0; y<_heightMap; y++)
            {
                var neighbhourWallTiles = GetNeighbhourWallCount(x, y);
                if (neighbhourWallTiles > CountWall)
                    _map[x, y] = 1;
                else if(neighbhourWallTiles < CountWall)
                    _map[x, y] = 0;
            }
        }
    }
    public void ClearTileMap()
    {
        if (_tileMapGround != null)
            _tileMapGround.ClearAllTiles();
    }
    private int GetNeighbhourWallCount(int gridX, int gridY)
    {
        var wallCount = 0;
        for(var neighbhourX = gridX-1; neighbhourX <= gridX+1; neighbhourX++)
        {
            for(var neighbhourY = gridY - 1; neighbhourY <= gridY + 1; neighbhourY++)
            {
                if(neighbhourX >= 0 && neighbhourX < _widhtMap && neighbhourY >= 0 && neighbhourY < _heightMap)
                {
                    if (neighbhourX != gridX || neighbhourY != gridY)
                        wallCount += _map[neighbhourX, neighbhourY];
                }
                else 
                {
                    wallCount++;
                }
            }
        }
        return wallCount;
    }

    private void RandomFillLevel()
    {
        var pseudoRandom = new System.Random();

        for(var x = 0; x<_widhtMap; x++)
        {
            for(var y = 0; y<_heightMap; y++)
            {
                if (x == 0 || x == _widhtMap - 1 || y == 0 || y == _heightMap - 1)
                    _map[x, y] = 1;

                else
                    _map[x, y] = (pseudoRandom.Next(0, 100) < _randomFillPercent) ? 1 : 0;
            }
        }
    }
}
