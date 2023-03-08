using PubSubSample.Models;
using PubSubSample.Subscribers;

namespace PubSubSample.Publishers
{
	public class Publisher : IPublisher
	{
		public delegate void NotifySubHandler(object sender, PublisherEventArgs e);

		public event NotifySubHandler? NotifySubscriber;

		public Dictionary<int, ISubscriber> Subscribers { get; set; } = new Dictionary<int, ISubscriber>();
		public List<PromotionItem> PromotionItems { get; set; } = new List<PromotionItem>();

		public void LoadPromotionItems(string filePromotionItems)
		{
			if (string.IsNullOrEmpty(filePromotionItems) || !File.Exists(filePromotionItems))
				throw new ArgumentException("Promotion file not found");

			var items = File.ReadAllLines(filePromotionItems);
			if (items != null && items.Any())
			{
				int line = 0;
				foreach (var item in items)
				{
					line++;
					if (line == 1)
						continue;

					var itemDetails = item.Split(',');
					if (itemDetails.Length == 4)
					{
						decimal sellingPrice = 0,
							discountPerc = 0,
							actualSellingPrice = 0;

						if (decimal.TryParse(itemDetails[1], out sellingPrice) &&
							decimal.TryParse(itemDetails[2], out discountPerc) &&
							decimal.TryParse(itemDetails[3], out actualSellingPrice))
							PromotionItems.Add(new PromotionItem()
							{
								Description = itemDetails[0],
								SellingPrice = sellingPrice,
								DiscountPercentage = discountPerc,
								ActualSellingPrice = actualSellingPrice,
							});
						else
							throw new Exception("Invalid promotion data");
					}
				}
			}
		}

		public void LoadSubscribers(string fileSubscribers)
		{
			if (string.IsNullOrEmpty(fileSubscribers) || !File.Exists(fileSubscribers))
				throw new ArgumentException("Subscriber file not found");

			int id = 0;
			var subscriberNames = File.ReadAllLines(fileSubscribers);
			if (subscriberNames.Any())
				foreach (var name in subscriberNames)
					AddSubscriber(++id, new Subscriber(name));
		}

		public void LoadData(string fileSubscribers, string filePromotionItems)
		{
			LoadSubscribers(fileSubscribers);
			LoadPromotionItems(filePromotionItems);
		}

		public void AddSubscriber(int key, ISubscriber subscriber)
		{
			if (subscriber == null)
				throw new NullReferenceException();

			Subscribers.Add(key, subscriber);
			this.NotifySubscriber += subscriber.ShowPromotion;
		}

		public void RemoveSubscriber(int key)
		{
			if (Subscribers.ContainsKey(key))
			{
				this.NotifySubscriber -= Subscribers[key].ShowPromotion;
				Subscribers.Remove(key);
			}
			else
				throw new Exception("Subscriber key not found");
		}

		public void RemoveSubscribers()
		{
			if (Subscribers.Any())
				foreach (var subscriber in Subscribers.ToList())
					RemoveSubscriber(subscriber.Key);
		}

		public bool Notify()
		{
			if (PromotionItems.Any())
			{
				string promotion = string.Empty;

				foreach (var promotionItem in PromotionItems)
				{
					promotion += $"Item: {promotionItem.Description} \r\nSelling Price: {promotionItem.SellingPrice.ToString("N2")}" +
						$"Discount (%): {promotionItem.DiscountPercentage.ToString("N2")} \r\nActual Selling Price: {promotionItem.SellingPrice.ToString("N2")}\r\n\r\n";
				}

				if (NotifySubscriber != null)
				{
					NotifySubscriber(this, new PublisherEventArgs(promotion));
					return true;
				}
			}

			return false;
		}
	}
}
