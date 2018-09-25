using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace OneDriveMigrator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static bool done = false;
        private static bool complete = false;

        public MainWindow()
        {
            InitializeComponent();
        }



        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!done)
            {
                e.Cancel = true;                
                this.WindowState = WindowState.Minimized;
            }
        }

        private void dataOutputHandler(object sender, DataReceivedEventArgs args)
        {
            if (!string.IsNullOrEmpty(args.Data))
            {
                this.Dispatcher.Invoke(() => {
                    txtBatchOutput.Text += ">" + args.Data + Environment.NewLine;
                    txtBatchOutput.ScrollToEnd();
                });                
            }            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
            //  Run the batch file
            Process batchFile = new Process();
            batchFile.StartInfo.FileName = @"onedrivemigrate.bat";
            batchFile.StartInfo.Arguments = "";
            batchFile.StartInfo.CreateNoWindow = true;
            batchFile.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            batchFile.StartInfo.RedirectStandardOutput = true;
            batchFile.StartInfo.RedirectStandardError = true;
            batchFile.StartInfo.RedirectStandardInput = true;
            batchFile.StartInfo.WorkingDirectory = System.AppContext.BaseDirectory;
            batchFile.StartInfo.UseShellExecute = false;
            batchFile.OutputDataReceived += new DataReceivedEventHandler(dataOutputHandler);

                try
                {
                    batchFile.Start();
                    batchFile.BeginOutputReadLine();
                    batchFile.BeginErrorReadLine();

                    batchFile.WaitForExit();
                    done = true;

                    if (batchFile.ExitCode == 0)
                    {
                        this.Dispatcher.Invoke(() => {
                            DoneHelpWindow doneWindow = new DoneHelpWindow();
                            doneWindow.Show();
                            this.Close();
                        });                        
                    }
                    else if (batchFile.ExitCode == 666)
                    {
                        throw new Exception("Unable to migrate as user has no Z: drive");
                    }
                    else
                    {
                        throw new Exception("Unable to migrate - see the output window for more information");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error migrating!" + ex.Message, "NOT COMPLETE", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });           
            
        }
    }
}
