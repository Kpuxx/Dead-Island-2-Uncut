using System;
using System.IO;
using System.Text;

// Define paths
var exePath = @"C:\Program Files (x86)\Steam\steamapps\common\Dead Island 2\DeadIsland\Binaries\Win64\DeadIsland-Win64-Shipping.exe";
var backupPath = @"C:\Program Files (x86)\Steam\steamapps\common\Dead Island 2\DeadIsland\Binaries\Win64\DeadIsland-Win64-Shipping-backup.exe";

if (File.Exists(exePath))
{
    // Create a Backup of the original executable
    if (!File.Exists(backupPath))
    {
        File.Copy(exePath, backupPath);
    } 

    Console.WriteLine("Creating Backup: done");

    // Search pattern for the Uncut String
    var searchBytes = Encoding.ASCII.GetBytes("{E73AE21F-9D45-4EC0-A87B-E43C95AADA74}");
    var replaceBytes = Encoding.ASCII.GetBytes("{6F25E1B9-A759-40CC-84E9-8BD415305942}");

    // Read the content of the executable
    var exeContent = File.ReadAllBytes(exePath);

    // Find the index of the search pattern
    var index = FindByteArrayIndex(exeContent, searchBytes);

    if (index == -1)
    {
        Console.WriteLine("Pattern not found in the executable.");
        Console.ReadLine();
        Environment.Exit(1);
    }

    // Replace the bytes and activate the uncut
    try
    {
        Array.Copy(replaceBytes, 0, exeContent, index, replaceBytes.Length);

        // Write the modified content back to the executable
        File.WriteAllBytes(exePath, exeContent);

        Console.WriteLine("Executable modified successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
        Console.ReadLine();
        throw;
    }
}
else
{
    Console.WriteLine("Executable not found.");
}

Console.ReadLine();

static int FindByteArrayIndex(byte[] array, byte[] pattern)
{
    for (int i = 0; i <= array.Length - pattern.Length; i++)
    {
        bool found = true;
        for (int j = 0; j < pattern.Length; j++)
        {
            if (array[i + j] != pattern[j])
            {
                found = false;
                break;
            }
        }
        if (found)
        {
            return i;
        }
    }
    return -1;
}
