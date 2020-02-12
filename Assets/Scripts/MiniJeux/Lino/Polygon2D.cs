using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Polygon2D : MiniGame
{
   
    public float width = 1;
    public float height = 1;
    public float size;

    public Texture textureCircle;
    public Material material;

    public GameObject cube;
    public GameObject spawner;

    public List<Point> points;
    public int pointsNumber;

    public int phase;

    private MeshFilter mf;
    private MeshRenderer mr;

    public void Start()
    {
        mf = gameObject.AddComponent<MeshFilter>();
        mr = gameObject.AddComponent<MeshRenderer>();

        mr.material = material;
        Mesh mesh = mf.mesh;
        

        var vertices = new Vector3[pointsNumber];
        int index = 0;

        for (int i = 0; i < pointsNumber ; i += 2)
        {
            
            Point newPoint = new Point(new Vector3(-1,i/2,0),new Vector2(10,10), textureCircle);
            vertices[index] = newPoint.position;
            //print(newPoint.position);
            points.Add(newPoint);
            index++;

            newPoint = new Point(new Vector3(1, i/2, 0), new Vector2(10, 10), textureCircle);
            vertices[index] = newPoint.position;
            //print(newPoint.position); 
            points.Add(newPoint);
            index++;
        }

        //Debug.Log(points.Count);

        mf.mesh.vertices = vertices;

        var tris = new int[( (pointsNumber + 1)/2 - 1 ) * 6];
        //Debug.Log("tris="+((pointsNumber + 1) / 2 - 1) * 6);

        int numt = 0;
         for (int i = 0 ; i < pointsNumber - 2 ; i += 2)
         {
            //Debug.Log(i);
            tris[numt + 0] = i + 0;
            tris[numt + 1] = i + 2;
            tris[numt + 2] = i + 1;

            tris[numt + 3] = i + 2;
            tris[numt + 4] = i + 3;
            tris[numt + 5] = i + 1;

            numt += 6;
         }

        //Debug.Log(numt);
        mesh.triangles = tris;
    }

    public override void ToUpdate()
    {
        if (phase == 0)
        {
            UpdateMesh();
        } 
        else if (phase == 1)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                Instantiate(cube, spawner.transform.position, Quaternion.identity);
            }
        } 
        else if (phase == 2)
        {
            Reset();
        }

        phase = phase % 3;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            phase++;
        }     
    }

    private void OnGUI()
    {
        for (int i = 0; i < points.Count ; i++)
        {
            Point p = points[i];
            Vector2 pos = Camera.main.WorldToScreenPoint(transform.position + new Vector3(p.position.x, p.position.y, 0)) ;
            pos.y = -pos.y + Screen.height;
            GUI.DrawTexture(new Rect(pos - p.size/2,p.size),p.texture);
        }
        


        if (phase == 0)
        {
            GUI.Label(new Rect(new Vector2(100, 100), new Vector2(256, 128)), "Déplace les points de la mesh puis appuye sur Espace pour recommencer !");
        }
        else if (phase == 1)
        {
            GUI.Label(new Rect(new Vector2(100, 100), new Vector2(256, 128)), "Appuye sur A pour créé un block et sur Espace pour recommencer !");
        }
        else if (phase == 2)
        {
           // GUI.Label(new Rect(new Vector2(100, 100), new Vector2(128, 64)), "Phase : " + phase);
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < points.Count; i++)
        {
            Point p = points[i];
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(p.position, 0.1f);
        }
    }

    private void UpdateMesh()
    {
        var mesh = new Mesh();
        mf.mesh = mesh;

        var vertices = new Vector3[pointsNumber];
        int index = 0;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(transform.position + new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        for (int i = 0; i < points.Count; i++)
        {
            vertices[i] = points[i].position;
            index++;

            Vector2 pos = Camera.main.WorldToScreenPoint(points[i].position);
            if (Vector2.Distance(Input.mousePosition, pos) < 20)
            {
                points[i].size = new Vector2(30, 30);
                if (Input.GetMouseButtonDown(0))
                {
                    points[i].selected = true;
                }
            }
            else
            {
                points[i].size = new Vector2(20, 20);
            }

            if (Input.GetMouseButtonUp(0))
            {
                points[i].selected = false;
            }

            if (points[i].selected)
            {
                points[i].position = mousePosition;
            }
        }

        mesh.vertices = vertices;

        var tris = new int[((pointsNumber + 1) / 2 - 1) * 6];
        //Debug.Log("tris=" + ((pointsNumber + 1) / 2 - 1) * 6);

        int numt = 0;
        for (int i = 0; i<pointsNumber - 2; i += 2)
        {
            //Debug.Log(i);
            tris[numt + 0] = i + 0;
            tris[numt + 1] = i + 2;
            tris[numt + 2] = i + 1;

            tris[numt + 3] = i + 2;
            tris[numt + 4] = i + 3;
            tris[numt + 5] = i + 1;

            numt += 6;
        }

        //Debug.Log(numt);
        mesh.triangles = tris;

        GetComponent<MeshCollider>().sharedMesh = mesh;
    }

    public override bool Verif() => gameWin;

    private void Reset()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private bool gameWin;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Cube")
        {
            gameWin = true;
            print("Game Win");
        }
    }
}
