using UnityEngine; 
using System.Collections; 
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 

public class SaveGame : MonoBehaviour {

	PlayerData _playerData; 
	Rect _Save, _Load, _SaveMSG, _LoadMSG; 
	bool _ShouldSave, _ShouldLoad,_SwitchSave,_SwitchLoad; 
	string _FileLocation,_FileName; 
	public GameObject _Player; 
	string _data; 
	
	void Start () {
		_FileLocation=Application.persistentDataPath;
		_FileName = "SaveData.xml";
		_playerData = new PlayerData(); 
	}
	
	public void saveLevel()
	{	
		_playerData._iUser.position = Manager.playerPos; 
		_playerData._iUser.rotation = Manager.playerRot; 
		_data = SerializeObject(_playerData); 
		CreateXML(); 
	}
	
	public void loadItems()
	{
		LoadXML(); 
		if(_data.ToString() != "") 
		{ 
			_playerData = (PlayerData)DeserializeObject(_data); 
			Manager.playerPos = _playerData._iUser.position;
			Manager.playerRot = _playerData._iUser.rotation; 
		} 
	}
	
   string UTF8ByteArrayToString(byte[] characters) 
   {      
		UTF8Encoding encoding = new UTF8Encoding(); 
		string constructedString = encoding.GetString(characters); 
		return (constructedString); 
   } 
 
   byte[] StringToUTF8ByteArray(string pXmlString) 
   { 
      UTF8Encoding encoding = new UTF8Encoding(); 
      byte[] byteArray = encoding.GetBytes(pXmlString); 
      return byteArray; 
   } 
 
	
   string SerializeObject(object pObject) 
   { 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(typeof(PlayerData)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString; 
   }

	object DeserializeObject(string pXmlizedString) 
   { 
		XmlSerializer xs = new XmlSerializer(typeof(PlayerData)); 
		MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		return xs.Deserialize(memoryStream); 
   } 

   void CreateXML() 
   { 
		StreamWriter writer; 
		FileInfo t = new FileInfo(_FileLocation+"/"+_FileName); 
		if(!t.Exists) 
		{ 
		 writer = t.CreateText(); 
		} 
		else 
		{ 
		 t.Delete(); 
		 writer = t.CreateText(); 
		} 
		writer.Write(_data); 
		writer.Close(); 
   }
	
	void LoadXML() 
	{ 		
		_FileName = "SaveData.xml";
		_FileLocation=Application.persistentDataPath;
		if(File.Exists(_FileLocation + "/" + _FileName)){
			StreamReader r = File.OpenText(_FileLocation + "/" + _FileName);
			string _info = r.ReadToEnd(); 
			r.Close(); 
			_data=_info; 
			Debug.Log("File Read");
		} else {
			_data = "";
		}
		
	}
	
	public class PlayerData{ 
			
		public Data _iUser; 
		
		public PlayerData() { } 
		
		public struct Data 
		{ 
			public Vector3 position;
			public Quaternion rotation;
		} 
	}
	
}

