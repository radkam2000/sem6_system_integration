import org.json.JSONArray;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.URL;
import java.util.Objects;
import java.util.stream.Collectors;

public class Main {
    public static void main(String[] args) {
        try {
//Test działania lokalnego REST API
            String temp_url = "http://localhost/IS_LAB6_REST/cities/read/";
            InputStream is = Functions.sendQuery(temp_url);
            JSONArray res = Functions.downloadResponse(is,"cities");
            Front.show(res);
        } catch (Exception e) {
            System.err.println("Wystąpił nieoczekiwany błąd!!! ");
                e.printStackTrace(System.err);
        }
    }
}