using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zephkelly
{
  public class Chunk
  {
    public Vector2Int Position { get; set; }
    public GameObject ChunkObject { get; set; }

    private Dictionary<Vector2, Asteroid> asteroids = 
      new Dictionary<Vector2, Asteroid>();

    public Dictionary<Vector2, Asteroid> Asteroids { 
      get => asteroids;
      set => asteroids = value;
    }

    public void SetChunkObject(Vector2Int _position, GameObject _chunkObject)
    {
      Position = _position;
      ChunkObject = _chunkObject;
    }

    public void AddAsteroid(Vector2 _position, Asteroid _asteroid)
    {
      asteroids.Add(_position, _asteroid);
    }
  }
}