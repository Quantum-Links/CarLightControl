using UnityEngine;

public class filetest : MonoBehaviour
{
	private string FileType;
	private void Start()
	{
		FileType = NativeFilePicker.ConvertExtensionToFileType("json");
	}
	public void TestFile()
	{
		NativeFilePicker.Permission permission = NativeFilePicker.PickFile((path) =>
		{
			if (path == null)
				Debug.Log("Operation cancelled");
			else
				Debug.Log("Picked file: " + path);
		}, new string[] { FileType });

		Debug.Log("Permission result: " + permission);

	}
}
