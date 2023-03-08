using PubSubSample.Publishers;
using PubSubSample.Subscribers;

namespace PubSubSampleTests
{
	[TestFixture]
	public class PublisherTest
	{
		private IPublisher Publisher { get; set; }
		private string FileSubscribers { get; set; }
		private string FilePromotionItems { get; set; }

		[SetUp]
		public void Setup()
		{
			Publisher = new Publisher();
			FileSubscribers = "subscribers.txt";
			FilePromotionItems = "promotion.txt";
		}

		[Test]
		public void TestLoadData()
		{
			try
			{
				Publisher.LoadData(FileSubscribers, FilePromotionItems);
				return;
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[Test]
		public void TestLoadSubsribers()
		{
			try
			{
				Publisher.LoadSubscribers(FileSubscribers);
				return;
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[Test]
		public void TestLoadSubsribersThrowsException()
		{
			var ex = Assert.Throws<ArgumentException>(() => { Publisher.LoadSubscribers(""); });
			StringAssert.Contains("Subscriber file not found", ex.Message);
		}

		[Test]
		public void TestLoadPromotionItems()
		{
			try
			{
				Publisher.LoadPromotionItems(FileSubscribers);
				return;
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[Test]
		public void TestLoadPromotionItemsThrowsException()
		{
			var ex = Assert.Throws<ArgumentException>(() => { Publisher.LoadPromotionItems(""); });
			StringAssert.Contains("Promotion file not found", ex.Message);
		}

		[Test]
		public void TestAddSubscriber()
		{
			try
			{
				Publisher.AddSubscriber(1, new Subscriber("Tester"));
				return;
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[Test]
		public void TestNotifySubscribersWithData()
		{
			Publisher.LoadData(FileSubscribers, FilePromotionItems);
			Assert.That(Publisher.Notify(), Is.True);
		}

		[Test]
		public void TestNotifySubscribersWithoutData()
		{
			Assert.That(Publisher.Notify(), Is.False);
		}

		[Test]
		public void TestRemoveSubscriber()
		{
			try
			{
				Publisher.AddSubscriber(1, new Subscriber("Tester"));
				Publisher.RemoveSubscriber(1);
				return;
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[Test]
		public void TestRemoveNonExistSubscriber()
		{
			var ex = Assert.Throws<Exception>(() => { Publisher.RemoveSubscriber(1); });
			StringAssert.Contains("Subscriber key not found", ex.Message.ToString());
		}

		[Test]
		public void TestRemoveSubscribers()
		{
			try
			{
				Publisher.LoadData(FileSubscribers, FilePromotionItems);
				Publisher.RemoveSubscribers();
				return;
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[Test]
		public void TestRemoveSubscribersWithoutData()
		{
			try
			{
				Publisher.RemoveSubscribers();
				return;
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[Test]
		public void TestShowPromotion()
		{
			try
			{
				var subscriber = new Subscriber("Tester");
				subscriber.ShowPromotion(subscriber, new PublisherEventArgs("Hello World"));
				return;
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}

		[Test]
		public void TestShowPromotionWithoutArg()
		{
			var ex = Assert.Throws<NullReferenceException>(() =>
			{
				var subscriber = new Subscriber("Tester");
				subscriber.ShowPromotion(subscriber, new PublisherEventArgs(null));
			});
			StringAssert.Contains("Object reference not set to an instance of an object", ex.Message.ToString());
		}
	}
}