namespace PubSubSample.Publishers
{
	public class PublisherEventArgs
	{
		public object? Data { get; set; }

		public PublisherEventArgs(object? data)
		{
			this.Data = data;
		}
	}
}
