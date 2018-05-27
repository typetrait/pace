using Pace.Common.Network;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pace.Client.System
{
    public class CommandShell
    {
        public Process Process { get; set; }
        public bool SessionOpen { get; set; }

        private PaceClient client;
        private StreamWriter streamWriter;

        public CommandShell(PaceClient client)
        {
            this.client = client;
        }

        public void CreateSession()
        {
            Process = new Process()
            {
                StartInfo = new ProcessStartInfo("cmd.exe")
                {
                    UseShellExecute = false,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                    WorkingDirectory = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System)),
                    Arguments = "/K"
                }
            };

            Process.Start();
            SessionOpen = true;
        }

        public void ExecuteCommand(string command)
        {
            if (streamWriter == null)
            {
                streamWriter = new StreamWriter(Process.StandardInput.BaseStream);
            }

            streamWriter.WriteLine(command);
            streamWriter.Flush();
        }
    }
}