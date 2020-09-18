using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Data {
    
    [Serializable]
    public class LevelDescription {

        [XmlAttribute("Name")] public string name;
        [XmlAttribute("Duration")] public double duration;

        [XmlElement("Scene")]
        public string scene;

        [XmlElement("EnemyDescription")]
        public EnemyDescription[] enemies;
    }

    [Serializable]
    public class EnemyDescription {
        [XmlElement("SpawnDate")]
        public float spawnDate;

        [XmlElement("SpawnPosition")]
        public Vector2 spawnPosition;

        [XmlElement("PrefabPath")]
        public string prefabPath;
    }
}