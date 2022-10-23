using UnityEngine.Timeline;

namespace Mapbox.Examples
{
	using UnityEngine;
	using Mapbox.Utils;
	using Mapbox.Unity.Map;
	using Mapbox.Unity.MeshGeneration.Factories;
	using Mapbox.Unity.Utilities;
	using System.Collections.Generic;

	public class SpawnOnMap : MonoBehaviour
	{
		[SerializeField]
		AbstractMap _map;

		[SerializeField]
		[Geocode]
		string[] _locationStrings;
		Vector2d[] _locations;

		[SerializeField]
		float _spawnScale = 100f;

		[SerializeField]
		GameObject _markerPrefab;

		List<GameObject> _spawnedObjects;
		
		public Dictionary<Vector2d, string> markers = new Dictionary<Vector2d, string>()
		{
			{new Vector2d(42.276743, -83.740304), "Angell Hall"}, 
			{new Vector2d(42.291609, -83.715868), "The Dude"}, 
			{new Vector2d(42.292728, -83.716728), "The BBB"}, 
			{new Vector2d(42.276062, -83.737242), "The UGLI"}, 
			{new Vector2d(42.280536, -83.738271), "Rackham"}, 
			{new Vector2d(42.292391, -83.714851), "EECS Building"}, 
			{new Vector2d(42.291708, -83.717068), "STAMPS Auditorium"}, 
			{new Vector2d(42.276357, -83.737747), "Hatcher Graduate Library"}, 
			{new Vector2d(42.275541, -83.740404), "Orion Statue - UMMA"},
			{new Vector2d(42.294468, -83.709906), "Ford Robotics Building"}
		};
		
		void Start()
		{
			_locations = new Vector2d[_locationStrings.Length];
			_spawnedObjects = new List<GameObject>();
			for (int i = 0; i < _locationStrings.Length; i++)
			{
				var locationString = _locationStrings[i];
				_locations[i] = Conversions.StringToLatLon(locationString);
				var instance = Instantiate(_markerPrefab);
				instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
				instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
				instance.GetComponent<MarkerInteraction>().SetMarkerName(markers[_locations[i]]);
				// TODO: Don't use Find
				instance.GetComponent<MarkerInteraction>().SetInfoObject(GameObject.Find(markers[_locations[i]]));
				instance.GetComponent<MarkerInteraction>().SetItemUnlock(i + 1);
				GameObject.Find(markers[_locations[i]]).SetActive(false);
				_spawnedObjects.Add(instance);
			}
		}

		private void Update()
		{
			int count = _spawnedObjects.Count;
			for (int i = 0; i < count; i++)
			{
				var spawnedObject = _spawnedObjects[i];
				var location = _locations[i];
				spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
				spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
			}
		}
	}
}