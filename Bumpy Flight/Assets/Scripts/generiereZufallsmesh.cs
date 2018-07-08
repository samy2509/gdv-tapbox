using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The RequireComponent attribute automatically adds required components as dependencies
[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]


public class generiereZufallsmesh : MonoBehaviour
{
	public  int 		laenge, breite	= 0;            // Laenge und Breite des Zufallsmeshes
	private Mesh 		mesh;                           // Mesh
	private int			meshGroesse		= 0;            // Meshgroesse
    private Vector3[] 	verts;                          // Vertices
	private Vector2[]   uvCoords;                       // UV Koordinaten
    private int[]       tris;                           // Dreiecke (Triangles)
    
	// Awake is called when the script instance is being loaded
    private void Awake()
    {
        laenge  = Random.Range(10, 20);
        breite  = Random.Range(5, 10);
        mesh    = new Mesh();

        Generiere();
    }

    /*
     *  Generiert Zufallsmesh
     */
    private void Generiere()
    {
        GetComponent<MeshFilter>().mesh = mesh;
		meshGroesse                     = (laenge + 1) * (breite + 1);
        verts                           = new Vector3[meshGroesse];
        uvCoords                        = new Vector2[meshGroesse];

        int i = 0;
        for (int y = 0; y <= breite; y++)
        {
            for (int x = 0; x <= laenge; x++)
            {
                verts[i]    = new Vector3(x, y);
                uvCoords[i] = new Vector2((float)x / laenge, (float)y / breite);

                i++;
            }
        }
        mesh.vertices   = verts;
        mesh.uv         = uvCoords;


        tris    = new int[laenge * breite * 6]; 
        // Rechteck Test
        // tris[0]              = 0;
        // tris[1] = tris[4]    = laenge + 1;
        // tris[2] = tris[3]    = 1;
        // tris[5]              = laenge + 2;

        int tri     = 0;
        int vert    = 0;
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
        mesh.triangles = tris;
        mesh.RecalculateNormals();

    }

}
