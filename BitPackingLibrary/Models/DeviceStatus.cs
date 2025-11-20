namespace BitPackingLibrary.Models;

public struct DeviceStatus
{
    public byte FirmwareVersion { get; set; }
    public byte BatteryLevel { get; set; }
    public bool IsOnline { get; set; }
    public DeviceError Error { get; set; }
    public short Temperature { get; set; }
}
