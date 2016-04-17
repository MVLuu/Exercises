using Microsoft.VisualStudio.TestTools.UnitTesting;
using BirthdayCollision;

namespace TestBirthdayCollision
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void BasicFunctional()
        {
            CollisionDetection collisionDetection = new CollisionDetection();
            int collisionDetectedAt = 0;

            for (int i = 0; i < 100; i++)
            {
                var result = collisionDetection.PercentTile(10000, i);

                if (result > 0.50)
                {
                    collisionDetectedAt = i;
                    break;
                }
            }

            if(22 >= collisionDetectedAt || collisionDetectedAt >= 24)
            {
                Assert.Fail("Result is outside expected range.");
            }
        }

        [TestMethod]
        public void Maximum()
        {
            CollisionDetection collisionDetection = new CollisionDetection();
            int collisionDetectedAt = 0;

            for (int i = 0; i < 100; i++)
            {
                var result = collisionDetection.PercentTile(10000, i);

                if (result >= 1)
                {
                    collisionDetectedAt = i;
                    break;
                }
            }

            int expectedValue = 79;
            if (collisionDetectedAt <= expectedValue)
            {
                Assert.Fail(string.Format("Result {0} is outside expected range {1}.", collisionDetectedAt, expectedValue));
            }
        }

        [TestMethod]
        public void Minimum()
        {
            CollisionDetection collisionDetection = new CollisionDetection();
            int collisionDetectedAt = 0;

            for (int i = 0; i < 100; i++)
            {
                var result = collisionDetection.PercentTile(10000, i);

                if (result >= 0.01)
                {
                    collisionDetectedAt = i;
                    break;
                }
            }

            if (collisionDetectedAt <= 0.02)
            {
                Assert.Fail("Result is outside expected range.");
            }
        }
    }
}
