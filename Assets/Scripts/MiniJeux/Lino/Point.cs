/*******************************
** BLONDELLE Lino
** Définit les points qui compose le polygone 2D
*******************************/
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
