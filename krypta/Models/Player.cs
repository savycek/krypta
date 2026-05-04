namespace krypta.Models;

public class Player
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Speed { get; set; } = 500f;

    public Player(float startX, float startY)
    {
        X = startX;
        Y = startY;
    }

    public void Move(float moveX, float moveY)
    {
        X += moveX;
        Y += moveY;
    }
}