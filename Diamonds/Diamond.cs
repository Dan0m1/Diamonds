namespace Diamonds
{
    class Diamond
    {
        public int X { get; set; }
        public int Y { get; set; }
        public ConsoleColor Color {  get; set; }
        public int Id { get; set; }
        public Diamond(int x, int y, ConsoleColor color, int id)
        {
            X = x;
            Y = y;
            Color = color;
            Id = id;
        }
    }
}