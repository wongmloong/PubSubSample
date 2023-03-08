using PubSubSample.Publishers;

namespace PubSubSample
{
	public class Program
	{
		static void Main(string[] args)
		{
			var publisher = new Publisher();
			publisher.LoadData("subscribers.txt", "promotion.txt");
			publisher.Notify();
			publisher.RemoveSubscribers();
		}
	}
}