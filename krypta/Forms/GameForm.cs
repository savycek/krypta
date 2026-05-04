using krypta.Graphics;
using krypta.Models;

namespace krypta.Forms;

public sealed partial class GameForm : Form
{
    private Player _player;
    private System.Windows.Forms.Timer? _gameLoop;
    private DateTime _lastFrameTime;
    private Level _currentLevel;
    
    private bool _moveUp;
    private bool _moveDown;
    private bool _moveLeft;
    private bool _moveRight;
    
    public GameForm()
    {
        InitializeComponent();
        
        int[,] mapData = {
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
            { 1, 0, 0, 0, 0, 0, 1, 0, 0, 1 },
            { 1, 0, 1, 1, 1, 0, 1, 0, 1, 1 },
            { 1, 0, 0, 0, 1, 0, 0, 0, 0, 1 },
            { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
        };
        _currentLevel = new Level(mapData);
        
        this.DoubleBuffered = true; // prevents flickering
        
        _player = new Player(64, 64);
        
        this.KeyPreview = true;
        this.KeyDown += GameForm_KeyDown;
        this.KeyUp += GameForm_KeyUp;
        
        InitializeGameLoop();
    }
    
    private void InitializeGameLoop()
    {
        _lastFrameTime = DateTime.Now;
        
        _gameLoop = new System.Windows.Forms.Timer();
        _gameLoop.Interval = 16; // ~60 FPS (1000ms / 60)
        _gameLoop.Tick += GameLoop_Tick;
        _gameLoop.Start();
    }
    
    private void GameLoop_Tick(object? sender, EventArgs e)
    {
        DateTime currentFrameTime = DateTime.Now;
        float deltaTime = (float)(currentFrameTime - _lastFrameTime).TotalSeconds;
        _lastFrameTime = currentFrameTime;
        
        UpdateGame(deltaTime);
        this.Invalidate();
    }
    
    private void UpdateGame(float time)
    {
        float dirX = 0;
        float dirY = 0;
        
        if (_moveUp) dirY -= 1;
        if (_moveDown) dirY += 1;
        if (_moveLeft) dirX -= 1;
        if (_moveRight) dirX += 1;
        
        // diagonal movement
        if (dirX != 0 && dirY != 0)
        {
            float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
            dirX /= length;
            dirY /= length;
        }
        
        float moveX = dirX * _player.Speed * time;
        float moveY = dirY * _player.Speed * time;

        float nextX = _player.X + moveX;
        float nextY = _player.Y + moveY;
        
        if (_currentLevel.IsWalkable(nextX, _player.Y, 40, 40)) 
            _player.X = nextX;
        
        if (_currentLevel.IsWalkable(_player.X, nextY, 40, 40)) 
            _player.Y = nextY;
    }

    private void GameForm_KeyDown(object? sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.W: _moveUp = true; break;
            case Keys.S: _moveDown = true; break;
            case Keys.A: _moveLeft = true; break;
            case Keys.D: _moveRight = true; break;
        }
    }
    
    private void GameForm_KeyUp(object? sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.W: _moveUp = false; break;
            case Keys.S: _moveDown = false; break;
            case Keys.A: _moveLeft = false; break;
            case Keys.D: _moveRight = false; break;
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        TileRenderer.Render(e.Graphics, _currentLevel);
        e.Graphics.FillEllipse(Brushes.Gold, _player.X, _player.Y, 40, 40);
    }
}