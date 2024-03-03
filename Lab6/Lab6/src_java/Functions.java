import org.json.JSONArray;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.stream.Collectors;

public class Functions {
    public static InputStream sendQuery(String temp_url) throws IOException {
        URL url = new URL(temp_url);
        System.out.println("Wysyłanie zapytania...");
        InputStream is = url.openStream();
        return is;
    }

    public static JSONArray downloadResponse(InputStream is, String jsonGet){
        String source = new BufferedReader(new
                InputStreamReader(is))
                .lines().collect(Collectors.joining("\n"));
        System.out.println("Przetwarzanie danych...");
        JSONObject json = new JSONObject(source);
        JSONArray recieveddata = (JSONArray) json.get(jsonGet);
        return  recieveddata;
    }
}
