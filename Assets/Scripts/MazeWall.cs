using UnityEngine;

public class MazeWall : MazeCellEdge {

	public void Initialize(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		base.Initialize (cell, otherCell, direction);
		cell.SetEdge(direction, this);
    }
}
