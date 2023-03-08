using PubSubSample.Subscribers;
using static PubSubSample.Publishers.Publisher;

namespace PubSubSample.Publishers
{
	public interface IPublisher
	{
		event NotifySubHandler? NotifySubscriber;

		void LoadPromotionItems(string filePromotionItems);

		void LoadSubscribers(string fileSubscribers);

		void LoadData(string fileSubscribers, string filePromotionItems);

		void AddSubscriber(int key, ISubscriber subscriber);

		void RemoveSubscriber(int key);

		void RemoveSubscribers();

		bool Notify();
	}
}
