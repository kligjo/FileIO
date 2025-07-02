using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace SystemIOExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            // File I/O Examples
            FileIOExamples();

            // Directory Operations
            DirectoryOperations();

            // Stream Examples
            StreamExamples();

            // Path Operations
            PathOperations();
        }

        static void FileIOExamples()
        {
            Console.WriteLine("=== FILE I/O EXAMPLES ===");

            string filePath = "proba.txt";

            // 1. Writing to a file (simple)
            File.WriteAllText(filePath, "Hello, World!\nThis is a test file.");
            Console.WriteLine("File written successfully.");

            // 2. Reading from a file (simple)
            string content = File.ReadAllText(filePath);
            Console.WriteLine($"File content:\n{content}");

            // 3. Appending to a file
            File.AppendAllText(filePath, "\nAppended line.");

            // 4. Writing multiple lines
            string[] lines = { "Line 1", "Line 2", "Line 3" };
            File.WriteAllLines("lines.txt", lines);

            // 5. Reading all lines
            string[] readLines = File.ReadAllLines("lines.txt");
            Console.WriteLine("\nLines from file:");
            foreach (string line in readLines)
            {
                Console.WriteLine($"- {line}");
            }

            // 6. Check if file exists
            if (File.Exists(filePath))
            {
                Console.WriteLine($"File {filePath} exists.");

                // Get file info
                FileInfo fileInfo = new FileInfo(filePath);
                Console.WriteLine($"Size: {fileInfo.Length} bytes");
                Console.WriteLine($"Created: {fileInfo.CreationTime}");
                Console.WriteLine($"Modified: {fileInfo.LastWriteTime}");
            }

            Console.WriteLine();
        }

        static void DirectoryOperations()
        {
            Console.WriteLine("=== DIRECTORY OPERATIONS ===");

            string dirPath = "TestDirectory";

            // 1. Create directory
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
                Console.WriteLine($"Directory '{dirPath}' created.");
            }

            // 2. Create subdirectories
            string subDir = Path.Combine(dirPath, "SubFolder");
            Directory.CreateDirectory(subDir);

            // 3. Create files in directory
            File.WriteAllText(Path.Combine(dirPath, "file1.txt"), "Content 1");
            File.WriteAllText(Path.Combine(dirPath, "file2.txt"), "Content 2");
            File.WriteAllText(Path.Combine(subDir, "subfile.txt"), "Sub content");

            // 4. List files in directory
            Console.WriteLine($"\nFiles in {dirPath}:");
            string[] files = Directory.GetFiles(dirPath);
            foreach (string file in files)
            {
                Console.WriteLine($"- {Path.GetFileName(file)}");
            }

            // 5. List all files recursively
            Console.WriteLine($"\nAll files (recursive):");
            string[] allFiles = Directory.GetFiles(dirPath, "*", SearchOption.AllDirectories);
            foreach (string file in allFiles)
            {
                Console.WriteLine($"- {file}");
            }

            // 6. List directories
            Console.WriteLine($"\nSubdirectories:");
            string[] directories = Directory.GetDirectories(dirPath);
            foreach (string dir in directories)
            {
                Console.WriteLine($"- {Path.GetFileName(dir)}");
            }

            // 7. Get current directory
            Console.WriteLine($"\nCurrent directory: {Directory.GetCurrentDirectory()}");

            Console.WriteLine();
        }

        static void StreamExamples()
        {
            Console.WriteLine("=== STREAM EXAMPLES ===");

            string streamFile = "stream_example.txt";

            // 1. FileStream for binary operations
            using (FileStream fs = new FileStream(streamFile, FileMode.Create))
            {
                byte[] data = Encoding.UTF8.GetBytes("Hello from FileStream!");
                fs.Write(data, 0, data.Length);
            }
            Console.WriteLine("Data written using FileStream.");

            // 2. StreamWriter for text writing
            using (StreamWriter writer = new StreamWriter("writer_example.txt"))
            {
                writer.WriteLine("Line 1 from StreamWriter");
                writer.WriteLine("Line 2 from StreamWriter");
                writer.Write("Partial line ");
                writer.WriteLine("completed.");
            }
            Console.WriteLine("Text written using StreamWriter.");

            // 3. StreamReader for text reading
            using (StreamReader reader = new StreamReader("writer_example.txt"))
            {
                Console.WriteLine("\nReading with StreamReader:");
                string line;
                int lineNumber = 1;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine($"{lineNumber}: {line}");
                    lineNumber++;
                }
            }

            // 4. BinaryWriter and BinaryReader
            string binaryFile = "binary_example.bin";

            // Writing binary data
            using (BinaryWriter bw = new BinaryWriter(File.Open(binaryFile, FileMode.Create)))
            {
                bw.Write(42);           // int
                bw.Write(3.14159);      // double
                bw.Write("Binary text"); // string
                bw.Write(true);         // bool
            }

            // Reading binary data
            using (BinaryReader br = new BinaryReader(File.Open(binaryFile, FileMode.Open)))
            {
                int intValue = br.ReadInt32();
                double doubleValue = br.ReadDouble();
                string stringValue = br.ReadString();
                bool boolValue = br.ReadBoolean();

                Console.WriteLine($"\nBinary data read:");
                Console.WriteLine($"Int: {intValue}");
                Console.WriteLine($"Double: {doubleValue}");
                Console.WriteLine($"String: {stringValue}");
                Console.WriteLine($"Bool: {boolValue}");
            }

            Console.WriteLine();
        }

        static void PathOperations()
        {
            Console.WriteLine("=== PATH OPERATIONS ===");

            string filePath = @"C:\Users\Example\Documents\test.txt";

            // Path manipulation
            Console.WriteLine($"Full path: {filePath}");
            Console.WriteLine($"Directory: {Path.GetDirectoryName(filePath)}");
            Console.WriteLine($"Filename: {Path.GetFileName(filePath)}");
            Console.WriteLine($"Filename without extension: {Path.GetFileNameWithoutExtension(filePath)}");
            Console.WriteLine($"Extension: {Path.GetExtension(filePath)}");
            Console.WriteLine($"Root: {Path.GetPathRoot(filePath)}");

            // Combining paths
            string combinedPath = Path.Combine("folder1", "folder2", "file.txt");
            Console.WriteLine($"Combined path: {combinedPath}");

            // Temporary paths
            Console.WriteLine($"Temp directory: {Path.GetTempPath()}");
            Console.WriteLine($"Temp filename: {Path.GetTempFileName()}");

            // Path validation
            char[] invalidChars = Path.GetInvalidFileNameChars();
            Console.WriteLine($"Invalid filename characters count: {invalidChars.Length}");

            Console.WriteLine();
        }

        // Cleanup method (call this at the end if needed)
        static void Cleanup()
        {
            // Clean up created files and directories
            try
            {
                if (File.Exists("example.txt")) File.Delete("example.txt");
                if (File.Exists("lines.txt")) File.Delete("lines.txt");
                if (File.Exists("stream_example.txt")) File.Delete("stream_example.txt");
                if (File.Exists("writer_example.txt")) File.Delete("writer_example.txt");
                if (File.Exists("binary_example.bin")) File.Delete("binary_example.bin");
                if (Directory.Exists("TestDirectory"))
                    Directory.Delete("TestDirectory", true);

                Console.WriteLine("Cleanup completed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cleanup error: {ex.Message}");
            }
        }
    }
}

