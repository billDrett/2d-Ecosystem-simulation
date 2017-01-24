using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class Pathfinding : MonoBehaviour {
 
    Rigidbody2D rb2D;    
    
    public Transform seeker;
    public Transform target;
    int frames;
    Vector3 targetChangePos;

    [Range(0,0.2f)] public float speed = 0.1f;
               
    Grid grid;
 
    void Awake() {
            // grid = GetComponent<Grid>();
            GameObject go = GameObject.Find("Map");
			grid = go.GetComponent<Grid>();
    }

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
 
    void Update() {
        /*foods = GameObject.FindGameObjectsWithTag("Food");
        if (foods.Length == 0) return;
        
        target = foods[0].transform;*/
        ////////////////////////////////////////////////////
        if (grid.NodeFromWorldPoint(target.position).walkable)
        {
            // if(frames%60 == 0)
            FindPath(seeker.position, target.position);
            

            if (grid.path != null && grid.path.Count > 0)
            {
                if (grid.path[0] != grid.NodeFromWorldPoint(seeker.position))
                {
                    Vector3 flatDirection = new Vector3(grid.path[0].worldPosition.x, grid.path[0].worldPosition.y, 0);
                    Vector3 moveDirection = (flatDirection - seeker.position);
                    Vector2 moveDirection2D = new Vector2(moveDirection.x, moveDirection.y);
                    moveDirection2D.Normalize();
                    // seeker.position += moveDirection * speed;
                    rb2D.MovePosition(rb2D.position + moveDirection2D * speed);
                }
            }
        }
        //else
        {
          //  target.position = nearestWalkableArea(grid.NodeFromWorldPoint(target.position));
        }
        

    }
 
    bool FindPath(Vector3 startPos, Vector3 targetPos) {
            Node startNode = grid.NodeFromWorldPoint(startPos);
            Node targetNode = grid.NodeFromWorldPoint(targetPos);
 
            List<Node> openSet = new List<Node>();
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);
 
            while (openSet.Count > 0) {
                    Node currentNode = openSet[0];
                    for (int i = 1; i < openSet.Count; i ++) {
                            if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost) {
                                    currentNode = openSet[i];
                            }
                    }
 
                    openSet.Remove(currentNode);
                    closedSet.Add(currentNode);
 
                    if (currentNode == targetNode) {
                            RetracePath(startNode,targetNode);
                            return true;
                    }
 
                    foreach (Node neighbour in grid.GetNeighbours(currentNode)) {
                            if (!neighbour.walkable || closedSet.Contains(neighbour)) {
                                    continue;
                            }
 
                            int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                            if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)) {
                                    neighbour.gCost = newMovementCostToNeighbour;
                                    neighbour.hCost = GetDistance(neighbour, targetNode);
                                    neighbour.parent = currentNode;
 
                                    if (!openSet.Contains(neighbour))
                                            openSet.Add(neighbour);
                            }
                    }
            }

            return false;
    }
 
    void RetracePath(Node startNode, Node endNode) {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;
 
            while (currentNode != startNode) {
                    path.Add(currentNode);
                    currentNode = currentNode.parent;
            }
                
            List<Node> waypoints = SimplifyPath(path);
            path = waypoints;

            path.Reverse();
 
            grid.path = path;
 
    }
 
    List<Node> SimplifyPath(List<Node> path) {
        List<Node> waypoints = new List<Node>();
        Vector2 directionOld = Vector2.zero;

        for (int i = 1; i < path.Count; i ++) {
            Vector2 directionNew = new Vector2(path[i-1].gridX - path[i].gridX,path[i-1].gridY - path[i].gridY);
            if (directionNew != directionOld) {
                waypoints.Add(path[i]);
            }
            directionOld = directionNew;
        }
        return waypoints;
    }

    int GetDistance(Node nodeA, Node nodeB) {
            int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
            int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
 
            if (dstX > dstY)
                    return 14*dstY + 10* (dstX-dstY);
            return 14*dstX + 10 * (dstY-dstX);
    }

    public Vector3 nearestWalkableArea(Node node)
    {
        List<Node> neighbours = grid.GetNeighbours(node); //use like a queue

        while(neighbours.Count != 0)
        {
            if(neighbours[0].walkable)
            {
                return neighbours[0].worldPosition;
            }
            else
            {
                neighbours.AddRange(grid.GetNeighbours(neighbours[0]));
                neighbours.RemoveAt(0);
            }
        }

        return Vector3.zero;
    }
}
 