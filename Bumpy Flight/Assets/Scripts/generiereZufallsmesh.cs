using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The RequireComponent attribute automatically adds required components as dependencies
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]


public class generiereZufallsmesh : MonoBehaviour
{
    public  int         laenge, breite  = 0;            // Laenge und Breite des Zufallsmeshes
    private Mesh        mesh;                           // Mesh
    private int         meshGroesse     = 0;            // Meshgroesse
    private Vector3[]   verts;                          // Vertices
    private Vector2[]   uvCoords;                       // UV Koordinaten
    private int[]       tris;                           // Dreiecke (Triangles)
    
    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        laenge  = Random.Range(30, 50);
        breite  = 8;
        mesh    = new Mesh();

        Generiere();
    }

    /*
     *  Generiert Zufallsmesh
     */
    private void Generiere()
    {
        GetComponent<MeshFilter>().mesh = mesh;
        meshGroesse                     = 5 * (laenge + 1) * (breite + 1);
        verts                           = new Vector3[meshGroesse];
        uvCoords                        = new Vector2[meshGroesse];

        int i = 0;

        //x-y-Ebene - Wand hinten
        for (int y = 0; y <= breite; y++)
        {
            for (int x = 0; x <= laenge; x++)
            {
                float zPos;

                if (x == 0 || x == laenge || y == 0 || y == breite){
                    zPos = 0;
                } else {
                    zPos = Random.Range(-1.0f, 0.0f);
                }
                verts[i]    = new Vector3(x, y, zPos);
                uvCoords[i] = new Vector2((float)x / laenge, (float)y / breite);

                i++;
            }
        }

        //y-z-Ebene1 - Wand links
        for (int y = 0; y <= breite; y++)
        {
            for (int z = -laenge; z <= 0; z++)
            {
                float xPos;

                if (z == 0 || z == laenge || z == -laenge || y == 0 || y == breite || y == -breite)
                {
                    xPos = 0;
                } else {
                    xPos = Random.Range(0.0f, 1.0f);
                }
                verts[i] = new Vector3(xPos, y, z);
                uvCoords[i] = new Vector2((float)z / laenge, (float)y / breite);

                i++;
            }
        }

        //y-z-Ebene2 - Wand rechts
        for (int y = 0; y <= breite; y++)
        {
            for (int z = 0; z >= -laenge; z--)
            {
                float xPos;

                if (z == 0 || z == laenge || z == -laenge || y == 0 || y == breite || y == -breite){
                    xPos = 0;
                } else {
                    xPos = Random.Range(-1.0f, 0.0f);
                }
                verts[i] = new Vector3(xPos + laenge, y, z);
                uvCoords[i] = new Vector2((float)z / laenge, (float)y / breite);

                i++;
            }
        }

        //x-z-Ebene1 - Boden
        for (int z = -breite; z <= 0; z++)
        {
            for (int x = 0; x <= laenge; x++)
            {
                float yPos;

                if (z == 0 || z == breite || z == -breite || x == 0 || x == laenge || x == -laenge){
                    yPos = 0;
                } else {
                    yPos = Random.Range(0.0f, 0.3f);
                }
                verts[i] = new Vector3(x, yPos, z);
                uvCoords[i] = new Vector2((float)x / laenge, (float)z / breite);

                i++;
            }
        }

        //x-z-Ebene2 - Decke
        for (int z = -breite; z <= 0; z++)
        {
            for (int x = laenge; x >= 0; x--)
            {
                float yPos;

                if (z == 0 || z == breite || z == -breite || x == 0 || x == laenge || x == -laenge){
                    yPos = 0;
                } else {
                    yPos = Random.Range(-1.5f, 0.0f);
                }
                verts[i] = new Vector3(x, yPos + breite, z);
                uvCoords[i] = new Vector2((float)x / laenge, (float)z / breite);

                i++;
            }
        }
        mesh.vertices   = verts;
        mesh.uv         = uvCoords;


        tris    = new int[5 * laenge * breite * 6]; 
        // Rechteck Test
        // tris[0]              = 0;
        // tris[1] = tris[4]    = laenge + 1;
        // tris[2] = tris[3]    = 1;
        // tris[5]              = laenge + 2;

        int tri     = 0;
        int vert    = 0;

        for (int anzahl = 0; anzahl < 5; anzahl++)
        {
            // Zeile
            for (int y = 0; y < breite; y++)
            {
                // Spalte
                for (int x = 0; x < laenge; x++)
                {
                    tris[tri]                       = vert;
                    tris[tri + 1] = tris[tri + 4]   = vert + laenge + 1;
                    tris[tri + 2] = tris[tri + 3]   = vert + 1;
                    tris[tri + 5]                   = vert + laenge + 2;

                    tri += 6;
                    vert++;
                }
                vert++;
            }
            vert += laenge + 1;
        }

        mesh.triangles = tris;
        mesh.RecalculateNormals();

    }
}
