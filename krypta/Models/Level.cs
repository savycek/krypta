namespace krypta.Models
{
    public class Level
    {
        public TileType[,] Grid { get; private set; }
        public int TileSize { get; } = 64;

        public Level(int[,] layout)
        {
            int height = layout.GetLength(0);
            int width = layout.GetLength(1);
            Grid = new TileType[height, width];

            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                Grid[y, x] = (TileType)layout[y, x];
        }
        
        public bool IsWalkable(float x, float y, float width, float height)
        {
            return IsTileWalkable(x, y) && 
                   IsTileWalkable(x + width, y) && 
                   IsTileWalkable(x, y + height) && 
                   IsTileWalkable(x + width, y + height);
        }
        private bool IsTileWalkable(float worldX, float worldY)
        {
            int gridX = (int)(worldX / TileSize);
            int gridY = (int)(worldY / TileSize);

            if (gridX < 0 || gridY < 0 || gridY >= Grid.GetLength(0) || gridX >= Grid.GetLength(1))
                return false;

            return Grid[gridY, gridX] == TileType.Floor;
        }
    }
}