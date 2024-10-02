<Query Kind="Program">
  <NuGetReference>Flurl</NuGetReference>
  <NuGetReference>Flurl.Http</NuGetReference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Flurl.Http</Namespace>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Security.Cryptography.X509Certificates</Namespace>
  <Namespace>System.Net.Security</Namespace>
</Query>

async Task Main()
{
	var idText = File.ReadAllText(Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), "surveyIDs.json"));
	var surveyIDs = JsonConvert.DeserializeObject<List<int>>(idText);
	using (var httpClientHandler = new HttpClientHandler())
	{
		httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
		using (var client = new HttpClient(httpClientHandler))
		{
			var path = "{0}/gettaggedfile";
			client.BaseAddress = new Uri("http://instinct.gongos.local/surveyapi/");
			client.DefaultRequestHeaders.Add("ZenRequestToken", "dXFoYWdzd3FzbDZhZncweWhoeG5mMzFCRjM4NTZBRDM2NEUzNXVxaGFnc3dxcw==");
			foreach (var surveyID in surveyIDs)
			{
				var filepath = Path.Combine(Path.GetDirectoryName(Util.CurrentQueryPath), $@"Exports\{surveyID}.txt");
				if (!File.Exists(filepath))
				{
					var httpResponse = await client.GetAsync(string.Format(path, surveyID));
					var jsonResponse = await httpResponse.Content.ReadAsStringAsync();
					var response = JsonConvert.DeserializeObject<Response>(jsonResponse);
					if (response.Success)
					{
						File.WriteAllBytes(filepath, Convert.FromBase64String(response.Data.ToString()));
						$"Wrote {surveyID}.txt".Dump();
						System.Threading.Thread.Sleep(500);
					}
					else
					{
						$"Error with {surveyID}: {response.Message}".Dump();
					}
				}
			}
		}
	}
}

// You can define other methods, fields, classes and namespaces here
public class Response
{
	public bool Success { get; set; }
	public string Message { get; set; }
	public object Data { get; set; }
}