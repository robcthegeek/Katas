namespace Katas.Model
{
    public class Player
    {
        public PlayerType Type { get; set; }
    }

    public enum PlayerType
    {
        White,
        Black
    }
}