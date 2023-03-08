using PubSubSample.Publishers;

namespace PubSubSample.Subscribers
{
	public interface ISubscriber
	{
		void ShowPromotion(object sender, PublisherEventArgs args);
	}
}
