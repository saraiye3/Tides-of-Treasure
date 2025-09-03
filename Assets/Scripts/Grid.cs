using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [System.Serializable]
    public struct PiecePrefab
    {
        public PieceType type;
        public GameObject prefab;
    }

    public enum PieceType
    {
        NORMAL,
        COUNT
    };

    private Dictionary<PieceType, GameObject> piecePrefabDict;
    private GamePiece[,] pieces; 
      
    public int xDim;
    public int yDim;
    
    public PiecePrefab[] piecePrefabs;
    public GameObject backgroundPrefab;
    public PieceType type;
    public GameObject prefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        piecePrefabDict = new Dictionary<PieceType, GameObject>();

        for(int i = 0; i < piecePrefabs.Length; i++)
        {
            if (!piecePrefabDict.ContainsKey(piecePrefabs[i].type))
            {
                piecePrefabDict.Add(piecePrefabs[i].type, piecePrefabs[i].prefab);
            }
        }

        for(int x = 0; x < xDim; x++)
        {
            for(int y = 0; y < yDim; y++)
            {
                GameObject background = (GameObject)Instantiate(backgroundPrefab, GetWorldPosition(x, y), Quaternion.identity);
                background.transform.parent = transform;
            }
        }

        pieces = new GamePiece[xDim, yDim];
        for (int x = 0; x < xDim; x++)
        {
            for (int y = 0; y < yDim; y++)
            {
                GameObject newPiece = (GameObject)Instantiate(piecePrefabDict[PieceType.NORMAL], Vector3.zero, Quaternion.identity);
                newPiece.name = "Piece(" + x + "," + y + ")";
                newPiece.transform.parent = transform;
                pieces[x,y] = newPiece.GetComponent<GamePiece>();
                pieces[x,y].Init(x, y, this, PieceType.NORMAL);

                if (pieces[x, y].IsMovable())
                {
                    pieces[x, y].MovableComponent.Move(x, y);
                }

                if (pieces[x, y].IsColored())
                {
                    pieces[x, y].ColorComponent.SetColor((ColorPiece.ColorType)Random.Range(0, pieces[x, y].ColorComponent.NumColors));
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector2 GetWorldPosition(int x, int y)
    {
        return new Vector2(
        x - xDim / 2f + 0.5f,
        y - yDim / 2f + 0.5f
    );
    }

}
