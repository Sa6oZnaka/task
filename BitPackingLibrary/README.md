# Device Status Bit-Packing Library

## Problem Description
You are tasked with developing a compact data transmission protocol for a fleet of Internet of Things (IoT) devices. Each device's comprehensive status must be transmitted as a single, minimum-sized **32-bit unsigned integer** (`uint`) to conserve bandwidth.

You must implement a utility library that can:
1.  **Pack** a human-readable `DeviceStatus` struct into a single 32-bit integer.
2.  **Unpack** the received integer back into the struct.

### Data Fields & Specifications
The 32 bits must be allocated to the following status fields. Note that the fields are **non-contiguous** and must be placed in a specific order within the 32-bit word.

| Field | Bits | Description | Range / Notes |
| :--- | :--- | :--- | :--- |
| **Firmware Version** | 6 | Versions V0.0 to V63.0 | Value 0 - 63 |
| **Battery Level** | 7 | Battery Percentage | 0 - 100 |
| **Online Status** | 1 | Connectivity State | 0 = Offline, 1 = Online |
| **Error Code** | 6 | Device Error State | 0 - 63 (See `DeviceError` enum) |
| **Temperature** | 12 | Ambient Temp (Celsius) | -2048°C to 2047°C (Signed) |

---

## Implementation Requirements

### 1. Data Structures
You must use the following definitions for the implementation.

```csharp
public enum DeviceError : byte
{
    None = 0,
    SensorFailure = 1,
    OverHeat = 2,
    ConnectionLost = 3,
    LowBattery = 4,
    MemoryFull = 5,
    FirmwareUpdateFailed = 6,
    CalibrationRequired = 7,
    TamperAlert = 8,
    MotorStall = 9,
    PowerFluctuation = 10,
    InvalidConfiguration = 11,
    HighHumidity = 12,
    LowAmbientLight = 13,
    HighAmbientLight = 14,
    StorageCorruption = 15,
    WatchdogReset = 16,
    AntennaMismatch = 17,
    AcousticAnomaly = 18,
    VibrationThresholdExceeded = 19,
    PressureSensorFault = 20,
    ActuatorFailure = 21,
    GeographicFenceBreach = 22,
    InternalClockError = 23,
    PeripheralNotResponding = 24,
    FutureUse = 63 
}

public struct DeviceStatus
{
    public byte FirmwareVersion; // 0-63
    public byte BatteryLevel;    // 0-100
    public bool IsOnline;        // true/false
    public DeviceError Error;    // Enum value
    public short Temperature;    // -2048 to 2047
}
```

### 2. Packing Structure (Bit Layout)
The 32-bit `uint` is structured with the **Least Significant Bit (LSB)** at position 0 and the **Most Significant Bit (MSB)** at position 31:

| Field | Bit Count | Bit Range (LSB to MSB) |
| :--- | :---: | :---: |
| Firmware Version | 6 | 0 - 5 |
| Battery Level | 7 | 6 - 12 |
| Online Status | 1 | 13 |
| Error Code | 6 | 14 - 19 |
| Temperature | 12 | 20 - 31 |

**Total bits used:** 6 + 7 + 1 + 6 + 12 = 32.

### 3. Methods to Implement

```csharp
public static class DeviceStatusPacker
{
    public static uint Pack(DeviceStatus status)
    {
        // TODO: Implement logic
    }

    public static DeviceStatus Unpack(uint packed)
    {
        // TODO: Implement logic
    }
}
```

---

## Constraints & Scoring Criteria

1.  **Bitwise Operations Only:** You **MUST** use bitwise shift (`<<`, `>>`), bitwise OR (`|`), and bitwise AND (`&`) operations. The use of helper classes like `BitConverter` or `StructLayout` is **forbidden**.
2.  **Data Validation:** The `Pack` method must validate all input data ranges from the `DeviceStatus` struct (e.g., Battery Level must be $\le$ 100, Temperature must be within the 12-bit signed range). If any value is out of the allowed range, throw an `ArgumentOutOfRangeException`.
3.  **Signed Integer Handling:** Special attention must be paid to the **Temperature** field. It is a 12-bit field that uses **Two's Complement** representation for negative numbers. You must correctly preserve the sign during both packing and unpacking (sign extension).