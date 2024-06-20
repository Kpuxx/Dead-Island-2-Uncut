# Dead Island 2 Uncut Gore Version Enabler

This C# console application allows German users to play the uncut gore version of Dead Island 2 by modifying the game's executable. The program creates a backup of the original executable before making any changes to ensure safety.

## Features

- Creates a backup of the original executable.
- Searches for a specific byte pattern in the executable.
- Replaces the found byte pattern with a new pattern to enable the uncut version.
- Provides error handling and user feedback.

## Prerequisites

- .NET SDK installed on your machine.

## Usage

1. Clone this repository or download the source code.

2. Open the solution in your preferred IDE (e.g., Visual Studio, Visual Studio Code).

3. Build the solution to restore dependencies and compile the program.

4. Run the application.

## Instructions

1. Ensure Dead Island 2 is installed on your machine via Steam.
2. Run the compiled application.
3. The program will create a backup of the original executable located at:
   `C:\Program Files (x86)\Steam\steamapps\common\Dead Island 2\DeadIsland\Binaries\Win64\DeadIsland-Win64-Shipping-backup.exe`
4. The program will search for the specific byte pattern in the executable and replace it to enable the uncut version.
5. Upon successful modification, you will see a message indicating the executable has been modified.

## Code

```csharp
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
```

## Disclaimer

This tool is provided as-is without any warranty. Use at your own risk. The author is not responsible for any damage caused by using this tool. Always ensure you have backups of important files before running such tools.