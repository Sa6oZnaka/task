namespace BitPackingLibrary;

public static class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("--- Device Status Packing Tests ---");

        #region Test 1: Standard Packing

        Console.WriteLine("\n[Test 1] Packing Standard Values...");

        DeviceStatus status1 = new()
        {
            FirmwareVersion = 33,
            BatteryLevel = 90,
            IsOnline = false,
            Error = DeviceError.AntennaMismatch,
            Temperature = 1345
        };

        uint packed1 = DeviceStatusPacker.Pack(status1);
        uint expected1 = 1410619041;

        PrintBinary("Your Result", packed1);
        PrintBinary("Expected", expected1);

        Console.WriteLine($"Pass: {packed1 == expected1}");

        #endregion

        #region Test 2: Negative Numbers (Two's Complement)

        Console.WriteLine("\n[Test 2] Packing Negative Temperature...");

        var status2 = new DeviceStatus
        {
            FirmwareVersion = 12,
            BatteryLevel = 33,
            IsOnline = true,
            Error = DeviceError.CoolingSystemInefficiency,
            Temperature = -345
        };

        uint packed2 = DeviceStatusPacker.Pack(status2);
        uint expected2 = 3933988940;

        if (packed2 != expected2)
        {
            PrintBinary("Your Result", packed2);
            PrintBinary("Expected", expected2);
        }

        Console.WriteLine($"Pass: {packed2 == expected2}");

        #endregion

        #region Test 3: Unpacking Max Values

        Console.WriteLine("\n[Test 3] Unpacking Max Values...");

        uint input3 = 2147481919;
        DeviceStatus unpacked3 = DeviceStatusPacker.Unpack(input3);

        bool t3Pass = unpacked3.FirmwareVersion == 63 &&
                      unpacked3.BatteryLevel == 100 &&
                      unpacked3.IsOnline == true &&
                      unpacked3.Error == DeviceError.FutureUse &&
                      unpacked3.Temperature == 2047;

        Console.WriteLine($"Pass: {t3Pass}");

        if (!t3Pass)
        {
            Console.WriteLine($"Got: FW:{unpacked3.FirmwareVersion} Bat:{unpacked3.BatteryLevel} Tmp:{unpacked3.Temperature}");
        }

        #endregion

        #region Test 4: Validation Logic (Bonus)

        Console.WriteLine("\n[Test 4] Testing Validation (Should Throw)...");

        DeviceStatus badStatus = new() { BatteryLevel = 101 };
        try
        {
            DeviceStatusPacker.Pack(badStatus);
            Console.WriteLine("Pass: False (Should have thrown exception)");
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("Pass: True (Exception caught)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Pass: False (Wrong exception type: {ex.GetType().Name})");
        }

        #endregion
    }

    private static void PrintBinary(string label, uint value)
    {
        Console.WriteLine($"{label,-15}: {Convert.ToString(value, 2).PadLeft(32, '0')} ({value})");
    }
}
