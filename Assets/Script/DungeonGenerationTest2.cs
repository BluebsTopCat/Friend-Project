using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DungeonTile
{
    public GameObject obj;
    public Vector2 pos;

    public DungeonTile(GameObject room, Vector2 position)
    {
        obj = room;
        pos = position;
    }
}
public class Connection
{
    public GameObject bridge;
    public Vector4 points;

    public Connection(Vector4 _point, GameObject tile)
    {
        points = _point;
        bridge = tile;
    }
}

public class DungeonGenerationTest2 : MonoBehaviour
{
    public int width;

    public int height;

    //public DungeonTile[] tiles;
    public GameObject room;
    public int numberofrooms;
    public int seed;
    public GameObject bridge;
    public GameObject bigisland;
    public List<Vector2> Largetiles = new List<Vector2>();
    public List<Connection> connections = new List<Connection>();
    public List<DungeonTile> tiles = new List<DungeonTile>();
    public GameObject parent;
    private static object locker = new object();

    // Start is called before the first frame update
    private void Start()
    {
        if (seed == 0) seed = (int) DateTime.Now.Ticks;

        Random.seed = seed;

        if (numberofrooms > width * height - 1)
        {
            numberofrooms = width * height - 1;
            Debug.Log("Error: more rooms then can fit!");
        }
        
        drawgrid();
       
        drawconnections();

        lock (locker)
        {
            findlargergrids();
        }

    }

    private void drawgrid()
    {
        var start = Instantiate(room, new Vector3(0, 0, 0), transform.rotation);
        start.transform.parent = parent.transform;
        tiles.Add(new DungeonTile(start, new Vector2(0, 0)));

        for (var i = 0; i < numberofrooms;)
        {
            var basepos = tiles[Random.Range(0, tiles.Count)].pos;

            var angle = Random.Range(1, 5);
            var newpos = basepos;
            switch (angle)
            {
                case 1:
                    newpos.x++;
                    break;
                case 2:
                    newpos.x--;
                    break;
                case 3:
                    newpos.y++;
                    break;
                case 4:
                    newpos.y--;
                    break;
            }

            if (tiles.Find(x => x.pos == newpos) == null && Mathf.Abs(newpos.x) < width / 2 + 1 &&
                Mathf.Abs(newpos.y) < width / 2 + 1)
            {
                var tile = Instantiate(room, new Vector3(newpos.x, newpos.y, 0), transform.rotation);
                tile.transform.parent = parent.transform;
                tiles.Add(new DungeonTile(tile, newpos));
                tile.name = "Block " + newpos;
                var newb = Instantiate(bridge, new Vector3((newpos.x + basepos.x) / 2, (newpos.y + basepos.y) / 2, 0),
                    transform.rotation);
                if (angle == 1 || angle == 2)
                    newb.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                else
                    newb.transform.eulerAngles = new Vector3(0f, 0f, 90f);
                i++;
                newb.transform.parent = parent.transform;
                connections.Add(new Connection(new Vector4(basepos.x, basepos.y, newpos.x, newpos.y), newb));
                newb.name = "Bridge: " + new Vector4(basepos.x, basepos.y, newpos.x, newpos.y);
            }
        }
    }

    private void drawconnections()
    {
        for (var b = 0; b < numberofrooms / 4;)
        {
            var bridgepos = tiles[Random.Range(0, tiles.Count)].pos;
            var angle = Random.Range(1, 5);
            var newbridgepos = bridgepos;
            switch (angle)
            {
                case 1:
                    newbridgepos.x++;
                    break;
                case 2:
                    newbridgepos.x--;
                    break;
                case 3:
                    newbridgepos.y++;
                    break;
                case 4:
                    newbridgepos.y--;
                    break;
            }

            if (
                connections.Find(d =>
                    d.points == new Vector4(bridgepos.x, bridgepos.y, newbridgepos.x, newbridgepos.y)) == null &&
                tiles.Find(x => x.pos == newbridgepos) != null)
            {
                var newb = Instantiate(bridge, new Vector3((newbridgepos.x + bridgepos.x) / 2, (newbridgepos.y + bridgepos.y) / 2, 0), transform.rotation);
                if (angle == 1 || angle == 2)
                    newb.transform.eulerAngles = new Vector3(0f, 0f, 0f);
                else
                    newb.transform.eulerAngles = new Vector3(0f, 0f, 90f);
                connections.Add(new Connection(new Vector4(bridgepos.x, bridgepos.y, newbridgepos.x, newbridgepos.y),
                    newb));
                newb.transform.parent = parent.transform;
                newb.name = "After the fact Bridge: " + new Vector4(bridgepos.x, bridgepos.y, newbridgepos.x, newbridgepos.y).ToString();
                b++;
            }
        }
    }

