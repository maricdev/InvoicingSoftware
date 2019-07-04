using System;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace Montesino_fakture.Controller

{
    public class SimpleLogger
    {
        private readonly string datetimeFormat;
        private readonly string logFilename;

        public SimpleLogger(bool append = false)
        {
            datetimeFormat = "dd.MM.yyyy HH:mm:ss";
            logFilename = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".log";

            // Log file header line
            string logHeader = logFilename + " je pokrenut.";

            //  WriteLine(System.DateTime.Now.ToString(datetimeFormat) + " " + logHeader, true);
        }

        /// <summary>
        /// Log a DEBUG message
        /// </summary>
        /// <param name="text">Message</param>
        public void Debug(string text)
        {
            WriteFormattedLog(LogLevel.DEBUG, text);
        }

        /// <summary>
        /// Log an ERROR message
        /// </summary>
        /// <param name="text">Message</param>
        public void Error(string text)
        {
            WriteFormattedLog(LogLevel.ERROR, text);
        }

        /// <summary>
        /// Log a FATAL ERROR message
        /// </summary>
        /// <param name="text">Message</param>
        public void Fatal(string text)
        {
            WriteFormattedLog(LogLevel.FATAL, text);
        }

        /// <summary>
        /// Log an INFO message
        /// </summary>
        /// <param name="text">Message</param>
        public void Info(string text)
        {
            WriteFormattedLog(LogLevel.INFO, text);
        }

        /// <summary>
        /// Log a TRACE message
        /// </summary>
        /// <param name="text">Message</param>
        public void Trace(string text)
        {
            WriteFormattedLog(LogLevel.TRACE, text);
        }

        /// <summary>
        /// Log a WARNING message
        /// </summary>
        /// <param name="text">Message</param>
        public void Warning(string text)
        {
            WriteFormattedLog(LogLevel.WARNING, text);
        }

        private void WriteFormattedLog(LogLevel level, string text)
        {
            string pretext;
            switch (level)
            {
                case LogLevel.TRACE:
                    pretext = System.DateTime.Now.ToString(datetimeFormat) + " [TRACE]   ";
                    break;

                case LogLevel.INFO:
                    pretext = System.DateTime.Now.ToString(datetimeFormat) + " [INFO]    ";
                    break;

                case LogLevel.DEBUG:
                    pretext = System.DateTime.Now.ToString(datetimeFormat) + " [DEBUG]   ";
                    break;

                case LogLevel.WARNING:
                    pretext = System.DateTime.Now.ToString(datetimeFormat) + " [WARNING] ";
                    break;

                case LogLevel.ERROR:
                    pretext = System.DateTime.Now.ToString(datetimeFormat) + " [ERROR]   ";
                    break;

                case LogLevel.FATAL:
                    pretext = System.DateTime.Now.ToString(datetimeFormat) + " [FATAL]   ";
                    break;

                default:
                    pretext = "";
                    break;
            }

            WriteLine(pretext + text);

            if (IsAvailableNetworkActive() == true) // PROVERAVA DA LI IMA INTERNETA DA NE BI PUKLO I U SLANJU
            {
                DialogResult dialogResult = MessageBox.Show("Došlo je do greške! \n\n" + text + "." + System.Environment.NewLine + "\n\nDa li želite da obavestite programera o grešci?", "GREŠKA", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Send_Email e = new Send_Email(logFilename, text); // SLANJE LOG FILE-A NA MAIL
                    System.IO.File.WriteAllText(logFilename, string.Empty); // PRAZNJENJE LOG FILEA
                }
            }
        }

        private void WriteLine(string text, bool append = true)
        {
            try
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(logFilename, append, System.Text.Encoding.UTF8))
                {
                    if (text != "")
                    {
                        writer.WriteLine(text);
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public static bool IsAvailableNetworkActive()
        {
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        [System.Flags]
        private enum LogLevel
        {
            TRACE,
            INFO,
            DEBUG,
            WARNING,
            ERROR,
            FATAL
        }
    }
}