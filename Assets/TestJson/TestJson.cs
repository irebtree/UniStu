using UnityEngine;
using System.Collections;
using LitJson;
using System;

public class Person
{
	// C# 3.0 auto-implemented properties
	public string   Name     { get; set; }
	public int      Age      { get; set; }
	public DateTime Birthday { get; set; }
}

public class TestJson : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//PersonToJson();
		//JsonToPerson();
		LoadAlbumData();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public static void PersonToJson()
	{
		Person bill = new Person();
		
		bill.Name = "William Shakespeare";
		bill.Age  = 51;
		bill.Birthday = new DateTime(1564, 4, 26);
		
		string json_bill = JsonMapper.ToJson(bill);
		
		Debug.Log(json_bill);
	}
	
	public static void JsonToPerson()
	{
		string json = @"
            {
                ""Name""     : ""Thomas More"",
                ""Age""      : 57,
                ""Birthday"" : ""02/07/1478 00:00:00""
            }";

		Debug.Log("*****" + json);
		Person thomas = JsonMapper.ToObject<Person>(json);
		
		Debug.Log("Thomas' age: == " + thomas.Age);
	}

	public static void LoadAlbumData()
	{
		string json = @"
          {
            ""album"" : {
              ""name""   : ""The Dark Side of the Moon"",
              ""artist"" : ""Pink Floyd"",
              ""year""   : 1973,
              ""tracks"" : [
                ""Speak To Me"",
                ""Breathe"",
                ""On The Run""
              ]
            }
          }
        ";


		Debug.Log("11111 = " + json );
		
		JsonData data = JsonMapper.ToObject(json);
		
		// Dictionaries are accessed like a hash-table
		Debug.Log("Album's name: " + data["album"]["name"]);
		
		// Scalar elements stored in a JsonData instance can be cast to
		// their natural types
		string artist = (string) data["album"]["artist"];
		int    year   = (int) data["album"]["year"];
		
		Debug.Log("Recorded by : " + artist + " in : " + year);
		
		// Arrays are accessed like regular lists as well
		Debug.Log("First track: " + data["album"]["tracks"][0]);
	}

}
