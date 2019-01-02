using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class FileCopy
    {
        public delegate void ProgressChangeDelegate(double Percentage, ref bool Cancel);
        public delegate void Completedelegate();
        public string SourceFilePath { get; set; }
        public string DestFilePath { get; set; }

        public event ProgressChangeDelegate OnProgressChanged; // Subscribe to this event to get update on progress
        public event Completedelegate OnComplete; // Subscribe to get update on when the copy has completed

        public FileCopy(string Source, string Dest)
        {
            this.SourceFilePath = Source;
            this.DestFilePath = Dest;

            OnProgressChanged += delegate { };
            OnComplete += delegate { };
        }

        public void Copy()
        {
            byte[] buffer = new byte[1024 * 1024]; // 1MB buffer
            bool cancelFlag = false;
            try
            {
                using (FileStream source = new FileStream(SourceFilePath, FileMode.Open, FileAccess.Read))
                {
                    long fileLength = source.Length;
                    using (FileStream dest = new FileStream(DestFilePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
                    {
                        long totalBytes = 0;
                        int currentBlockSize = 0;

                        try
                        {
                            while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                totalBytes += currentBlockSize;
                                double percentage = (double)totalBytes * 100.0 / fileLength;
                                dest.Write(buffer, 0, currentBlockSize);
                                cancelFlag = false;
                                OnProgressChanged(percentage, ref cancelFlag);

                                if (cancelFlag == true)
                                {
                                    try
                                    {
                                        if (File.Exists(DestFilePath))
                                            File.Delete(DestFilePath);
                                    }
                                    catch (Exception)
                                    {
                                        throw;
                                    }
                                    break;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            throw;
                        }
                    }
                }
                OnComplete();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

