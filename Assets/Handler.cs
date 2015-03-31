using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Handler : MonoBehaviour
{
    float doubleclickDelay = 0.2f;
    List<ColoredRectangle> rectangles = new List<ColoredRectangle>();
    public static List<Connection> connections = new List<Connection>();
    float doubleclickedAt;
    ColoredRectangle rectangleToDrag;
    MouseConnection mouseConnection;

	void Start ()
    {
        mouseConnection = new MouseConnection();
	}
	
	void Update ()
    {
        if ( Input.GetMouseButton( 0 ) )
        {
            if ( !ReferenceEquals( rectangleToDrag, null ) ) // dragging
            {
                if( !IsOverlappingAnotherRectangle( rectangleToDrag ) )
                {
                    Vector2 initialPosition = rectangleToDrag.rect.center;
                    rectangleToDrag.Move( MouseCoords() );
                    if( IsOverlappingAnotherRectangle( rectangleToDrag ) )
                    {
                        rectangleToDrag.Move( initialPosition );
                    }
                }
            }
        }

        if ( Input.GetMouseButtonDown( 0 ) )
        {
            if ( Time.time - doubleclickedAt < doubleclickDelay
                 &&
                 !ReferenceEquals( GetClickedRectangle(), null ) ) // doubleclick
            {
                Debug.Log( "called" );
                GetClickedRectangle().Trash();
            }
            else // singleclick
            {
                ColoredRectangle newRectangle = new ColoredRectangle( MouseCoords() );

                if ( IsOverlappingAnotherRectangle( newRectangle ) )
                {
                    rectangleToDrag = GetClickedRectangle();
                }
                else
                {
                    rectangles.Add( newRectangle );
                }
            }

            doubleclickedAt = Time.time;
        }
        else if ( Input.GetMouseButtonUp( 0 ) )
        {
            rectangleToDrag = null;
        }

        HandleRectangleConnection();
        GarbageCollection();
	}

    ColoredRectangle rectangleToConnect;
    ColoredRectangle rectangleToConnect2;
    void HandleRectangleConnection()
    {
        if ( Input.GetMouseButtonDown( 1 ) )
        {
            if( ReferenceEquals( rectangleToConnect, null ) )
            {
                rectangleToConnect = GetClickedRectangle();
                mouseConnection.Enable( rectangleToConnect );
            }
            else
            {
                rectangleToConnect2 = GetClickedRectangle();
                Connection newConnection = new Connection( rectangleToConnect, rectangleToConnect2 );

                if( !ReferenceEquals( rectangleToConnect2, null )
                    &&
                    !ReferenceEquals( rectangleToConnect2, rectangleToConnect )
                    &&
                    !connections.Find( connection => ReferenceEquals( connection, newConnection ) ) )
                {
                    connections.Add( newConnection );
                }
                else
                {
                    newConnection.Trash();
                }

                rectangleToConnect = null;
                mouseConnection.Disable();
            }
        }
    }

    void GarbageCollection()
    {
        rectangles.RemoveAll( rectangle => rectangle.toDestroy == true );
        connections.RemoveAll( connection => connection.toDestroy == true );
    }

	void OnGUI ()
    {
        DrawRectangles();
        DrawConnections();
        mouseConnection.Draw();
	}

    void DrawRectangles()
    {
        foreach ( ColoredRectangle rectangle in rectangles )
        {
            DrawRect( rectangle );
        }
    }

    void DrawConnections()
    {
        foreach ( Connection connection in connections )
        {
            connection.Draw();
        }
    }

    void DrawRect( ColoredRectangle rectangle )
    {
        Texture2D texture = new Texture2D( 1, 1 );
        texture.SetPixel( 0, 0, rectangle.color );
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box( rectangle.rectConvertedSpeciallyForGUIBoxMethod, GUIContent.none );
    }

    Vector2 MouseCoords()
    {
        return new Vector2( Input.mousePosition.x, Input.mousePosition.y );
    }

    bool IsOverlappingAnotherRectangle( ColoredRectangle controlRectangle )
    {
        foreach ( ColoredRectangle rectangle in rectangles )
        {
            if( !ReferenceEquals( controlRectangle, rectangle )
                &&
                controlRectangle.rect.Overlaps( rectangle.rect ) )
            {
                return true;
            }
        }

        return false;
    }

    ColoredRectangle GetClickedRectangle()
    {
        foreach ( ColoredRectangle rectangle in rectangles )
        {
            if( rectangle.rect.Contains( MouseCoords() ) )
            {
                return rectangle;
            }
        }

        return null;
    }
}
