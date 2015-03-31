using UnityEngine;

public class ColoredRectangle : Object
{
    const int width = 50;
    const int height = 25;

    public Rect rect;
    public Color color;
    public bool toDestroy;

    public ColoredRectangle( Vector2 center )
    {
        rect = new Rect( 0, 0, width, height );
        Move( center );
        color = RandomColor();
    }
    
    public void Move( Vector2 to )
    {
        rect.center = new Vector2( to.x, to.y );
    }

    public Rect rectConvertedSpeciallyForGUIBoxMethod // that's because GUI methods are somewhat stupid
    {
        get
        {
            Rect convertedRect = rect;
            convertedRect.center = new Vector2( convertedRect.center.x, Screen.height - convertedRect.center.y );
            return convertedRect;
        }
    }

    public void Trash()
    {
        foreach ( Connection connection in Handler.connections )
        {
            if( ReferenceEquals( connection.rectangleA, this )
                ||
                ReferenceEquals( connection.rectangleB, this ) )
            {
                connection.Trash();
            }
        }

        toDestroy = true;
    }

    private Color RandomColor()
    {
        return new Color( Random.value, Random.value, Random.value );
    }
}
