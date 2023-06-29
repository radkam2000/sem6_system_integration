import org.json.JSONArray;
import org.json.JSONObject;

public class Front {
    public static void show(JSONArray recieveddata){
        for (Object c: recieveddata) {
            JSONObject o = (JSONObject) c;
            for (String key : o.keySet()) {
                System.out.println(key + ": " + o.get(key));
            }
            System.out.println();
        }
    }
}
