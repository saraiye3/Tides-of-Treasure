using UnityEngine;

public class GamePiece : MonoBehaviour
{
    public int score;

    private int x;
    private int y;

    public int X
    {
        get { return x; }
        set
        {
            if (IsMovable()) { x = value; }
        }
    }

    public int Y
    {
        get { return y; }
        set
        {
            if (IsMovable()) { y = value; }
        }
    }

    private Grid.PieceType type;

    public Grid.PieceType Type
    {
        get { return type; }
    }

    private Grid grid;

    public Grid GridRef
    {
        get { return grid; }
    }

    private MovablePiece movableComponent;

    public MovablePiece MovableComponent
    {
        get { return movableComponent; }
    }

    private ColorPiece colorComponent;

    public ColorPiece ColorComponent
    {
        get { return colorComponent; }
    }

    //referance to clearablepiece
    private ClearablePiece clearableComponent;

    public ClearablePiece ClearableComponent
    {
        get { return clearableComponent; }
    }

    void Awake()
    {
        movableComponent = GetComponent<MovablePiece>();
        colorComponent = GetComponent<ColorPiece>();
        clearableComponent = GetComponent<ClearablePiece>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Init(int _x, int _y, Grid _grid, Grid.PieceType _type)
    {
        x = _x;
        y = _y;
        grid = _grid;
        type = _type;
    }

    private void OnMouseEnter() //called when mouse enters (hovers on) an element
    {
        grid.EnterPiece(this);
    }

    private void OnMouseDown() //called when mouse is pressed inside an element
    {
        grid.PressPiece(this);
    }

    private void OnMouseUp() //called when mouse is released
    {
        grid.ReleasePiece();
    }

    public bool IsMovable()
    {
        return movableComponent != null;
    }

    public bool IsColored()
    {
        return colorComponent != null;
    }

    public bool IsClearable()
    {
        return clearableComponent != null;
    }
}

