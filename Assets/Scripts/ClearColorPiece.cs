using UnityEngine;

public class ClearColorPiece : ClearablePiece
{
    private ColorPiece.ColorType color;

    public ColorPiece.ColorType Color
    {
        get { return color; }
        set { color = value; }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public override void Clear()
    {
        base.Clear();
        piece.GridRef.ClearColor(color);
    }
}
