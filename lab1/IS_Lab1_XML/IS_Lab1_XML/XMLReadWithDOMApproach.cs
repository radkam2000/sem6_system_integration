using System.Reflection.PortableExecutable;
using System.Xml;
using System.Xml.XPath;

internal class XMLReadWithDOMApproach
{
public static void Read(string filepath)
    {
        // odczyt zawartości dokumentu
        XmlDocument doc = new XmlDocument();
        doc.Load(filepath);
        string postac;
        string sc;
        int count = 0;
        var drugs = doc.GetElementsByTagName("produktLeczniczy");

        foreach (XmlNode d in drugs)
        {
            postac = d.Attributes.GetNamedItem("postac").Value;
            sc = d.Attributes.GetNamedItem("nazwaPowszechnieStosowana").Value;
            if (postac == "Krem" && sc == "Mometasoni furoas")
                count++;
        }
        Console.WriteLine("Liczba produktów leczniczych w postaci kremu, których jedyną substancją czynną jest Mometasoni furoas {0}", count);


        Dictionary<string, List<string>> produktyLecznicze;
        produktyLecznicze = new Dictionary<string, List<string>>();
        count = 0;
        foreach (XmlNode d in drugs)
        {
            postac = d.Attributes.GetNamedItem("postac").Value;
            sc = d.Attributes.GetNamedItem("nazwaPowszechnieStosowana").Value;
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


        postac = "";
        sc = "";
        Dictionary<string, int> producenci_Krem = new Dictionary<string, int>();
        Dictionary<string, int> producenci_Tabletka = new Dictionary<string, int>();

        foreach(XmlNode d in drugs)
        {
            postac = d.Attributes.GetNamedItem("postac").Value;
            sc = d.Attributes.GetNamedItem("podmiotOdpowiedzialny").Value;

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
        var keyOfMaxValueKrem = producenci_Krem.Aggregate((x, y) => x.Value > y.Value ? x : y).Key; // "a
        var keyOfMaxValueTabletka = producenci_Tabletka.Aggregate((x, y) => x.Value > y.Value ? x : y).Key; // "a

        Console.WriteLine("Najwiekszy producent Kremow: {0}", keyOfMaxValueKrem);
        Console.WriteLine("Najwiekszy producent Tabletek: {0}", keyOfMaxValueTabletka);

        Dictionary<string, int> producenciK = new Dictionary<string, int>();
        foreach (XmlNode d in drugs)
        {
            var podmiot = d.Attributes.GetNamedItem("podmiotOdpowiedzialny").Value;
            postac = d.Attributes.GetNamedItem("postac").Value;
            if (postac == "Krem") { 
                if (producenciK.ContainsKey(podmiot))
                {
                    producenciK[podmiot]++;
                }
                else
                {
                    producenciK.Add(podmiot, 1);
                }
            }
        }
        var sorted = producenciK.OrderByDescending(producent => producent.Value);
        int temp = 0;
        Console.WriteLine("Trzech największych producentów kremów");
        foreach (var producent in sorted)
        {
            if (temp < 3)
            {
                temp++;
            }
            else
            {
                break;
            }
            Console.WriteLine("{0}", producent);
        }
    }
}