    private void findlargergrids()
    {
        foreach (var dg in tiles)
        {
            var top = connections.Find(d => d.points == new Vector4(dg.pos.x, dg.pos.y, dg.pos.x - 1, dg.pos.y)) != null
                  ? connections.Find(d => d.points == new Vector4(dg.pos.x, dg.pos.y, dg.pos.x - 1, dg.pos.y))
                  : connections.Find(d => d.points == new Vector4(dg.pos.x - 1, dg.pos.y, dg.pos.x, dg.pos.y));

            var right = connections.Find(d => d.points == new Vector4(dg.pos.x, dg.pos.y, dg.pos.x, dg.pos.y - 1)) != null
                    ? connections.Find(d => d.points == new Vector4(dg.pos.x, dg.pos.y, dg.pos.x, dg.pos.y - 1))
                    : connections.Find(d => d.points == new Vector4(dg.pos.x, dg.pos.y - 1, dg.pos.x, dg.pos.y));

            var left = connections.Find(d => d.points == new Vector4(dg.pos.x - 1, dg.pos.y, dg.pos.x - 1, dg.pos.y - 1)) != null
                    ? connections.Find(d => d.points == new Vector4(dg.pos.x - 1, dg.pos.y, dg.pos.x - 1, dg.pos.y - 1))
                    : connections.Find(d => d.points == new Vector4(dg.pos.x - 1, dg.pos.y - 1, dg.pos.x - 1, dg.pos.y));

            var bottom = connections.Find(d => d.points == new Vector4(dg.pos.x, dg.pos.y - 1, dg.pos.x - 1, dg.pos.y - 1)) != null
                    ? connections.Find(d => d.points == new Vector4(dg.pos.x, dg.pos.y - 1, dg.pos.x - 1, dg.pos.y - 1))
                    : connections.Find(d => d.points == new Vector4(dg.pos.x - 1, dg.pos.y - 1, dg.pos.x, dg.pos.y - 1));


            if (top != null && left != null && right != null && bottom != null && !Largetiles.Contains(dg.pos) &&
                !Largetiles.Contains(new Vector2(dg.pos.x - 1, dg.pos.y)) &&
                !Largetiles.Contains(new Vector2(dg.pos.x, dg.pos.y - 1)) &&
                !Largetiles.Contains(new Vector2(dg.pos.x - 1, dg.pos.y - 1)))
            {
                Debug.Log("Found box at " + dg.pos + " - " + new Vector2(dg.pos.x - 1, dg.pos.y - 1));

                var largetile = Instantiate(bigisland, new Vector3(dg.pos.x - 0.5f, dg.pos.y - 0.5f, 0f),
                    transform.rotation);
                largetile.name = "Large Tile: " + dg.pos + " - " + new Vector2(dg.pos.x - 1, dg.pos.y - 1);
                largetile.transform.parent = parent.transform;
                Largetiles.Add(dg.pos);
                Largetiles.Add(new Vector2(dg.pos.x - 1, dg.pos.y));
                Largetiles.Add(new Vector2(dg.pos.x - 1, dg.pos.y - 1));
                Largetiles.Add(new Vector2(dg.pos.x, dg.pos.y - 1));
                
                Destroy(top.bridge);
                connections.Remove(top);
                Destroy(bottom.bridge);
                connections.Remove(bottom);
                Destroy(left.bridge);
                connections.Remove(left);
                Destroy(right.bridge);
                connections.Remove(right);
                Destroy(tiles.Find(tile => tile.pos == dg.pos).obj);
                Destroy(tiles.Find(tile => tile.pos == new Vector2(dg.pos.x - 1, dg.pos.y)).obj);
                Destroy(tiles.Find(tile => tile.pos == new Vector2(dg.pos.x - 1, dg.pos.y - 1)).obj);
                Destroy(tiles.Find(tile => tile.pos == new Vector2(dg.pos.x, dg.pos.y - 1)).obj);
            }
        }
    }
}