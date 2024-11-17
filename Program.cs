using HtmlAgilityPack;

namespace domainfo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("You must provide a domain name");
                Console.WriteLine("Usage: domainfo [DOMAIN]");
                return;
            }

            string[] dataNames = {"Domain Name", "Creation Date", "Updated Date", "Registrar Registration Expiration Date", "Registrant Organization", "Registrant Country", "Admin Organization", "Admin Country"};
            string url = "https://www.whois.com/whois/";
            string website = args[0];
            string finalUrl = $"{url}{website}";

            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(finalUrl);

            var dataNode = doc.DocumentNode.SelectSingleNode("//*[@class='df-raw']");

            if(dataNode == null )
            {
                Console.WriteLine("This domain has not been registered yet or invalid input");
                return;
            } 

            string rawData = dataNode.InnerText;
            if(!rawData.Contains("DNSSEC"))
            {
                Console.WriteLine("Something is wrong.");
                return;
            }

            string[] rawDataSplit = rawData.Split("DNSSEC");
            string[] infos = rawDataSplit[0].Split("\n");

            for (int i = 0; i < infos.Length; i++) 
            {
                if (dataNames.Contains(infos[i].Split(": ")[0]))
                {
                    Console.WriteLine($"{infos[i].Trim()} ");
                }
            }
            
        }
    }
}
