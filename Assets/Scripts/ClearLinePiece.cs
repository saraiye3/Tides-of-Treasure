using UnityEngine;

public class ClearLinePiece : ClearablePiece
{
    public bool isRow; //If the piece clears a column, isRow = false

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public override void Clear()
    {
        base.Clear();

        if(isRow) 
        {
            piece.GridRef.ClearRow(piece.Y);
        }
        else
        {
            piece.GridRef.ClearColumn(piece.X);
        }
    }
}   
