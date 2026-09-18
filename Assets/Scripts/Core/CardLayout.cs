using UnityEngine;

public static class CardLayout
{
    public static readonly Vector2[] Positions = new Vector2[]
    {
        new Vector2(  0,   0),
        new Vector2(  0, -75),
        new Vector2( 53, -53),
        new Vector2( 75,   0),
        new Vector2( 53,  53),
        new Vector2(  0,  75),
        new Vector2(-53,  53),
        new Vector2(-75,   0),
    };

    public const float CardRadius = 160f; 
}