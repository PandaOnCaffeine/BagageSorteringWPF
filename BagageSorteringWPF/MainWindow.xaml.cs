using BagageSorteringWPF.Classes;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BagageSorteringWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int checkInsAmount = 0;
        public int gateAmount = 0;
        public Check_In[] checkIns;

        public BufferQueue _sortingBuffer = new BufferQueue("Sorting", 25);
        public BufferQueue[] _gatesBuffers = new BufferQueue[10];

        private SortingMachine _sortingMachine;

        public Thread[] _checkInThreads = new Thread[10];
        public Thread[] _gatesThreads = new Thread[10];

        public MainWindow()
        {
            InitializeComponent();

            checkIns = new Check_In[10];

            RunGateThread();

            RunCheckInThread();

            RunSortingThread();
        }
        private void RunCheckInThread()
        {
            ListBox newBox = NewCheckInBox(CheckInsStackPanel, "CheckIn", checkInsAmount);
            BufferQueue checkInBuffer = new BufferQueue(newBox.Name, 10);

            checkIns[checkInsAmount] = new Check_In(checkInBuffer, newBox, _sortingBuffer, gateAmount, Dispatcher);

            Check_In test = checkIns[checkInsAmount];

            _checkInThreads[checkInsAmount] = new Thread(() => test.Run());
            _checkInThreads[checkInsAmount].Start();

            checkInsAmount++;
        }
        private void RemoveCheckIn()
        {
            checkIns[checkInsAmount-1].Stop();
            _checkInThreads[checkInsAmount-1].Join();
            while (_checkInThreads[checkInsAmount-1].IsAlive == true)
            {

            }
            checkIns[checkInsAmount - 1] = null;
            _checkInThreads[checkInsAmount - 1] = null;
            CheckInsStackPanel.Children.RemoveAt(checkInsAmount-1);
            checkInsAmount--;
        }
        private void RunSortingThread()
        {
            ListBox newBox = NewCheckInBox(SortingStackPanel, "Sorting", 1);
            _sortingMachine = new SortingMachine(_sortingBuffer, _gatesBuffers, newBox, Dispatcher);
            Thread sortingThread = new Thread(() => _sortingMachine.Run());
            sortingThread.Start();
        }
        private void RunGateThread()
        {
            ListBox newBox = NewCheckInBox(GateStackPanel, "Gate", gateAmount);
            _gatesBuffers[gateAmount] = new BufferQueue(newBox.Name, 10);

            BufferQueue buffer = _gatesBuffers[gateAmount];

            Gate gate = new Gate(buffer, newBox, Dispatcher);
            _gatesThreads[gateAmount] = new Thread(() => gate.open());
            _gatesThreads[gateAmount].Start();
            gateAmount++;
        }
        private ListBox NewCheckInBox(StackPanel panel, string name, int amount)
        {
            ListBox newBox = null;
            // Use Dispatcher.Invoke to update UI controls
            Dispatcher.Invoke(() =>
            {
                newBox = new ListBox();
                newBox.Name = $"{name}{amount + 1}";
                newBox.SetBinding(ListBox.ItemsSourceProperty, new Binding("Lines") { Mode = BindingMode.OneWay });
                newBox.Margin = new Thickness(10);
                panel.Children.Add(newBox);
            });
            return newBox;
        }
        private void AddCheckInButtonClick(object sender, RoutedEventArgs e)
        {
            RunCheckInThread();
        }

        private void RemoveCheckInButtonClick(object sender, RoutedEventArgs e)
        {
            RemoveCheckIn();
        }
        private void AddGateButtonClick(object sender, RoutedEventArgs e)
        {
            RunGateThread();
            for (int i = 0; i < checkInsAmount; i++)
            {
                checkIns[i].GateAmount = gateAmount;
                
            }
        }
    }
}
