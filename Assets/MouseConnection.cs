using UnityEngine;

public class MouseConnection
{
    public ColoredRectangle rectangle;
    public LineRenderer lineRenderer = new LineRenderer();
    public bool enabled;

    public MouseConnection()
    {
        enabled = false;
        initLineRenderer();
    }

    public void Enable( ColoredRectangle _rectangle )
    {
        if( !ReferenceEquals( _rectangle, null ) )
        {
            rectangle = _rectangle;
            enabled = true;
        }
    }

    public void Disable()
    {
        enabled = false;
        lineRenderer.enabled = false;
    }

    void initLineRenderer()
    {
        lineRenderer = new GameObject().AddComponent( "LineRenderer" ) as LineRenderer;
        lineRenderer.SetColors( Color.red, Color.red );
        lineRenderer.SetWidth( 0.03f, 0.03f );
    }

    public void Draw()
    {
        if( enabled )
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(
                0,
                Camera.main.ScreenToWorldPoint(
                new Vector3(
                    rectangle.rect.center.x,
                    rectangle.rect.center.y,
                    10 )
                )
            );
            lineRenderer.SetPosition(
                1,
                Camera.main.ScreenToWorldPoint(
                new Vector3(
                    Input.mousePosition.x,
                    Input.mousePosition.y,
                    10 )
                )
            );
        }
    }
}
