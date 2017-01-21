using UnityEngine;

public class MazeCeiling : MazeCellEdge {

	public void Initialize(MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		base.Initialize (cell, otherCell, direction);
		cell.SetCeiling(direction, this);
	}
}
