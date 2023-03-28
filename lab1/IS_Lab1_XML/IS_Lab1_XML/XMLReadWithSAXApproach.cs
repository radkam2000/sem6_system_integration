using System.Xml;

internal class XMLReadWithSAXApproach
{
    public static void Read(string filepath)
    {
        // konfiguracja początkowa dla XmlReadera
        XmlReaderSettings settings = new XmlReaderSettings();
        settings.IgnoreComments = true;
        settings.IgnoreProcessingInstructions = true;
        settings.IgnoreWhitespace = true;
        // odczyt zawartości dokumentu
        XmlReader reader = XmlReader.Create(filepath, settings);
        // zmienne pomocnicze
        int count = 0;
        string postac = "";
        string sc = "";
        reader.MoveToContent();
        // analiza każdego z węzłów dokumentu
        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.Element && reader.Name == "produktLeczniczy")
            {
                postac = reader.GetAttribute("postac");
                sc = reader.GetAttribute("nazwaPowszechnieStosowana");
                if (postac == "Krem" && sc == "Mometasoni furoas") count++;
            }
        }
        Console.WriteLine("Liczba produktów leczniczych w postaci kremu, których jedyną substancją czynną jest Mometasoni furoas {0}", count);
        reader.Close();


        Dictionary<string, List<string>> produktyLecznicze;
        produktyLecznicze = new Dictionary<string, List<string>>();
        reader = XmlReader.Create(filepath, settings);
        count = 0;
        reader.MoveToContent();
        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.Element && reader.Name == "produktLeczniczy")

            {
                postac = reader.GetAttribute("postac");
                sc = reader.GetAttribute("nazwaPowszechnieStosowana");
                
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
        }
        foreach (KeyValuePair<string, List<string>> produkt in produktyLecznicze)
        {
            if (produkt.Value.Count > 1) count++;
        }
        Console.WriteLine("Liczba produktów leczniczych w różnych formach: {0}", count);
        reader.Close();


        postac = "";
        sc = "";
        Dictionary<string, int>producenci_Krem = new Dictionary<string, int>();
        Dictionary<string, int> producenci_Tabletka = new Dictionary<string, int>();
        reader = XmlReader.Create(filepath, settings);
        reader.MoveToContent();
        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.Element && reader.Name == "produktLeczniczy")

            {
                postac = reader.GetAttribute("postac");
                sc = reader.GetAttribute("podmiotOdpowiedzialny");

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
        }
        var keyOfMaxValueKrem = producenci_Krem.Aggregate((x, y) => x.Value > y.Value ? x : y).Key; // "a
        var keyOfMaxValueTabletka = producenci_Tabletka.Aggregate((x, y) => x.Value > y.Value ? x : y).Key; // "a

        Console.WriteLine("Najwiekszy producent Kremow: {0}", keyOfMaxValueKrem);
        Console.WriteLine("Najwiekszy producent Tabletek: {0}", keyOfMaxValueTabletka);
        reader.Close();

        postac = "";
        sc = "";
        reader = XmlReader.Create(filepath, settings);
        reader.MoveToContent();
        Dictionary<string, int> producenciK = new Dictionary<string, int>();
        while (reader.Read())
        {
            if (reader.NodeType == XmlNodeType.Element && reader.Name == "produktLeczniczy")
            {
                var podmiot = reader.GetAttribute("podmiotOdpowiedzialny");
                postac = reader.GetAttribute("postac");
                if (postac == "Krem")
                {
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