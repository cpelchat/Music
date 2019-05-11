using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleApp

{
    public class Music
    {
        private static List<MusicData> MusicDataList = new List<MusicData>();
        public Music(string[] args)
        {
            string txtFilePath = string.Empty;
            string reportFilePath = string.Empty;

            string startupPath = Directory.GetCurrentDirectory();

            if (Debugger.IsAttached)
            {
                txtFilePath = Path.Combine(startupPath, "SampleMusicPlaylist.txt");
                reportFilePath = Path.Combine(startupPath, "SampleMusicPlaylist.txt");
            }

            else
            {
                if (args.Length != 2)
                {
                    Console.WriteLine("Invalid call.\n Valid call example : MusicPlaylistAnalyzer <music_txt_file_path> <report_file_path>");
                    Console.ReadLine();
                    return;
                }
                else
                {
                    txtFilePath = args[0];
                    reportFilePath = args[1];

                    if (!txtFilePath.Contains("\\"))
                    {
                        txtFilePath = Path.Combine(startupPath, txtFilePath);
                    }

                    if (!reportFilePath.Contains("\\"))
                    {
                        reportFilePath = Path.Combine(startupPath, reportFilePath);
                    }
                }
            }

            if (File.Exists(txtFilePath))
            {
                if (ReadData(txtFilePath))
                {
                    try
                    {
                        var file = File.Create(reportFilePath);
                        file.Close();
                    }
                    catch (Exception fe)
                    {
                        Console.WriteLine($"Unable to create report file at : {reportFilePath}");
                    }

                    WriteReport(reportFilePath);
                }
            }
            else
            {
                Console.Write($"Music data file does not exist at path: {txtFilePath}");
            }

            Console.ReadLine();
        }

        private static bool ReadData(string filePath)
        {
            Console.WriteLine($"Reading data from file : {filePath}");
            try
            {
                int columns = 0;
                string[] musicDataLines = File.ReadAllLines(filePath);
                for (int index = 0; index < musicDataLines.Length; index++)
                {
                    string musicDataLine = musicDataLines[index];
                    string[] data = musicDataLine.Split(',');

                    if (index == 0)
                    {
                        columns = data.Length;
                    }
                    else
                    {
                        if (columns != data.Length)
                        {
                            Console.WriteLine($"Row {index} contains {data.Length} values. It should contain {columns}.");
                            return false;
                        }
                        else
                        {
                            try
                            {
                                MusicData musicData = new MusicData();
                                musicData.Plays = Convert.ToInt32(data[0]);
                                musicData.Alternative = Convert.ToInt32(data[1]);
                                musicData.HipHop = Convert.ToInt32(data[2]);
                                musicData.FishBowl = Convert.ToInt32(data[3]);
                                musicData.Seven = Convert.ToInt32(data[4]);
                                musicData.EightFive = Convert.ToInt32(data[5]);
                                musicData.Longest = Convert.ToInt32(data[6]);
                                MusicDataList.Add(musicData);
                            }
                            catch (InvalidCastException e)
                            {
                                Console.WriteLine($"Row {index} contains invalid value.");
                                return false;
                            }
                        }
                    }
                }
                Console.WriteLine($"Data read completed successfully.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in reading data from txt file.");
                throw ex;
            }
        }
        private static void WriteReport(string filePath)
        {
            try
            {
                if (MusicDataList != null && MusicDataList.Any())
               
            }
        }
    }
}