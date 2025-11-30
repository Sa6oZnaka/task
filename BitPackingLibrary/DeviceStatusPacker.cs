using BitPackingLibrary.Models;

public static class DeviceStatusPacker
{
    private const int MaxFirmwareVersion = 63;
    private const int MaxBatteryLevel = 100;
    private const int MaxErrorCode = 63;
    private const int MinTemperature = -2048;
    private const int MaxTemperature = 2047;

    public static uint Pack(DeviceStatus status)
    {
        if (status.FirmwareVersion > MaxFirmwareVersion)
            throw new ArgumentOutOfRangeException(nameof(status.FirmwareVersion));
        if (status.BatteryLevel > MaxBatteryLevel)
            throw new ArgumentOutOfRangeException(nameof(status.BatteryLevel));
        if ((int)status.Error < 0 || (int)status.Error > MaxErrorCode)
            throw new ArgumentOutOfRangeException(nameof(status.Error));
        if (status.Temperature < MinTemperature || status.Temperature > MaxTemperature)
            throw new ArgumentOutOfRangeException(nameof(status.Temperature));
        
        uint packed = 0;

        var firmwarePart = (uint)status.FirmwareVersion;
        var batteryPart = (uint)status.BatteryLevel;
        var onlinePart = status.IsOnline ? 1U : 0U;
        var errorPart = (uint)status.Error;
        var tempPart = (uint)((ushort)status.Temperature & 0x0FFF); // 12

        packed = firmwarePart; // 6
        packed |= batteryPart << 6; // 7
        packed |= onlinePart << 13; // 1
        packed |= errorPart << 14; // 6
        packed |= tempPart << 20; // 12

        return packed;
    }

    public static DeviceStatus Unpack(uint packed)
    {
        DeviceStatus status = new DeviceStatus();

        status.FirmwareVersion = (byte)(packed & 63);
        status.BatteryLevel = (byte)((packed >> 6) & 127);
        status.IsOnline = ((packed >> 13) & 1) == 1;
        status.Error = (DeviceError)((packed >> 14) & 63);
        int temp = (int)((packed >> 20) & 0x0FFF);

        if ((temp & 0x800) != 0)
            temp |= unchecked((int)0xFFFFF000);

        status.Temperature = (short)temp;

        return status;
    }
}
