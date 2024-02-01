﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BagageSorteringWPF.Classes
{
    internal class Startup
    {
        static ManualResetEventSlim pauseEvent = new ManualResetEventSlim(true);
        //private bool _running = true;
        StackPanel _stackPanel;
        public Startup(StackPanel stackPanel)
        {
            Console.Out.WriteLine("Test");
            _stackPanel = stackPanel;
            _stackPanel.Name = "checkInStackPanel";
        }
        public void Run()
        {
            //_running = true;
            //while (_running)
            //{
            //    /*
            //    //Locks
            //    object writerLock = new object();

            //    //Sizes for Gates
            //    int gateX = 101;
            //    int gateY = 2;
            //    int gateWidth = 60;
            //    int gateHeight = 10;

            //    //Sizes for CheckIns
            //    int checkInX = 3;
            //    int checkInY = 2;
            //    int checkInWidth = 40;
            //    int checkInHeight = 10;

            //    //Amounts of gates and CheckIns and Max amount
            //    int gateAmount = 3;
            //    int maxGates = 6;
            //    int checkInsAmount = 3;
            //    int maxCheckIns = 6;

            //    //Buffer Arrays
            //    Buffer[] gateBuffers = new Buffer[gateAmount];
            //    Buffer sortingBuffer = new Buffer("Sorting Machine", 15);

            //    //Gate Array, CheckIns Array and SortingMachine
            //    Gate[] gates = new Gate[maxGates];
            //    Check_In[] checkIns = new Check_In[maxCheckIns];
            //    SortingMachine sorting = new SortingMachine(sortingBuffer, gateBuffers, new TextBox("Sorting Machine", 50, 30, 47, 2, writerLock));

            //    //Threads
            //    Thread[] gateThreads = new Thread[maxGates];
            //    Thread[] checkInsThreads = new Thread[maxCheckIns];
            //    Thread sortingThread = new Thread(new ThreadStart(sorting.Run));


            //    //Generate Startup Gates
            //    for (int i = 1; i < gateAmount + 1; i++)
            //    {
            //        gateBuffers[i - 1] = new Buffer($"Gate {i}", 10);
            //        gates[i - 1] = new Gate(gateBuffers[i - 1], new TextBox($"Gate {i}", gateWidth, gateHeight, gateX, gateY, writerLock));
            //        gateThreads[i - 1] = new Thread(new ThreadStart(gates[i - 1].open));
            //        gateThreads[i - 1].Start();
            //        gateY += 14;
            //    }

            //    //Generate Startup CheckIns
            //    for (int i = 1; i < checkInsAmount + 1; i++)
            //    {
            //        checkIns[i - 1] = new Check_In(new Buffer($"CheckIn{i}", 10), new TextBox($"Check In {i}", checkInWidth, checkInHeight, checkInX, checkInY, writerLock), sortingBuffer, gateAmount, pauseEvent);
            //        checkInsThreads[i - 1] = new Thread(new ThreadStart(checkIns[i - 1].Run));
            //        checkInsThreads[i - 1].Start();
            //        checkInY += 14;
            //    }
            //    sortingThread.Start();


            //    //Main Thread Menu

            //    int xArrowPos = 164;
            //    int yArrowPos = 2;
            //    TextBox menu = new TextBox("Menu", 30, 25, xArrowPos + 7, 2, writerLock);
            //    WriteMenu(menu);
            //    //bool done = false;
            //    bool running = true;

            //    WriteAtText(writerLock, "---->", xArrowPos, yArrowPos, ConsoleColor.Green);

            //    while (running)
            //    {
            //        switch (Console.ReadKey().Key)
            //        {
            //            case ConsoleKey.UpArrow:
            //                if (yArrowPos > 2)
            //                {
            //                    WriteAtText(writerLock, "     ", xArrowPos, yArrowPos, ConsoleColor.White);
            //                    yArrowPos--;
            //                    WriteAtText(writerLock, "---->", xArrowPos, yArrowPos, ConsoleColor.Green);
            //                }
            //                break;
            //            case ConsoleKey.DownArrow:
            //                if (yArrowPos < 25)
            //                {
            //                    WriteAtText(writerLock, "     ", xArrowPos, yArrowPos, ConsoleColor.White);
            //                    yArrowPos++;
            //                    WriteAtText(writerLock, "---->", xArrowPos, yArrowPos, ConsoleColor.Green);
            //                }
            //                break;
            //            case ConsoleKey.Enter:
            //                WriteAtText(writerLock, "     ", xArrowPos, yArrowPos, ConsoleColor.White);
            //                PauseThread();
            //                TextBox miniMenu;
            //                switch (yArrowPos)
            //                {
            //                    case 2:
            //                        if (checkInsAmount == maxCheckIns)
            //                        {
            //                            WriteAtText(writerLock, "                              ", 175, 30, ConsoleColor.Red);
            //                            WriteAtText(writerLock, "Max CheckIns Reached", 175, 30, ConsoleColor.Red);
            //                        }
            //                        else
            //                        {
            //                            miniMenu = new TextBox("Mini Menu", 40, 10, xArrowPos + 45, 2, writerLock);
            //                            miniMenu.WriteAt("Skriv Checkin Buffer Limit:", ConsoleColor.White);
            //                            string limit = WriteInMiniMenu(writerLock, xArrowPos + 45, 3);
            //                            int limitamount = Convert.ToInt32(limit);
            //                            checkIns[checkInsAmount] = new Check_In(
            //                                new Buffer($"CheckIn{checkInsAmount + 1}", limitamount),
            //                                new TextBox($"Check In {checkInsAmount + 1}", checkInWidth, checkInHeight, checkInX, checkInY, writerLock),
            //                                sortingBuffer,
            //                                gateAmount,
            //                                pauseEvent);
            //                            checkInsThreads[checkInsAmount] = new Thread(new ThreadStart(checkIns[checkInsAmount].Run));
            //                            checkInsThreads[checkInsAmount].Start();
            //                            checkInsAmount++;
            //                            checkInY += 14;
            //                            miniMenu.RemoveBox();
            //                        }
            //                        ResumeThread();
            //                        WriteAtText(writerLock, "---->", xArrowPos, yArrowPos, ConsoleColor.Green);
            //                        break;
            //                    case 3:
            //                        if (checkInsAmount == 1)
            //                        {
            //                            WriteAtText(writerLock, "                              ", 175, 30, ConsoleColor.Red);
            //                            WriteAtText(writerLock, "Minimum CheckIns Reached", 175, 30, ConsoleColor.Red);
            //                        }
            //                        else
            //                        {
            //                            bool done = false;
            //                            while (!done)
            //                            {

            //                                miniMenu = new TextBox("Mini Menu", 40, 10, xArrowPos + 45, 2, writerLock);
            //                                miniMenu.WriteAt("Skriv \"Confirm\" To delete a Check in:", ConsoleColor.White);
            //                                string confirmText = WriteInMiniMenu(writerLock, xArrowPos + 45, 3);
            //                                ResumeThread();
            //                                if (confirmText == "Confirm")
            //                                {
            //                                    checkIns[checkInsAmount - 1].Stop();
            //                                    checkIns[checkInsAmount - 1] = null;
            //                                    checkInsThreads[checkInsAmount - 1] = null;
            //                                    checkInsAmount--;
            //                                    checkInY -= 14;
            //                                    done = true;
            //                                }
            //                                else
            //                                {
            //                                    WriteAtText(writerLock, "                              ", 175, 30, ConsoleColor.Red);
            //                                    WriteAtText(writerLock, "\"Confirm\" Not typed right", 175, 30, ConsoleColor.Red);
            //                                }
            //                                miniMenu.RemoveBox();
            //                            }
            //                        }
            //                        ResumeThread();
            //                        WriteAtText(writerLock, "---->", xArrowPos, yArrowPos, ConsoleColor.Green);
            //                        break;
            //                    case 4:
            //                        break;
            //                    case 5:
            //                        break;
            //                    case 6:
            //                        break;
            //                    default:
            //                        ResumeThread();
            //                        WriteAtText(writerLock, "---->", xArrowPos, yArrowPos, ConsoleColor.Green);
            //                        break;
            //                }
            //                break;
            //            default:
            //                ResumeThread();
            //                break;
            //        }
            //    }*/
            //    for (int i = 0; i < 3; i++)
            //    {
            //        AddRichTextBox();
            //        checkInY += 130;
            //    }
            //    Thread.Sleep(10000);
            //    //Width="250" Height="120" HorizontalAlignment="Left" VerticalAlignment="Top" FontSize="16" Grid.Column="2"
            //}
        }
        public void AddRichTextBox(string name)
        {
            RichTextBox box = new RichTextBox();
            box.Margin = new Thickness(10, 10, 0, 0);
            box.Width = 250;
            box.Height = 120;
            box.HorizontalAlignment = 0;
            box.VerticalAlignment = 0;
            box.FontSize = 16;
            box.Name = name;
            _stackPanel.Children.Add(box);
        }

        public void RemoveRichTextBox()
        {
            // Sjekk om det er minst en RichTextBox igjen før du prøver å fjerne
            if (_stackPanel.Children.Count > 0)
            {
                // Fjern den siste RichTextBox fra StackPanel
                _stackPanel.Children.RemoveAt(_stackPanel.Children.Count - 1);
            }
            else
            {
                MessageBox.Show("Ingen RichTextBox-er å fjerne.");
            }
        }
        /*
        private string WriteInMiniMenu(object writerLock, int x, int y)
        {
            string returnText = "";
            string otherText = "";
            bool done = false;
            int xPos = x;
            int yPos = y;
            while (!done)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                try
                {
                    Monitor.Enter(writerLock);

                    switch (keyInfo.Key)
                    {
                        case ConsoleKey.Enter:
                            done = true;
                            break;
                        case ConsoleKey.Backspace:
                            if (returnText.Length > 0)
                            {

                                otherText = returnText.Remove(returnText.Length - 1);
                                returnText = otherText;
                                xPos--;
                                Console.SetCursorPosition(xPos, yPos);
                                Console.Write(' ');
                            }
                            break;
                        default:
                            returnText += keyInfo.KeyChar;
                            Console.SetCursorPosition(xPos, yPos);
                            Console.Write(keyInfo.KeyChar);
                            xPos++;
                            break;
                    }
                }
                finally
                {
                    Monitor.Exit(writerLock);
                }
            }
            return returnText;
        }
        private void WriteAtText(object writerLock, string Text, int x, int y, ConsoleColor color)
        {
            try
            {
                Monitor.Enter(writerLock);

                Console.ForegroundColor = color;
                Console.SetCursorPosition(x, y);
                Console.Write(Text);

                Console.ResetColor();
            }
            finally
            {
                Monitor.Exit(writerLock);
            }
        }
        private void WriteMenu(TextBox box)
        {
            box.WriteAt("1-Opret ny Check In", ConsoleColor.White);
            box.WriteAt("2-Fjern Check In", ConsoleColor.White);
            box.WriteAt("3-Opret ny Gate", ConsoleColor.White);
            box.WriteAt("4-Fjern Gate", ConsoleColor.White);
            box.WriteAt("5-Pause Program", ConsoleColor.White);
            box.WriteAt("6-Exit Program", ConsoleColor.White);
        }
        private void PauseThread()
        {
            pauseEvent.Reset(); // Reset the event to block the thread
        }
        private void ResumeThread()
        {
            pauseEvent.Set(); // Set the event to unblock the thread
        }
        */
    }
}
