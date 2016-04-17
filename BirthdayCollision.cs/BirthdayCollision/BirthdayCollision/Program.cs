namespace BirthdayCollision
{
    class Program
    {
        static void Main(string[] args)
        {
            CollisionDetection collisionDetection = new CollisionDetection();
            for (int i = 0; i < 100; i++)
            {
                var result = collisionDetection.PercentTile(100000, i);
                if (result > 0.50)
                {
                    continue;
                }

                // ~85, 0.99999
                // ~41, 0.90
                // ~37, 0.85
                // ~35, 0.80
                // ~32, 0.75
                // ~23, 0.50
            }
        }
    }
}
