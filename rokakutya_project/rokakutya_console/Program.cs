using rokakutya_console.StateRepresentation;

namespace rokakutya_console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FoxCatchingPlayer player = new FoxCatchingPlayer();

            player.Play();
            Console.ReadLine();
        }
    }
}
