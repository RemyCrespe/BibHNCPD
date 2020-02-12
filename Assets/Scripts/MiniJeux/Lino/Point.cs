//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Point
{ 
    [SerializeField]
    public Vector2 position;
    public Vector2 size;
    public Texture texture;
    public bool selected;
    public Point(Vector2 _position, Vector2 _size, Texture _texture)
    {
        position = _position;
        size = _size;
        texture = _texture;
        selected = false;
    }
}
