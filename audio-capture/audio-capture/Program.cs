using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAudioApp
{

    // TODO: add some compression
    // TODO: add remote player
    // TODO: add discovery/connection
    // TODO: add automatic muting of host speakers

    class Program
    {

        WaveFileWriter _writer;
        IWaveIn waveIn;

        static void Main(string[] args)
        {
            File.Delete("test.wav");

            Program instance = new Program();
            instance.Run();

            Console.WriteLine("Press enter to terminate.");
            Console.ReadLine();
            instance.Close();
        }

        public void Run()
        {
            var filename = "test.wav";
            waveIn = new WasapiLoopbackCapture();
            waveIn.DataAvailable += OnDataAvailable;
            //waveIn.RecordingStopped += waveIn_RecordingStopped;


            _writer = new WaveFileWriter(filename, waveIn.WaveFormat);

            waveIn.StartRecording();


        }

        private void OnDataAvailable(object sender, WaveInEventArgs waveInEventArgs)
        {
            if (_writer == null)
            {
                throw new NullReferenceException();
            }

            _writer.Write(waveInEventArgs.Buffer, 0, waveInEventArgs.BytesRecorded);

            //byte[] by = Float32toInt16(waveInEventArgs.Buffer, 0, waveInEventArgs.BytesRecorded);

        }

        public void Close()
        {
            _writer.Flush();
            _writer.Dispose();
            waveIn.Dispose();
        }
    }
}
