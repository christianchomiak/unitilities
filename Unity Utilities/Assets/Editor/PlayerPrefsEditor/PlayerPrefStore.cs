public class PlayerPrefStore {

	public class PrefValue {
		public PlayerPrefsEditor.PrefType type;
		public string stringValue;
		public int intValue;
		public float floatValue;
	}

	public string name;
	public bool isMarkedForDelete;
	public PrefValue value;
	public PrefValue initial;
	
	
	public string StringType {
		get {
			if (this.value.type == PlayerPrefsEditor.PrefType.Float) return "real"; 
			if (this.value.type == PlayerPrefsEditor.PrefType.Int) return "integer"; 
			return "string"; 
		}
	}
	
	public string StringValue {
		get {
			if (this.value.type == PlayerPrefsEditor.PrefType.Float) return this.value.floatValue.ToString(); 
			if (this.value.type == PlayerPrefsEditor.PrefType.Int) return this.value.intValue.ToString (); 
			return this.value.stringValue; 
		}
	}
	
	public bool Changed {
		get {
			if (initial.type != value.type) return true;
			switch (value.type) {
				case PlayerPrefsEditor.PrefType.Float:
					if (value.floatValue != initial.floatValue) return true;
					break;
				case PlayerPrefsEditor.PrefType.Int:
					if (value.intValue != initial.intValue) return true;
					break;
				case PlayerPrefsEditor.PrefType.String:
					if (value.stringValue != initial.stringValue) return true;
					break;
			}
			return false;
		}
	}

	public PlayerPrefStore(string name, string prefType, string valueTxt) {
		this.name = name;
		value = new PrefValue();
		initial = new PrefValue();
		switch (prefType) {
			case "integer":
				value.intValue = initial.intValue = int.Parse(valueTxt);
				value.type = initial.type = PlayerPrefsEditor.PrefType.Int;
				break;
			case "real":
				value.floatValue = initial.floatValue = float.Parse(valueTxt);
				value.type = initial.type = PlayerPrefsEditor.PrefType.Float;
				break;
			case "string":
				value.stringValue = initial.stringValue = valueTxt;
				value.type = initial.type = PlayerPrefsEditor.PrefType.String;
				break;
		}
	}

	public void Reset() {
		value.intValue = initial.intValue;
		value.stringValue= initial.stringValue;
		value.floatValue = initial.floatValue;
		value.type = initial.type;
	}

	public void Save() {
		initial.intValue = value.intValue;
		initial.stringValue = value.stringValue;
		initial.floatValue = value.floatValue;
		initial.type = value.type;
	}
}