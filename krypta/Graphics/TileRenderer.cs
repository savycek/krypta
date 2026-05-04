using krypta.Models;

namespace krypta.Graphics
{
    public static class TileRenderer
    {
        private static readonly Dictionary<TileType, Image> Textures = new Dictionary<TileType, Image>();

        static TileRenderer()
        {
            try 
            {
                string basePath = AppDomain.CurrentDomain.BaseDirectory;
                Textures[TileType.Floor] = Image.FromFile(Path.Combine(basePath, "Assets", "floor.png"));
                Textures[TileType.Wall] = Image.FromFile(Path.Combine(basePath, "Assets", "wall.png"));
            }
            catch (Exception ex)
            {
                Console.WriteLine("failed to load textures: " + ex.Message);
            }
        }

        public static void Render(System.Drawing.Graphics g, Level level)
        {
            for (int y = 0; y < level.Grid.GetLength(0); y++)
            {
                for (int x = 0; x < level.Grid.GetLength(1); x++)
                {
                    TileType type = level.Grid[y, x];
                    
                    if (Textures.ContainsKey(type))
                    {
                        g.DrawImage(Textures[type], x * level.TileSize, y * level.TileSize, level.TileSize, level.TileSize);
                    }
                    else
                    {
                        // fallback
                        g.FillRectangle(Brushes.Magenta, x * level.TileSize, y * level.TileSize, level.TileSize, level.TileSize);
                    }
                }
            }
        }
    }
}