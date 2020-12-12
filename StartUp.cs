namespace Bakery
{
    using Bakery.Core;
    using Bakery.Models.BakedFoods;
    using Bakery.Models.Drinks;
    using Bakery.Models.Tables;

    public class StartUp
    {
        public static void Main(string[] args)
        {

            var engine = new Engine();

            engine.Run();
        }
    }
}
