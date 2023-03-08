using PubSubSample.Publishers;

namespace PubSubSample.Subscribers
{
	public class Subscriber : ISubscriber
	{
		public string Name { get; set; }

        public Subscriber(string name)
        {
			this.Name = name;
        }

        public void ShowPromotion(object sender, PublisherEventArgs args)
		{
			if (args == null || args.Data == null)
				throw new NullReferenceException();

			Console.WriteLine();
			Console.WriteLine("There is a promotion on going.");
			Console.WriteLine(args.Data);
			Console.WriteLine();
		}
	}
}
