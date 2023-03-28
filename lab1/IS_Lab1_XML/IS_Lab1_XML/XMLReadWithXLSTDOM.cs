using System.Xml.XPath;
using System.Xml;
using System.Reflection.PortableExecutable;

internal class XMLReadWithXLSTDOM
{
    public static void Read(string filepath)
    {
        XPathDocument document = new XPathDocument(filepath);
        XPathNavigator navigator = document.CreateNavigator();
        XmlNamespaceManager manager = new
        XmlNamespaceManager(navigator.NameTable);
        manager.AddNamespace("x","http://rejestrymedyczne.ezdrowie.gov.pl/rpl/eksport-danych-v1.0");
        XPathExpression query = navigator.Compile("/x:produktyLecznicze/x:produktLeczniczy[@postac='Krem' and @nazwaPowszechnieStosowana='Mometasoni furoas']");
        query.SetContext(manager);
        int count = navigator.Select(query).Count;
        Console.WriteLine("Liczba produktów leczniczych w postaci kremu, których jedyną substancją czynną jest Mometasoni furoas {0}", count );


        XPathExpression query1 = navigator.Compile("/x:produktyLecznicze/x:produktLeczniczy");
        query1.SetContext(manager);
        Dictionary<string, List<string>> produktyLecznicze;
        produktyLecznicze = new Dictionary<string, List<string>>();
        count = 0;
        foreach (XPathNavigator produkt in navigator.Select(query1))
        {

                var postac = produkt.GetAttribute("postac","");
                var sc = produkt.GetAttribute("nazwaPowszechnieStosowana","");

                if (produktyLecznicze.ContainsKey(sc))
                {
                    for (int i = 0; i < produktyLecznicze.Count; i++)
                    {
                        if (produktyLecznicze[sc][i] == postac) break;
                        else produktyLecznicze[sc].Add(postac);
                    }
                }
                else
                {
                    List<string> list = new List<string>();
                    list.Add(postac);
                    produktyLecznicze.Add(sc, list);
                }

        }
        foreach (KeyValuePair<string, List<string>> produkt in produktyLecznicze)
        {
            if (produkt.Value.Count > 1) count++;
        }
        Console.WriteLine("Liczba produktów leczniczych w różnych formach: {0}", count);


        XPathExpression query2 = navigator.Compile("/x:produktyLecznicze/x:produktLeczniczy");
        query2.SetContext(manager);
        Dictionary<string, int> producenci_Krem = new Dictionary<string, int>();
        Dictionary<string, int> producenci_Tabletka = new Dictionary<string, int>();

        foreach (XPathNavigator produkt in navigator.Select(query2))
        {
            var postac = produkt.GetAttribute("postac", "");
            var sc = produkt.GetAttribute("podmiotOdpowiedzialny", "");
            if (postac.Contains("Krem"))
            {
                if (producenci_Krem.ContainsKey(sc))
                {
                    producenci_Krem[sc]++;
                }
                else
                {
                    producenci_Krem.Add(sc, 1);
                }
            }
            if (postac.Contains("Tablet"))
            {
                if (producenci_Tabletka.ContainsKey(sc))
                {
                    producenci_Tabletka[sc]++;
                }
                else
                {
                    producenci_Tabletka.Add(sc, 1);
                }
            }
        }
        var keyOfMaxValueKrem = producenci_Krem.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
        var keyOfMaxValueTabletka = producenci_Tabletka.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

        Console.WriteLine("Najwiekszy producent Kremow: {0}", keyOfMaxValueKrem);
        Console.WriteLine("Najwiekszy producent Tabletek: {0}", keyOfMaxValueTabletka);

        Dictionary<string,int> producenciK = new Dictionary<string, int>();
        XPathExpression query3 = navigator.Compile("/x:produktyLecznicze/x:produktLeczniczy[@postac='Krem']");
        query3.SetContext(manager);
        foreach(XPathNavigator produkt in navigator.Select(query3))
        {
            var podmiot = produkt.GetAttribute("podmiotOdpowiedzialny", "");
            if (producenciK.ContainsKey(podmiot))
            {
                producenciK[podmiot]++;
            }
            else
            {
                producenciK.Add(podmiot, 1);
            }
        }
        var sorted = producenciK.OrderByDescending(producent => producent.Value);
        int temp = 0;
        Console.WriteLine("Trzech największych producentów kremów");
        foreach(var producent in sorted)
        {
            if (temp < 3) 
            {
                temp++;
            }
            else 
            { 
                break; 
            }
            Console.WriteLine("{0}",producent);
        }
    }
}