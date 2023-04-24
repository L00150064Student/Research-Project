
namespace v4MobileApp.Models;
public class SensorData
{
    public DateTime created_at { get; set; }
    public string sensorName { get; set; }
    public float field1 { get; set; }
    public float field2 { get; set; }
    public float field3 { get; set; }
    public float field4 { get; set; }
    public float field5 { get; set; }
    public float field6 { get; set; }
    public float field7 { get; set; }
    public float field8 { get; set; }

    public SensorData() // default constructor
    {
        this.created_at = DateTime.MinValue;
        this.sensorName = "";
        this.field1 = 0;
        this.field2 = 0;
        this.field3 = 0;
        this.field4 = 0;
        this.field5 = 0;
        this.field6 = 0;
        this.field7 = 0;
        this.field8 = 0;

    }

    public SensorData(DateTime created_at, string sensorName, float field1, float field2, float field3, float field4, float field5, float field6, float field7, float field8)
    {
        this.created_at = created_at;
        this.sensorName = sensorName;
        this.field1 = field1;
        this.field2 = field2;
        this.field3 = field3;
        this.field4 = field4;
        this.field5 = field5;
        this.field6 = field6;
        this.field7 = field7;
        this.field7 = field8;
    }
}