// Additional utility class for advanced file operations
public static class FileUtilities
{
    // Copy file with progress
    public static void CopyFileWithProgress(string source, string destination)
    {
        using (var sourceStream = new FileStream(source, FileMode.Open, FileAccess.Read))
        using (var destStream = new FileStream(destination, FileMode.Create, FileAccess.Write))
        {
            byte[] buffer = new byte[4096];
            long totalBytes = sourceStream.Length;
            long copiedBytes = 0;
            int bytesRead;

            while ((bytesRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                destStream.Write(buffer, 0, bytesRead);
                copiedBytes += bytesRead;

                int progress = (int)((copiedBytes * 100) / totalBytes);
                Console.Write($"\rCopying: {progress}%");
            }
            Console.WriteLine(" - Complete!");
        }
    }

    // Read file in chunks
    public static IEnumerable<string> ReadFileInChunks(string filePath, int chunkSize = 1024)
    {
        using (var reader = new StreamReader(filePath))
        {
            char[] buffer = new char[chunkSize];
            int charsRead;

            while ((charsRead = reader.Read(buffer, 0, chunkSize)) > 0)
            {
                yield return new string(buffer, 0, charsRead);
            }
        }
    }

    // Safe file operations with exception handling
    public static bool SafeWriteAllText(string path, string content)
    {
        try
        {
            File.WriteAllText(path, content);
            return true;
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Access denied.");
            return false;
        }
        catch (DirectoryNotFoundException)
        {
            Console.WriteLine("Directory not found.");
            return false;
        }
        catch (IOException ex)
        {
            Console.WriteLine($"IO Exception: {ex.Message}");
            return false;
        }
    }
}