<Query Kind="Program">
  <Namespace>System.Xml.Serialization</Namespace>
</Query>

void Main()
{

	var path = Path.Combine(
			Path.GetDirectoryName(Util.CurrentQueryPath),
			"Transactions_2024.xml");
			path.Dump();
	var text = File.ReadAllText(path);
	XmlSerializer serializer = new XmlSerializer(typeof(YahooFantasyContent));
	List<YahooTransaction> transactions;
	using (StringReader reader = new StringReader(text))
	{
	   var data = (YahooFantasyContent)serializer.Deserialize(reader);
	   transactions = data.League.Transactions;
	}
	
	if(transactions is null || transactions.Count == 0)
	{
		return;
	}
	
	var faabTransactions = transactions
		.Where(t => (t.Type == "add/drop" || t.Type == "add") && t.Status == "successful")
		.Select(t => CreateTransaction(t));
		
		faabTransactions.Where(t => t.Bids.Any(b => b.BidPrice > 0)).OrderByDescending(t => t.Bids.FirstOrDefault().BidPrice).Dump();
}

public FaabTransaction CreateTransaction(YahooTransaction transaction)
{	
	var playerAdded = transaction.Players.FirstOrDefault(p => p.TransactionData.Type == "add");
	if (playerAdded is null)
	{
		return null;
	}
	
	return new FaabTransaction
	{			
		PlayerName = playerAdded.Name.Full,
		Date = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(transaction.Timestamp).ToString("MM/dd/yy"),
		Bids = new List<FaabBid>
		{
			new FaabBid
			{
				BidPrice = transaction.FaabBid,
				TeamName = playerAdded.TransactionData.DestinationTeamName,
				Winner = true
			},
		}
	};
}

public class FaabTransaction
{
	public string PlayerName { get; set; }
	public string Date { get; set; }
	public List<FaabBid> Bids { get; set; }
}

public class FaabBid
{
	public int BidPrice { get; set; }
	public string TeamName { get; set; }
	public bool Winner { get; set; }
}

public class YahooName
{

	[XmlElement(ElementName = "full")]
	public string Full { get; set; }

	[XmlElement(ElementName = "first")]
	public string First { get; set; }

	[XmlElement(ElementName = "last")]
	public string Last { get; set; }
}

public class YahooTransactionData
{

	[XmlElement(ElementName = "type")]
	public string Type { get; set; }

	[XmlElement(ElementName = "source_type")]
	public string SourceType { get; set; }

	[XmlElement(ElementName = "destination_type")]
	public string DestinationType { get; set; }

	[XmlElement(ElementName = "destination_team_key")]
	public string DestinationTeamKey { get; set; }

	[XmlElement(ElementName = "destination_team_name")]
	public string DestinationTeamName { get; set; }

	[XmlElement(ElementName = "source_team_key")]
	public string SourceTeamKey { get; set; }

	[XmlElement(ElementName = "source_team_name")]
	public string SourceTeamName { get; set; }
}

public class YahooPlayer
{

	[XmlElement(ElementName = "player_key")]
	public string PlayerKey { get; set; }

	[XmlElement(ElementName = "player_id")]
	public int PlayerId { get; set; }

	[XmlElement(ElementName = "name")]
	public YahooName Name { get; set; }

	[XmlElement(ElementName = "display_position")]
	public string DisplayPosition { get; set; }

	[XmlElement(ElementName = "position_type")]
	public string PositionType { get; set; }

	[XmlElement(ElementName = "transaction_data")]
	public YahooTransactionData TransactionData { get; set; }
}

public class YahooTransaction
{

	[XmlElement(ElementName = "transaction_key")]
	public string TransactionKey { get; set; }

	[XmlElement(ElementName = "transaction_id")]
	public int TransactionId { get; set; }

	[XmlElement(ElementName = "type")]
	public string Type { get; set; }

	[XmlElement(ElementName = "status")]
	public string Status { get; set; }

	[XmlElement(ElementName = "timestamp")]
	public int Timestamp { get; set; }

	[XmlArray("players")]
	[XmlArrayItem("player", typeof(YahooPlayer))]
	public List<YahooPlayer> Players { get; set; }

	[XmlElement(ElementName = "faab_bid")]
	public int FaabBid { get; set; }
}

public class YahooLeague
{

	[XmlElement(ElementName = "league_key")]
	public string LeagueKey { get; set; }

	[XmlElement(ElementName = "league_id")]
	public int LeagueId { get; set; }

	[XmlElement(ElementName = "name")]
	public string Name { get; set; }

	[XmlElement(ElementName = "url")]
	public string Url { get; set; }

	[XmlElement(ElementName = "num_teams")]
	public int NumTeams { get; set; }

	[XmlElement(ElementName = "league_type")]
	public string LeagueType { get; set; }

	[XmlElement(ElementName = "renew")]
	public string Renew { get; set; }

	[XmlElement(ElementName = "renewed")]
	public object Renewed { get; set; }

	[XmlElement(ElementName = "season")]
	public int Season { get; set; }

	[XmlArray("transactions")]
	[XmlArrayItem("transaction", typeof(YahooTransaction))]
	public List<YahooTransaction> Transactions { get; set; }
}

[XmlRoot("fantasy_content")]
public class YahooFantasyContent
{

	[XmlElement(ElementName = "league")]
	public YahooLeague League { get; set; }
}

