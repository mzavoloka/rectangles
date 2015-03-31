using UnityEngine;

public class Connection : Object
{
    public ColoredRectangle rectangleA;
    public ColoredRectangle rectangleB;
    public LineRenderer lineRenderer = new LineRenderer();
    public bool toDestroy;

    private GameObject obj;

    public Connection( ColoredRectangle _rectangleA, ColoredRectangle _rectangleB )
    {
        rectangleA = _rectangleA;
        rectangleB = _rectangleB;
        initLineRenderer();
    }

    void initLineRenderer()
    {
        obj = new GameObject();
        lineRenderer = obj.AddComponent( "LineRenderer" ) as LineRenderer;
        lineRenderer.SetColors( Color.red, Color.red );
        lineRenderer.SetWidth( 0.03f, 0.03f );
    }

    public void Draw()
    {
        lineRenderer.SetPosition(
            0,
            Camera.main.ScreenToWorldPoint(
            new Vector3(
                rectangleA.rect.center.x,
                rectangleA.rect.center.y,
                10 )
            )
        );
        lineRenderer.SetPosition(
            1,
            Camera.main.ScreenToWorldPoint(
            new Vector3(
                rectangleB.rect.center.x,
                rectangleB.rect.center.y,
                10 )
            )
        );
    }

    public void Trash()
    {
        Destroy( lineRenderer );
        Destroy( obj );
        toDestroy = true;
    }
}
