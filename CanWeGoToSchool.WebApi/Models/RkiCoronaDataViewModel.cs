
using System.Collections.Generic;

namespace CanWeGoToSchool.WebApi.Services
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class UniqueIdField
    {
        public string name { get; set; }
        public bool isSystemMaintained { get; set; }
    }

    public class SpatialReference
    {
        public int wkid { get; set; }
        public int latestWkid { get; set; }
    }

    public class Field
    {
        public string name { get; set; }
        public string type { get; set; }
        public string alias { get; set; }
        public string sqlType { get; set; }
        public int length { get; set; }
        public object domain { get; set; }
        public object defaultValue { get; set; }
    }

    public class Attributes
    {
        public string GEN { get; set; }
        public string BL { get; set; }
        public string BL_ID { get; set; }
        public string county { get; set; }
        public string last_update { get; set; }
        public double cases7_per_100k { get; set; }
        public double cases7_bl_per_100k { get; set; }
        public string cases7_per_100k_txt { get; set; }
        public string BEZ { get; set; }
    }

    public class Feature
    {
        public Attributes attributes { get; set; }
    }

    public class Root
    {
        public string objectIdFieldName { get; set; }
        public UniqueIdField uniqueIdField { get; set; }
        public string globalIdFieldName { get; set; }
        public string geometryType { get; set; }
        public SpatialReference spatialReference { get; set; }
        public List<Field> fields { get; set; }
        public List<Feature> features { get; set; }
    }

 }