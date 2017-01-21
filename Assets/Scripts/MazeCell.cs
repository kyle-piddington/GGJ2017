﻿using UnityEngine;

public class MazeCell : MonoBehaviour {

	public IntVector2 coordinates;

	private MazeCellEdge[] edges = new MazeCellEdge[MazeDirections.Count];

	private int initializedEdgeCount;

	public MazeCellEdge getEdge	(MazeDirection direction) {
		return edges [(int)direction];
	}

	public bool IsFullyInitialized {
		get {
			return initializedEdgeCount == MazeDirections.Count;
		}
	}

	public void SetEdge (MazeDirection direction, MazeCellEdge edge) {
		edges [(int)direction] = edge;
		initializedEdgeCount += 1;
	}

	public void SetCeiling (MazeDirection direction, MazeCellEdge edge) {
		edges [(int)direction] = edge;
	}

	public MazeDirection RandomUninitializedDirection {
		get {
			int skips = Random.Range (0, MazeDirections.Count - initializedEdgeCount);
			for (int i = 0; i < MazeDirections.Count; i++) {
				if (edges[i] == null) {
					if (skips == 0)
						return (MazeDirection)i;
					skips -= 1;
				}
			}
			throw new System.InvalidOperationException("MazeCell has no uninitialized directions left.");
		}
	}
}
