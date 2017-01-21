using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour {

	public MazeCell cellPrefab;
	
	private MazeCell[,] cells;

	public float generationStepDelay;

	public IntVector2 size;

	public MazePassage passagePrefab;
	public MazeWall wallPrefab;
	public MazeEmpty emptyPrefab;
	public MazeCeiling ceilingPrefab;

	public int randomizer;

	public MazeCell GetCell (IntVector2 coordinates) {
		return cells [coordinates.x, coordinates.z];
	}

	private void CreatePassage (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazePassage passage = Instantiate (passagePrefab) as MazePassage;
		passage.Initialize (cell, otherCell, direction);
		passage = Instantiate (passagePrefab) as MazePassage;
		passage.Initialize (otherCell, cell, direction.GetOpposite ());
	}

	private void CreateWall  (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazeWall wall = Instantiate (wallPrefab) as MazeWall;
		wall.Initialize (cell, otherCell, direction);
		if (otherCell != null) {
			wall = Instantiate (wallPrefab) as MazeWall;
			wall.Initialize (otherCell, cell, direction.GetOpposite ());
		}
	}

	private void CreateEmpty (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazeEmpty empty = Instantiate (emptyPrefab) as MazeEmpty;
		empty.Initialize (cell, otherCell, direction);
		if (otherCell != null) {
			empty = Instantiate (emptyPrefab) as MazeEmpty;
			empty.Initialize (otherCell, cell, direction.GetOpposite ());
		}
	}

	private void CreateCeiling (MazeCell cell, MazeCell otherCell, MazeDirection direction) {
		MazeCeiling ceiling = Instantiate (ceilingPrefab) as MazeCeiling;
		ceiling.Initialize (cell, otherCell, direction);
		ceiling = Instantiate (ceilingPrefab) as MazeCeiling;
		ceiling.Initialize (otherCell, cell, direction.GetOpposite ());
	}

	private void DoFirstGenerationStep (List<MazeCell> activeCells) {
		activeCells.Add (CreateCell (RandomCoordinates));
	}

	private void DoNextGenerationStep (List<MazeCell> activeCells) {
		int currentIndex = activeCells.Count - 1;
		MazeCell currentCell = activeCells[currentIndex];
		if (currentCell.IsFullyInitialized) {
			activeCells.RemoveAt(currentIndex);
			return;
		}
		MazeDirection direction = currentCell.RandomUninitializedDirection;
		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
		if (ContainsCoordinates(coordinates)) {
			MazeCell neighbor = GetCell(coordinates);
			if (neighbor == null) {
				neighbor = CreateCell(coordinates);
				CreatePassage(currentCell, neighbor, direction);
				CreateCeiling(currentCell, neighbor, direction);
				activeCells.Add(neighbor);
			}
			else {
				randomizer = Random.Range (0, 100);
				if (randomizer < 50)
					CreateEmpty (currentCell, neighbor, direction);
				else
					CreateWall(currentCell, neighbor, direction);
			}
		}
		else {
			CreateWall(currentCell, null, direction);
		}
	}

	public IEnumerator Generate() {
		WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
		cells = new MazeCell[size.x, size.z];
		List<MazeCell> activeCells = new List<MazeCell>();
		DoFirstGenerationStep (activeCells);
		while (activeCells.Count > 0) {
			yield return delay;
			DoNextGenerationStep(activeCells);
		}
	}

	public IntVector2 RandomCoordinates {
				get {
						return new IntVector2 (Random.Range (0, size.x), Random.Range (0, size.z));
				}
		}

	public bool ContainsCoordinates (IntVector2 coordinates) {
				return coordinates.x >= 0 && coordinates.x < size.x && coordinates.z >= 0 && coordinates.z < size.z;
		}

	private MazeCell CreateCell (IntVector2 coordinates) {
		MazeCell newCell = Instantiate (cellPrefab) as MazeCell;
		cells[coordinates.x, coordinates.z] = newCell;
		newCell.coordinates = coordinates;
		newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
		newCell.transform.parent = transform;
		newCell.transform.localPosition = 
			new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
		return newCell;
	}
}
