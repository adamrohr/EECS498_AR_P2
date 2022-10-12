namespace Mapbox.Unity.Location
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.IO;
	using Mapbox.Unity.Utilities;
	using Mapbox.Utils;
	using UnityEngine;

	/// <summary>
	/// <para>
	/// The EditorLocationProvider is responsible for providing mock location data via log file collected with the 'LocationProvider' scene
	/// </para>
	/// </summary>
	public class EditorLocationProviderLocationLog : AbstractEditorLocationProvider
	{


		/// <summary>
		/// The mock "latitude, longitude" location, respresented with a string.
		/// You can search for a place using the embedded "Search" button in the inspector.
		/// This value can be changed at runtime in the inspector.
		/// </summary>
		[SerializeField]
		private TextAsset _locationLogFile;
		private float deltaLatitude = 0.0f;
		private float deltaLongitude = 0.0f;

		public static float playerMovement = 0.0f;

		// private LocationLogReader _logReader;
		// private IEnumerator<Location> _locationEnumerator;


// #if UNITY_EDITOR
// 		protected override void Awake()
// 		{
// 			base.Awake();
// 			_logReader = new LocationLogReader(_locationLogFile.bytes);
// 			_locationEnumerator = _logReader.GetLocations();
// 		}
// #endif

		private void Update() {
			// 1 Degree of Latitude is 69 miles (364,000ft)
			// 1 Degree of Longitude is 54.6 miles (288,200ft)
			if(Input.GetKey(KeyCode.RightArrow)) {
				// Longitude up
				deltaLongitude = 0.000001f;
				SetLocation();
			}
			if(Input.GetKey(KeyCode.LeftArrow)) {
				// Longitude down
				deltaLongitude = -0.000001f;
				SetLocation();
			}
			if(Input.GetKey(KeyCode.UpArrow)) {
				// Latitude up
				deltaLatitude = 0.000001f;
				SetLocation();
			}
			if(Input.GetKey(KeyCode.DownArrow)) {
				// Longitude down
				deltaLatitude = -0.000001f;
				SetLocation();
			}
			// TODO: Determine what cutoff the trees should start growing at based on the movement
			playerMovement += Mathf.Abs(deltaLatitude) + Mathf.Abs(deltaLongitude);
		}

		// private void OnDestroy()
		// {
		// 	if (null != _locationEnumerator)
		// 	{
		// 		_locationEnumerator.Dispose();
		// 		_locationEnumerator = null;
		// 	}
		// 	if (null != _logReader)
		// 	{
		// 		_logReader.Dispose();
		// 		_logReader = null;
		// 	}
		// }


		protected override void SetLocation()
		{
			// if (null == _locationEnumerator) { return; }

			// // no need to check if 'MoveNext()' returns false as LocationLogReader loops through log file
			// _locationEnumerator.MoveNext();
			_currentLocation.LatitudeLongitude += new Vector2d(deltaLatitude, deltaLongitude);
		}
	}
}
